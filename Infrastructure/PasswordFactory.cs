using System;
using System.Collections.Generic;
using System.Text;
using Core;

namespace Infrastructure
{
    public class PasswordFactory
    {
            public Random RandomNumber = new Random(); // creates the random number generator that will be used

            int Adjectives, Nouns, Verbs, Numbers, Colors, RNG = 0; //creates variables to decide where in each list the password should be generated from  (numbers)
            int AdjPrev, NounsPrev, VerbPrev, NumbersPrev, ColorsPrev = 0;//creates variable to store previous above variables
            int charmin = 15;  //stores the smallest character size a password can be
            int charmax = 18;//maximum size a password can be
            int numLimit = 999;//stores number for the biggest number that can be added to the end of the password
            int wordsAmount;//stores number of words to be added to the next password
            public string[] linesAD, linesNoun, linesVerb, linesColor; //creates arrays for the password generator to use for storing words
            public int insts; //secret Var

            public string GeneratePassword() //first func to be run in this class
            {
                //Thread.Sleep(insts * 2000);
                insts++;
                generateRandomWords(); //assigns random numbers and ensures they are legal
                string output;

                switch (RNG)// checks how the word should end up look
                {
                    case int n when (RNG <= 33):
                        output = genAdjectiveList() + genNounList() + GenNumber();
                        break;
                    case int n when (RNG <= 66 && RNG >= 34):
                        output = genColorList() + genNounList() + GenNumber();
                        break;
                    case int n when (RNG <= 99 && RNG >= 67):
                        output = genNounList() + GenNumber();
                        break;
                }
                if (wordsAmount == 3) //checks if system wants three words or two
                {
                    switch (RNG)
                    {
                        case int n when (RNG <= 33):
                            output = genAdjectiveList() + genNounList() + GenNumber();
                            break;
                        case int n when (RNG <= 66 && RNG >= 34):
                            output = genColorList() + genNounList() + GenNumber();
                            break;
                        case int n when (RNG <= 99 && RNG >= 67):
                            output = genAdjectiveList() + output;
                            break;
                    }
                }

                if (output.Length <= charmin || output.Length >= charmax)
                {
                    GeneratePassword(); //rerun code until we create a password that is viable (between min and max chars)
                }


                //Phonetics alph = new Phonetics(); //creates an instance of the dictionary
                Phonetics.PhoneticEnatorPos = 0; //resets the current position of the phonetic reader (second text box)
                return Phonetics.PhoneticEnator(output); //runs function to convert the password into phonetic alphabet
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
}
