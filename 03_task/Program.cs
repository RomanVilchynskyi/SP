internal class Program
{
    static int totalWords = 0;
    static int totalLines = 0;
    static int totalPunctuation = 0;
    static object lockObject = new object();

    static void AnalyzeFile(string filePath)
    {
        string text = File.ReadAllText(filePath);
        int words = text.Split(new char[] { ' ', '\n', '\r', '\t' }).Length;
        int lines = File.ReadAllLines(filePath).Length;
        int punctuation = text.Count(c => ",.;:!?\"(){}[]<>/".Contains(c));

        Interlocked.Add(ref totalWords, words);
        Interlocked.Add(ref totalLines, lines);

        lock (lockObject)
        {
            totalPunctuation += punctuation;
        }
    }
    private static void Main(string[] args)
    {
        string directoryPath = @"C:\Users\MASTER\Desktop\test"; 
        if (!Directory.Exists(directoryPath))
        {
            Console.WriteLine("Directory not found");
            return;
        }

        string[] files = Directory.GetFiles(directoryPath, "*.txt");
        Thread[] threads = new Thread[files.Length];

        for (int i = 0; i < files.Length; i++)
        {
            string file = files[i];
            threads[i] = new Thread(() => AnalyzeFile(file));
            threads[i].Start();
        }

        foreach (Thread thread in threads)
        {
            thread.Join();
        }

        Console.WriteLine($"Total words: {totalWords}");
        Console.WriteLine($"Total lines: {totalLines}");
        Console.WriteLine($"Total punctuation marks: {totalPunctuation}");
    }
}