//==========================================================
// Type your name and student ID here.
//==========================================================

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public enum TokenCategory {
    SYMBOL, EOL, ILLEGAL, EOF
}

public class Token {
    TokenCategory category;
    String lexeme;
    public TokenCategory Category {
        get { return category; }
    }
    public String Lexeme {
        get { return lexeme; }
    }
    public Token(TokenCategory category, String lexeme) {
        this.category = category;
        this.lexeme = lexeme;
    }
    public override String ToString() {
        return String.Format("[{0}, \"{1}\"]", Category, Lexeme);
    }
}

public class Scanner {
    readonly String input;
    static readonly Regex regex = new Regex(@"([a-z])|(\n)|(.)");
    public Scanner(String input) {
        this.input = input;
    }
    public IEnumerable<Token> Start() {
        foreach (Match m in regex.Matches(input)) {
            if (m.Groups[1].Success) {
                yield return new Token(TokenCategory.SYMBOL, m.Value);
            } else if (m.Groups[2].Success) {
                yield return new Token(TokenCategory.EOL, m.Value);
            } else if (m.Groups[3].Success) {
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
    public char Prog() {
        var result = Expr();
        Expect(TokenCategory.EOF);
        return result;
    }
    public char Expr() {
        return Symbol();
    }
    public char Symbol() {
        if (Current == TokenCategory.SYMBOL) {
            var token = Expect(TokenCategory.SYMBOL);
            return token.Lexeme[0];
        } else {
            throw new SyntaxError();
        }
    }
}

public class Fook {
    public static void Main(String[] args) {
        try {
            while (true) {
                Console.Write("> ");
                var line = Console.ReadLine();
                if (line == null) {
                    break;
                }
                var parser = new Parser(new Scanner(line).Start().GetEnumerator());
                var result = parser.Prog();
                Console.WriteLine(result);
            }
        } catch (SyntaxError) {
            Console.WriteLine("Syntax Error!");
        }
    }
}
