using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoshopCrack
{
    public partial class Form1 : Form
    {

        public string links = "";
        public string last = "";

        public Form1()
        {
            InitializeComponent();

            this.MaximizeBox = false;
            //this.MinimizeBox = false;

            INIManager manager = new INIManager("C:\\my.ini");
            string link = manager.GetPrivateString("main", "link");

            textBox1.Text = link;
            set_link(link);

            if (link.Equals(""))
            {
                button1.Visible = false;
            }
            else
            {
                //change_text();
            }
        }

        static string ReverseStr(string str)
        {
            String ret = "";
            int yes = 0;
            Random rnd = new Random();
            int randN = rnd.Next(2, str.Length - 2);
            for (var i = 0; i <= str.Length - 1; i++)
            {
                if (i == randN)
                {
                    if (yes == 0)
                    {
                        ret += rnd.Next(1, 11);
                        yes = 1;
                        //MessageBox.Show(ret+"\n"+ str);
                    }
                    else
                    {
                        ret += str[i];
                    }
                }
                else
                {
                    ret += str[i];
                }

            }
            return ret;
        }

        private void change_text()
        {
            string str = string.Empty;
            using (System.IO.StreamReader reader = System.IO.File.OpenText(@"" + links))
            {
                str = reader.ReadToEnd();
            }

            string pattern = @"[0-9]{24}";
            Regex regex = new Regex(pattern);

            foreach (Match match in regex.Matches(str))
            {
                string neww = ReverseStr(match.Value);


                if (last != neww)
                {
                    //MessageBox.Show(neww+"\n"+match.Value);
                    str = regex.Replace(str, neww);
                    last = neww;
                }

            }


            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"" + links))
            {
                file.Write(str);
                str = string.Empty;
                regex = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                MessageBox.Show("OK","Crack");
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            change_text();
            //timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            change_text();
        }


        private void set_link(string link)
        {
            links = link + "/application.xml";
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                this.ShowInTaskbar = false;
                notifyIcon1.Visible = true;
                this.Hide();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = false;
                this.ShowInTaskbar = false;
                notifyIcon1.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!textBox1.Text.Equals(""))
            {
                INIManager manager = new INIManager("C:\\my.ini");
                manager.WritePrivateString("main", "link", textBox1.Text);
                button1.Visible = true;
                set_link(textBox1.Text);
            }
        }
    }
}
