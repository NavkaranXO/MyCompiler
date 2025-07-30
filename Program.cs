// See https://aka.ms/new-console-template for more information

string source = "Hello how are you?";

Lexer lexer = new Lexer(source);

while (lexer.Peek() != '\0')
{
    Console.Write(lexer._currentChar);
    lexer.NextChar();
}

