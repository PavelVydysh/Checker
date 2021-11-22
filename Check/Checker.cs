using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Check
{
    public partial class Checker : UserControl
    {
        Image blackCheck = new Bitmap(Properties.Resources.black, new Size(45, 45));
        Image whiteCheck = new Bitmap(Properties.Resources.white, new Size(45, 45));
        bool Damka = false;
        public Checker()
        {
            InitializeComponent();
        }

        public void SetColorOfCheker(int x)//задаём цвет шашки
        {
            if (x == 1)
                BackgroundImage = whiteCheck;
            else if (x == 2)
                BackgroundImage = blackCheck;
        }

        public void SetBackColorOfCell(int i, int j)//задаём задний цвет ячейки
        {
            if (((i % 2 != 0) && (j % 2 == 0)) || ((i % 2 == 0) && (j % 2 != 0))) //идет по нечётным строкам и чётным столбцам и идёт по чётным стркоам и нечётным столбцам
            {
                BackColor = Color.Gray;
            }
            else
            BackColor = Color.White;
        }
    }
}
