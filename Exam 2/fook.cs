//==========================================================
// Solution de problem 1.
//
// LL(1) Grammar:
// 
//      prog       -> expr EOF
//      expr       -> (symbol | "{" expr_list "}")("+"|"-")*
//      expr_list  -> expr ("," expr)*
//==========================================================

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public enum TokenCategory {
    SYMBOL, SUCCESSOR, PREDECESSOR, MAX_LEFT, MAX_RIGHT, 
    COMMA, EOL, ILLEGAL, EOF
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
    static readonly Regex regex = new Regex(@"([a-z])|(\+)|(\-)|(\{)|(\})|(\,)|(\n)|([ \t])|(.)");
    public Scanner(String input) {
        this.input = input;
    }
    public IEnumerable<Token> Start() {
        foreach (Match m in regex.Matches(input)) {
            if (m.Groups[1].Success) {
                yield return new Token(TokenCategory.SYMBOL, m.Value);
            } else if (m.Groups[2].Success) {
                yield return new Token(TokenCategory.SUCCESSOR, m.Value);
            } else if (m.Groups[3].Success) {
                yield return new Token(TokenCategory.PREDECESSOR, m.Value);
            } else if (m.Groups[4].Success) {
                yield return new Token(TokenCategory.MAX_LEFT, m.Value);
            } else if (m.Groups[5].Success) {
                yield return new Token(TokenCategory.MAX_RIGHT, m.Value);
            } else if (m.Groups[6].Success) {
                yield return new Token(TokenCategory.COMMA, m.Value);
            } else if (m.Groups[7].Success) {
                yield return new Token(TokenCategory.EOL, m.Value);
            } else if (m.Groups[8].Success) {
                continue;
            } else if (m.Groups[9].Success) {
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
        var result = 0;
        switch (Current) {
        case TokenCategory.SYMBOL:
            var token = Expect(TokenCategory.SYMBOL);
            result = token.Lexeme[0];
            break;
        case TokenCategory.MAX_LEFT:
            Expect(TokenCategory.MAX_LEFT);
            result = ExprList();
            Expect(TokenCategory.MAX_RIGHT);
            break;
        default:
            throw new SyntaxError();
        }
        while (Current == TokenCategory.SUCCESSOR 
                || Current == TokenCategory.PREDECESSOR) {
            if (Current == TokenCategory.SUCCESSOR) {
                Expect(TokenCategory.SUCCESSOR);
                result = result == 'z' ? 'a' : result + 1;
            } else if (Current == TokenCategory.PREDECESSOR) {
                Expect(TokenCategory.PREDECESSOR);
                result = result == 'a' ? 'z' : result - 1;
            }
        }
        return (char) result;
    }
    
    public char ExprList() {
        var result = Expr();
        while (Current == TokenCategory.COMMA) {
            Expect(TokenCategory.COMMA);
            var other = Expr();
            result = result > other ? result : other;
        }
        return result;
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
