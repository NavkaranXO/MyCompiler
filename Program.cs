// See https://aka.ms/new-console-template for more information

using MyCompiler.Tokens;

string source = "!=<=-*";

Lexer lexer = new Lexer(source);

Token token = lexer.getToken();

while (token._tokenType != TokenType.EOF)
{
    Console.WriteLine(token._tokenType);
    token = lexer.getToken();
}

