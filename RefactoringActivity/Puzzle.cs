namespace RefactoringActivity;

public class Puzzle
{
    public string Name;
    private string _question;
    private string _answer;
    
    public Puzzle(string name, string question, string answer)
    {
        Name = name;
        _question = question;
        _answer = answer;
    }

    public bool Solve()
    {
        Console.WriteLine($"Puzzle: {_question}");
        Console.Write("Your answer: ");
        string playerAnswer = Console.ReadLine()?.ToLower();
        return playerAnswer == _answer.ToLower();
    }
    
}