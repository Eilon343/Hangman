using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using Windows.UI.Xaml.Shapes;

namespace Hangman
{
    //class that will manage the game
    public class GameManager
    {
        private TextBlock[] _tb;

        private bool _isPlayerRight;
        private int _countRight = 1;
        private int _strikes = 1;
        private const int _loseStrike = 10;

        public int CountRight
        {
            get { return _countRight; }
            set { _countRight = value; }
        }
        public bool IsPlayerRight
        {
            get { return _isPlayerRight; }
        }
        public int Strikes
        {
            get { return _strikes; }
        }
        //creating the empty text blocks
        public void CreatingTextBlocks(Grid myGrid, Word w1)
        {
            int x = 10;
            _tb = new TextBlock[w1.WordLettersLength];
            for (int i = 0; i < _tb.Length; i++)
            {
                _tb[i] = new TextBlock();
                _tb[i].Text = "_";
                _tb[i].Width = 200;
                _tb[i].Height = 100;
                _tb[0].Margin = new Thickness(10, 600, 0, 0);
                _tb[i].Margin = new Thickness(x + 10, 600, 0, 0);
                x += 100;
                _tb[i].FontSize = 50;
                myGrid.Children.Add(_tb[i]);
            }
        }
        //clearing the game when its a lose or a win
        public void ClearGame(ListBox _CharList, Grid myGrid, Word w1, TextBox Strikes, Rectangle[] Drawings,Button NewGame)
        {
            _countRight = 0;
            for (int i = 0; i < Drawings.Length; i++)
                Drawings[i].Visibility = Visibility.Collapsed;
            _strikes = 1;
            _CharList.Items.Clear();
            for (int i = 0; i < w1.WordLettersLength; i++)
                myGrid.Children.Remove(_tb[i]);
            Strikes.Text = "";
            NewGame.Visibility = Visibility.Visible;
        }
        //checking if the player guess is right or wrong
        public void GuessedRight(LettersGuessed L1, Word w1,ListBox CharList)
        {
            if (CharList.SelectedItem != null)
            {
                _isPlayerRight = false;
                for (int i = 0; i < w1.WordLettersLength; i++)
                {
                    if (w1.WordChars[i] == L1.LetterGuessed)
                    {
                        _countRight++;
                        CharList.Items.Remove(CharList.SelectedItem);
                        _tb[i].Text = w1.WordChars[i].ToString();
                        _isPlayerRight = true;
                    }
                    else if (w1.WordChars[i] != L1.LetterGuessed && _isPlayerRight != true)
                        _isPlayerRight = false;
                }
            }
        }
        //removing the guessed letter from the list box and updating the strike counter
        public void RemoveGuessedLetters(ListBox CharList, TextBox Strikes)
        {
            if (CharList.SelectedItem != null)
            {
                if (_isPlayerRight == false)
                {
                    Strikes.Text = $"Strikes: {_strikes}";
                    _strikes++;
                    CharList.Items.Remove(CharList.SelectedItem);
                }
            }
        }
        //checking win
        public bool Win(Word W1, ListBox _CharList)
        {
            if (_countRight == W1.WordLettersLength)
            {
                return true;
            }
            return false;
        }
        //cheking lose
        public bool Lose(ListBox _CharList)
        {
            if (_strikes == _loseStrike)
            {
                return true;
            }
            return false;
        }
    }
}
