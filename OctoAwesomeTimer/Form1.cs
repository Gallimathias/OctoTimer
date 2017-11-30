using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace TimerOctoAwesome
{
      public partial class Form1 : Form
    {
        readonly Stopwatch stop = new Stopwatch();
        public Timer timer = new Timer();

        private bool dragging;
        private Point pointClicked;
        Options opt;
        public bool _CommitMessagebox;
        public bool _Farbverlauf;
        public bool _RandomColor;
        public bool _ShowHours;
        public bool _ShowMiliseconds;
        public int _Yellow;
        public int _Red;
        public int _Random;



        public Form1()
        {
            InitializeComponent();
			
            timer.Tick += new EventHandler(timer_Tick);
            _CommitMessagebox = Settings1.Default._CommitMessagebox;
            _Farbverlauf = Settings1.Default._Farbverlauf;
            _RandomColor = Settings1.Default._RandomColor;
            _ShowHours = Settings1.Default._ShowHours;
            _ShowMiliseconds = Settings1.Default._ShowMiliseconds;
            _Yellow = Settings1.Default._Yellow;
            _Red = Settings1.Default._Red;
            _Random = Settings1.Default._Random;
            opt = new Options(this);
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            if (stop.Elapsed.TotalMinutes < _Random || !_RandomColor)
            {
                timer.Stop();
                return;
            }
                
            Random rand = new Random();
            
                Coloring(Color.FromArgb(rand.Next(1, 255), rand.Next(1, 255), rand.Next(1, 255)));
            
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!dragging) return;

            var pointMoveTo = this.PointToScreen(new Point(e.X, e.Y));
            pointMoveTo.Offset(-pointClicked.X, -pointClicked.Y);
            this.Location = pointMoveTo;
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                pointClicked = new Point(e.X, e.Y);
            }
            else
                dragging = false;

        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            MouseUpEvent(sender, e);
        }


        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!dragging) return;
            var pointMoveTo = this.PointToScreen(new Point(e.X, e.Y));
            pointMoveTo.Offset(-pointClicked.X, -pointClicked.Y);
            this.Location = pointMoveTo;

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                pointClicked = new Point(e.X, e.Y);
            }
            else
                dragging = false;

        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            MouseUpEvent(sender, e);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Coloring(Color.FromArgb(0, 254, 0));
            this.TopMost = true;
            if (Settings1.Default.WindowLocation != new Point(0, 0))
                this.Location = Settings1.Default.WindowLocation;
            if (Settings1.Default.WindowSize != new Size(0, 0))
                this.Size = Settings1.Default.WindowSize;
            this.Opacity = Settings1.Default._Opacity / 100d;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            string str = "";
            if (stop.Elapsed.Hours < 10 && _ShowHours)
                str = "0" + stop.Elapsed.Hours + ":";
            else if (_ShowHours)
                str = stop.Elapsed.Hours + ":";
            if (stop.Elapsed.Minutes < 10)
                str += "0" + (stop.Elapsed.Minutes) + ":";
            else
                str += (stop.Elapsed.Minutes) + ":";
            if (stop.Elapsed.Seconds < 10)
                str += "0" + (stop.Elapsed.Seconds);
            else
                str += (stop.Elapsed.Seconds);
            str += _ShowMiliseconds ? ":" : "";
            if (stop.Elapsed.Milliseconds < 10 && _ShowMiliseconds)
                str += "00" + (stop.Elapsed.Milliseconds);
            else if (stop.Elapsed.Milliseconds < 100 && _ShowMiliseconds)
                str += "0" + (stop.Elapsed.Milliseconds);
            else if (_ShowMiliseconds)
                str += (stop.Elapsed.Milliseconds);
            label1.Text = str;

            
            switch (_Farbverlauf)
            {
                case true:
                if (stop.Elapsed.TotalMinutes > _Random && _RandomColor)
                {
                    timer.Interval = Settings1.Default._RandomIntervall;
                    timer.Start();
                }
            	else
            		Farbverlauf();
                break;
                case false:
                if (stop.Elapsed.TotalMinutes >= _Random && _RandomColor)
                {
                    timer.Interval = Settings1.Default._RandomIntervall;
                    timer.Start();
                }
                else if (stop.Elapsed.Minutes >= _Red)
                    Coloring(Color.Red);
                else if (stop.Elapsed.Minutes >= _Yellow)
                    Coloring(Color.Yellow);
                else
                    Coloring(Color.Green);
                break;
            }
        }

        private void label1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }



        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        public void Coloring(Color color)
        {
            this.BackColor = color;
            label1.BackColor = color;

        }


        public int green = 254;
        public int red = 0;

        private void Farbverlauf()
        {        	
        	double Colourr = 0d;
        	if (stop.Elapsed.Minutes < _Yellow)
        	{
        		green = 254;
        		red = 0;
                Colourr = 127d/(_Yellow * 60);
        	}
            else if (stop.Elapsed.Minutes < _Red)
            {
            	green = 127;
            	red = 127;
            	Colourr = 127d/((_Red) * 60);
            }
            else
            {
            	Coloring(Color.Red);
            		return;
            }
            	


            if (!_Farbverlauf)return;
            if (stop.Elapsed.Minutes < _Yellow)
            {
            	green = green - (int)(Colourr * stop.Elapsed.TotalSeconds);
            	red = red + (int)(Colourr * stop.Elapsed.TotalSeconds);
            }
            else if (stop.Elapsed.Minutes < _Red)
            {
             	green -= (int)(Colourr * (stop.Elapsed.TotalSeconds - _Yellow * 60));
             	red += (int)(Colourr * (stop.Elapsed.TotalSeconds- _Yellow * 60));
            }

                Coloring(Color.FromArgb(red, green, 0));
        }
        
        

        private void MouseUpEvent(object sender, MouseEventArgs e)
        {
            dragging = false;
            switch (e.Button)
            {
                case MouseButtons.Right:
                    stop.Reset();
                    if (_CommitMessagebox)
                        MessageBox.Show("COMMIT!!\nCOMMIT!!");
                    if (_Farbverlauf)
                        Coloring(Color.FromArgb(red, green, 0));
                    else
                        Coloring(Color.Green);
                    red = 0;
                    green = 254;
                    break;
                case MouseButtons.Middle:
                    opt.Show();
                    break;
                case MouseButtons.Left:
                    if (stop.IsRunning)
                    {
                        stop.Stop();
                        timer.Stop();
                    }
                    else
                    {
                        stop.Start();
                    }
                    break;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings1.Default.WindowLocation = this.Location;
            Settings1.Default.WindowSize = Size;
            Settings1.Default.Save();
            if (stop.Elapsed.TotalMinutes >= _Yellow && _CommitMessagebox)
                MessageBox.Show("Commit!!!\nCommit!!!");
        }

    }
}
