namespace MyCompiler.Tokens;

public class Token
{
    public readonly string _token;
    public TokenType _tokenType;
    public Token(string token, TokenType tokenType)
    {
        _token = token;
        _tokenType = tokenType;
    }
    
}