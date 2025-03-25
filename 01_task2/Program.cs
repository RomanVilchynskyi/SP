using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        Process process = new Process();
        process.StartInfo.FileName = "notepad.exe"; 

        process.Start();

        Console.WriteLine("Enter '1' to wait for finishing or enter '2' to force end the process:");
        string choice = Console.ReadLine()!;

        if (choice == "1")
        {
            process.WaitForExit();
            Console.WriteLine($"Process finished with code: {process.ExitCode}");
        }
        else if (choice == "2")
        {
            process.Kill();
            Console.WriteLine("Process ended forcly");
        }
    }
}