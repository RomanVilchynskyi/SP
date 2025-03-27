using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _02_task;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    static int[] numbers = new int[10000];
    static int min, max;
    static double average;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void GenerateNumbers()
    {
        Random rand = new Random();
        for (int i = 0; i < numbers.Length; i++)
            numbers[i] = rand.Next(1, 10001);
    }

    private void FindMin()
    {
        min = numbers.Min();
    }

    private void FindMax()
    {
        max = numbers.Max();
    }

    private void CalculateAverage()
    {
        average = numbers.Average();
    }

    private void StartCalculation(object sender, RoutedEventArgs e)
    {
        GenerateNumbers();

        Thread min_ = new Thread(FindMin);
        Thread max_ = new Thread(FindMax);
        Thread avg_ = new Thread(CalculateAverage);

        min_.Start();
        max_.Start();
        avg_.Start();

        min_.Join();
        max_.Join();
        avg_.Join();

        Application.Current.Dispatcher.Invoke(() =>
        {
            MessageBox.Show($"Min: {min}\nMax: {max}\nAverage: {average}");
        });
    }
}