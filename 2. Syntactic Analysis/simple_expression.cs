/*

    Scanner + Parser for the following simple expression language:

    Expr -> Expr "+" Term
    Expr -> Term
    Term -> Term "*" Pow
    Term -> Pow
    Pow  -> Fact "^" Pow
    Pow  -> Fact
    Fact -> Int
    Fact -> "(" Expr ")"

    Convert into LL(1):

    Prog -> Expr EOF
    Expr -> Term ("+" Term)*
    Term -> Pow ("*" Pow)*
    Pow  -> Fact ("^" Pow)?
    Fact -> Int | "(" Expr ")"

*/

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public enum TokenCategory {
    PLUS, TIMES, POW, PAR_OPEN, PAR_CLOSE, INT, EOF, ILLEGAL
}

public class Token {
    public TokenCategory Category { get; }
    public String Lexeme { get; }
    public Token(TokenCategory category, String lexeme) {
        Category = category;
        Lexeme = lexeme;
    }
    public override String ToString() {
        return String.Format("[{0}, \"{1}\"]", Category, Lexeme);
    }
}

public class Scanner {
    readonly String input;
    static readonly Regex regex = new Regex(@"([+])|([*])|([(])|([)])|(\d+)|(\s)|(\^)|(.)");
    public Scanner(String input) {
        this.input = input;
    }
    public IEnumerable<Token> Start() {
        foreach (Match m in regex.Matches(input)) {
            if (m.Groups[1].Length > 0) {
                yield return new Token(TokenCategory.PLUS, m.Value);
            } else if (m.Groups[2].Length > 0) {
                yield return new Token(TokenCategory.TIMES, m.Value);
            } else if (m.Groups[3].Length > 0) {
                yield return new Token(TokenCategory.PAR_OPEN, m.Value);
            } else if (m.Groups[4].Length > 0) {
                yield return new Token(TokenCategory.PAR_CLOSE, m.Value);
            } else if (m.Groups[5].Length > 0) {
                yield return new Token(TokenCategory.INT, m.Value);
            } else if (m.Groups[6].Length > 0) {
                continue;
            } else if (m.Groups[7].Length > 0) {
                yield return new Token(TokenCategory.POW, m.Value);
            } else if (m.Groups[8].Length > 0) {
                yield return new Token(TokenCategory.ILLEGAL, m.Value);
            }
        }
        yield return new Token(TokenCategory.EOF, "");
    }
}

class SyntaxError: Exception {
}

public class Parser {
    IEnumerator<Token> tokenStream;
    public Parser(IEnumerator<Token> tokenStream) {
        this.tokenStream = tokenStream;
        this.tokenStream.MoveNext();
    }
    public TokenCategory Current {
        get { return tokenStream.Current.Category; }
    }
    public Token Expect(TokenCategory category) {
        if (Current == category) {
            Token current = tokenStream.Current;
            tokenStream.MoveNext();
            return current;
        } else {
            throw new SyntaxError();
        }
    }
    public int Prog() {
        var result = Expr();
        Expect(TokenCategory.EOF);
        return result;
    }
    public int Expr() {
        var result = Term();
        while (Current == TokenCategory.PLUS) {
            Expect(TokenCategory.PLUS);
            result += Term();
        }
        return result;
    }
    public int Term() {
        var result = Pow();
        while (Current == TokenCategory.TIMES) {
            Expect(TokenCategory.TIMES);
            result *= Pow();
        }
        return result;
    }
    public int Pow() {
        var result = Fact();
        if (Current == TokenCategory.POW) {
            Expect(TokenCategory.POW);
            var expo = Pow();
            result = (int) Math.Pow(result, expo);
        }
        return result;
    }
    public int Fact() {
        switch (Current) {
        case TokenCategory.INT:
            var t = Expect(TokenCategory.INT);
            return Convert.ToInt32(t.Lexeme);
        case TokenCategory.PAR_OPEN:
            Expect(TokenCategory.PAR_OPEN);
            var value = Expr();
            Expect(TokenCategory.PAR_CLOSE);
            return value;
        default:
            throw new SyntaxError();
        }
    }
}

public class SimpleExpression {
    public static void Main() {
        var line = Console.ReadLine();
        var parser = new Parser(new Scanner(line).Start().GetEnumerator());
        try {
            var result = parser.Prog();
            Console.WriteLine(result);
        } catch (SyntaxError) {
            Console.WriteLine("Bad syntax!");
        }
    }
}
