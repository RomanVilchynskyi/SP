using System.Diagnostics;
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
using System.Windows.Threading;

namespace _01_TaskManager;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    DispatcherTimer _timer = null;
    public MainWindow()
    {
        InitializeComponent();
        _timer = new DispatcherTimer();
        _timer.Interval = new TimeSpan(0, 0, 20);
        _timer.Tick += _timer_Tick;
        _timer.Start();
    }

    private void _timer_Tick(object? sender, EventArgs e)
    {
        Refresh(sender, null);
    }
    private void Refresh(object sender, RoutedEventArgs e)
    {
        if (grid != null)
        {
            grid.ItemsSource = Process.GetProcesses();
        }
    }

    private void RadioButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is RadioButton rb && _timer != null)
        {
            string text = rb.Content.ToString();
            int interval = int.Parse(text);
            _timer.Interval = TimeSpan.FromSeconds(interval);
        }
    }

    private void Kill(object sender, RoutedEventArgs e)
    {
        if (grid.SelectedItem is Process process)
        {
            try
            {
                process.Kill();
                MessageBox.Show($"Process {process.ProcessName} finished");
                Refresh(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

    }

    private void ShowDetail(object sender, RoutedEventArgs e)
    {
        if (grid.SelectedItem is Process process)
        {
            try
            {
                string details = $"Name: {process.ProcessName}\nID: {process.Id}\nProcessor time: {process.TotalProcessorTime}\n Priority: {process.PriorityClass}";
                MessageBox.Show(details, "Process details");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }

    private void Go(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(ProcessNameBox.Text))
        {
            try
            {
                Process.Start(ProcessNameBox.Text);
                MessageBox.Show($"Process {ProcessNameBox.Text} started");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Start error: {ex.Message}");
            }
        }
    }
}