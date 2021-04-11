using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;

namespace Rains
{
    class Animator
    {
        private delegate void CreateDropsDelegate(int k);
        public static int dropID;
        private List<Drop> dr = new List<Drop>();
        private BufferedGraphics bg;
        private Graphics MainGraphics;
        private Thread t,t2;
        public bool stop = false;
        private int cnt;
        public Animator(Graphics g)
        {
            MainGraphics = g;
            MainGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            bg = BufferedGraphicsManager.Current.Allocate(g, Rectangle.Round(g.VisibleClipBounds));
        }

        public void UpdateGraphics(Graphics g)
        {
            MainGraphics = g;
            MainGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            bg = BufferedGraphicsManager.Current.Allocate(g, Rectangle.Round(g.VisibleClipBounds));
        }

        public void Start()
        {
            stop = false;
            Drop.stopAll = false;
            if (t == null || !t.IsAlive)
            {
                t = new Thread(new ThreadStart(Animate));
                t.Start();
            }
            if (t2 == null || !t2.IsAlive)
            {
                t2 = new Thread(new ParameterizedThreadStart(CreateDrop)); 
                t2.Start(Drop.panelSize.Width / 10);
            }
        }
        private void Animate()
        {       
            while (!stop)
            {
                bg.Graphics.Clear(Color.White);
                dr = dr.FindAll(it => it.IsAlive);
                cnt = dr.Count;
                for (int i = 0; i < cnt; i++)
                {
                    dr[i].Paint(bg.Graphics);           
                }
                try
                {
                    bg.Render(MainGraphics);
                }
                catch(Exception e) { }
            }
           
        }
        public void StopAll()
        {
            stop = true;
            Drop.stopAll = true;
        }
        private void CreateDrop(object k)
        {
            for (int i = 0; i < (int)k; i++)
            {
                var d = new Drop();
                dr.Add(d);
                d.Start();
                Thread.Sleep(60);
            }
        }

        private void DeleteDrops()
        {
            cnt = dr.Count;
            for (int i = cnt-1; i >= 0; i--)
            {
                dr[i].stop = true;
            }
        }
        private void AddNewDrops(int k)
        {
            t2.Join(0);
            if (t2 == null || !t2.IsAlive)
            {
                t2 = new Thread(new ParameterizedThreadStart(CreateDrop)); 
                t2.Start(k);
            }
            cnt = dr.Count;
            DeleteDrops();
            cnt = dr.Count;
        }
        public void AddNewDrop()
        {
            switch (dropID)
            {
                case 0:
                    AddNewDrops(Drop.panelSize.Width / 60);
                    break;
                case 1:
                    AddNewDrops(Drop.panelSize.Width / 50);
                    break;
                case 2:
                    AddNewDrops(Drop.panelSize.Width / 40);
                    break;
                case 3:
                    AddNewDrops(Drop.panelSize.Width / 30);
                    break;
                case 4:
                    AddNewDrops(Drop.panelSize.Width / 20);
                    break;
                case 5:
                    AddNewDrops(Drop.panelSize.Width / 10);
                    break;
                case 6:
                    AddNewDrops(Drop.panelSize.Width / 10 + 7);
                    break;
                case 7:
                    AddNewDrops(Drop.panelSize.Width / 10 + 8);
                    break;
                case 8:
                    AddNewDrops(Drop.panelSize.Width / 10 + 9);
                    break;
                case 9:
                    AddNewDrops(Drop.panelSize.Width / 10 + 10);
                    break;
                case 10:
                    AddNewDrops(Drop.panelSize.Width / 10 + 11);
                    break;  
            }
        }


    }
}
