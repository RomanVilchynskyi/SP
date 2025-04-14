using System.IO;
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

namespace exam;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private List<string> searchResults = new List<string>();
    public MainWindow()
    {
        InitializeComponent();
    }

    private int CountWordInFile(string filePath, string word)
    {
        int wordCount = 0;
        try
        {
            string fileContent = File.ReadAllText(filePath);
            wordCount = fileContent.Split(new[] { word }, StringSplitOptions.None).Length - 1;
        }
        catch (Exception)
        {
            
        }
        return wordCount;
    }
    private async void SearchBtn(object sender, RoutedEventArgs e)
    {
        listbox.Items.Clear();
        progressbar.Value = 0;

        string directoryPath = DirectoryPathTextBox.Text;
        string searchWord = SearchWordTextBox.Text;

        if (string.IsNullOrEmpty(directoryPath) || string.IsNullOrEmpty(searchWord))
        {
            MessageBox.Show("Enter the right path way");
            return;
        }

        try
        {
            var allFiles = Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories);
            int totalFiles = allFiles.Length;
            int processedFiles = 0;

            List<Task> searchTasks = new List<Task>();
            searchResults.Clear();
            foreach (var file in allFiles)
            {
                searchTasks.Add(Task.Run(() =>
                {
                    int wordCount = CountWordInFile(file, searchWord);
                    Dispatcher.Invoke(() =>
                    {
                        listbox.Items.Add(new
                        {
                            FileName = System.IO.Path.GetFileName(file),
                            FilePath = file,
                            WordCount = wordCount
                        });

                        FileNameTB.Text = System.IO.Path.GetFileName(file);
                        FilePathTB.Text = file;
                        WordCountTB.Text = wordCount.ToString();

                        string saveLine = $"File name: {System.IO.Path.GetFileName(file)} Path: {file} | Number of occurences --> {wordCount}";
                        searchResults.Add(saveLine);

                    });

                    processedFiles++;
                    Dispatcher.Invoke(() => progressbar.Value = (processedFiles * 100.0) / totalFiles);
                }));
            }

            await Task.WhenAll(searchTasks);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}");
        }
    }

    private void SaveBtn(object sender, RoutedEventArgs e)
    {
        if (searchResults.Count == 0)
        {
            MessageBox.Show("Not found");
            return;
        }

        Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
        saveFileDialog.Filter = "Text files (*.txt)|*.txt";
        saveFileDialog.Title = "Save results";
        saveFileDialog.FileName = "Search results.txt";

        if (saveFileDialog.ShowDialog() == true)
        {
            File.WriteAllLines(saveFileDialog.FileName, searchResults);
            MessageBox.Show("Results saved successfully");
        }
    }

}