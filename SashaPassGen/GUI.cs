using System;
using System.Windows.Forms;
using Core;

namespace SashaPassGen
{
    public partial class GUI : Form
    {
        private string _passcode = string.Empty;
        private int _position = 0;


        public GUI()
        {
            InitializeComponent();
        }


        public static string FormatPhoOutput(char character)
        {
            Console.WriteLine("runnings");

            ////
            ///this checks what type of output should be used for the current letter
            ///and then adds the correct flavor text to it
            ///
            ///currently only lowercase, uppercase, number
            ///to add: special characters, white spacces
            ////

            var phonetic = Phonetics.Convert(character);
             

            if (char.IsLower(character)) //checks if the current letter is lowcase or uppercase
            {
                return "Lowercase " + character + " for " + phonetic;
            }
            if (char.IsUpper(character))
            {
                return "Uppercase " + character + " for " + phonetic;
            }
            if (
                character == '@'
                || character == '$'
                || character == '!'
                || character == '#'
            ) { return "The " + phonetic + " symbol"; }
              
            return "The number " + character;
        }


        private void Generate_Click_1(object sender, EventArgs e)
        {
            GenerateStuff GS = new GenerateStuff();
            if (checkBox1.Checked) //do you want to use special characters
            {
                GS.GeneratePassword();
                generatedPasscode = Phonetics.generateSpecialChar(_passcode);
                textBox1.Text = GenerateStuff.FinalOutput;

                PhoOutput.Text = Phonetics.Enator();
            }
            else
            {

                GS.GeneratePassword();
                textBox1.Text = GenerateStuff.FinalOutput;
                PhoOutput.Text = Phonetics.Enator(_passcode);

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
                _position++;
                PhoOutput.Text = Phonetics.Enator(_passcode[_position]);
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
                PhoOutput.Text = Phonetics.Enator();
            }
        } 
    }
}
