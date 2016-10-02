using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static Figure activeFigure = null;
        int sizeoffield = 30; //размер одной клетки на доске в пикселях
        private static bool whosMove = true;
        //переменная, которая показывает кто ходит. 
        //Если правда - белые ходят, неправда - черные.
        private static Figure[,] board = new Figure[8, 8]; 
        private static Pawn superPawn = null; // переменная для превращения пешки.
        private PictureBox boardPictureBox = new PictureBox(); // картинка доски

        private void Start()
            //Начальная расстановка и присвоение переменных
            //начального значения на случай новой игры
        {
            whosMove = true;
            activeFigure = null;
            superPawn = null;
            board_pictureBox.Enabled = true;
            label4.Text = "Никому";
            label6.Text = "Никому";
            label2.Text = "Белые";
            board[0, 0] = new Castle(0, 0, false);
            board[1, 0] = new Knight(1, 0, false);
            board[2, 0] = new Bishop(2, 0, false);
            board[3, 0] = new Queen(3, 0, false);
            board[4, 0] = new King(4, 0, false);
            board[5, 0] = new Bishop(5, 0, false);
            board[6, 0] = new Knight(6, 0, false);
            board[7, 0] = new Castle(7, 0, false);
            for (int x = 0; x < 8; x++)
            {
                board[x, 1] = new Pawn(x, 1, false);
            }

            for (int x = 0; x < 8; x++)
            {
                for (int y = 2; y < 6; y++)
                {
                    board[x, y] = new EmptyFigure(x, y);

                }

            }
            for (int x = 0; x < 8; x++)
            {
                board[x, 6] = new Pawn(x, 6, true);
            }
            board[0, 7] = new Castle(0, 7, true);
            board[1, 7] = new Knight(1, 7, true);
            board[2, 7] = new Bishop(2, 7, true);
            board[3, 7] = new Queen(3, 7, true);
            board[4, 7] = new King(4, 7, true);
            board[5, 7] = new Bishop(5, 7, true);
            board[6, 7] = new Knight(6, 7, true);
            board[7, 7] = new Castle(7, 7, true);
            panel1.Controls.Clear();
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (!(board[x, y] is EmptyFigure))
                    {

                        panel1.Controls.Add(board[x, y].GetPictorgam);

                    }
                }
            }

            boardPictureBox.Location = new Point(0, 0);
            boardPictureBox.Size = new Size(240, 240);
            boardPictureBox.Image = new Bitmap(Properties.Resources.Доска);
            panel1.Controls.Add(boardPictureBox);
        }
        private void SuperPawn(Pawn superPawn, string typeOfNewFigure) 
            //функция для превращения пешки в другие фигуры
        {
            int x = superPawn.PositionX;
            int y = superPawn.PositionY;
            bool colorOfTheFigure = Convert.ToBoolean(superPawn.GetColorOfTheFigure);

            switch (typeOfNewFigure)
            {
                case "Ферзь":
                    board[x, y] = new Queen(x, y, colorOfTheFigure);
                    panel1.Controls.Add(board[x, y].GetPictorgam);
                    break;
                case "Ладья":
                    board[x, y] = new Castle(x, y, colorOfTheFigure);
                    panel1.Controls.Add(board[x, y].GetPictorgam);
                    break;
                case "Конь":
                    board[x, y] = new Knight(x, y, colorOfTheFigure);
                    panel1.Controls.Add(board[x, y].GetPictorgam);
                    break;
                case "Слон":
                    board[x, y] = new Bishop(x, y, colorOfTheFigure);
                    panel1.Controls.Add(board[x, y].GetPictorgam);
                    break;
            }
            panel1.Controls.Remove(boardPictureBox); 
            panel1.Controls.Add(boardPictureBox);
            panel1.Controls.Remove(superPawn.GetPictorgam);
            superPawn = null;
            Queen_Button.Enabled = false;
            Bishop_Button.Enabled = false;
            Castle_Button.Enabled = false;
            Knight_Button.Enabled = false;
            board_pictureBox.Enabled = true;
            labelsuperpawn.Text = "";
            ChechBoard();
        }
        private int check_Coordinate(int c) //в эту функцию передаются координаты клеток в пикселях
        {

            if (c < sizeoffield) return 0;
            if (c > sizeoffield && c < 2 * sizeoffield) return 1;
            if (c > 2 * sizeoffield && c < 3 * sizeoffield) return 2;
            if (c > 3 * sizeoffield && c < 4 * sizeoffield) return 3;
            if (c > 4 * sizeoffield && c < 5 * sizeoffield) return 4;
            if (c > 5 * sizeoffield && c < 6 * sizeoffield) return 5;
            if (c > 6 * sizeoffield && c < 7 * sizeoffield) return 6;
            if (c > 7 * sizeoffield && c < 8 * sizeoffield) return 7;
            return -1;
        }
        private void pictureBox1_Click(object sender, EventArgs e) //клик по доске
        {
            MouseEventArgs eve = e as MouseEventArgs;
            int x = check_Coordinate(eve.X);
            int y = check_Coordinate(eve.Y);

            if (x >= 0 && y >= 0)
            {
                if (activeFigure != null)
                {
                    if (board[x, y].GetColorOfTheFigure == whosMove)
                    {
                        activeFigure = board[x, y];
                    }
                    else
                    {
                        if (activeFigure.Go(board[x, y], board, panel1))
                        {
                            whosMove = !(whosMove);
                            if (activeFigure is Pawn)
                            {
                                if ((activeFigure.GetColorOfTheFigure == true && 
                                    activeFigure.PositionY == 0)||
                                    ( activeFigure.GetColorOfTheFigure == false && 
                                    activeFigure.PositionY == 7))
                                {
                                    superPawn = activeFigure as Pawn;
                                    Queen_Button.Enabled = true;
                                    Bishop_Button.Enabled = true;
                                    Castle_Button.Enabled = true;
                                    Knight_Button.Enabled = true;
                                    labelsuperpawn.Text = "На что заменить пешку?";
                                    board_pictureBox.Enabled = false;
                                }
                            }
                            ChechBoard();
                        }
                        activeFigure = null;
                    }
                }
                else
                {
                    if (!(board[x, y] is EmptyFigure) && board[x, y].GetColorOfTheFigure == whosMove)
                    {
                        activeFigure = board[x, y];
                    }
                }
            }
        }
        private void ChechBoard()
            //функция проверки наличия шахов и матов на доске
        {
            bool? isitcheck = CheckClass.IsItCheck(board);

            if (isitcheck != null)
            {
                if (isitcheck == true)
                {
                    label6.Text = "Белым";
                }
                else
                {
                    label6.Text = "Черным";
                }

                bool? whoisonmate = CheckClass.IsItMate(board, Convert.ToBoolean(isitcheck));
                if (whoisonmate != null)
                {
                    if (whoisonmate == true)
                    {
                        label4.Text = "Белым";

                    }
                    else
                    {
                        label4.Text = "Черным";

                    }
                    board_pictureBox.Enabled = false;
                    //отключение возмозможности ходить
                }
            }
            else
            {
                label4.Text = "Никому";
                label6.Text = "Никому";
            }

            if (whosMove)
            {
                label2.Text = "Белые";
            }
            else
            {
                label2.Text = "Черные";
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Start();
        }

        private void Queen_Button_Click(object sender, EventArgs e) //тут и далее функции клика по кнопке превращения
        {
            SuperPawn(superPawn, Queen_Button.Text);
        }

        private void Knight_Button_Click(object sender, EventArgs e)
        {
            SuperPawn(superPawn, Knight_Button.Text);
        }

        private void Bishop_Button_Click(object sender, EventArgs e)
        {
            SuperPawn(superPawn, Bishop_Button.Text);
        }

        private void Castle_Button_Click(object sender, EventArgs e)
        {
            SuperPawn(superPawn, Castle_Button.Text);
        }

        private void New_Game_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
