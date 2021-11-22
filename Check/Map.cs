using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Check
{
    class Map
    {
        public bool isMoving;//находится ли шашка в состоянии хода
        public int player;//какой игрок ходит
        public Checker prevCheck;//состояние последней нажатой шашки
        public Checker pressedCheck; // нажатая кнопка
        const int sizeOfMap = 8; //Размер игрового поля
        public const int cellSize = 50;
        public List<Checker> cells = new List<Checker>(); // Лист ячеек
        public int[,] map = new int[sizeOfMap, sizeOfMap];

        public Map()
        {
            Init();
            GenerateMap();
        }
        void GenerateMap()//генерация игрового поля
        {
            for (int i = 0; i < sizeOfMap; i++)
            {
                for (int j = 0; j < sizeOfMap; j++)
                {
                    Checker cell = new Checker();
                    //cell.Size = new Size(cell.Width, cell.Height);
                    cell.Location = new Point(j * cell.Height, i * cell.Width);//меняем позицию ячейки
                    cell.SetColorOfCheker(map[i, j]);
                    cell.SetBackColorOfCell(i, j);
                    cells.Add(cell);
                }
            }
        }

        public Color GetPrevCheckerColor()//получем цвет нажатой шашки
        {
            if (((prevCheck.Location.Y/cellSize % 2) != 0) && ((prevCheck.Location.X/cellSize % 2) == 0)) 
            {
                return Color.Gray;
            }
            else if (((prevCheck.Location.Y / cellSize % 2) == 0) && ((prevCheck.Location.X / cellSize % 2) != 0)) 
            {
                return Color.Gray;
            }
            return Color.White;
        }

        void SetActivePlayer()//меняем игрока
        {
            if (player == 1)
                player = 2;
            else
                player = 1;
        }

        private void Init()//Инициализация игры
        {
            player = 1;
            isMoving = false;
            prevCheck = null;

            map = new int[sizeOfMap, sizeOfMap]
                {
                    {0,1,0,1,0,1,0,1},
                    {1,0,1,0,1,0,1,0},
                    {0,1,0,1,0,1,0,1},
                    {0,0,0,0,0,0,0,0},
                    {0,0,0,0,0,0,0,0},
                    {2,0,2,0,2,0,2,0},
                    {0,2,0,2,0,2,0,2},
                    {2,0,2,0,2,0,2,0}
                };
        }
    }
}
