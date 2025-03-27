internal class Program
{
    static bool isPrime = true;
    static bool isFibonacci = true;
    static void GeneratePrimes(int min, int max)
    {
        for (int num = min; num <= max && isPrime; num++)
        {
            if (IsPrime(num))
                Console.WriteLine($"Prime - {num}");
        }
    }
    static bool IsPrime(int num)
    {
        if (num < 2) return false;
        for (int i = 2; i * i <= num; i++)
        {
            if (num % i == 0) return false;
        }
        return true;
    }

    static void GenerateFibonacci()
    {
        long a = 0, b = 1;
        while (isFibonacci)
        {
            Console.WriteLine($"Fibonacci - {a}");
            long temp = a + b;
            a = b;
            b = temp;
            Thread.Sleep(500); 
        }
    }
    private static void Main(string[] args)
    {
        Console.Write("Enter min: ");
        string min = Console.ReadLine()!;
        int min_;
        if (string.IsNullOrEmpty(min))
            min_ = 2;
        else
            min_ = int.Parse(min);


        Console.Write("Enter max: ");
        string max = Console.ReadLine()!;
        int max_;
        if (string.IsNullOrEmpty(max))
            max_ = int.MaxValue;
        else
            max_ = int.Parse(max);

        Thread prime = new Thread(() => GeneratePrimes(min_, max_));
        Thread fibonacci = new Thread(GenerateFibonacci);

        prime.Start();
        fibonacci.Start();

        Console.WriteLine("Enter any key");
        Console.ReadKey();

        isPrime = false;
        isFibonacci = false;

        prime.Join();
        fibonacci.Join();
    }
}