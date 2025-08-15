using MyCompiler.Tokens;
public class Lexer
{
    private readonly string _source;

    public char _currentChar;

    private int _currentPosition = -1;


    public Lexer(string source)
    {
        _source = source + "\n";
        NextChar();
    }

    public void NextChar()
    {
        _currentPosition += 1;
        _currentChar = _currentPosition < _source.Length ? _source[_currentPosition] : '\0';
    }

    public char Peek()
    {
        if (_currentPosition + 1 < _source.Length)
        {
            return _source[_currentPosition + 1];
        }
        else return '\0';
    }

    public void SkipWhitespace()
    {
        while (_currentChar == ' ' || _currentChar == '\t' || _currentChar == '\r')
        {
            NextChar();
        }
    }

    public void SkipComment()
    {
        if (_currentChar == '#')
        {
            while (_currentChar != '\n')
            {
                NextChar();
            }
        }
    }

    public void Abort(string message)
    {
        Console.WriteLine($"Lexing Error: {message}");
        System.Environment.Exit(1);
    }

    public Token getToken()
    {
        SkipWhitespace();
        SkipComment();

        //Checking for digits
        if (char.IsDigit(_currentChar))
        {
            int startPos = _currentPosition;
            //Moving to next char until there is no number char
            while (char.IsDigit(Peek()))
            {
                NextChar();
            }
            //Peeking to see if there is a decimal
            if (Peek() == '.')
            {
                NextChar();
                //Atleast one char shoudl be digit after decimal
                if (!char.IsDigit(Peek()))
                {
                    Abort("Invalid character in number!");
                }
                while (char.IsDigit(Peek()))
                {
                    NextChar();
                }
            }
            //Move onto the next char as otherwise it will remain on the same number tokenizing it forever
            NextChar();
            string newToken = _source[startPos.._currentPosition];
            return new Token(newToken, TokenType.NUMBER);
        }

        //check if keyword or identifier
        if (char.IsLetter(_currentChar))
        {
            int startPos = _currentPosition;

            //first word cannot be digit for ident or keyword but the others can be
            while (char.IsLetterOrDigit(Peek()))
            {
                NextChar();
            }

            string newToken = _source[startPos.._currentPosition];
        }

        Token? token = null;
        switch (_currentChar)
        {
            case '+':
                token = new Token(_currentChar.ToString(), TokenType.PLUS);
                break;

            case '-':
                token = new Token(_currentChar.ToString(), TokenType.MINUS);
                break;

            case '*':
                token = new Token(_currentChar.ToString(), TokenType.ASTERISK);
                break;

            case '/':
                token = new Token(_currentChar.ToString(), TokenType.SLASH);
                break;

            case '\n':
                token = new Token(_currentChar.ToString(), TokenType.NEWLINE);
                break;

            case '\0':
                token = new Token(_currentChar.ToString(), TokenType.EOF);
                break;

            case '=':
                if (Peek() == '=')
                {
                    char lastChar = _currentChar;
                    NextChar();
                    string newChar = lastChar.ToString() + _currentChar.ToString();
                    token = new Token(newChar, TokenType.EQEQ);
                }
                else
                    token = new Token(_currentChar.ToString(), TokenType.EQ);
                break;
            case '>':
                if (Peek() == '=')
                {
                    char lastChar = _currentChar;
                    NextChar();
                    string newChar = lastChar.ToString() + _currentChar.ToString();
                    token = new Token(newChar, TokenType.GTEQ);
                }
                else
                    token = new Token(_currentChar.ToString(), TokenType.GT);
                break;

            case '<':
                if (Peek() == '=')
                {
                    char lastChar = _currentChar;
                    NextChar();
                    string newChar = lastChar.ToString() + _currentChar.ToString();
                    token = new Token(newChar, TokenType.LTEQ);
                }
                else
                    token = new Token(_currentChar.ToString(), TokenType.LT);
                break;

            case '!':
                if (Peek() == '=')
                {
                    char lastChar = _currentChar;
                    NextChar();
                    string newChar = lastChar.ToString() + _currentChar.ToString();
                    token = new Token(newChar, TokenType.NOTEQ);
                }
                else
                    Abort("Expected !=, Got !" + Peek().ToString());
                break;

            case '\"':
                NextChar();
                int startPos = _currentPosition;

                while (_currentChar != '\"')
                {
                    if (_currentChar == '\n' || _currentChar == '\t' || _currentChar == '\r')
                    {
                        Abort("Invalid token in string: " + _currentChar);
                    }
                    NextChar();
                }
                string newToken = _source[startPos.._currentPosition];
                token = new Token(newToken, TokenType.STRING);
                break;

            default:
                Abort($"Invalid Token {_currentChar}");
                break;
        }
        NextChar();
#pragma warning disable CS8603 // Possible null reference return.
        return token;
#pragma warning restore CS8603 // Possible null reference return.
    }
}
