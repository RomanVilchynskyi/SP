using System;
using System.Threading;

class FibonacciCounter
{
    private int n1 = 1, n2 = 1, evenCount = 0;
    private object lock_ = new object();

    public void CalcFibonacci()
    {
        for (int i = 1; i <= 10; i++)
        {
            lock (lock_)
            {
                int tmp = n1 + n2;
                n1 = n2;
                n2 = tmp;

                if (tmp % 2 == 0)
                    evenCount++;

                Console.WriteLine($"{i,-10}{n1,-10}{n2,-10}{evenCount}");
            }
            Thread.Sleep(100);
        }
    }
}

class Program
{
    static void Main()
    {
        FibonacciCounter counter = new FibonacciCounter();
        Console.WriteLine("Iteration   n1        n2        evenCount");
        Thread t1 = new Thread(counter.CalcFibonacci);
        Thread t2 = new Thread(counter.CalcFibonacci);
        Thread t3 = new Thread(counter.CalcFibonacci);

        t1.Start();
        t2.Start();
        t3.Start();

        t1.Join();
        t2.Join();
        t3.Join();
    }
}
