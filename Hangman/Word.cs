using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Hangman
{
    //class that have a word bank and will randomize a word to guess
    public class Word
    {
        private int _wordLettersLength;
        private char[] _wordChars;
        private string[] _wordBankEasy = { "PEN", "RICE", "NOTE", "WAY", "GAME", "HANG", "MAN", "EXIT", "YES","MARS"};
        private string[] _wordBankHard = { "BETTER", "BOTTOM", "BREATH", "COUPLE", "CREATE", "HEALTH", "MARKET","GENRE", "EARLY", "LARGE" };
        private string _chosenWord;
        private Random _r = new Random();

        public char[] WordChars
        {
            get { return _wordChars; }
        }
        public int WordLettersLength
        {
            get { return _wordLettersLength; }
        }
        public string ChosenWord
        {
            get { return _chosenWord; }
        }
        //randomizing a word
        public string RandomWord(bool IsEasyPressed, bool IsHardPressed)
        {
            if (IsEasyPressed == true)
            {
                _chosenWord = _wordBankEasy[_r.Next(_wordBankEasy.Length)];
            }
            else if (IsHardPressed == true)
            {
                _chosenWord = _wordBankHard[_r.Next(_wordBankHard.Length)];
            }
            return _chosenWord;
        }
        //converting the word to letters
        public char[] ConvertWordToLetters()
        {
            _wordChars = _chosenWord.ToCharArray();
            _wordLettersLength = _chosenWord.Length;
            return _wordChars;
        }
    }
}
