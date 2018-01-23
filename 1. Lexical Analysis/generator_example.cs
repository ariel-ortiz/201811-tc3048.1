using System;
using System.Collections.Generic;

public class GeneratorExample {

    // Generator method
    public static IEnumerable<int> Start() {
        var c = 1;
        while (c < 10000) {
            yield return c;
            c *= 2;
        }
    }

    public static void Main() {
        var enumerable = Start();
        var enumerator = enumerable.GetEnumerator();
        while (enumerator.MoveNext()) {
            Console.WriteLine(enumerator.Current);
        }
    }
}
