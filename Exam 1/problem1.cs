//==========================================================
// Solution de problem 1.
//==========================================================

using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Exam1 {
    public class Problem1 {
        public static void Main(String[] args) {
            if (args.Length != 1) {
                Console.WriteLine("Please specify the input file.");
                Environment.Exit(1);
            }
            String fileName = args[0];
            String fileContents = File.ReadAllText(fileName);
            Regex regex = new Regex(@"^[Cc*].*\n?", RegexOptions.Multiline);
            Console.Write(regex.Replace(fileContents, ""));
        }
    }
}
