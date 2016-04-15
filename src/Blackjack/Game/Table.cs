using Blackjack.CardGame;
using Blackjack.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack
{
    class Table
    {
        Deck _deck;
        Input _input;
                
        int _playerWins, _playerLosses, _playerTies, _playerBlackjacks;
        
        int _height, _width;

        HandState _handState = HandState.NoHand;

        CardDisplay _cardDisplay;
        TextDisplay _textDisplay;
        ChipDisplay _chipDisplay;
        
        int _playerChips, _playerBet;

        int _currentHand;

        private Hand DealerHand;
        
        private List<Hand> PlayerHands;
        
        public Table()
        {
            DealerHand = new Hand();

            PlayerHands = new List<Hand>() { new Hand() };
            
            //_deck = new SplitDeck(6);
            _deck = new RegularDeck(6);            
        }

        public void Initialize(Input input, int height, int width, CardDisplay cardDisplay, TextDisplay textDisplay, ChipDisplay chipDisplay)
        {
            _input = input;

            _height = height;
            _width = width;

            _cardDisplay = cardDisplay;
            _textDisplay = textDisplay;
            _chipDisplay = chipDisplay;

            _playerChips = 1000; // Start at 1000
            _playerBet = 10; // Default to 10

            _deck.ShuffleDeck();
        }

        public void Update(GameTime gameTime)
        {
            #region Deal

            if (_input.Deal && _handState == HandState.NoHand && _playerChips > 0)
            {
                Reset();

                _playerChips -= _playerBet; // Take Chips first
                
                DealerHand.Add(_deck.Draw());
                PlayerHands[0].Add(_deck.Draw());
                DealerHand.Add(_deck.Draw());
                PlayerHands[0].Add(_deck.Draw());

                PlayerHands[0].HandBet = _playerBet; // Set Default Bet

                //End the round early if Player or dealer has Blackjack
                if (PlayerHands[0].IsBlackjack || DealerHand.IsBlackjack)
                {
                    _handState = HandState.CloseHand;
                    return;
                }

                _handState = HandState.PlayerTurn;
            }

            #endregion

            #region Player Turn

            if (_handState == HandState.PlayerTurn)
            {
                if (PlayerHands[_currentHand].Count == 1)
                {
                    PlayerHands[_currentHand].Add(_deck.Draw()); // Hand was just split and needs second card
                }

                if (PlayerHands[_currentHand].IsBust)
                {
                    EndHand();
                    return;
                }
                else
                {
                    if (_input.Stay)
                    {
                        EndHand();
                        return;
                    }

                    //Hit Logic
                    if (_input.Hit)
                    {
                        PlayerHands[_currentHand].Add(_deck.Draw());
                    }

                    //Double Down Logic
                    if (_input.DoubleDown && PlayerHands[_currentHand].CanDoubleDown && _playerChips >= _playerBet)
                    {
                        _playerChips -= _playerBet; // Deduct increase bet
                        PlayerHands[_currentHand].HandBet *= 2; // Double Player's Bet

                        PlayerHands[_currentHand].Add(_deck.Draw());

                        EndHand();
                        return;
                    }

                    if (_input.Split && PlayerHands[0].CanSplit && PlayerHands.Count < 4)
                    {
                        //TODO - Add Split Logic
                        PlayerHands.Add(new Hand());

                        _playerChips -= _playerBet; // Deduct increase bet
                        PlayerHands[PlayerHands.Count - 1].HandBet = _playerBet; // Add bet to new hand

                        //Move one card from the first hand to the second
                        PlayerHands[PlayerHands.Count - 1].Add(PlayerHands[_currentHand][1]); // Add to Newest Hand
                        PlayerHands[_currentHand].RemoveAt(1);

                        //Draw Card for First Hand
                        PlayerHands[_currentHand].Add(_deck.Draw());
                    }
                }
            }

            #endregion

            #region Dealer Turn

            if (_handState == HandState.DealerTurn)
            {
                if (DealerHand.HandValue < 17 || 
                    (DealerHand.HandValue == 17 && DealerHand.IsSoft))
                {
                    DealerHand.Add(_deck.Draw());
                }
                else
                {
                    _handState = HandState.CloseHand;
                }

                return;
            }

            #endregion

            #region End Hand
            
            if (_handState == HandState.CloseHand)
            {
                #region Determine Outcome

                foreach (Hand hand in PlayerHands)
                {
                    if (hand.IsBlackjack) // Player Has Blackjack - Blackjack
                    {
                        hand.Result = HandResult.Blackjack;
                    }
                    else if (hand.IsBust) // Player Bust - Lose
                    {
                        hand.Result = HandResult.Lose;
                    }
                    else if (DealerHand.IsBust) // Dealer Bust - Win
                    {
                        hand.Result = HandResult.Win;
                    }
                    else if (hand.HandValue > DealerHand.HandValue) // Player Higher Score - Win
                    {
                        hand.Result = HandResult.Win;
                    }
                    else if (hand.HandValue < DealerHand.HandValue) // Player Lower Score - Lost
                    {
                        hand.Result = HandResult.Lose;
                    }
                    else if (hand.HandValue == DealerHand.HandValue) // Player Same Score - Draw
                    {
                        hand.Result = HandResult.Draw;
                    }
                    else
                    {
                        throw new Exception("Invalid Result");
                    }
                }                

                #endregion

                #region Update Results
                
                foreach (Hand hand in PlayerHands)
                {
                    switch (hand.Result)
                    {
                        case HandResult.None:
                            break;
                        case HandResult.Blackjack:
                            _playerChips += (int)(hand.HandBet * 2.5); // Pay 2.5 Times Winner's Bet
                            _playerBlackjacks++;
                            _playerWins++;
                            break;
                        case HandResult.Win:
                            _playerChips += (int)(hand.HandBet * 2); // Double Winner's Bet
                            _playerWins++;
                            break;
                        case HandResult.Lose:
                            _playerLosses++;
                            break;
                        case HandResult.Draw:
                            _playerChips += (int)(hand.HandBet); // Return Player's Bet
                            _playerTies++;
                            break;
                        default:
                            break;
                    }
                }

                #endregion
                
                //Shuffle Deck if there are less than 52 Cards remaining
                if (_deck.CardsRemaining < 52)
                {
                    _deck.ShuffleDeck();
                }
                                
                _handState = HandState.NoHand;
                return;
            }

            #endregion
        }

        // TODO - Switch Active Hand
        // TODO - Display Win/Lose/Draw Stats
        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw Dealer Hand
            for (int i = 0; i < DealerHand.Count; i++)
            {
                if (_handState == HandState.PlayerTurn && i == 0)
                {
                    //Draw the first Dealer card face down if it's the player's turn
                    _cardDisplay.DrawCardBack(spriteBatch, 10 + (i * 30), 10);
                }
                else{
                    _cardDisplay.DrawCard(spriteBatch, DealerHand[i], 10 + (i * 30), 10);
                }                
            }

            //Draw Player Hand
            for (int j = 0; j < PlayerHands[0].Count; j++)
            {
                _cardDisplay.DrawCard(spriteBatch, PlayerHands[0][j], 10 + (j * 30), _height - 205);
            }                       

            //Draw Dealer Score
            string dealerHandValueDisplay = (_handState == HandState.PlayerTurn) ? "??" : DealerHand.HandValue.ToString();
            _textDisplay.DrawString(spriteBatch, string.Format("Dealer Score: {0}", dealerHandValueDisplay), new Vector2(10, 200));

            //Draw Player Score
            _textDisplay.DrawString(spriteBatch, string.Format("Player Score: {0}", PlayerHands[0].HandValue), new Vector2(10, 240));

            //Draw Hand Indicator
            if (_currentHand == 0 && PlayerHands.Count > 1)
            {
                _textDisplay.DrawString(spriteBatch, "<<", new Vector2(250, 240));
            }

            //Draw Split Hands
            if (PlayerHands.Count > 1)
            {
                for (int handIndex = 1; handIndex < PlayerHands.Count; handIndex++) // Start at the second Hand, the first has already been drawn
                {
                    for (int cardIndex = 0; cardIndex < PlayerHands[handIndex].Count; cardIndex++)
                    {
                        _cardDisplay.DrawCardMiniature(spriteBatch, PlayerHands[handIndex][cardIndex], 275 + (150 * (handIndex - 1)) + (cardIndex * 15), _height - 205);
                        _textDisplay.DrawString(spriteBatch, string.Format("{0}", PlayerHands[handIndex].HandValue), new Vector2(310 + (150 * (handIndex - 1)), _height - 80));
                        
                        // Draw Hand Indicator
                        if (handIndex == _currentHand && _handState == HandState.PlayerTurn)
                        {
                            _textDisplay.DrawString(spriteBatch, ">>", new Vector2(270 + (150 * (handIndex - 1)), _height - 80));
                        }
                    }
                }
            }
            
            string handStateDisplay = "";

            //Draw Hand State - Player Turn or Deal
            if (_handState == HandState.PlayerTurn)
            {
                handStateDisplay = "Player's Turn";
            }
            else if (_handState == HandState.NoHand)
            {
                handStateDisplay = "Deal?";
            }

            _textDisplay.DrawString(spriteBatch, handStateDisplay, new Vector2(250, 20));
            
            //Draw Hand Result - Win Lose Draw
            string resultDisplay = "";

            if (_handState == HandState.NoHand)
            {
                if (PlayerHands.Count > 1)
                {
                    resultDisplay = "Splits!";
                }
                else
                {
                    switch (PlayerHands[0].Result)
                    {
                        case HandResult.Blackjack:
                            resultDisplay = "Blackjack!";
                            break;
                        case HandResult.Win:
                            resultDisplay = "Player Wins";
                            break;
                        case HandResult.Lose:
                            resultDisplay = "Player Loses";
                            break;
                        case HandResult.Draw:
                            resultDisplay = "Player Ties";
                            break;
                        default:
                            break;
                    }
                }

                _textDisplay.DrawString(spriteBatch, resultDisplay, new Vector2(250, 50));
            }

            //Draw Chips
            _chipDisplay.DrawChip(spriteBatch, 650, 10, _playerChips);
            _textDisplay.DrawString(spriteBatch, string.Format("Bet: {0}", PlayerHands[0].HandBet), new Vector2(665, 140));
        }

        private void Reset()
        {
            DealerHand.Clear();
            PlayerHands.Clear();

            PlayerHands.Add(new Hand());

            _currentHand = 0;
        }

        private void EndHand()
        {
            if (_currentHand == PlayerHands.Count -1)
            {
                //If All hands bust, close hands, otherwise transfer to dealer
                _handState = PlayerHands.All(hand => hand.IsBust) ? HandState.CloseHand : HandState.DealerTurn;
            }
            else
            {
                _currentHand++;
            }
        }
    }
}