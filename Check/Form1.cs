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

        public void PressTheChecker(object sender, EventArgs e)//когда нажали на шашку
        {
            
            if (userMap.prevCheck != null)
            {
                userMap.prevCheck.BackColor = userMap.GetPrevCheckerColor(userMap.prevCheck);
            }

            userMap.pressedCheck = (sender as Checker);

            int aboutChecker = userMap.map[userMap.pressedCheck.Location.Y/50, userMap.pressedCheck.Location.X / 50];//получаем значение ячейки из матрицы

            if (aboutChecker != 0 && aboutChecker == userMap.player)
            {
                userMap.pressedCheck.BackColor = Color.Yellow;
                userMap.isMoving = true;
            }
            else 
            {
                if (userMap.isMoving)
                {
                    userMap.map[userMap.pressedCheck.Location.Y / 50, userMap.pressedCheck.Location.X / 50] = userMap.map[userMap.prevCheck.Location.Y / 50, userMap.prevCheck.Location.X / 50];
                    userMap.map[userMap.prevCheck.Location.Y / 50, userMap.prevCheck.Location.X / 50] = aboutChecker;
                    userMap.pressedCheck.BackgroundImage = userMap.prevCheck.BackgroundImage;
                    userMap.prevCheck.BackgroundImage = null;
                    userMap.isMoving = false;
                }
            }
            userMap.prevCheck = userMap.pressedCheck;
        }

        public void CloseSteps()
        {
            for (int i = 0; i < 64; i++)
            {
                    userMap.cells[i].BackColor = userMap.GetPrevCheckerColor(userMap.cells[i]);
            }
        }

        public bool IsInsideBorders(int ti, int tj)
        {
            if (ti >= 8 || tj >= 8 || ti < 0 || tj < 0)
            {
                return false;
            }
            return true;
        }

        public void ActivateAllButtons()
        {
            for (int i = 0; i < 8; i++)
            {
                    userMap.cells[i].Enabled = true;
            }
        }

        public void DeactivateAllButtons()
        {
            for (int i = 0; i < 8; i++)
            {
                    userMap.cells[i].Enabled = false;
            }
        }

        public void ShowProceduralEat(int i, int j, bool isOneStep = true)
        {
            int dirX = i - userMap.pressedCheck.Location.Y / 50;
            int dirY = j - userMap.pressedCheck.Location.X / 50;
            dirX = dirX < 0 ? -1 : 1;
            dirY = dirY < 0 ? -1 : 1;
            int il = i;
            int jl = j;
            bool isEmpty = true;
            while (IsInsideBorders(il, jl))
            {
                if (userMap.map[il, jl] != 0 && userMap.map[il, jl] != userMap.player)
                {
                    isEmpty = false;
                    break;
                }
                il += dirX;
                jl += dirY;

                if (isOneStep)
                    break;
            }
            if (isEmpty)
                return;
            List<Checker> toClose = new List<Checker>();
            bool closeSimple = false;
            int ik = il + dirX;
            int jk = jl + dirY;
            while (IsInsideBorders(ik, jk))
            {
                if (userMap.map[ik, jk] == 0)
                {
                    if (HasEatStep(ik, jk, new int[2] { dirX, dirY }, isOneStep))
                    {
                        closeSimple = true;
                    }
                    else
                    {
                        toClose.Add(userMap.cells[ik]);
                    }
                    userMap.cells[ik].BackColor = Color.Yellow;
                    userMap.cells[ik].Enabled = true;
                    userMap.countEatSteps++;
                }
                else break;
                if (isOneStep)
                    break;
                jk += dirY;
                ik += dirX;
            }
            if (closeSimple && toClose.Count > 0)
            {
                CloseSimpleSteps(toClose);
            }

        }

        public void CloseSimpleSteps(List<Checker> simpleSteps)
        {
            if (simpleSteps.Count > 0)
            {
                for (int i = 0; i < simpleSteps.Count; i++)
                {
                    simpleSteps[i].BackColor = userMap.GetPrevCheckerColor(simpleSteps[i]);
                    simpleSteps[i].Enabled = false;
                }
            }
        }

        public bool HasEatStep(int IFigure, int JFigure, int[] direction, bool isDamka)
        {
            bool stepEat = false;
            int j = JFigure + 1;
            for (int i = IFigure - 1; i >= 0; i--)
            {
                if (userMap.player == 1 && isDamka && !userMap.isContinue) break;
                if (IsInsideBorders(i, j))
                {
                    if (userMap.map[i, j] != 0 && userMap.map[i, j] != userMap.player)
                    {
                        stepEat = true;
                        if (!IsInsideBorders(i - 1, j - 1))
                            stepEat = false;
                        else if (userMap.map[i - 1, j - 1] != 0)
                            stepEat = false;
                        else return stepEat;
                    }
                }
                if (direction[0] == 1 && direction[1] == -1 && !isDamka) break;
                if (j < 7)
                    j++;
                else break;

                if (isDamka)
                    break;
            }

            j = JFigure - 1;
            for (int i = IFigure - 1; i >= 0; i--)
            {
                if (userMap.player == 1 && isDamka && !userMap.isContinue) break;
                if (direction[0] == 1 && direction[1] == 1 && !isDamka) break;
                if (IsInsideBorders(i, j))
                {
                    if (userMap.map[i, j] != 0 && userMap.map[i, j] != userMap.player)
                    {
                        stepEat = true;
                        if (!IsInsideBorders(i - 1, j - 1))
                            stepEat = false;
                        else if (userMap.map[i - 1, j - 1] != 0)
                            stepEat = false;
                        else return stepEat;
                    }
                }
                if (j > 0)
                    j--;
                else break;

                if (isDamka)
                    break;
            }

            j = JFigure - 1;
            for (int i = IFigure + 1; i < 8; i++)
            {
                if (userMap.player == 2 && isDamka && !userMap.isContinue) break;
                if (direction[0] == -1 && direction[1] == 1 && !isDamka) break;
                if (IsInsideBorders(i, j))
                {
                    if (userMap.map[i, j] != 0 && userMap.map[i, j] != userMap.player)
                    {
                        stepEat = true;
                        if (!IsInsideBorders(i - 1, j - 1))
                            stepEat = false;
                        else if (userMap.map[i - 1, j - 1] != 0)
                            stepEat = false;
                        else return stepEat;
                    }
                }
                if (j > 0)
                    j--;
                else break;

                if (isDamka)
                    break;
            }

            j = JFigure + 1;
            for (int i = IFigure + 1; i < 8; i++)
            {
                if (userMap.player == 2 && isDamka && !userMap.isContinue) break;
                if (direction[0] == -1 && direction[1] == -1 && !isDamka) break;
                if (IsInsideBorders(i, j))
                {
                    if (userMap.map[i, j] != 0 && userMap.map[i, j] != userMap.player)
                    {
                        stepEat = true;
                        if (!IsInsideBorders(i - 1, j - 1))
                            stepEat = false;
                        else if (userMap.map[i - 1, j - 1] != 0)
                            stepEat = false;
                        else return stepEat;
                    }
                }
                if (j < 7)
                    j++;
                else break;

                if (isDamka)
                    break;
            }
            return stepEat;
        }
    }
}
