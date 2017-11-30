using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimerOctoAwesome
{
    public partial class Options : Form
    {
        Form1 form1;
        public Options(Form1 form)
        {
            InitializeComponent();
            form1 = form;
            CommitCheckBox.Checked = form1._CommitMessagebox;
            FarbverlaufCheckBox.Checked = form1._Farbverlauf;
            randomColorCheckBox.Checked = form1._RandomColor;
            trackBar1.Value = form1._Yellow;
            trackBar2.Value = form1._Red;
            trackBar3.Value = form1._Random;
            trackBar4.Value = Settings1.Default._RandomIntervall/100;
            trackBar5.Value=Settings1.Default._Opacity;
            ShowHourCheckBox.Checked = form1._ShowHours;
            ShowMilisecondsCheckBox.Checked = form1._ShowMiliseconds;
            label3.Text = "Yellow color at minute: " + trackBar1.Value;
            label4.Text = "Red color at minute: " + trackBar2.Value;
            RandomColorLabel.Text = "Random color at minute: " + trackBar3.Value;
            label6.Text = "Random color Intervall ms: " + Settings1.Default._RandomIntervall;
            OpacityLabel.Text = "Opacity of timer: " + trackBar5.Value + "%";
        }

        private void Options_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Settings1.Default._CommitMessagebox = form1._CommitMessagebox;
            Settings1.Default._Farbverlauf = form1._Farbverlauf;
            Settings1.Default._RandomColor = form1._RandomColor;
            Settings1.Default._ShowHours = form1._ShowHours;
            Settings1.Default._ShowMiliseconds = form1._ShowMiliseconds;
            Settings1.Default._Yellow = form1._Yellow;
            Settings1.Default._Red = form1._Red;
            Settings1.Default._Random = form1._Random;
            Settings1.Default.Save();
            e.Cancel = true;
        }

        private void CommitCheckBox_CheckedChanged(object sender, EventArgs e)
        {
			form1._CommitMessagebox = (sender as CheckBox).Checked ? true : false;
        }

        private void ShowHourCheckBox_CheckedChanged(object sender, EventArgs e)
        {
			form1._ShowHours = (sender as CheckBox).Checked ? true : false;
            SizeConfiguration((sender as CheckBox));
        }

        private void ShowMilisecondsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
			form1._ShowMiliseconds = (sender as CheckBox).Checked ? true : false;
            SizeConfiguration((sender as CheckBox));
        }

        private void SizeConfiguration(CheckBox cb)
        {
            switch (cb.Name)
            {
                case "ShowMilisecondsCheckBox":
                    if (cb.Checked)
                        form1.Width += 72;
                    else
                        form1.Width -= 72;
                    break;
                case "ShowHourCheckBox":
                    if (cb.Checked)
                        form1.Width += 50;
                    else
                        form1.Width -= 50;
                    break;
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (trackBar1.Value >= trackBar2.Value)
                trackBar2.Value = trackBar1.Value + 1;
            if (trackBar1.Value >= trackBar3.Value)
                trackBar3.Value = trackBar1.Value + 2;
            label3.Text = "Yellow color at minute: " + trackBar1.Value;
            form1._Yellow = trackBar1.Value;
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            if (trackBar2.Value <= trackBar1.Value)
                trackBar1.Value = trackBar2.Value - 1;
            if (trackBar2.Value >= trackBar3.Value)
                trackBar3.Value = trackBar2.Value + 1;
            label4.Text = "Red color at minute: " + trackBar2.Value;
            form1._Red = trackBar2.Value;
        }

        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            if (trackBar3.Value <= trackBar1.Value)
                trackBar1.Value = trackBar3.Value - 2;
            if (trackBar3.Value <= trackBar2.Value)
                trackBar2.Value = trackBar3.Value - 1;
            RandomColorLabel.Text = "Random color at minute: " + trackBar3.Value;
            form1._Random = trackBar3.Value;
        }

        private void randomColorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            form1._RandomColor = (sender as CheckBox).Checked ? true : false;
            if ((sender as CheckBox).Checked)
            {
                panel3.Show();
                this.Height += 65;
            }
            else
            {
                panel3.Hide();
                this.Height -= 65;
             }
        }
        void FarbverlaufCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			form1._Farbverlauf = FarbverlaufCheckBox.Checked ? true : false;
            if (FarbverlaufCheckBox.Checked)
                form1.Coloring(Color.FromArgb(form1.red, form1.green, 0));
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            label6.Text = "Random color Intervall ms: " + (trackBar4.Value * 100);
            Settings1.Default._RandomIntervall = trackBar4.Value * 100;
            Settings1.Default.Save();
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            OpacityLabel.Text = "Opacity of timer: " + trackBar5.Value + "%";
            form1.Opacity = trackBar5.Value / 100d;
            Settings1.Default._Opacity = trackBar5.Value;
            Settings1.Default.Save();
        }
    }
}
