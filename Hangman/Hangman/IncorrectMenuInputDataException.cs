namespace Hangman;

public class IncorrectMenuInputDataException : ArgumentException
{
    public IncorrectMenuInputDataException()
    {  
    }

    public IncorrectMenuInputDataException(string message)
        : base(message)
    {
    }

    public IncorrectMenuInputDataException(string message, ArgumentException inner)
        : base(message, inner)
    {

    }
}
