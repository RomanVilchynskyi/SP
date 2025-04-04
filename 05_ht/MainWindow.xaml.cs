using System.Numerics;
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

namespace _05_ht;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async Task<long> CalculateFactorialAsync(int num)
    {
        await Task.Delay(1000); 
        return CalculateFactorial(num);
    }

    private long CalculateFactorial(int num)
    {
        long result = 1;
        for (int i = 2; i <= num; i++)
            result *= i;
        return result;
    }
    private async void Calculate(object sender, RoutedEventArgs e)
    {
        if (int.TryParse(ibox.Text, out int num) && num >= 0)
        {
            if (num > 20)
            {
                MessageBox.Show("Number is too big for long");
                return;
            }

            list.Items.Add($"Calculations {num}! ...");

            long result = await CalculateFactorialAsync(num);

            list.Items.Add($"{num}! = {result}");
        }
    }
}