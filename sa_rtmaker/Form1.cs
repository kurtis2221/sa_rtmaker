using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;

using ReadMemory;

namespace sa_rtmaker
{
    public partial class Form1 : Form
    {
        string[] tp = new string[10];

        public Form1()
        {
            InitializeComponent();
            //Name Protection
            tp[0] = "y";
            tp[1] = "u";
            tp[2] = "s";
            tp[3] = " ";
            tp[4] = "K";
            tp[5] = " ";
            tp[6] = "t";
            tp[7] = "i";
            tp[8] = "r";
            tp[9] = "B";
            this.Text += tp[3] + tp[9] + tp[0] + tp[5] + tp[4] + tp[1] +
                tp[8] + tp[6] + tp[7] + tp[2];
        }

        public const int levelimg = 256;

        PictureBox[] level = new PictureBox[levelimg];
        int[] imagecode = new int[levelimg];
        bool isinsetmode = false;
        int tpX, tpY;

        string GetLine(string fileName, int line)
        {
            using (var sr = new StreamReader(fileName))
            {
                for (int i = 1; i < line; i++)
                    sr.ReadLine();
                return sr.ReadLine();
            }
        }

        Memory mem;

        public bool IsProcessOpen(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.Contains(name))
                {
                    return true;
                }
            }
            return false;
        }

        private void SetPlayerPos(float x, float y, float z)
        {
            if (mem.ReadOffset(0xB6F5F0, 0x534) == 1)
            {
                int test = mem.ReadPointer(0xB6F3B8);
                test += 0x14;
                byte[] Buffer = BitConverter.GetBytes((float)x);
                mem.WritePointer((uint)test, 0x30, Buffer);
                Buffer = BitConverter.GetBytes((float)y);
                mem.WritePointer((uint)test, 0x34, Buffer);
                Buffer = BitConverter.GetBytes((float)z);
                mem.WritePointer((uint)test, 0x38, Buffer);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int X = 12, Y = 12;

            for (int i = 0; i < levelimg; i++)
            {
                imagecode[i] = 0;
                level[i] = new PictureBox();
                level[i].Image = new Bitmap(Properties.Resources.p5);
                level[i].Location = new Point(X,Y);
                level[i].Size = new Size(32,32);
                level[i].SizeMode = PictureBoxSizeMode.StretchImage;
                level[i].Click += new EventHandler(Level_Click);
                Controls.Add(level[i]);
                X += 32;
                if (X == 524)
                {
                    X = 12;
                    Y += 32;
                }
            }
            panel1.BackColor = Color.Blue;
            panel2.BackColor = Color.LightGray;
            panel3.BackColor = Color.LightGray;
            panel4.BackColor = Color.LightGray;
            panel5.BackColor = Color.LightGray;
            //Images
            pictureBox1.Image = Properties.Resources.p1_0;
            pictureBox2.Image = Properties.Resources.p2_0;
            pictureBox3.Image = Properties.Resources.p3_0;
            pictureBox4.Image = Properties.Resources.p4;
            pictureBox5.Image = Properties.Resources.p5;
        }

        void Level_Click(object sender, EventArgs e)
        {
            if (isinsetmode)
            {
                for (int i = 0; i < levelimg; i++)
                {
                    if (sender == level[i])
                    {
                        marker.Location = new Point(level[i].Location.X + 9, level[i].Location.Y + 9);
                        tpX = 0 + 100 * (i - 16 * (i / 16));
                        tpY = 0 - 100 * (i / 16);
                        textBox1.Text = tpX.ToString();
                        textBox2.Text = tpY.ToString();
                        isinsetmode = false;
                        button4.Text = "Set Teleport Point";
                        checkBox1.Checked = true;
                        groupBox1.Enabled = true;
                    }
                }
            }
            else
            {
                if (panel1.BackColor == Color.Blue)
                {
                    for (int i = 0; i < levelimg; i++)
                    {
                        if (sender == level[i])
                        {
                            if (radioButton1.Checked)
                            {
                                level[i].Image = Properties.Resources.p1_0;
                                imagecode[i] = 10;
                            }
                            if (radioButton2.Checked)
                            {
                                level[i].Image = Properties.Resources.p1_90;
                                imagecode[i] = 11;
                            }
                            if (radioButton3.Checked)
                            {
                                level[i].Image = Properties.Resources.p1_0;
                                imagecode[i] = 10;
                            }
                            if (radioButton4.Checked)
                            {
                                level[i].Image = Properties.Resources.p1_90;
                                imagecode[i] = 11;
                            }
                            break;
                        }
                    }
                }
                else if (panel2.BackColor == Color.Blue)
                {
                    for (int i = 0; i < levelimg; i++)
                    {
                        if (sender == level[i])
                        {
                            if (radioButton1.Checked)
                            {
                                level[i].Image = Properties.Resources.p2_0;
                                imagecode[i] = 20;
                            }
                            if (radioButton2.Checked)
                            {
                                level[i].Image = Properties.Resources.p2_90;
                                imagecode[i] = 21;
                            }
                            if (radioButton3.Checked)
                            {
                                level[i].Image = Properties.Resources.p2_180;
                                imagecode[i] = 22;
                            }
                            if (radioButton4.Checked)
                            {
                                level[i].Image = Properties.Resources.p2_270;
                                imagecode[i] = 23;
                            }
                            break;
                        }
                    }
                }
                else if (panel3.BackColor == Color.Blue)
                {
                    for (int i = 0; i < levelimg; i++)
                    {
                        if (sender == level[i])
                        {
                            if (radioButton1.Checked)
                            {
                                level[i].Image = Properties.Resources.p3_0;
                                imagecode[i] = 30;
                            }
                            if (radioButton2.Checked)
                            {
                                level[i].Image = Properties.Resources.p3_90;
                                imagecode[i] = 31;
                            }
                            if (radioButton3.Checked)
                            {
                                level[i].Image = Properties.Resources.p3_180;
                                imagecode[i] = 32;
                            }
                            if (radioButton4.Checked)
                            {
                                level[i].Image = Properties.Resources.p3_270;
                                imagecode[i] = 33;
                            }
                            break;
                        }
                    }
                }
                else if (panel4.BackColor == Color.Blue)
                {
                    for (int i = 0; i < levelimg; i++)
                    {
                        if (sender == level[i])
                        {
                            level[i].Image = pictureBox4.Image;
                            imagecode[i] = 40;
                            break;
                        }
                    }
                }
                else if (panel5.BackColor == Color.Blue)
                {
                    for (int i = 0; i < levelimg; i++)
                    {
                        if (sender == level[i])
                        {
                            level[i].Image = pictureBox5.Image;
                            imagecode[i] = 0;
                            break;
                        }
                    }
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Blue;
            panel2.BackColor = Color.LightGray;
            panel3.BackColor = Color.LightGray;
            panel4.BackColor = Color.LightGray;
            panel5.BackColor = Color.LightGray;
            PicToRot();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.LightGray;
            panel2.BackColor = Color.Blue;
            panel3.BackColor = Color.LightGray;
            panel4.BackColor = Color.LightGray;
            panel5.BackColor = Color.LightGray;
            PicToRot();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.LightGray;
            panel2.BackColor = Color.LightGray;
            panel3.BackColor = Color.Blue;
            panel4.BackColor = Color.LightGray;
            panel5.BackColor = Color.LightGray;
            PicToRot();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.LightGray;
            panel2.BackColor = Color.LightGray;
            panel3.BackColor = Color.LightGray;
            panel4.BackColor = Color.Blue;
            panel5.BackColor = Color.LightGray;
            PicToRot();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.LightGray;
            panel2.BackColor = Color.LightGray;
            panel3.BackColor = Color.LightGray;
            panel4.BackColor = Color.LightGray;
            panel5.BackColor = Color.Blue;
            PicToRot();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            PicToRot();
        }

        private void PicToRot()
        {
            if (panel1.BackColor == Color.Blue)
            {
                if (radioButton1.Checked) pictureBox1.Image = Properties.Resources.p1_0;
                if (radioButton2.Checked) pictureBox1.Image = Properties.Resources.p1_90;
                if (radioButton3.Checked) pictureBox1.Image = Properties.Resources.p1_0;
                if (radioButton4.Checked) pictureBox1.Image = Properties.Resources.p1_90;
            }
            else if (panel2.BackColor == Color.Blue)
            {
                if (radioButton1.Checked) pictureBox2.Image = Properties.Resources.p2_0;
                if (radioButton2.Checked) pictureBox2.Image = Properties.Resources.p2_90;
                if (radioButton3.Checked) pictureBox2.Image = Properties.Resources.p2_180;
                if (radioButton4.Checked) pictureBox2.Image = Properties.Resources.p2_270;
            }
            else if (panel3.BackColor == Color.Blue)
            {
                if (radioButton1.Checked) pictureBox3.Image = Properties.Resources.p3_0;
                if (radioButton2.Checked) pictureBox3.Image = Properties.Resources.p3_90;
                if (radioButton3.Checked) pictureBox3.Image = Properties.Resources.p3_180;
                if (radioButton4.Checked) pictureBox3.Image = Properties.Resources.p3_270;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton5.Checked) LoadTrack(64);
            if (radioButton6.Checked) LoadTrack(256);
            GC.Collect(0, GCCollectionMode.Forced);
        }

        private void LoadTrack(int blocks)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < blocks; i++)
                {
                    imagecode[i] = Convert.ToInt32(GetLine(ofd.FileName, i + 1));
                    //Part1
                    if (imagecode[i] == 10) level[i].Image = new Bitmap(Properties.Resources.p1_0);
                    if (imagecode[i] == 11) level[i].Image = new Bitmap(Properties.Resources.p1_90);
                    //Part2
                    if (imagecode[i] == 20) level[i].Image = new Bitmap(Properties.Resources.p2_0);
                    if (imagecode[i] == 21) level[i].Image = new Bitmap(Properties.Resources.p2_90);
                    if (imagecode[i] == 22) level[i].Image = new Bitmap(Properties.Resources.p2_180);
                    if (imagecode[i] == 23) level[i].Image = new Bitmap(Properties.Resources.p2_270);
                    //Part3
                    if (imagecode[i] == 30) level[i].Image = new Bitmap(Properties.Resources.p3_0);
                    if (imagecode[i] == 31) level[i].Image = new Bitmap(Properties.Resources.p3_90);
                    if (imagecode[i] == 32) level[i].Image = new Bitmap(Properties.Resources.p3_180);
                    if (imagecode[i] == 33) level[i].Image = new Bitmap(Properties.Resources.p3_270);
                    //Part4
                    if (imagecode[i] == 40) level[i].Image = new Bitmap(Properties.Resources.p4);
                    //Part5
                    if (imagecode[i] == 0) level[i].Image = new Bitmap(Properties.Resources.p5);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton5.Checked) SaveTrack(64);
            if (radioButton6.Checked) SaveTrack(256);
            GC.Collect(0, GCCollectionMode.Forced);
        }

        private void SaveTrack(int blocks)
        {
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(sfd.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                for (int i = 0; i < blocks; i++)
                {
                    sw.WriteLine(Convert.ToString(imagecode[i]));
                }
                sw.Close();
                fs.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (radioButton8.Checked)
            {
                if (radioButton5.Checked) MakeRacetrack(64);
                if (radioButton6.Checked) MakeRacetrack(256);
            }
            else if (radioButton7.Checked)
            {
                if (radioButton5.Checked) MakeRacetrackMP(64);
                if (radioButton6.Checked) MakeRacetrackMP(256);
            }
        }

        private void MakeRacetrackMP(int blocks)
        {
            int X = 0, Y = 0;
            FileStream fs;
            if (!File.Exists("rtm_saved.txt"))
                fs = new FileStream(fbd.SelectedPath + "rtm_saved.txt", FileMode.CreateNew, FileAccess.Write);
            else
                fs = new FileStream(fbd.SelectedPath + "rtm_saved.txt", FileMode.Truncate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("//Generated with Racetrack Maker 3.0");
            for (int i = 0; i < blocks; i++)
            {
                //Part1
                if (imagecode[i] == 10) sw.WriteLine("CreateObject(18632, " + X + ", " + Y + ", 2000, 0, 0, 0);");
                if (imagecode[i] == 11) sw.WriteLine("CreateObject(18632, " + X + ", " + Y + ", 2000, 0, 0, 90);");
                //Part2
                if (imagecode[i] == 20) sw.WriteLine("CreateObject(18633, " + X + ", " + Y + ", 2000, 0, 0, 0);");
                if (imagecode[i] == 21) sw.WriteLine("CreateObject(18633, " + X + ", " + Y + ", 2000, 0, 0, 90);");
                if (imagecode[i] == 22) sw.WriteLine("CreateObject(18633, " + X + ", " + Y + ", 2000, 0, 0, 0);");
                if (imagecode[i] == 23) sw.WriteLine("CreateObject(18633, " + X + ", " + Y + ", 2000, 0, 0, 0);");
                //Part3
                if (imagecode[i] == 30) sw.WriteLine("CreateObject(18634, " + X + ", " + Y + ", 2000, 0, 0, 0);");
                if (imagecode[i] == 31) sw.WriteLine("CreateObject(18634, " + X + ", " + Y + ", 2000, 0, 0, 90);");
                if (imagecode[i] == 32) sw.WriteLine("CreateObject(18634, " + X + ", " + Y + ", 2000, 0, 0, 180);");
                if (imagecode[i] == 33) sw.WriteLine("CreateObject(18634, " + X + ", " + Y + ", 2000, 0, 0, 270);");
                //Part4
                if (imagecode[i] == 40) sw.WriteLine("CreateObject(18635, " + X + ", " + Y + ", 2000, 0, 0, 0);");
                //Part5
                if (imagecode[i] == 0) sw.WriteLine("CreateObject(18636, " + X + ", " + Y + ", 2000, 0, 0, 0);");
                X += 100;
                if (X == 100 * Math.Sqrt(blocks))
                {
                    X = 0;
                    Y -= 100;
                }
            }
            sw.WriteLine("//Bounds");
            for (int i = 1; i < blocks; i++)
            {
                sw.WriteLine(GetLine("sa_rtmaker" + blocks + ".dat",i));
            }
            sw.WriteLine("");
            sw.WriteLine("//Teleport command");
            sw.WriteLine(@"if (strcmp(cmdtext, ""/tele-race"", true) == 0)");
            sw.WriteLine("{");
            sw.WriteLine("     SetPlayerPos(playerid, 0, 0, 2000);");
            sw.WriteLine("    return 1;");
            sw.WriteLine("}");
            sw.Close();
            fs.Close();
            MessageBox.Show("Racetrack generated succesfully in rtm_saved.txt.", "Success",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MakeRacetrack(int blocks)
        {
            if (!File.Exists(@"data\race\race.ipl"))
            {
                RegistryKey key = Registry.LocalMachine;
                RegistryKey key2 = key.OpenSubKey("SOFTWARE\\Wow6432Node\\Rockstar Games\\GTA San Andreas\\Installation");

                string tmp;
                tmp = Convert.ToString(key2.GetValue("ExePath"));
                if(tmp.Length > 0 || tmp != null) fbd.SelectedPath = tmp.Substring(1, tmp.Length - 12);
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    int X = 0, Y = 0;
                    FileStream fs;
                    if (!File.Exists(fbd.SelectedPath + @"\data\race\race.ipl"))
                        fs = new FileStream(fbd.SelectedPath + @"\data\race\race.ipl", FileMode.CreateNew, FileAccess.Write);
                    else
                        fs = new FileStream(fbd.SelectedPath + @"\data\race\race.ipl", FileMode.Truncate, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine("inst");
                    for (int i = 0; i < blocks; i++)
                    {
                        //Part1
                        if (imagecode[i] == 10) sw.WriteLine("18632, trackp1, 0, " + X + ", " + Y + ", 2000, 0, 0, 0, 1, -1");
                        if (imagecode[i] == 11) sw.WriteLine("18632, trackp1, 0, " + X + ", " + Y + ", 2000, 0, 0, 0.707106312094, 0.707107250279, -1");
                        //Part2
                        if (imagecode[i] == 20) sw.WriteLine("18633, trackp2, 0, " + X + ", " + Y + ", 2000, 0, 0, 0, 1, -1");
                        if (imagecode[i] == 21) sw.WriteLine("18633, trackp2, 0, " + X + ", " + Y + ", 2000, 0, 0, 0.707106312094, 0.707107250279, -1");
                        if (imagecode[i] == 22) sw.WriteLine("18633, trackp2, 0, " + X + ", " + Y + ", 2000, 0, 0, 0.999999999999, 1.32679489668e-06, -1");
                        if (imagecode[i] == 23) sw.WriteLine("18633, trackp2, 0, " + X + ", " + Y + ", 2000, 0, 0, 0.707108188464, -0.707105373907, -1");
                        //Part3
                        if (imagecode[i] == 30) sw.WriteLine("18634, trackp3, 0, " + X + ", " + Y + ", 2000, 0, 0, 0, 1, -1");
                        if (imagecode[i] == 31) sw.WriteLine("18634, trackp3, 0, " + X + ", " + Y + ", 2000, 0, 0, 0.707106312094, 0.707107250279, -1");
                        if (imagecode[i] == 32) sw.WriteLine("18634, trackp3, 0, " + X + ", " + Y + ", 2000, 0, 0, 0.999999999999, 1.32679489668e-06, -1");
                        if (imagecode[i] == 33) sw.WriteLine("18634, trackp3, 0, " + X + ", " + Y + ", 2000, 0, 0, 0.707108188464, -0.707105373907, -1");
                        //Part4
                        if (imagecode[i] == 40) sw.WriteLine("18635, trackp4, 0, " + X + ", " + Y + ", 2000, 0, 0, 0, 1, -1");
                        //Part5
                        if (imagecode[i] == 0) sw.WriteLine("18636, trackp5, 0, " + X + ", " + Y + ", 2000, 0, 0, 0, 1, -1");
                        X += 100;
                        if (X == 100 * Math.Sqrt(blocks))
                        {
                            X = 0;
                            Y -= 100;
                        }
                    }
                    sw.WriteLine("end");
                    sw.Close();
                    fs.Close();
                    MessageBox.Show("Racetrack generated succesfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                int X = 0, Y = 0;
                FileStream fs = new FileStream(@"data\race\race.ipl", FileMode.Truncate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("inst");
                for (int i = 0; i < blocks; i++)
                {
                    //Part1
                    if (imagecode[i] == 10) sw.WriteLine("18632, trackp1, 0, " + X + ", " + Y + ", 2000, 0, 0, 0, 1, -1");
                    if (imagecode[i] == 11) sw.WriteLine("18632, trackp1, 0, " + X + ", " + Y + ", 2000, 0, 0, 0.707106312094, 0.707107250279, -1");
                    //Part2
                    if (imagecode[i] == 20) sw.WriteLine("18633, trackp2, 0, " + X + ", " + Y + ", 2000, 0, 0, 0, 1, -1");
                    if (imagecode[i] == 21) sw.WriteLine("18633, trackp2, 0, " + X + ", " + Y + ", 2000, 0, 0, 0.707106312094, 0.707107250279, -1");
                    if (imagecode[i] == 22) sw.WriteLine("18633, trackp2, 0, " + X + ", " + Y + ", 2000, 0, 0, 0.999999999999, 1.32679489668e-06, -1");
                    if (imagecode[i] == 23) sw.WriteLine("18633, trackp2, 0, " + X + ", " + Y + ", 2000, 0, 0, 0.707108188464, -0.707105373907, -1");
                    //Part3
                    if (imagecode[i] == 30) sw.WriteLine("18634, trackp3, 0, " + X + ", " + Y + ", 2000, 0, 0, 0, 1, -1");
                    if (imagecode[i] == 31) sw.WriteLine("18634, trackp3, 0, " + X + ", " + Y + ", 2000, 0, 0, 0.707106312094, 0.707107250279, -1");
                    if (imagecode[i] == 32) sw.WriteLine("18634, trackp3, 0, " + X + ", " + Y + ", 2000, 0, 0, 0.999999999999, 1.32679489668e-06, -1");
                    if (imagecode[i] == 33) sw.WriteLine("18634, trackp3, 0, " + X + ", " + Y + ", 2000, 0, 0, 0.707108188464, -0.707105373907, -1");
                    //Part4
                    if (imagecode[i] == 40) sw.WriteLine("18635, trackp4, 0, " + X + ", " + Y + ", 2000, 0, 0, 0, 1, -1");
                    //Part5
                    if (imagecode[i] == 0) sw.WriteLine("18636, trackp5, 0, " + X + ", " + Y + ", 2000, 0, 0, 0, 1, -1");
                    X += 100;
                    if (X == 100 * Math.Sqrt(blocks))
                    {
                        X = 0;
                        Y -= 100;
                    }
                }
                sw.WriteLine("end");
                sw.Close();
                fs.Close();
                MessageBox.Show("Racetrack generated succesfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text != "Cancel")
            {
                groupBox1.Enabled = false;
                button4.Text = "Cancel";
                isinsetmode = true;
            }
            else
            {
                groupBox1.Enabled = true;
                button4.Text = "Set Teleport Point";
                isinsetmode = false;
            }
        }

        bool isattached = false;

        private void button5_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (IsProcessOpen("gta_sa"))
                {
                    if (isattached == false)
                    {
                        mem = new Memory("gta_sa", 0x001F0FFF);
                        SetPlayerPos((float)tpX, (float)tpY, (float)2001);
                        isattached = true;
                    }
                    SetPlayerPos((float)tpX, (float)tpY, (float)2001);
                }
                else
                {
                    MessageBox.Show("GTA:SA is not running!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Warp point not set!", "Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            marker.Location = new Point(620,539);
            checkBox1.Checked = false;
        }

        bool iskeydown = false;

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (iskeydown == false)
            {
                if (e.KeyCode == Keys.R)
                {
                    if (radioButton1.Checked) radioButton2.Checked = true;
                    else if (radioButton2.Checked) radioButton3.Checked = true;
                    else if (radioButton3.Checked) radioButton4.Checked = true;
                    else if (radioButton4.Checked) radioButton1.Checked = true;
                    PicToRot();
                    iskeydown = true;
                }
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            iskeydown = false;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            GenerateBlocks(64, 12, 12, 64, 64, 64, 64, 12, 8);
            GC.Collect(0, GCCollectionMode.Forced);
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            GenerateBlocks(256 ,12, 12, 32, 32, 32, 32, 12, 16);
            GC.Collect(0, GCCollectionMode.Forced);
        }

        private void GenerateBlocks(int blocks, int Xs, int Ys,
            int w, int h, int Xp, int Yp, int rX, int Xmax)
        {
            int X = Xs, Y = Ys;
            for (int i = 0; i < blocks; i++)
            {
                imagecode[i] = 0;
                level[i].Image = new Bitmap(Properties.Resources.p5);
                level[i].Location = new Point(X, Y);
                level[i].Size = new Size(w, h);
                X += Xp;
                if (i+1 == Xmax*((i+1)/Xmax))
                {
                    X = rX;
                    Y += Yp;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!File.Exists(@"data\race\race.ipl") || !File.Exists("sa_rtmaker64.ipl"))
            {
                RegistryKey key = Registry.LocalMachine;
                RegistryKey key2 = key.OpenSubKey("SOFTWARE\\Wow6432Node\\Rockstar Games\\GTA San Andreas\\Installation");

                string tmp;
                tmp = Convert.ToString(key2.GetValue("ExePath"));
                if (tmp.Length > 0 || tmp != null) fbd.SelectedPath = tmp.Substring(1, tmp.Length - 12);
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(fbd.SelectedPath + @"\data\race\race_bound.ipl")) File.Delete(fbd.SelectedPath + @"\data\race\race_bound.ipl");
                    File.Copy("sa_rtmaker64.ipl", fbd.SelectedPath + @"\data\race\race_bound.ipl");
                    MessageBox.Show("Changed bounds to 8x8", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if (File.Exists(@"data\race\race_bound.ipl")) File.Delete(@"data\race\race_bound.ipl");
                File.Copy("sa_rtmaker64.ipl", @"data\race\race_bound.ipl");
                MessageBox.Show("Changed bounds to 8x8", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (!File.Exists(@"data\race\race.ipl") || !File.Exists("sa_rtmaker256.ipl"))
            {
                RegistryKey key = Registry.LocalMachine;
                RegistryKey key2 = key.OpenSubKey("SOFTWARE\\Wow6432Node\\Rockstar Games\\GTA San Andreas\\Installation");

                string tmp;
                tmp = Convert.ToString(key2.GetValue("ExePath"));
                if (tmp.Length > 0 || tmp != null) fbd.SelectedPath = tmp.Substring(1, tmp.Length - 12);
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    if(File.Exists(fbd.SelectedPath + @"\data\race\race_bound.ipl")) File.Delete(fbd.SelectedPath + @"\data\race\race_bound.ipl");
                    File.Copy("sa_rtmaker256.ipl", fbd.SelectedPath + @"\data\race\race_bound.ipl");
                    MessageBox.Show("Changed bounds to 16x16", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if (File.Exists(@"data\race\race_bound.ipl")) File.Delete(@"data\race\race_bound.ipl");
                File.Copy("sa_rtmaker256.ipl", @"data\race\race_bound.ipl");
                MessageBox.Show("Changed bounds to 16x16", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}