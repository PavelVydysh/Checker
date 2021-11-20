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
    public partial class Form1 : Form
    {
        Map userMap = new Map();

        public Form1()
        {
            InitializeComponent();

            foreach (Checker cell in userMap.cells)
            {
                cell.Click += new EventHandler(PressTheChecker);
                Controls.Add(cell);
            }
        }

        public void PressTheChecker(object sender, EventArgs e)
        {
            if (userMap.prevCheck != null)
            {
                userMap.prevCheck.BackColor = userMap.GetPrevCheckerColor();
            }

            userMap.pressedCheck = (sender as Checker);

            int aboutChecker = userMap.map[userMap.pressedCheck.Location.Y/45, userMap.pressedCheck.Location.X / 45];//получаем значение ячейки из матрицы

            if (aboutChecker != 0 && aboutChecker == userMap.player)
            {
                userMap.pressedCheck.BackColor = Color.Yellow;
                userMap.isMoving = true;
            }
            else 
            {
                if (userMap.isMoving)
                {
                    userMap.map[userMap.pressedCheck.Location.Y / 45, userMap.pressedCheck.Location.X / 45] = userMap.map[userMap.prevCheck.Location.Y / 45, userMap.prevCheck.Location.X / 45];
                    userMap.map[userMap.prevCheck.Location.Y / 45, userMap.prevCheck.Location.X / 45] = aboutChecker;
                    userMap.pressedCheck.BackgroundImage = userMap.prevCheck.BackgroundImage;
                    userMap.prevCheck.BackgroundImage = null;
                    userMap.isMoving = false;
                }
            }
            userMap.prevCheck = userMap.pressedCheck;
        }
    }
}
