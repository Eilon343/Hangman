using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml;

namespace Hangman
{
    //class that will draw the right parts of the hangman
    public class HangmanDraw
    {
        private int _countWrongs = 0;

        public int CountWrongs
        {
            get { return _countWrongs; }
            set { _countWrongs = value; }
        }
        //i made a counter that counting the player strikes
        //and according to the player strike its making the right part of the hangman visibla
        public void DrawOnePart(Rectangle[] drawings, GameManager gm, ListBox CharList)
        {
            if (CharList.SelectedItem != null)
            {
                if (gm.IsPlayerRight == false)
                {
                    drawings[_countWrongs].Visibility = Visibility.Visible;
                    _countWrongs++;
                }
            }
        }
    }
}
