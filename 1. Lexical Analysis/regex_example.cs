using System;
using System.Text.RegularExpressions;

public class RegExExample {

    public static void Main() {
        var regex = new Regex(@"([_A-Za-z]\w*)|(\d+)");
        var str = "hello 123 hi_234 0001";
        foreach (Match m in regex.Matches(str)) {
            if (m.Groups[1].Success) {
                Console.WriteLine($"Found an identifier: {m.Value}"
                    + $" ({m.Index}, {m.Length})"); 
                
            } else if (m.Groups[2].Success) {
                Console.WriteLine($"Found an integer literal: {m.Value}"
                    + $" ({m.Index}, {m.Length})");
            }
        }
    }
}
