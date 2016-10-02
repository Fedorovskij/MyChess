using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Chess
{
    static class CheckClass 
    {
        public static Figure[,] BoardAfterGo( Figure activeFigure, Figure goToFigure, Figure[,] allFigures) 
            //функция которая возвращает доску если ход будет произведем,
            //при этом не изменяя саму доску. 
        {
            Figure[,] board = new Figure[8, 8];
            for (int i = 0;i<8;i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    board[i, k] = allFigures[i, k].Clone() as Figure;
                }
            }
            int x =activeFigure.PositionX;
            int y = activeFigure.PositionY;
            board[x, y].PositionX = goToFigure.PositionX;
            board[x, y].PositionY = goToFigure.PositionY;
            board[goToFigure.PositionX, goToFigure.PositionY] = activeFigure;
            board[x, y] = new EmptyFigure(x, y);
            return board;
        }
        public static bool? IsItCheck(Figure[,] allfigures)
            //Функция проверки поставлен ли кому-то шах на доске
            //Если белым шах вернет правду, если черныи неправду, если никому - нулевая ссылка
        {

            
            List<Figure> whiteFigures = new List<Figure>();
            List<Figure> blackFigures = new List<Figure>();
            Figure blackKing = null;
            Figure whiteKing = null;
            foreach (Figure figure in allfigures)
            {
                if (!(figure is EmptyFigure))
                {
                    if (figure is King)
                    {

                        if (figure.GetColorOfTheFigure == true)
                        {
                            whiteKing = figure;
                        }
                        else
                        {
                            blackKing = figure;
                        }
                    }
                    else
                    {
                        if (figure.GetColorOfTheFigure == true)
                        {
                            whiteFigures.Add(figure);
                        }
                        else
                        {
                            blackFigures.Add(figure);
                        }
                    }
                }
            }
            foreach (Figure figure in whiteFigures)
            {
                if (figure.CheckPosibleMoves(allfigures).Contains(blackKing))
                {
                    return false;
                }
            }
            foreach (Figure figure in blackFigures)
            {
                if (figure.CheckPosibleMoves(allfigures).Contains(whiteKing))
                {
                    return true;
                }
            }
            return null;
        }
        public static bool? IsItMate(Figure[,] allfigures, bool whoIsOnCheck)
        //Функция проверки поставлен ли кому-то мат на доске
        //Если белым мат вернет правду, если черныи неправду, если никому - нулевая ссылка
        {
            List<Figure> whiteFigures = new List<Figure>();
            List<Figure> blackFigures = new List<Figure>();
            Figure blackKing = null;
            Figure whiteKing = null;
            bool _whoIsOnCheck = whoIsOnCheck;
            foreach (Figure figure in allfigures)
            {
                if (!(figure is EmptyFigure))
                {
                    if (figure.GetColorOfTheFigure == true)
                    {
                        whiteFigures.Add(figure);
                    }
                    else
                    {
                        blackFigures.Add(figure);
                    }
                }
                if (figure is King)
                {
                    King king = figure as King;
                    if (king.GetColorOfTheFigure == true)
                    {
                        whiteKing = king;
                    }
                    else
                    {
                        blackKing = king;
                    }
                }
            }
            if (whoIsOnCheck == true)
            {
                foreach (Figure figure in whiteFigures)
                {
                    foreach (Figure posiblemove in figure.CheckPosibleMoves(allfigures))
                    {
                        if (IsItCheck(BoardAfterGo(figure, posiblemove, allfigures)) == null)
                        {
                            return null;
                        }
                    }
                }
            }
            if (whoIsOnCheck == false)
            {
                foreach (Figure figure in blackFigures)
                {
                    foreach (Figure posiblemove in figure.CheckPosibleMoves(allfigures))
                    {
                        if (IsItCheck(BoardAfterGo(figure, posiblemove, allfigures)) == null)
                        {
                            return null;
                        }
                    }
                }
            }
            return _whoIsOnCheck;
        }
    }
    abstract class Figure : ICloneable
    {
        protected int sizeoffield = 30; 
        public abstract bool? GetColorOfTheFigure { get; } //возвращает цвет фигуры
        public abstract PictureBox GetPictorgam{ get; }// возвращает пиктограмму фигуры на доске
        public abstract List<Figure> CheckPosibleMoves(Figure[,] mass); 
        //возврящение всех возможных ходов какой либо фигуры
        public abstract void UpdatePictogram();
        // обновление фона пикрограммы при перехода с поля на поле
        public bool Go( Figure goToFigure, Figure[,] allFigures, Panel panel1 ) 
            //функция хода, меняет доску и обновляет отображение

        {
            int x = PositionX;
            int y = PositionY;
            if (CheckPosibleMoves(allFigures).Contains(goToFigure))
            {
               if (CheckClass.IsItCheck(CheckClass.BoardAfterGo(this, goToFigure, allFigures)) != GetColorOfTheFigure)
                {
                    if ((this is King&& PositionX == 4))
                        //рокировка
                    {
                        if (goToFigure == allFigures[x + 2, y])
                        {
                            bool? c = allFigures[x + 3, y].Go(allFigures[x + 1, y], allFigures, panel1);
                        }
                        if (goToFigure == allFigures[x - 2, y])
                        {
                            bool? c = allFigures[x - 4, y].Go(allFigures[x - 1, y], allFigures, panel1);
                        }
                    }
                    if (!(goToFigure is EmptyFigure))
                    {
                        panel1.Controls.Remove(goToFigure.GetPictorgam);
                    }
                    PositionX = goToFigure.PositionX;
                    PositionY = goToFigure.PositionY;
                    allFigures[PositionX, PositionY] = this;
                    allFigures[x, y] = new EmptyFigure(x, y);
                    UpdatePictogram();
                    return true;
                }
            }
            return false;
        }

        public abstract object Clone();

        //изменение, или возвращение положения по x,y
        public abstract int PositionX { get; set; }
        public abstract int PositionY { get; set; }
    }
    class King : Figure
    {
        private PictureBox pictogram = new PictureBox();
        
        private readonly Bitmap pictorgamOnBlack;
        private readonly Bitmap pictorgamOnWhite;
        private readonly bool _colorOfTheFigure;
        int positionX;
        int positionY;
        public King(int x, int y, bool colorOfTheFigure)
        {
            positionX = x;
            positionY = y;
            _colorOfTheFigure = colorOfTheFigure;
            if (colorOfTheFigure)
            {
                pictorgamOnBlack = new Bitmap(Properties.Resources.БелыйКорольНаЧерном);
                pictorgamOnWhite = new Bitmap(Properties.Resources.БелыйКорольНаБелом);
            }
            else
            {
                pictorgamOnBlack = new Bitmap(Properties.Resources.ЧерныйКорольНаЧерном);
                pictorgamOnWhite = new Bitmap(Properties.Resources.ЧерныйКорольНаБелом);
            }
            pictogram.Size = new Size(30, 30);
            UpdatePictogram();
        }
        public override PictureBox GetPictorgam
        {
            get
            {
                return pictogram;
            }
        }
        public override bool? GetColorOfTheFigure
        {
            get
            {
                return _colorOfTheFigure;
            }
        }

        public override int PositionX
        {
            get
            {
                return positionX;
            }

            set
            {
                positionX = value;
            }
        }
        public override int PositionY
        {
            get
            {
                return positionY;
            }
            set
            {
                positionY = value;
            }
        }
        public override List<Figure> CheckPosibleMoves(Figure[,] allFigures)
        {
            List<Figure> PosibleMoves = new List<Figure>();
            int x = positionX;
            int y = positionY;
            for (int i = x-1; i<x+2; i++ )
            {
                for(int k = y - 1; k < y + 2; k++)
                {
                    if (i > -1 && i < 8 && k > -1 && k < 8)
                    {
                        if (allFigures[i, k] is EmptyFigure ||
                            allFigures[i, k].GetColorOfTheFigure != GetColorOfTheFigure)
                        {
                            PosibleMoves.Add(allFigures[i, k]);
                        }
                    }
                }
            }
            if( positionX==4&&CheckClass.IsItCheck(allFigures)!=GetColorOfTheFigure)
            {
                if ((GetColorOfTheFigure == true && positionY == 7)||
                    (GetColorOfTheFigure == false && positionY == 0))
                {
                    if (allFigures[x + 1, y] is EmptyFigure && 
                        allFigures[x + 2, y] is EmptyFigure && 
                        allFigures[x + 3, y] is Castle &&
                        allFigures[x + 3, y].GetColorOfTheFigure == GetColorOfTheFigure)
                    {
                        PosibleMoves.Add(allFigures[x + 2, y]);
                    }
                    if (allFigures[x - 1, y] is EmptyFigure && 
                        allFigures[x - 2, y] is EmptyFigure && 
                        allFigures[x - 3, y] is EmptyFigure)
                    {
                        if (allFigures[x - 4, y] is Castle && 
                            allFigures[x - 4, y].GetColorOfTheFigure == GetColorOfTheFigure)
                            PosibleMoves.Add(allFigures[x - 2, y]);
                    }
                }
            }
            return PosibleMoves;
        }

        public override void UpdatePictogram()
        {
            pictogram.Location = new Point(PositionX* sizeoffield, PositionY* sizeoffield);
            if ((PositionX % 2 == 0 && PositionY % 2 != 0) || (PositionX % 2 != 0 && PositionY % 2 == 0))
            {
                pictogram.Image = pictorgamOnBlack;
            }
            else
            {
                pictogram.Image = pictorgamOnWhite;
            }
        }

        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
    class Queen : Figure
    {
        private PictureBox pictogram = new PictureBox();
        private readonly Bitmap pictorgamOnBlack;
        private readonly Bitmap pictorgamOnWhite;
        private readonly bool _colorOfTheFigure;
        int positionX;
        int positionY;
        public Queen(int x, int y, bool colorOfTheFigure)
        {
            positionX = x;
            positionY = y;
            _colorOfTheFigure = colorOfTheFigure;
            if (colorOfTheFigure)
            {
                pictorgamOnBlack = new Bitmap(Properties.Resources.БелыйФерзьНаЧерном);
                pictorgamOnWhite = new Bitmap(Properties.Resources.БелыйФерзьНаБелом);
            }
            else
            {
                pictorgamOnBlack = new Bitmap(Properties.Resources.ЧерныйФерзьНаЧерном);
                pictorgamOnWhite = new Bitmap(Properties.Resources.ЧерныйФерзьНаБелом);
            }
            pictogram.Size = new Size(30, 30);
            UpdatePictogram();
        }
        public override void UpdatePictogram()
        {
            pictogram.Location = new Point(PositionX * sizeoffield, PositionY * sizeoffield);
            if ((PositionX % 2 == 0 && PositionY % 2 != 0) ||
                (PositionX % 2 != 0 && PositionY % 2 == 0))
            {
                pictogram.Image = pictorgamOnBlack;
            }
            else
            {
                pictogram.Image = pictorgamOnWhite;
            }
        }
        public override PictureBox GetPictorgam
        {
            get
            {
                return pictogram;
            }
        }
        public override bool? GetColorOfTheFigure
        {
            get
            {
                return _colorOfTheFigure;
            }
        }
        public override int PositionX
        {
            get
            {
                return positionX;
            }
            set
            {
                positionX = value;
            }
        }
        public override int PositionY
        {
            get
            {
                return positionY;
            }

            set
            {
                positionY = value;
            }
        }
        public override List<Figure> CheckPosibleMoves(Figure[,] allFigures)
        {
            List<Figure> PosibleMoves = new List<Figure>();
            int posX = positionX;
            int posY = positionY;
            for (int x = posX + 1, y = posY; x < 9; x++)
            {
                if (x > -1 && x < 8&& y > -1 && y < 8)
                {

                    if (allFigures[x, y] is EmptyFigure)
                    {
                        PosibleMoves.Add(allFigures[x, y]);

                    }
                    else
                    {
                        if (allFigures[x, y].GetColorOfTheFigure == GetColorOfTheFigure)
                        {
                            break;
                        }
                        if (allFigures[x, y].GetColorOfTheFigure != GetColorOfTheFigure)
                        {
                            PosibleMoves.Add(allFigures[x, y]);
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            for (int x = posX - 1, y = posY; x > -1; x--)
            {
                if (x > -1 && x < 8 && y > -1 && y < 8)
                {
                    if (allFigures[x, y] is EmptyFigure)
                    {
                        PosibleMoves.Add(allFigures[x, y]);

                    }
                    else
                    {
                        if (allFigures[x, y].GetColorOfTheFigure == GetColorOfTheFigure)
                        {
                            break;
                        }
                        if (allFigures[x, y].GetColorOfTheFigure != GetColorOfTheFigure)
                        {
                            PosibleMoves.Add(allFigures[x, y]);
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            for (int x = posX , y = posY-1; y > -1; y--)
            {
                if (x > -1 && x < 8 && y > -1 && y < 8)
                {

                    if (allFigures[x, y] is EmptyFigure)
                    {
                        PosibleMoves.Add(allFigures[x, y]);

                    }
                    else
                    {
                        if (allFigures[x, y].GetColorOfTheFigure == GetColorOfTheFigure)
                        {
                            break;
                        }
                        if (allFigures[x, y].GetColorOfTheFigure != GetColorOfTheFigure)
                        {
                            PosibleMoves.Add(allFigures[x, y]);
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            for (int x = posX, y = posY+1; y <9; y++)
            {
                if (x > -1 && x < 8 && y > -1 && y < 8)
                {

                    if (allFigures[x, y] is EmptyFigure)
                    {
                        PosibleMoves.Add(allFigures[x, y]);

                    }
                    else
                    {
                        if (allFigures[x, y].GetColorOfTheFigure == GetColorOfTheFigure)
                        {
                            break;
                        }
                        if (allFigures[x, y].GetColorOfTheFigure != GetColorOfTheFigure)
                        {
                            PosibleMoves.Add(allFigures[x, y]);
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
          
            for (int x = posX + 1, y = posY + 1; x < 9 && y < 9; x++, y++)
            {
                if (x > -1 && x < 8 && y > -1 && y < 8)
                {

                    if (allFigures[x, y] is EmptyFigure)
                    {
                        PosibleMoves.Add(allFigures[x, y]);

                    }
                    else
                    {
                        if (allFigures[x, y].GetColorOfTheFigure == GetColorOfTheFigure)
                        {
                            break;
                        }
                        if (allFigures[x, y].GetColorOfTheFigure != GetColorOfTheFigure)
                        {
                            PosibleMoves.Add(allFigures[x, y]);
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            for (int x = posX - 1, y = posY - 1; x > -1 && y > -1; x--, y--)
            {
                if (x > -1 && x < 8 && y > -1 && y < 8)
                {

                    if (allFigures[x, y] is EmptyFigure)
                    {
                        PosibleMoves.Add(allFigures[x, y]);

                    }
                    else
                    {
                        if (allFigures[x, y].GetColorOfTheFigure == GetColorOfTheFigure)
                        {
                            break;
                        }
                        if (allFigures[x, y].GetColorOfTheFigure != GetColorOfTheFigure)
                        {
                            PosibleMoves.Add(allFigures[x, y]);
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            for (int x = posX - 1, y = posY + 1; x > -1 && y < 9; x--, y++)
            {
                if (x > -1 && x < 8 && y > -1 && y < 8)
                {

                    if (allFigures[x, y] is EmptyFigure)
                    {
                        PosibleMoves.Add(allFigures[x, y]);

                    }
                    else
                    {
                        if (allFigures[x, y].GetColorOfTheFigure == GetColorOfTheFigure)
                        {
                            break;
                        }
                        if (allFigures[x, y].GetColorOfTheFigure != GetColorOfTheFigure)
                        {
                            PosibleMoves.Add(allFigures[x, y]);
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            for (int x = posX + 1, y = posY - 1; x < 9 && y > -1; x++, y--)
            {
                if (x > -1 && x < 8 && y > -1 && y < 8)
                {

                    if (allFigures[x, y] is EmptyFigure)
                    {
                        PosibleMoves.Add(allFigures[x, y]);

                    }
                    else
                    {
                        if (allFigures[x, y].GetColorOfTheFigure == GetColorOfTheFigure)
                        {
                            break;
                        }
                        if (allFigures[x, y].GetColorOfTheFigure != GetColorOfTheFigure)
                        {
                            PosibleMoves.Add(allFigures[x, y]);
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            return PosibleMoves;
        }
        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
    class Castle : Figure //Ладья
    {
        private PictureBox pictogram = new PictureBox();
        private readonly Bitmap pictorgamOnWhite;
        private readonly Bitmap pictorgamOnBlack;
        private readonly bool _colorOfTheFigure;
        int positionX;
        int positionY;
        public Castle(int x, int y, bool colorOfTheFigure)
        {
            positionX = x;
            positionY = y;
            _colorOfTheFigure = colorOfTheFigure;
            if (colorOfTheFigure)
            {
                pictorgamOnBlack = new Bitmap(Properties.Resources.БелаяЛадьяНаЧерном);
                pictorgamOnWhite = new Bitmap(Properties.Resources.БелаяЛадьяНаБелом);
            }
            else
            {
                pictorgamOnBlack = new Bitmap(Properties.Resources.ЧернаяЛадьяНаЧерном);
                pictorgamOnWhite = new Bitmap(Properties.Resources.ЧернаяЛадьяНаБелом);
            }
            pictogram.Size = new Size(30, 30);
            UpdatePictogram();
        }
        public override void UpdatePictogram()
        {
            pictogram.Location = new Point(PositionX * sizeoffield, PositionY * sizeoffield);
            if ((PositionX % 2 == 0 && PositionY % 2 != 0) ||
                (PositionX % 2 != 0 && PositionY % 2 == 0))
            {
                pictogram.Image = pictorgamOnBlack;
            }
            else
            {
                pictogram.Image = pictorgamOnWhite;
            }
        }
        public override PictureBox GetPictorgam
        {
            get
            {
                return pictogram;
            }
        }
        public override bool? GetColorOfTheFigure
        {
            get
            {
                return _colorOfTheFigure;
            }
        }
        public override int PositionX
        {
            get
            {
                return positionX;
            }
            set
            {
                positionX = value;
            }
        }
        public override int PositionY
        {
            get
            {
                return positionY;
            }
            set
            {
                positionY = value;
            }
        }
        public override List<Figure> CheckPosibleMoves(Figure[,] allFigures)
        {
            List<Figure> PosibleMoves = new List<Figure>();
            int posX = positionX;
            int posY = positionY;
            for (int x = posX + 1, y = posY; x < 9; x++)
            {
                if (x > -1 && x < 8 && y > -1 && y < 8)
                {
                    if (allFigures[x, y] is EmptyFigure)
                    {
                        PosibleMoves.Add(allFigures[x, y]);
                    }
                    else
                    {
                        if (allFigures[x, y].GetColorOfTheFigure == GetColorOfTheFigure)
                        {
                            break;
                        }
                        if (allFigures[x, y].GetColorOfTheFigure != GetColorOfTheFigure)
                        {
                            PosibleMoves.Add(allFigures[x, y]);
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            for (int x = posX - 1, y = posY; x > -1; x--)
            {
                if (x > -1 && x < 8 && y > -1 && y < 8)
                {

                    if (allFigures[x, y] is EmptyFigure)
                    {
                        PosibleMoves.Add(allFigures[x, y]);

                    }
                    else
                    {
                        if (allFigures[x, y].GetColorOfTheFigure == GetColorOfTheFigure)
                        {
                            break;
                        }
                        if (allFigures[x, y].GetColorOfTheFigure != GetColorOfTheFigure)
                        {
                            PosibleMoves.Add(allFigures[x, y]);
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            for (int x = posX, y = posY - 1; y > -1; y--)
            {
                if (x > -1 && x < 8 && y > -1 && y < 8)
                {

                    if (allFigures[x, y] is EmptyFigure)
                    {
                        PosibleMoves.Add(allFigures[x, y]);

                    }
                    else
                    {
                        if (allFigures[x, y].GetColorOfTheFigure == GetColorOfTheFigure)
                        {
                            break;
                        }
                        if (allFigures[x, y].GetColorOfTheFigure != GetColorOfTheFigure)
                        {
                            PosibleMoves.Add(allFigures[x, y]);
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            for (int x = posX, y = posY + 1; y < 9; y++)
            {
                if (x > -1 && x < 8 && y > -1 && y < 8)
                {

                    if (allFigures[x, y] is EmptyFigure)
                    {
                        PosibleMoves.Add(allFigures[x, y]);

                    }
                    else
                    {
                        if (allFigures[x, y].GetColorOfTheFigure == GetColorOfTheFigure)
                        {
                            break;
                        }
                        if (allFigures[x, y].GetColorOfTheFigure != GetColorOfTheFigure)
                        {
                            PosibleMoves.Add(allFigures[x, y]);
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            return PosibleMoves;
        }
        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
    class Bishop : Figure //Слон
    {
        private PictureBox pictogram = new PictureBox();
        private readonly Bitmap pictorgamOnBlack;
        private readonly Bitmap pictorgamOnWhite;
        private readonly bool _colorOfTheFigure;
        int positionX;
        int positionY;
        public Bishop(int x, int y, bool colorOfTheFigure)
        {
            positionX = x;
            positionY = y;
            _colorOfTheFigure = colorOfTheFigure;
            if (colorOfTheFigure)
            {
                pictorgamOnBlack = new Bitmap(Properties.Resources.БелыйСлонНаЧерном);
                pictorgamOnWhite = new Bitmap(Properties.Resources.БелыйСлонНаБелом);
            }
            else
            {
                pictorgamOnBlack = new Bitmap(Properties.Resources.ЧерныйСлонНаЧерном);
                pictorgamOnWhite = new Bitmap(Properties.Resources.ЧерныйСлонНаБелом);
            }
            pictogram.Size = new Size(30, 30);
            UpdatePictogram();
        }
        public override void UpdatePictogram()
        {
            pictogram.Location = new Point(PositionX * sizeoffield, PositionY * sizeoffield);
            if ((PositionX % 2 == 0 && PositionY % 2 != 0) || 
                (PositionX % 2 != 0 && PositionY % 2 == 0))
            {
                pictogram.Image = pictorgamOnBlack;
            }
            else
            {
                pictogram.Image = pictorgamOnWhite;
            }
        }
        public override PictureBox GetPictorgam
        {
            get
            {
                return pictogram;
            }
        }
        public override bool? GetColorOfTheFigure
        {
            get
            {
                return _colorOfTheFigure;
            }
        }
        public override int PositionX
        {
            get
            {
                return positionX;
            }

            set
            {
                positionX = value;
            }
        }
        public override int PositionY
        {
            get
            {
                return positionY;
            }
            set
            {
                positionY = value;
            }
        }
        public override List<Figure> CheckPosibleMoves(Figure[,] allFigures)
        {
            List<Figure> PosibleMoves = new List<Figure>();
            int posX = positionX;
            int posY = positionY;
            for (int x = posX + 1, y = posY + 1; x < 9 && y < 9; x++, y++)
            {
                if (x > -1 && x < 8 && y > -1 && y < 8)
                {

                    if (allFigures[x, y] is EmptyFigure)
                    {
                        PosibleMoves.Add(allFigures[x, y]);

                    }
                    else
                    {
                        if (allFigures[x, y].GetColorOfTheFigure == GetColorOfTheFigure)
                        {
                            break;
                        }
                        if (allFigures[x, y].GetColorOfTheFigure != GetColorOfTheFigure)
                        {
                            PosibleMoves.Add(allFigures[x, y]);
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            for (int x = posX - 1, y = posY - 1; x > -1 && y > -1; x--, y--)
            {
                if (x > -1 && x < 8 && y > -1 && y < 8)
                {

                    if (allFigures[x, y] is EmptyFigure)
                    {
                        PosibleMoves.Add(allFigures[x, y]);

                    }
                    else
                    {
                        if (allFigures[x, y].GetColorOfTheFigure == GetColorOfTheFigure)
                        {
                            break;
                        }
                        if (allFigures[x, y].GetColorOfTheFigure != GetColorOfTheFigure)
                        {
                            PosibleMoves.Add(allFigures[x, y]);
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            for (int x = posX - 1, y = posY + 1; x > -1 && y < 9; x--, y++)
            {
                if (x > -1 && x < 8 && y > -1 && y < 8)
                {
                    if (allFigures[x, y] is EmptyFigure)
                    {
                        PosibleMoves.Add(allFigures[x, y]);

                    }
                    else
                    {
                        if (allFigures[x, y].GetColorOfTheFigure == GetColorOfTheFigure)
                        {
                            break;
                        }
                        if (allFigures[x, y].GetColorOfTheFigure != GetColorOfTheFigure)
                        {
                            PosibleMoves.Add(allFigures[x, y]);
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            for (int x = posX + 1, y = posY - 1; x < 9 && y > -1; x++, y--)
            {
                if (x > -1 && x < 8 && y > -1 && y < 8)
                {

                    if (allFigures[x, y] is EmptyFigure)
                    {
                        PosibleMoves.Add(allFigures[x, y]);

                    }
                    else
                    {
                        if (allFigures[x, y].GetColorOfTheFigure == GetColorOfTheFigure)
                        {
                            break;
                        }
                        if (allFigures[x, y].GetColorOfTheFigure != GetColorOfTheFigure)
                        {
                            PosibleMoves.Add(allFigures[x, y]);
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            return PosibleMoves;
        }
        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
    class Knight : Figure //конь
    {
        private PictureBox pictogram = new PictureBox();
        private readonly Bitmap pictorgamOnBlack;
        private readonly Bitmap pictorgamOnWhite;
        private readonly bool _colorOfTheFigure;
        int positionX;
        int positionY;
        public Knight(int x, int y, bool colorOfTheFigure)
        {
            positionX = x;
            positionY = y;
            _colorOfTheFigure = colorOfTheFigure;
            if (colorOfTheFigure)
            {
                pictorgamOnBlack = new Bitmap(Properties.Resources.БелыйКоньНаЧерном);
                pictorgamOnWhite = new Bitmap(Properties.Resources.БелыйКоньНаБелом);
            }
            else
            {
                pictorgamOnBlack = new Bitmap(Properties.Resources.ЧерныйКоньНаЧерном);
                pictorgamOnWhite = new Bitmap(Properties.Resources.ЧерныйКоньНаБелом);
            }
            pictogram.Size = new Size(30, 30);
            UpdatePictogram();
        }
        public override void UpdatePictogram()
        {
            pictogram.Location = new Point(PositionX * sizeoffield, PositionY * sizeoffield);
            if ((PositionX % 2 == 0 && PositionY % 2 != 0) || 
                (PositionX % 2 != 0 && PositionY % 2 == 0))
            {
                pictogram.Image = pictorgamOnBlack;
            }
            else
            {
                pictogram.Image = pictorgamOnWhite;
            }
        }
        public override bool? GetColorOfTheFigure
        {
            get
            {
                return _colorOfTheFigure;
            }
        }

        public override int PositionX
        {
            get
            {
                return positionX;
            }

            set
            {
                positionX = value;
            }
        }

        public override int PositionY
        {
            get
            {
                return positionY;
            }

            set
            {
                positionY = value;
            }
        }
        public override PictureBox GetPictorgam
        {
            get
            {
                return pictogram;
            }
        }
        public override List<Figure> CheckPosibleMoves(Figure[,] allFigures)
        {
            List<Figure> PosibleMoves = new List<Figure>();
            int posX = positionX;
            int posY = positionY;
            if (posX - 2 > -1 && posX - 2 < 8 && posY -1 > -1 && posY - 1 < 8)
            {
                if (allFigures[posX - 2, posY - 1] is EmptyFigure)
                {
                    PosibleMoves.Add(allFigures[posX - 2, posY - 1]);

                }
                else
                {
                    if (allFigures[posX - 2, posY - 1].GetColorOfTheFigure != GetColorOfTheFigure)
                    {
                        PosibleMoves.Add(allFigures[posX - 2, posY - 1]);
                    }
                }

            }
            if (posX - 2 > -1 && posX - 2 < 8 && posY + 1 > -1 && posY + 1 < 8)
            {
                if (allFigures[posX - 2, posY + 1] is EmptyFigure)
                {
                    PosibleMoves.Add(allFigures[posX - 2, posY + 1]);

                }
                else
                {
                    if (allFigures[posX - 2, posY + 1].GetColorOfTheFigure != GetColorOfTheFigure)
                    {
                        PosibleMoves.Add(allFigures[posX - 2, posY + 1]);
                    }
                }

            }
            if (posX - 1 > -1 && posX - 1 < 8 && posY - 2 > -1 && posY - 2 < 8)
            {
                if (allFigures[posX - 1, posY - 2] is EmptyFigure)
                {
                    PosibleMoves.Add(allFigures[posX - 1, posY - 2]);

                }
                else
                {
                    if (allFigures[posX - 1, posY - 2].GetColorOfTheFigure != GetColorOfTheFigure)
                    {
                        PosibleMoves.Add(allFigures[posX - 1, posY - 2]);
                    }
                }

            }
            if (posX - 1 > -1 && posX - 1 < 8 && posY + 2 > -1 && posY + 2 < 8)
            {
                if (allFigures[posX - 1, posY + 2] is EmptyFigure)
                {
                    PosibleMoves.Add(allFigures[posX - 1, posY + 2]);

                }
                else
                {
                    if (allFigures[posX - 1, posY + 2].GetColorOfTheFigure != GetColorOfTheFigure)
                    {
                        PosibleMoves.Add(allFigures[posX - 1, posY + 2]);
                    }
                }

            }
            if (posX + 1 > -1 && posX + 1 < 8 && posY - 2 > -1 && posY - 2 < 8)
            {
                if (allFigures[posX + 1, posY - 2] is EmptyFigure)
                {
                    PosibleMoves.Add(allFigures[posX + 1, posY - 2]);

                }
                else
                {
                    if (allFigures[posX + 1, posY - 2].GetColorOfTheFigure != GetColorOfTheFigure)
                    {
                        PosibleMoves.Add(allFigures[posX + 1, posY - 2]);
                    }
                }

            }
            if (posX + 2 > -1 && posX + 2 < 8 && posY - 1 > -1 && posY - 1 < 8)
            {
                if (allFigures[posX + 2, posY - 1] is EmptyFigure)
                {
                    PosibleMoves.Add(allFigures[posX + 2, posY - 1]);

                }
                else
                {
                    if (allFigures[posX + 2, posY - 1].GetColorOfTheFigure != GetColorOfTheFigure)
                    {
                        PosibleMoves.Add(allFigures[posX + 2, posY - 1]);
                    }
                }

            }
            if (posX + 2 > -1 && posX + 2 < 8 && posY + 1 > -1 && posY + 1 < 8)
            {
                if (allFigures[posX + 2, posY + 1] is EmptyFigure)
                {
                    PosibleMoves.Add(allFigures[posX + 2, posY + 1]);

                }
                else
                {
                    if (allFigures[posX + 2, posY + 1].GetColorOfTheFigure != GetColorOfTheFigure)
                    {
                        PosibleMoves.Add(allFigures[posX + 2, posY + 1]);
                    }
                }

            }
            if (posX + 1 > -1 && posX + 1 < 8 && posY + 2 > -1 && posY + 2 < 8)
            {
                if (allFigures[posX + 1, posY + 2] is EmptyFigure)
                {
                    PosibleMoves.Add(allFigures[posX + 1, posY + 2]);

                }
                else
                {
                    if (allFigures[posX + 1, posY + 2].GetColorOfTheFigure != GetColorOfTheFigure)
                    {
                        PosibleMoves.Add(allFigures[posX + 1, posY + 2]);
                    }
                }

            }
            return PosibleMoves;
        }
        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
    class Pawn : Figure //пешка
    {
        private PictureBox pictogram = new PictureBox();
        private readonly Bitmap pictorgamOnBlack;
        private readonly Bitmap pictorgamOnWhite;
        private readonly bool _colorOfTheFigure; 
        int positionX;
        int positionY;
        public Pawn(int x, int y, bool colorOfTheFigure)
        {
            positionX = x;
            positionY = y;
            _colorOfTheFigure = colorOfTheFigure;
            if (colorOfTheFigure)
            {
                pictorgamOnBlack = new Bitmap(Properties.Resources.БелаяПешкаНаЧерном);
                pictorgamOnWhite = new Bitmap(Properties.Resources.БелаяПешкаНаБелом);
            }
            else
            {
                pictorgamOnBlack = new Bitmap(Properties.Resources.ЧернаяПешкаНаЧерном);
                pictorgamOnWhite = new Bitmap(Properties.Resources.ЧернаяПешкаНаБелом);
            }
            pictogram.Size = new Size(30, 30);
            UpdatePictogram();
        }
        public override void UpdatePictogram()
        {
            pictogram.Location = new Point(PositionX * sizeoffield, PositionY * sizeoffield);
            if ((PositionX % 2 == 0 && PositionY % 2 != 0) || 
                (PositionX % 2 != 0 && PositionY % 2 == 0))
            {
                pictogram.Image = pictorgamOnBlack;
            }
            else
            {
                pictogram.Image = pictorgamOnWhite;
            }
        }
        public override bool? GetColorOfTheFigure
        {
            get
            {
                return _colorOfTheFigure;
            }
        }
        public override int PositionX
        {
            get
            {
                return positionX;
            }
            set
            {
                positionX = value;
            }
        }
        public override int PositionY
        {
            get
            {
                return positionY;
            }
            set
            {
                positionY = value;
            }
        }
        public override PictureBox GetPictorgam
        {
            get
            {
                return pictogram;
            }
        }

        public override List<Figure> CheckPosibleMoves(Figure[,] allFigures)
        {
            List<Figure> PosibleMoves = new List<Figure>();
            int posX = positionX;
            int posY = positionY;

            if (GetColorOfTheFigure == true)
            {
                if (posX > -1 && posX < 8 && posY - 1 > -1 && posY - 1 < 8)
                {
                    if (allFigures[posX, posY - 1] is EmptyFigure)
                    {
                        PosibleMoves.Add(allFigures[posX, posY - 1]);
                    }
                }
                if (posX > -1 && posX < 8 && posY - 2 > -1 && posY - 2 < 8)
                {
                    if (PositionY == 6 && allFigures[posX, posY - 2] is EmptyFigure)
                    {
                        PosibleMoves.Add(allFigures[posX, posY - 2]);
                    }
                }
                if (posX - 1 > -1 && posX - 1 < 8 && posY - 1 > -1 && posY - 1 < 8)
                {
                    if (!(allFigures[posX - 1, posY - 1]is EmptyFigure) &&
                        allFigures[posX - 1, posY - 1].GetColorOfTheFigure != GetColorOfTheFigure)
                    {
                        PosibleMoves.Add(allFigures[posX - 1, posY - 1]);
                    }
                }
                if (posX + 1 > -1 && posX + 1 < 8 && posY - 1 > -1 && posY - 1 < 8)
                {
                    if (!(allFigures[posX + 1, posY - 1] is EmptyFigure) &&
                        allFigures[posX + 1, posY - 1].GetColorOfTheFigure != GetColorOfTheFigure)
                    {
                        PosibleMoves.Add(allFigures[posX + 1, posY - 1]);
                    }
                }
                return PosibleMoves;
            }
            else
            {
                if (posX > -1 && posX < 8 && posY + 1 > -1 && posY + 1 < 8)
                {
                    if (allFigures[posX, posY + 1] is EmptyFigure)
                    {
                        PosibleMoves.Add(allFigures[posX, posY + 1]);
                    }
                }
                if (posX > -1 && posX < 8 && posY + 2 > -1 && posY + 2 < 8)
                {
                    if (PositionY == 1 && allFigures[posX, posY + 2] is EmptyFigure)
                    {
                        PosibleMoves.Add(allFigures[posX, posY + 2]);
                    }
                }
                if (posX + 1 > -1 && posX + 1 < 8 && posY + 1 > -1 && posY + 1 < 8)
                {
                    if (!(allFigures[posX + 1, posY + 1] is EmptyFigure) &&
                        allFigures[posX + 1, posY + 1].GetColorOfTheFigure != GetColorOfTheFigure)
                    {
                        PosibleMoves.Add(allFigures[posX + 1, posY + 1]);
                    }
                }
                if (posX - 1 > -1 && posX - 1 < 8 && posY + 1 > -1 && posY + 1 < 8)
                {
                    if (!(allFigures[posX - 1, posY + 1] is EmptyFigure) && 
                        allFigures[posX - 1, posY + 1].GetColorOfTheFigure != GetColorOfTheFigure)
                    {
                        PosibleMoves.Add(allFigures[posX - 1, posY + 1]);
                    }
                }
                return PosibleMoves;
            }
        }
        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
    class EmptyFigure : Figure //класс пустая фигура
    {
        //Класс пустой фигуры нужен лишь для удобства,
        //чтоб исключить работу с нулевыми ссылками и избежать исключений
        int positionX;
        int positionY;
        public EmptyFigure(int x, int y)
        {
            positionX = x;
            positionY = y;
        }
        public override bool? GetColorOfTheFigure
        {
            get
            {
                return null;
            }
        }
        public override int PositionX
        {
            get
            {
                return positionX;
            }

            set
            {
                positionX = value;
            }
        }
        public override PictureBox GetPictorgam
        {
            get
            {
                return null;
            }
        }
        public override int PositionY
        {
            get
            {
                return positionY;
            }
            set
            {
                positionY = value;
            }
        }
        public override List<Figure> CheckPosibleMoves(Figure[,] allFigures)
        {
           return null;
        }
        public override void UpdatePictogram()
        {
        }
        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
}
