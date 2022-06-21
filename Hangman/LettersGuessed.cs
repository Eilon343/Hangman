using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;

namespace Hangman
{
    //class that will check what is the letter that the player guessed
    public class LettersGuessed
    {
        private string _chosenLetter;
        private char _letterGuessed;
        Word w = new Word();

        public char LetterGuessed
        {
            get { return _letterGuessed; }
        }
        public void CreateCharList(ListBox _CharList)
        {
            for (char i = 'A'; i <= 'Z'; i++)
            {
                _CharList.Items.Add(i);
                _CharList.FontSize = 11.5;
            }
        }
        public char CheckGuessChar(ListBox _CharList, Word w1)
        {
            if (_CharList.SelectedItem != null)
            {
                _chosenLetter = _CharList.SelectedItem.ToString();
                _chosenLetter.ToCharArray();
                _letterGuessed = _chosenLetter[0];
            }
            return _letterGuessed;
        }
    }
}



