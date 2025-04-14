internal class Program
{
    static long Factorial(int n)
    {
        long result = 1;
        for (int i = 2; i <= n; i++) result *= i;
        return result;
    }

    static int CountDigits(int n) => n.ToString().Length;

    static int SumDigits(int n)
    {
        int sum = 0;
        while (n != 0)
        {
            sum += n % 10; 
            n /= 10;       
        }
        return sum;
    }
    private static void Main(string[] args)
    {
        /*Console.Write("Enter number: ");
        int number = int.Parse(Console.ReadLine());

        Parallel.Invoke(
            () => Console.WriteLine($"Factorial: {Factorial(number)}"),
            () => Console.WriteLine($"Number of digits: {CountDigits(number)}"),
            () => Console.WriteLine($"Sum: {SumDigits(number)}")
        );*/

        ///
        /*Console.Write("Initial number: ");
        int start = int.Parse(Console.ReadLine());
        Console.Write("Finite number: ");
        int end = int.Parse(Console.ReadLine());

        using (StreamWriter writer = new StreamWriter("multiplication_table.txt"))
        {
            Parallel.For(start, end + 1, i =>
            {
                for (int j = 1; j <= 10; j++)
                {
                    string line = $"{i} * {j} = {i * j}";
                    lock (writer)
                    {
                        writer.WriteLine(line);
                    }
                }
            });
        }

        Console.WriteLine("Multiplication table saved in file");*/


        //
        /*var numbers = File.ReadAllLines("numbers.txt").Select(int.Parse).ToList();

        Parallel.ForEach(numbers, n =>
        {
            Console.WriteLine($"Factorial {n} = {Factorial(n)}");
        });*/




        //
        var numbers2 = File.ReadAllLines("numbers2.txt").Select(int.Parse).ToList();

        var sum = numbers2.AsParallel().Sum();
        var min = numbers2.AsParallel().Min();
        var max = numbers2.AsParallel().Max();

        Console.WriteLine($"Sum: {sum}");
        Console.WriteLine($"Min: {min}");
        Console.WriteLine($"Max: {max}");
    }
}