namespace Hangman;

public static class Drawing
{
    public const string Pillar = """
     -------|
     |      
     |       
     |       
     |       
     |_______
     """;
    public const string AttemptOne = """
     -------|
     |      0
     |       
     |       
     |       
     |_______
     """;
    public const string AttemptTwo = """
     -------|
     |      0
     |      |       
     |       
     |       
     |_______
     """;
    public const string AttemptThree = """
     -------|
     |      0
     |      |\       
     |       
     |       
     |_______
     """;
    public const string AttemptFour = """
     -------|
     |      0
     |     /|\       
     |       
     |       
     |_______
     """;
    public const string AttemptFive = """
     -------|
     |      0
     |     /|\       
     |       \
     |       
     |_______
     """;
    public const string AttemptSix = """
     -------|
     |      0
     |     /|\       
     |     / \
     |       
     |_______
     """;
}
