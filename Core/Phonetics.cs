using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{

    public static class Phonetics
    {
        /// <summary>
        /// this creates the dictionary that this class will use to convert the password into NATO phonetic alphabet
        /// </summary>
        private static Dictionary<string, string> PhoneticDict = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
        {
            {"a", "Alpha"},
            {"b", "Bravo"},
            {"c", "Charlie"},
            {"d", "Delta"},
            {"e", "Echo"},
            {"f", "Foxtrot"},
            {"g", "Golf"},
            {"h", "Hotel"},
            {"i", "India"},
            {"j", "Juliett"},
            {"k", "Kilo"},
            {"l", "Lima"},
            {"m", "Mike"},
            {"n", "November"},
            {"o", "Oscar"},
            {"p", "Papa"},
            {"q", "Quebec"},
            {"r", "Romeo"},
            {"s", "Sierra"},
            {"t", "Tango"},
            {"u", "Uniform"},
            {"v", "Victor"},
            {"w", "Whiskey"},
            {"x", "X-ray"},
            {"y", "Yankee"},
            {"z", "Zulu"},
            {"@", "At"},
            {"#", "Hash"},
            {"!", "Exclamation mark"},
            {"$", "Dollar Sign"},
        };


        public static char[] SpecialCharCheck;
        static Dictionary<char, char> SpcialDic = new Dictionary<char, char>
        {
            { 'a', '@'},
            { 'h', '#'},
            { 'i', '!'},
            { 's', '$'},
        };

        public static string generateSpecialChar(string conv)
        {
            SpecialCharCheck = conv.ToCharArray();
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


        public static string Convert(char character)
        { 
            if (!PhoneticDict.TryGetValue(character.ToString(), out var phonetic))
                return "Invalid Character";
            return phonetic;
        }

    }
}
