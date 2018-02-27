using System;

public class Dynamic {

    public static void method(int x) {
        Console.WriteLine("method(int)");
    }

    public static void method(String x) {
        Console.WriteLine("method(String)");
    }

    public static void Main() {
        dynamic d = 1;
        method(d);
        d = "hello";
        method(d);
    }
}