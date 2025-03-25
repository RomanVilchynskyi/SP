using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        Process process = new Process();
        process.StartInfo.FileName = "notepad.exe"; 

        process.Start();
        process.WaitForExit();

        Console.WriteLine($"child process exited with code: {process.ExitCode}");
    }
}