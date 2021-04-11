using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Threading;

namespace Rains
{
    class Drop
    {
        public static int speedID = 5;
        private int speed;
        private int newSpeed;
        public static int windID = 5;
        private int h = 1;
        public static Size panelSize = new Size(1,1);
        public Point[] points;
        public Color DropColor;
        private Point position;
        static public bool stopAll;
        public bool stop;
        private Thread t;

        private void WindRender()
        {
            switch (windID)
            {
                case 0:
                    dx = -5;
                    break;
                case 1:
                    dx = -4;
                    break;
                case 2:
                    dx = -3;
                    break;
                case 3:
                    dx = -2;
                    break;
                case 4:
                    dx = -1;
                    break;
                case 5:
                    dx = 0;
                    break;
                case 6:
                    dx = 1;
                    break;
                case 7:
                    dx = 2;
                    break;
                case 8:
                    dx = 3;
                    break;
                case 9:
                    dx = 4;
                    break;
                case 10:
                    dx = 5;
                    break; 
            }
        }
        private void SpeedRender()
        {
            switch (speedID)
            {
                case 0:
                    newSpeed = speed + 8;
                    break;
                case 1:
                    newSpeed = speed + 7;
                    break;
                case 2:
                    newSpeed = speed + 6;
                    break;
                case 3:
                    newSpeed = speed + 5;
                    break;
                case 4:
                    newSpeed = speed + 4;
                    break;
                case 5:
                    newSpeed = speed;
                    break;
                case 6:
                    newSpeed = speed - 5;
                    break;
                case 7:
                    newSpeed = speed - 6;
                    break;
                case 8:
                    newSpeed = speed - 7;
                    break;
                case 9:
                    newSpeed = speed - 8;
                    break;
                case 10:
                    newSpeed = speed - 9;
                    break;
            }
        }

        public bool IsAlive
        {
            get => (t != null && t.IsAlive);
        }
        public int XPosition
        {
            get => position.X; 
            set
            {
                if(value <0 || value > panelSize.Width)
                {
                    if (value < 0) position.X = panelSize.Width;
                    if (value > panelSize.Width) position.X = 0;
                }
                else
                {
                    position.X = value;
                }
            }
        }

        public int YPosition
        {
            get => position.Y;
            set
            {
                if (value > panelSize.Height+h) position.Y = -100;
                else
                {
                    position.Y = value;
                }
                
            }
        }
        private Size DropSize;
        private int size = 10;
        public int Size
        {
            get => size; //Лямда выражение
            set
            {
                if (value < 10) size = 10;
                else if (value > 100) size = 100;
                else size = value;
            }
        }
        public static Random random = new Random();
        private int dx;
        private int dy;
        public Drop()
        {
            size = random.Next(10, 50);
            DropSize = new Size(size, size);
            dy = random.Next(1,5);
            speed = random.Next(10, 30);
            var r = random.Next(255);
            var g = random.Next(255);
            var b = random.Next(255);
            position = new Point(random.Next(0, panelSize.Width - size),
                                 -100);
            DropColor = Color.FromArgb(r, g, b);
        }

        public void Paint(Graphics g) 
        {     
            position.X = XPosition;
            position.Y = YPosition;
            points = new Point[3] {new Point(position.X,position.Y+DropSize.Width/2+1),
                                   new Point(position.X + DropSize.Height,position.Y+DropSize.Width/2+1),
                                   new Point(position.X + DropSize.Height/2,position.Y-30)};
            h = (int)Math.Sqrt(points[0].Y - points[2].Y);
            Pen pen = new Pen(DropColor);
            Brush brush = new SolidBrush(DropColor);
            Rectangle rec = new Rectangle(position, DropSize);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.FillPie(brush, rec, 0, 180);
            g.FillPolygon(brush,points);
        }
        private void Move()
        {
            SpeedRender();
            WindRender();
            XPosition += dx;
            YPosition += dy; 
        }

        public void Start()
        {
            stop = false;
            t = new Thread(
                new ThreadStart(Run)
                );
            t.Start();
        }

        private void Run()
        {
            while (!stopAll && !stop)
            {
                Move();
                Thread.Sleep(newSpeed);
            }
        }
    }
}
