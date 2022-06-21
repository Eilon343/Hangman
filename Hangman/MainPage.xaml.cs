using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using Windows.UI.Xaml.Shapes;



namespace Hangman
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private bool _isNewPressed = false;
        private bool _isEasyPressed = false;
        private bool _isHardPressed = false;
        private LettersGuessed _letter = new LettersGuessed();
        private GameManager _gm = new GameManager();
        private Word _word = new Word();
        private HangmanDraw _drawing = new HangmanDraw();
        private Rectangle[] _hangman;


        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView view = ApplicationView.GetForCurrentView();
            _hangman = new Rectangle[9] { Rec_Pole, Rec_Bar, Rec_Hang, Rec_Head, Rec_Body, Rec_Hand1, Rec_Hand2, Rec_Leg1, Rec_Leg2 };
        }
        private void NewGame_btn_Click(object sender, RoutedEventArgs e)
        {
            Welcome_txt.Visibility = Visibility.Collapsed;
            if (_isNewPressed == false)
            {
                _isEasyPressed = false;
                _isHardPressed = false;
                NewGame_btn.Visibility = Visibility.Collapsed;
                Easy_btn.Visibility = Visibility.Visible;
                Hard_btn.Visibility = Visibility.Visible;
            }
            _drawing.CountWrongs = 0;
            _isNewPressed = true;
        }
        //method that putting error messeges if the player pressed
        //for example: guess button when the game havn't started yet
        public async void ErrorMesseges()
        {
            if (_isNewPressed == false)
                await new MessageDialog("Error: Press New Game!").ShowAsync();
            else if (_isEasyPressed == false && _isHardPressed == false)
                await new MessageDialog("Error: Please Choose Difficulty!").ShowAsync();
            else if (CharList.SelectedItem == null)
                await new MessageDialog("Error: Please select a letter!").ShowAsync();
        }
        //checking player lose or win putting messages and clearing the game
        public async void WinOrLose()
        {
            if (_gm.Win(_word, CharList) == true)
            {
                await new MessageDialog("Won").ShowAsync();
                await new MessageDialog("Press New Game to start over").ShowAsync();
                _gm.ClearGame(CharList, myGrid, _word, Strikes_txt, _hangman, NewGame_btn);
                _isNewPressed = false;
            }
            if (_gm.Lose(CharList) == true)
            {
                await new MessageDialog($"Lost, the word was: {_word.ChosenWord}").ShowAsync();
                await new MessageDialog("Press New Game to start over").ShowAsync();
                _gm.ClearGame(CharList, myGrid, _word, Strikes_txt, _hangman, NewGame_btn);
                _isNewPressed = false;
            }
        }
        //checking what the user guessed if its right or wrong, drawing the hangman according to the player guess, removing the guessed letter from the list box and checking if the user won or lost
        public void GameProgress()
        {
            _letter.CheckGuessChar(CharList, _word);
            _gm.GuessedRight(_letter, _word, CharList);
            _drawing.DrawOnePart(_hangman, _gm, CharList);
            _gm.RemoveGuessedLetters(CharList, Strikes_txt);
        }
        private  void Guess_btn_Click(object sender, RoutedEventArgs e)
        {
            ErrorMesseges();
            GameProgress();
            WinOrLose();
        }
        public void DifficultyButtonVisible()
        {
            Easy_btn.Visibility = Visibility.Collapsed;
            Hard_btn.Visibility = Visibility.Collapsed;
        }
        public void CreatingStartScreen()
        {
            _gm.CountRight = 0;
            _word.RandomWord(_isEasyPressed, _isHardPressed);
            _word.ConvertWordToLetters();
            _gm.CreatingTextBlocks(myGrid, _word);
            _letter.CreateCharList(CharList);
        }
        private void Easy_btn_Click(object sender, RoutedEventArgs e)
        {
            _isEasyPressed = true;
            CreatingStartScreen();
            DifficultyButtonVisible();
        }

        private void Hard_btn_Click(object sender, RoutedEventArgs e)
        {
            _isHardPressed = true;
            CreatingStartScreen();
            DifficultyButtonVisible();
        }
    }
}
