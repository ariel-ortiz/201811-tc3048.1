//==========================================================
// Solution to problem 2.
//==========================================================

using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Exam1 {
    public class Problem2 {
        public static void Main(String[] args) {
            if (args.Length != 1) {
                Console.WriteLine("Please specify input file.");
                Environment.Exit(1);
            }
            String fileName = args[0];
            String fileContent = File.ReadAllText(fileName);
            Regex regex = new Regex(@"(&[#]x([0-9a-fA-F]+);)|(.|\n)");
            foreach (Match m in regex.Matches(fileContent)) {
                if (m.Groups[1].Success) {
                    int dec = Convert.ToInt32(m.Groups[2].Value, 16);
                    Console.Write("&#" + dec + ";");
                } else {
                    Console.Write(m.Value);
                }
            }
        }
    }
}
