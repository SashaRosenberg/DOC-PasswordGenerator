﻿using System;
using System.Windows.Forms;

namespace SashaPassGen
{
    public partial class GUI : Form
    {
        public GUI()
        {
            InitializeComponent();
        }

        private void Generate_Click_1(object sender, EventArgs e)
        {
            GenerateStuff GS = new GenerateStuff();
            if (checkBox1.Checked) //do you want to use special characters
            {
                GS.GeneratePassword();
                GenerateStuff.FinalOutput = Phonetics.generateSpecialChar(GenerateStuff.FinalOutput);
                textBox1.Text = GenerateStuff.FinalOutput;

                PhoOutput.Text = Phonetics.PhoneticEnator();
            }
            else
            {

                GS.GeneratePassword();
                textBox1.Text = GenerateStuff.FinalOutput;
                PhoOutput.Text = Phonetics.PhoneticEnator();

            }
        }

        private void Copy_Click_1(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null)
            {

            }
            else
            {
                Phonetics.PhoneticEnatorPos++;
                PhoOutput.Text = Phonetics.PhoneticEnator();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null)
            {

            }
            else
            {
                Phonetics.PhoneticEnatorPos--;
                PhoOutput.Text = Phonetics.PhoneticEnator();
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            GenerateStuff GS = new GenerateStuff();

            for (int i = 0; i <= 1000000; i++)
            {
                Console.WriteLine("----------------");

                GS.GeneratePassword();
                textBox1.Text = GenerateStuff.FinalOutput;
                PhoOutput.Text = Phonetics.PhoneticEnator();
                Console.WriteLine(GenerateStuff.FinalOutput);
            }
        }
    }
}
