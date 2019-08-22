using System;
using System.Windows.Forms;

namespace SashaPassGen
{
    public partial class GUI : Form
    {
        public GUI()
        {
            InitializeComponent();
        }

        private void GUI_Load(object sender, EventArgs e)
        {

        }

        GenerateStuff GS = new GenerateStuff();

        private void Generate_Click_1(object sender, EventArgs e)
        {
            textBox2.Text = GenerateStuff.FinalOutput;
            GS.GeneratePassword();
            textBox1.Text = GenerateStuff.FinalOutput;
            Phonetics pho = new Phonetics();
            PhoOutput.Text = pho.PhoneticEnator();
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
                Phonetics pho = new Phonetics();
                Phonetics.PhoneticEnatorPos++;
                PhoOutput.Text = pho.PhoneticEnator();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null)
            {

            }
            else
            {
                Phonetics pho = new Phonetics();
                Phonetics.PhoneticEnatorPos--;
                PhoOutput.Text = pho.PhoneticEnator();
            }
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
