using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;

/// <summary>
/// this code was designed and written by Sasha Rosenberg
/// feel free to take it, but plz credit <3
/// also if you're looking at this to hire me, this was done in three hours from concept to delivery inbetween calls
/// 
/// Version 1.8.3
/// Small changes to Lists (christmas themed)
/// 
/// - 
/// Major changes to how many time words are passed between methods to increase stability
///     Removed validate() method, it is now run inside "checkrandomstuff()"
///     Changed the way RNG is calculated and treated to accomidate for future updates (specifically, more lists)
///     Changed the way RNG is calculated and treated to accomidate for future updates (specifically, more lists)
///         now supports up to 99 lists
/// 
/// Added new "Use special characters" option
///     this will replace a character with a special version (a into @) a set amount of times per word (currently 1)
///     option is now toggle-able
///     
/// Added limitations to final output password
///     will now recreate password if its not within a range (currently 15-19)
///     Changed conditions for when three words will be chosen
///     
///     
/// Added Christmas words
/// 
/// Added Special Characters to Phonetics class
/// 
///                 SMALL STUFF
/// Fixed Bug in Phonetics class that caused it to run twice 
/// Changed number limit to 999, allowing more number options
/// Changed likelyhood for a color to be generated
/// 
/// todo: Updated finaloutput variables to increase length
/// current plans: add verb variables before the code starts
/// add additional numbers
/// </summary>

namespace SashaPassGen
{

    public class FunkyPassword : Form
    {
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

        int Adjectives, Nouns, Verbs, Numbers, Colors, RNG = 0; //creates variables to decide where in each list the password should be generated from  (numbers)
        int AdjPrev, NounsPrev, VerbPrev, NumbersPrev, ColorsPrev = 0;//creates variable to store previous above variables
        int charmin = 15;  //stores the smallest character size a password can be
        int charmax = 18;//maximum size a password can be
        int numLimit = 999;//stores number for the biggest number that can be added to the end of the password
        int wordsAmount;//stores number of words to be added to the next password
        public string[] linesAD, linesNoun, linesVerb, linesColor; //creates arrays for the password generator to use for storing words
        public int insts; //secret Var

        public void GeneratePassword() //first func to be run in this class
        {
            //Thread.Sleep(insts * 2000);
            insts++;
            generateRandomWords(); //assigns random numbers and ensures they are legal
            switch (RNG)// checks how the word should end up look
            {
                case int n when (RNG <= 33):
                    FinalOutput = genAdjectiveList() + genNounList() + GenNumber();
                    break;
                case int n when (RNG <= 66 && RNG >= 34):
                    FinalOutput = genColorList() + genNounList() + GenNumber();
                    break;
                case int n when (RNG <= 99 && RNG >= 67):
                    FinalOutput = genNounList() + GenNumber();
                    break;
            }
            if (wordsAmount == 3) //checks if system wants three words or two
            {
                switch (RNG)
                {
                    case int n when (RNG <= 33):
                        FinalOutput = genAdjectiveList() + genNounList() + GenNumber();
                        break;
                    case int n when (RNG <= 66 && RNG >= 34):
                        FinalOutput = genColorList() + genNounList() + GenNumber();
                        break;
                    case int n when (RNG <= 99 && RNG >= 67):
                        FinalOutput = genAdjectiveList() + FinalOutput;
                        break;
                }
            }

            if (FinalOutput.Length <= charmin || FinalOutput.Length >= charmax)
            {
                GeneratePassword(); //rerun code until we create a password that is viable (between min and max chars)
            }


            //Phonetics alph = new Phonetics(); //creates an instance of the dictionary
            Phonetics.PhoneticEnatorPos = 0; //resets the current position of the phonetic reader (second text box)
            Phonetics.PhoneticEnator(); //runs function to convert the password into phonetic alphabet
        }


        void generateRandomWords()
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
                Validate(linesAD, Adjectives);

            }
            for (; Nouns == NounsPrev;)
            {

                Nouns = RandomNumber.Next(linesNoun.Length);
                Validate(linesNoun, Nouns);

            }
            for (; Numbers == NumbersPrev;)
            {
                Numbers = RandomNumber.Next(10, numLimit);
            }
            for (; Colors == ColorsPrev;)
            {
                Colors = RandomNumber.Next(linesColor.Length);
                Validate(linesColor, Colors);

            }
            ////for (; Verbs == VerbPrev;)
            ////{
            ////    Verbs = RandomNumber.Next(linesVerb.Length);
            ////    Validate(linesVerb, Verbs);

            ////}

            RNG = RandomNumber.Next(0, 100); //generates the number that will check against rngRange
            wordsAmount = RandomNumber.Next(2, 4);

            ////
            //these set the previous numbers to be current, so next time it is checked (when generate is ran) we can ensure it wont be repeated
            ///
            AdjPrev = Adjectives;
            NounsPrev = Nouns;
            NumbersPrev = Numbers;
            ColorsPrev = Colors;
            VerbPrev = Verbs;


        }


        ////
        ///these strings all do the same thing
        ////
        void Validate(string[] list, int entry)
        {
            if (list[entry] == "")
            {
                generateRandomWords();
            }

        }
        public static bool didFinalOutputFail = false;

        void Validate()
        {
            //failed on length check
            Console.WriteLine("Failed :(   || " + FinalOutput + " + " + FinalOutput.Length);
            didFinalOutputFail = true;
            //checkRandomThings(false);
        }

        string genColorList()
        {
            string Result = "Blank"; //sets variable that will hold the output of this string


            Result = linesColor[Colors]; //chooses what word to use based on checkRandomThings's number that was generated
            Result = char.ToUpper(Result[0]) + Result.Substring(1).ToLower(); //formats the word to be cap first and the rest lower 

            return Result;
        }
        string genAdjectiveList()
        {
            string Result = "Blank";

            Result = linesAD[Adjectives];
            if (Adjectives == AdjPrev)
            {

            }
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

    static class Phonetics
    {
        /// <summary>
        /// this creates the dictionary that this class will use to convert the password into NATO phonetic alphabet
        /// </summary>
        static Dictionary<char, string> PhoneticDict = new Dictionary<char, string>
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
            { '@', "At"},
            { '#', "Hash"},
            { '!', "Exclamation mark"},
            { '$', "Dollar Sign"},
    };
        static public char[] SpecialCharCheck;
        static Dictionary<char, char> SpcialDic = new Dictionary<char, char>
        {
            { 'a', '@'},
            { 'h', '#'},
            { 'i', '!'},
            { 's', '$'},
        };
        static public string generateSpecialChar(string conv)
        {
            SpecialCharCheck= conv.ToCharArray();
            int CurSpecial = 0;
            int SpecialLimit = 1;
            for (int counter = 0; counter < conv.Length; counter++)
            {
                if (Char.IsUpper(SpecialCharCheck[counter]))
                {
                    CurSpecial = 0;
                    SpecialCharCheck[counter] = SpecialCharCheck[counter];
                }
                else if (CurSpecial == SpecialLimit)
                {
                    SpecialCharCheck[counter] = SpecialCharCheck[counter];
                }
                else
                {
                    try
                    {
                        SpecialCharCheck[counter] = SpcialDic[SpecialCharCheck[counter]];
                        CurSpecial = CurSpecial + 1;
                    }
                    catch (System.Collections.Generic.KeyNotFoundException)
                    {
                        SpecialCharCheck[counter] = SpecialCharCheck[counter];
                    }

                }
            }
            conv = new string(SpecialCharCheck);
            return conv;
        }

        //#13
        public static short PhoneticEnatorPos = 0; //stores the current position of phonetic letter being shown 
        static void numCheck()
        {
            if (PhoneticEnatorPos < 0) //makes sure this var stays positive
                PhoneticEnatorPos = 0;

            if (PhoneticEnatorPos == GenerateStuff.FinalOutput.Length) //makes sure this var stays below the current size of the password
                PhoneticEnatorPos--;

        }

        public static string PhoneticEnator()
        {
            numCheck();//checks that PhoneticEnatorPos is in a legal position
            Console.WriteLine("runnings");

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
            else if (
                   GenerateStuff.FinalOutput[PhoneticEnatorPos] == '@'
                || GenerateStuff.FinalOutput[PhoneticEnatorPos] == '$'
                || GenerateStuff.FinalOutput[PhoneticEnatorPos] == '!'
                || GenerateStuff.FinalOutput[PhoneticEnatorPos] == '#'
                    ) { return "The " + PhoneticDict[GenerateStuff.FinalOutput[PhoneticEnatorPos]] + " symbol"; }

            else
            {
                return "The number " + GenerateStuff.FinalOutput[PhoneticEnatorPos];
            }
        }
    }
}

