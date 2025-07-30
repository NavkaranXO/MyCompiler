
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
        if (_currentPosition < _source.Length)
        {
            return _source[_currentPosition];
        }
        else return '\0';
    }
}
