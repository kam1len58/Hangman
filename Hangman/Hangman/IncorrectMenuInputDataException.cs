using System.Collections.Immutable;

namespace Hangman;

public class IncorrectMenuInputDataException : Exception
{
    public IncorrectMenuInputDataException()
    {  
    }

    public IncorrectMenuInputDataException(string message)
        : base(message)
    {
    }

    public IncorrectMenuInputDataException(string message, Exception inner)
        : base(message, inner)
    {

    }
}
