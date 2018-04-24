/*
  Deep Lingo API.
  Copyright (C) 2018 Ariel Ortiz, ITESM CEM

  To compile this module as a DLL:

                mcs /t:library deeplingolib.cs

  To link this DLL to a program written in C#:

                mcs /r:deeplingolib.dll someprogram.cs

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.

  This program is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Text;
using System.Collections.Generic;

namespace DeepLingo {

    public class Utils {

        private static int currentHandleID = 0;

        private static Dictionary<int, List<int>> handles =
            new Dictionary<int, List<int>>();

        //----------------------------------------------------------------------
        // Prints i to stdout as a decimal integer. Does not print a new line
        // at the end. Returns 0.
        public static int Printi(int i) {
            Console.Write(i);
            return 0;
        }

        //----------------------------------------------------------------------
        // Prints a character to stdout, where c is its Unicode code point.
        // Does not print a new line at the end. Returns 0.
        public static int Printc(int c) {
            Console.Write(char.ConvertFromUtf32(c));
            return 0;
        }

        //----------------------------------------------------------------------
        // Prints s to stdout as a string. s must be a handle to an array list
        // containing zero or more Unicode code points. Does not print a new
        // line at the end. Returns 0.
        public static int Prints(int s) {
            CheckHandle(s);
            StringBuilder builder = new StringBuilder();
            for (int i = 0, n = Size(s); i < n; i++) {
                builder.Append(char.ConvertFromUtf32(Get(s, i)));
            }
            Console.Write(builder.ToString());
            return 0;
        }

        //----------------------------------------------------------------------
        // Prints a newline character to stdout. Returns 0.
        public static int Println() {
            Console.WriteLine();
            return 0;
        }

        //----------------------------------------------------------------------
        // Reads from stdin a signed decimal integer and return its value. Does
        // not return until a valid integer has been read.
        public static int Readi() {
            string input;
            int result;
            do {
                input = Console.ReadLine();
            } while (!int.TryParse(input, out result));

            return result;
        }

        //----------------------------------------------------------------------
        // Reads from stdin a string (until the end of line) and returns a
        // handle to a newly created array list containing the Unicode code
        // points of all the characters read, excluding the end of line.
        public static int Reads() {
            string input = Console.ReadLine();
            int handle = New(0);
            foreach (int i in AsCodePoints(input)) {
                Add(handle, i);
            }
            return handle;
        }

        //----------------------------------------------------------------------
        // Creates a new array list object with n elements and returns its
        // handle. All the elements of the array list are set to zero. Throws
        // an exception if n is less than zero.
        public static int New(int n) {
            if (n < 0) {
                throw new Exception("Can't create a negative size array.");
            }
            int handle = currentHandleID++;
            handles.Add(handle, new List<int>());
            for (int i = 0; i < n; i++) {
                Add(handle, 0);
            }
            return handle;
        }

        //----------------------------------------------------------------------
        // Returns the size (number of elements) of the array list referenced
        // by handle h. Throws an exception if h is not a valid handle.
        public static int Size(int h) {
            CheckHandle(h);
            return handles[h].Count;
        }

        //----------------------------------------------------------------------
        // Adds x at the end of the array list referenced by handle h.
        // Returns 0. Throws an exception if h is not a valid handle.
        public static int Add(int h, int x) {
            CheckHandle(h);
            handles[h].Add(x);
            return 0;
        }

        //----------------------------------------------------------------------
        // Returns the value at index i from the array list referenced by
        // handle h. Throws an exception if i is out of bounds or if h is not
        // a valid handle.
        public static int Get(int h, int i) {
            CheckHandle(h);
            return handles[h][i];
        }

        //----------------------------------------------------------------------
        // Sets to x the element at index i of the array list referenced by
        // handle h. Returns 0. Throws an exception if i is out of bounds or
        // if h is not a valid handle.
        public static int Set(int h, int i, int x) {
            CheckHandle(h);
            handles[h][i] = x;
            return 0;
        }
        
        //----------------------------------------------------------------------
        // Returns the result of raising b to the power of e. Throws an
        // exception if the result doesn't fit into a 64-bit signed integer. 
        public static int Pow(int b, int e) {
            int result = 1;
            for (int i = 0; i < e; i++) {
                checked {
                    result *= b;
                }
            }
            return result;
        }

        //----------------------------------------------------------------------
        // Local function that checks if h is a valid array list handle.
        private static void CheckHandle(int h) {
            if (!handles.ContainsKey(h)) {
                throw new Exception("Invalid array handle.");
            }
        }

        //----------------------------------------------------------------------
        // Local function that allows obtaining all the individual Unicode code
        // points of a given string.
        private static IEnumerable<int> AsCodePoints(string str) {
            for(int i = 0; i < str.Length; i++) {
                yield return char.ConvertToUtf32(str, i);
                if (char.IsHighSurrogate(str, i)) {
                    i++;
                }
            }
        }
    }
}
