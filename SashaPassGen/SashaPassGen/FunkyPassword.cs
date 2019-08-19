using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

/// <summary>
/// this code was designed and written by Sasha Rosenberg
/// feel free to take it, but plz credit <3
/// also if you're looking at this to hire me, this was done in three hours from concept to delivery inbetween calls
/// Version 1.0
/// todo: add crash prevention and text clensing
/// </summary>

namespace SashaPassGen
{


    public class FunkyPassword : Form
    {
        //public List<string> Animals;
        // public List<>

        static string password;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new GUI());
            GenerateStuff GS = new GenerateStuff();
            GS.GeneratePassword();
            password = GS.FinalOutput;
            //get list of adjectives
            Console.WriteLine(password + "!!!");

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            GS.GeneratePassword();
            password = GS.FinalOutput;
            //get list of adjectives
            Console.WriteLine(password + "!!!");
        }

    }
    public class GenerateStuff
    {
        public string FinalOutput;
        public Random rnd1 = new Random();
        public Random rnd2 = new Random();
        public Random rnd3 = new Random();
        int Adjectives, Noun, Number = 0;
        int Num_store = 99;
        public string[] linesAD, linesNoun;


        void checkRandomShit()
        {
            linesAD = Properties.Resources.adjectives.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            linesNoun = Properties.Resources.animals.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            Adjectives = rnd1.Next(linesAD.Length);
            Noun = rnd2.Next(linesNoun.Length);
            while (Noun == Adjectives)
            {
                Noun = rnd2.Next(linesNoun.Length);
            }

            Number = rnd3.Next(10, Num_store);
            while (Number == Adjectives || Number == Noun)
            {
                Number = rnd3.Next(10, Num_store);
            }
            Console.WriteLine(Adjectives + Noun + Number);

        }
        public void GeneratePassword()
        {
            checkRandomShit();
            FinalOutput = genAdjectiveList() + genNounList() + GenNumber();
        }

        string genAdjectiveList()
        {
            string Result = "Blank";

            Result = linesAD[Adjectives];
            Result =  char.ToUpper(Result[0]) + Result.Substring(1).ToLower();

            return Result;
        }
        string GenNumber()
        {
            return Number.ToString();
        }
        string genNounList()
        {
            string Result = "Blank";
            Result = linesNoun[Noun];
            return char.ToUpper(Result[0]) + Result.Substring(1).ToLower();
        }

    }

}

