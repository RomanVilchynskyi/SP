internal class Program
{
    private static void Main(string[] args)
    {
        /*string filePath = @"C:\Users\MASTER\Desktop\numbers.txt";
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found");
            return;
        }

        string text = File.ReadAllText(filePath);

        List<int> numbers = text
           .Split(new[] { ' ', '\n','\t' })
           .Where(s => s != "")           
           .Select(s => int.Parse(s))
           .ToList();

        int uniqueCount = numbers
            .AsParallel()
            .Distinct()
            .Count();

        Console.WriteLine($"Total unique numbers: {uniqueCount}");*/

        string filePath = @"C:\Users\MASTER\Desktop\numbers.txt";
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found");
            return;
        }

        string text = File.ReadAllText(filePath);

        List<int> numbers = text
            .Split(new[] { ' ', '\n','\r' ,'\t' })
            .Where(s => s != "")
            .AsParallel()
            .Select(s => int.Parse(s))
            .ToList();

        int maxLen = 1;
        int currentLen = 1;
        int startIndex = 0;
        int maxStart = 0;

        for (int i = 1; i < numbers.Count; i++)
        {
            if (numbers[i] > numbers[i - 1])
            {
                currentLen++;
            }
            else
            {
                if (currentLen > maxLen)
                {
                    maxLen = currentLen;
                    maxStart = startIndex;
                }
                currentLen = 1;
                startIndex = i;
            }
        }

        if (currentLen > maxLen)
        {
            maxLen = currentLen;
            maxStart = startIndex;
        }

        List<int> result = new List<int>();

        for (int i = maxStart; i < maxStart + maxLen; i++)
        {
            result.Add(numbers[i]);
        }

        Console.WriteLine("Greatest length:");
        Console.WriteLine("Length: " + maxLen);
        Console.WriteLine("Numbers: " + string.Join(" ", result));
    }
}