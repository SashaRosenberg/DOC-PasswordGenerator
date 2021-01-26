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
    
}

