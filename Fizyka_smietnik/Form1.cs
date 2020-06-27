﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace Fizyka_smietnik
{

    public partial class Form1 : Form
    {
        Thread drawThread;
        Thread physicsThread;

        public bool drawingRunning = false;
        public bool physicsRuning = false;
        public bool physicsPause = false;

        public long physicsFPS = 0;
        public long drawFPS = 0;

        Font drawFont = new Font("Arial", 8);

        SolidBrush blackBrush = new SolidBrush(Color.Black);

        Particle[] particles;

        public Form1()
        {
            InitializeComponent();
        }

            //NARYSOWANIE GRANIC
        public void drawBorders(Graphics drawing)
        {
            Pen borderPen = new Pen(Color.Black);
            drawing.DrawLine(borderPen, 0, 0, pictureBox1.Size.Width - 1, 0);
            drawing.DrawLine(borderPen, 0, pictureBox1.Size.Height - 1, pictureBox1.Size.Width - 1, pictureBox1.Size.Height - 1);
            drawing.DrawLine(borderPen, 0, 0, 0, pictureBox1.Size.Height - 1);
            drawing.DrawLine(borderPen, pictureBox1.Size.Width - 1, 0, pictureBox1.Size.Width - 1, pictureBox1.Size.Height - 1);
        }

        public void drawParticles(Graphics drawing)
        {
            SolidBrush particleBrush = new SolidBrush(Color.Black);
            Pen blackPen = new Pen(Color.Black);
            for (int i = 0; i < particles.Length; i++)
            {
                particleBrush.Color = particles[i].color;
                drawing.FillEllipse(particleBrush, (float)((particles[i].X - particles[i].Radius) / 1), (float)((particles[i].Y - particles[i].Radius) / 1), 2 * particles[i].Radius, 2 * particles[i].Radius);
                drawing.DrawEllipse(blackPen, (float)(particles[i].X - particles[i].Radius), (float)(particles[i].Y - particles[i].Radius), 2 * particles[i].Radius, 2 * particles[i].Radius);
                blackPen.Color = Color.Black;
            }
        }

            //NARYSOWANIE CALEJ KLATKI NA OKNIE APLIKACJI

        public void drawFrame(Graphics drawing ,Bitmap bmg)
        {
            Rectangle box = new Rectangle(0, 0, bmg.Width, bmg.Height);
            if (drawingRunning)
            {
                if (pictureBox1.InvokeRequired)         //NIE MAM POJECIA DLACZEGO ALE MUSIALEM TO W TEN SPOSOB ZROBIC
                {
                    pictureBox1.Invoke(new MethodInvoker(
                         delegate ()
                         {
                             if (pictureBox1.Image!=null)
                             pictureBox1.Image.Dispose();
                             pictureBox1.Image = bmg.Clone( box, bmg.PixelFormat);
                         }));
                }
                else
                    pictureBox1.Image = bmg.Clone(box, bmg.PixelFormat);
            }
            drawing.Clear(Color.White);
        }

        public void showFPS(Graphics drawing ,long dFPS, long pFPS)
        {
            drawing.DrawString(("Draw FPS: " + dFPS.ToString() + " Physics FPS: " + pFPS.ToString()), drawFont, blackBrush, 5 , 5);
        }

        public void showParticleInfo (Graphics drawing)
        {
            for (int i = 0; i < particles.Length && i < 30; i++)
                drawing.DrawString(i.ToString() + ") X: " + particles[i].X.ToString() + " Y: " + particles[i].Y.ToString() + " VelX: " + particles[i].velX.ToString() + " VelY: " + particles[i].velY.ToString(), drawFont , blackBrush, 100 , 10*(i + 2));
        }

        public void draw()
        {
            Bitmap bmg = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics SimDrawing = Graphics.FromImage(bmg);
            Stopwatch drawWatch = new Stopwatch();
            drawWatch.Start();
            while (drawingRunning)
            {
                drawWatch.Restart();
                drawFrame(SimDrawing , bmg);
                showFPS(SimDrawing, drawFPS, physicsFPS);
                showParticleInfo(SimDrawing);
                drawParticles(SimDrawing);
                drawBorders(SimDrawing);
                drawWatch.Stop();
                drawFPS = Stopwatch.Frequency/ drawWatch.ElapsedTicks;
            }
        }

        public void physics()
        {
            long PhysycsTicks = 0;
            Stopwatch PhysicsTimer = new Stopwatch();

            PhysicsTimer.Start();
            while (physicsRuning)
            {
                if (particles != null)
                {
                    while (physicsPause)
                    {
                        Thread.Sleep(100);
                        PhysicsTimer.Restart();
                    }
                    for (int i = 0; i < particles.Length; i++)
                    {
                        particles[i].updateparticle(PhysycsTicks,particles);
                    }
                    PhysicsTimer.Stop();
                    PhysycsTicks = PhysicsTimer.ElapsedTicks;
                    PhysicsTimer.Restart();
                    physicsFPS =Stopwatch.Frequency/ PhysycsTicks;
                }
                Thread.Sleep(1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random rng = new Random(); //UTWORZENIE SEEDA RNG


            if (physicsThread != null)
            {
                physicsThread.Abort();
            }

            int numberofparticles = 5;          //WCZYTANIE WARTOSCI Z OKNA
            int maxvel = 100;
            if (textBox1.Text!="")
                numberofparticles = Convert.ToInt32(textBox1.Text);
            if (textBox2.Text != "")
                maxvel = Convert.ToInt32(textBox2.Text);
           
                        //UTWORZENIE TABLICY CZASTEK

            particles = new Particle[numberofparticles];
            for (int i = 0; i < numberofparticles; i++)
                particles[i] = new Particle(10, pictureBox1.Width , pictureBox1.Height, maxvel , rng);

                //UTWORZENIE THREADA DLA RYSOWANIA ORAZ FIZYKI
            if (drawThread == null)
            {
                drawingRunning = true;
                drawThread = new Thread(draw);
                drawThread.Start();
            }
                physicsRuning = true;
                physicsPause = true;
                button2.Text = "Physics Paused";
                physicsThread = new Thread(physics);
                physicsThread.Start();
                physicsRuning = true;
            System.Threading.Thread.Sleep(1000);
        }

                        // PAUZOWANIE FIZYKI
        private void button2_Click(object sender, EventArgs e)
        {
            if (physicsRuning)
            {
                if (physicsPause)
                {
                    physicsPause = false;
                    button2.Text = "Physics Unpaused";
                }
                else
                {
                    physicsPause = true;
                    button2.Text = "Physics Paused";
                }
            }
        }

                    // ZAKONCZENIE WATKOW PRZED ZAMKNIECIEM OKNA
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (drawThread!=null)
            drawThread.Abort();
            if (physicsThread!=null)
            physicsThread.Abort();
        }
    }

    public class Particle
    {

        geometry geomath = new geometry();
        public double X;
        public double Y;
        public int Radius;
        public double velX;
        public double velY;
        public Color color = Color.Transparent;

        public Particle(int x, int y, int radius, int velx = 0, int vely = 0)
        {
            X = x;
            Y = y;
            Radius = radius;
            velX = velx;
            velY = vely;
        }

        public Particle(int radius, int width, int height, int maxvel, Random rng)          //LOSOWE UTWORZENIE czastki
        {
            Radius = radius;
            X = 10 + rng.Next() % (width - radius - 1);
            Y = 10 + rng.Next() % (height - radius - 1);
            if ((1 + rng.Next()) % 2 == 1)
                velX = rng.Next() % (maxvel);
            else
                velX = -rng.Next() % (maxvel);
            if ((1 + rng.Next()) % 2 == 1)
                velY = rng.Next() % (maxvel);
            else
                velY = -rng.Next() % (maxvel);
        }

        public void updateparticle(long ticks , Particle[] particles)
        {
            X += ((double)ticks / Stopwatch.Frequency) * velX;
            Y += ((double)ticks / Stopwatch.Frequency) * velY;
            velY += ((double)ticks / Stopwatch.Frequency) * 1000;
            bordercollision();
            multipleparticlescollisions(particles);
        }

        public bool particlecollision(Particle secondparticle)
        {
            if (secondparticle != this)
            {    if (geomath.distance(X, Y, secondparticle.X, secondparticle.Y) <= Radius + secondparticle.Radius)
                {
                    return true;
                }
            }
            return false;
        }
        public bool multipleparticlescollisions(Particle[] particles)
        {
            bool touched = false;
            for (int i = 0; i<particles.Length;i++)
            {
                if (particlecollision(particles[i]))
                {
                    touched = true;
                    color = Color.Red;
                }
            }
            if (!touched)
            {
                color = Color.Transparent;
                return false;
            }
            else
                return true;
        }
        public void bordercollision()
        {
            if (X < Radius && velX < 0)
                velX = -velX*0.7;
            if (X > 574 - Radius - 1 && velX>0)
                velX = -velX*0.7;
            if (Y > 384- Radius-1 && velY > 0)
                velY = (-velY)*0.9;
            if (Y < Radius && velY < 0)
                velY = (-velY)*0.90;
        }
        public void resolvecollision( Particle collidedParticle)
        {
            //TODO
        }
    }

    public class geometry
    {
        public geometry() { }
        public double distance(double x1 ,double y1 ,double x2, double y2)
        {
            double dx = x1 - x2;
            double dy = y1 - y2;
            if (dx < 0) dx = -dx;
            if (dy < 0) dy = -dy;
            return (double)Math.Sqrt((dy * dy + dx * dx));
        }

    }
}