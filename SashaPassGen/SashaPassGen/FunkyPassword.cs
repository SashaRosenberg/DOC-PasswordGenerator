using System;
using System.Collections.Generic;
using System.Windows.Forms;

/// <summary>
/// this code was designed and written by Sasha Rosenberg
/// feel free to take it, but plz credit <3
/// also if you're looking at this to hire me, this was done in three hours from concept to delivery inbetween calls
/// 
/// Version 1.5 
/// - 
/// fixed #11 and made slight optimizations on when items are being created. comments added
/// 
/// todo: waiting on feedback
/// </summary>

namespace SashaPassGen
{
    public class FunkyPassword : Form
    {
        //public List<string> Animals;
        // public List<>

        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.Run(new GUI()); // runs interface form
        }

    }
    public class GenerateStuff //class responsable for running password generation
    {
        public static string FinalOutput; // stores variable for the final password that is generated 
        public Random RandomNumber = new Random(); // creates the random number generator that will be used

        int Adjectives, Nouns, Numbers, Colors, RNG = 0; //creates variables to decide where in each list the password should be generated from  (numbers)
        int AdjPrev, NounsPrev, NumbersPrev, ColorsPrev = 0;//creates variable to store previous above variables

        int numLimit = 99;//stores number for the biggest number that can be added to the end of the password
        int rngRange = 80; //parcentage out of 100 that a color will generate inplace of a adjective
        public string[] linesAD, linesNoun, linesColor; //creates arrays for the password generator to use for storing words
        public void GeneratePassword() //first func to be run in this class
        {
            checkRandomShit(); //assigns random numbers and ensures they are legal

            if (RNG <= rngRange) //creates the password with adjective (if the random number generator is within the field)
            {
                FinalOutput = genAdjectiveList() + genNounList() + GenNumber(); 

            }
            else if (RNG >= rngRange)//creates the password with color (if the random number generator is within the field)
            {
                FinalOutput = genColorList() + genNounList() + GenNumber();

            }


            Phonetics alph = new Phonetics(); //creates an instance of the dictionary
            Phonetics.PhoneticEnatorPos = 0; //resets the current position of the phonetic reader (second text box)
            alph.PhoneticEnator(); //runs function to convert the password into phonetic alphabet
        }


        void checkRandomShit()
        {
            if (linesAD == null) //checks to ensure program is not reinstanciating lists every generate, but will run on boot (if arrays are empty)
            {
                linesAD = Properties.Resources.adjectives.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                linesNoun = Properties.Resources.animals.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                linesColor = Properties.Resources.colors.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            }

            ////
            //these loops checks if the new number that was chosen is the same as the last number, if it is, generate a new one.
            //this ensures that generations are not repeating (as much)
            ////
            for (; Adjectives == AdjPrev;) 
            {
                Adjectives = RandomNumber.Next(linesAD.Length);
            }
            for (; Nouns == NounsPrev;)
            {
                Nouns = RandomNumber.Next(linesNoun.Length);
            }
            for (; Numbers == NumbersPrev;)
            {
                Numbers = RandomNumber.Next(10, numLimit);
            }
            for (; Colors == ColorsPrev;)
            {
                Colors = RandomNumber.Next(linesColor.Length);
            }

            RNG = RandomNumber.Next(0, 100); //generates the number that will check against rngRange

            ////
            //these set the previous numbers to be current, so next time it is checked (when generate is ran) we can ensure it wont be repeated
            ///
            AdjPrev = Adjectives;
            NounsPrev = Nouns;
            NumbersPrev = Numbers;
            ColorsPrev = Colors;
        }

        ////
        ///these strings all do the same thing
        ////
        string genColorList()
        {
            string Result = "Blank"; //sets variable that will hold the output of this string

            Result = linesColor[Colors]; //chooses what word to use based on checkRandomShit's number that was generated
            Result = char.ToUpper(Result[0]) + Result.Substring(1).ToLower(); //formats the word to be cap first and the rest lower 

            return Result;
        }
        string genAdjectiveList()
        {
            string Result = "Blank";

            Result = linesAD[Adjectives];
            Result = char.ToUpper(Result[0]) + Result.Substring(1).ToLower();

            return Result;
        }
        string genNounList()
        {
            string Result = "Blank";
            Result = linesNoun[Nouns];
            return char.ToUpper(Result[0]) + Result.Substring(1).ToLower();
        }
        string GenNumber()
        {
            return Numbers.ToString();
        }
    }

    public class Phonetics
    {
        /// <summary>
        /// this creates the dictionary that this class will use to convert the password into NATO phonetic alphabet
        /// </summary>
        Dictionary<char, string> PhoneticDict = new Dictionary<char, string>()
        {
            { 'a', "Alpha"},
            { 'b', "Bravo"},
            { 'c', "Charlie"},
            { 'd', "Delta"},
            { 'e', "Echo"},
            { 'f', "Foxtrot"},
            { 'g', "Golf"},
            { 'h', "Hotel"},
            { 'i', "India"},
            { 'j', "Juliett"},
            { 'k', "Kilo"},
            { 'l', "Lima"},
            { 'm', "Mike"},
            { 'n', "November"},
            { 'o', "Oscar"},
            { 'p', "Papa"},
            { 'q', "Quebec"},
            { 'r', "Romeo"},
            { 's', "Sierra"},
            { 't', "Tango"},
            { 'u', "Uniform"},
            { 'v', "Victor"},
            { 'w', "Whiskey"},
            { 'x', "X-ray"},
            { 'y', "Yankee"},
            { 'z', "Zulu"},
            { 'A', "Alpha"},
            { 'B', "Bravo"},
            { 'C', "Charlie"},
            { 'D', "Delta"},
            { 'E', "Echo"},
            { 'F', "Foxtrot"},
            { 'G', "Golf"},
            { 'H', "Hotel"},
            { 'I', "India"},
            { 'J', "Juliett"},
            { 'K', "Kilo"},
            { 'L', "Lima"},
            { 'M', "Mike"},
            { 'N', "November"},
            { 'O', "Oscar"},
            { 'P', "Papa"},
            { 'Q', "Quebec"},
            { 'R', "Romeo"},
            { 'S', "Sierra"},
            { 'T', "Tango"},
            { 'U', "Uniform"},
            { 'V', "Victor"},
            { 'W', "Whiskey"},
            { 'X', "X-ray"},
            { 'Y', "Yankee"},
            { 'Z', "Zulu"},
            { '0', "0"},
            { '1', "1"},
            { '2', "2"},
            { '3', "3"},
            { '4', "4"},
            { '5', "5"},
            { '6', "6"},
            { '7', "7"},
            { '8', "8"},
            { '9', "9"},

    };
        //#13
        public static short PhoneticEnatorPos = 0; //stores the current position of phonetic letter being shown 
        private void numCheck()
        {
            if (PhoneticEnatorPos < 0) //makes sure this var stays positive
                PhoneticEnatorPos = 0;

            if (PhoneticEnatorPos == GenerateStuff.FinalOutput.Length) //makes sure this var stays below the current size of the password
                PhoneticEnatorPos--;
            
        }

        public string PhoneticEnator()
        {
            numCheck();//checks that PhoneticEnatorPos is in a legal position
            
            
            ////
            ///this checks what type of output should be used for the current letter
            ///and then adds the correct flavor text to it
            ///
            ///currently only lowercase, uppercase, number
            ///to add: special characters, white spacces
            ////
            if (char.IsLower(GenerateStuff.FinalOutput[PhoneticEnatorPos])) //checks if the current letter is lowcase or uppercase
            {
                return "Lowercase " + GenerateStuff.FinalOutput[PhoneticEnatorPos].ToString() + " for " + PhoneticDict[GenerateStuff.FinalOutput[PhoneticEnatorPos]];
            }
            else if (char.IsUpper(GenerateStuff.FinalOutput[PhoneticEnatorPos]))
            {
                return "Uppercase " + GenerateStuff.FinalOutput[PhoneticEnatorPos].ToString() + " for " + PhoneticDict[GenerateStuff.FinalOutput[PhoneticEnatorPos]];
            }
            else
            {
                return "The number " + PhoneticDict[GenerateStuff.FinalOutput[PhoneticEnatorPos]];
            }
        }
    }
}

