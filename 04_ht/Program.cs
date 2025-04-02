using System.Text.RegularExpressions;

internal class Program
{
    private static string AnalyzeText(string text)
    {
        int sentenceCount = text.Count(c => c == '.' || c == '!' || c == '?');
        int charCount = text.Length;
        int wordCount = text.Split(new[] { ' ', '\n', '\t' }).Length;
        int questionCount = text.Count(c => c == '?');
        int exclamationCount = text.Count(c => c == '!');

        return $"Sentences: {sentenceCount}\nCharacters: {charCount}\nWords: {wordCount}\nQuestions: {questionCount}\nExclamations: {exclamationCount}";
    }
    private static void Main(string[] args)
    {
        Console.WriteLine("Enter text:");
        string text = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(text))
        {
            Console.WriteLine("Error");
            return;
        }

        string result = Task.Run(() => AnalyzeText(text)).Result;
        Console.WriteLine(result);

        Console.WriteLine("Do you want to save the result to a file? yes/no");
        string choice = Console.ReadLine().ToLower();

        if (choice == "yes")
        {
            Console.WriteLine("Enter file name:");
            string fileName = Console.ReadLine();
            File.WriteAllText(fileName, result);
            Console.WriteLine($"Result saved to {fileName}");
        }
        
    }
}
