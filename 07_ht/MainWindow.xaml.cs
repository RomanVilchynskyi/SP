using Microsoft.Win32;
using System.IO;
using System.Windows.Forms;
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

namespace _07_ht;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
// MainWindow.xaml.cs

public partial class MainWindow : Window
{
    private List<Person> people = new List<Person>();

    public MainWindow()
    {
        InitializeComponent();
    }

    private Person ParsePerson(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        return new Person
        {
            FullName = lines[0].Split(':')[1],
            City = lines[1].Split(':')[1],
            ExperienceYears = int.Parse(lines[2].Split(':')[1].Replace("років", "")),
            Salary = int.Parse(lines[3].Split(':')[1])
        };
    }

    private void UpdateList()
    {
        list.ItemsSource = null;
        list.ItemsSource = people;
        combobox.ItemsSource = people.Select(p => p.City).Distinct().ToList();
    }

    private void LoadSingleResumeBtn(object sender, RoutedEventArgs e)
    {
        System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            var person = ParsePerson(dialog.FileName);
            people.Add(person);
            UpdateList();
        }
    }

    private void LoadMultipleResumes(object sender, RoutedEventArgs e)
    {
        System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog { Multiselect = true };
        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            foreach (var file in dialog.FileNames)
            {
                var person = ParsePerson(file);
                people.Add(person);
            }
            UpdateList();
        }
    }

    private void LoadFromFolder(object sender, RoutedEventArgs e)
    {
        var folderDialog = new FolderBrowserDialog(); 
        var result = folderDialog.ShowDialog();

        if (result == System.Windows.Forms.DialogResult.OK)
        {
            var files = Directory.GetFiles(folderDialog.SelectedPath, "*.txt");
            foreach (var file in files)
            {
                var person = ParsePerson(file);
                people.Add(person);
            }
            UpdateList();
        }
    }

    

    

    private void ShowMostExperienced(object sender, RoutedEventArgs e)
    {
        var result = people.OrderByDescending(p => p.ExperienceYears).FirstOrDefault();
        text.Text = result != null ? result.ToString() : "Немає даних.";
    }

    private void ShowLeastExperienced(object sender, RoutedEventArgs e)
    {
        var result = people.OrderBy(p => p.ExperienceYears).FirstOrDefault();
        text.Text = result != null ? result.ToString() : "Немає даних.";
    }

    private void ShowLowestSalary(object sender, RoutedEventArgs e)
    {
        var result = people.OrderBy(p => p.Salary).FirstOrDefault();
        text.Text = result != null ? result.ToString() : "Немає даних.";
    }

    private void ShowHighestSalary(object sender, RoutedEventArgs e)
    {
        var result = people.OrderByDescending(p => p.Salary).FirstOrDefault();
        text.Text = result != null ? result.ToString() : "Немає даних.";
    }

    private void ShowByCityBtn(object sender, RoutedEventArgs e)
    {
        string selectedCity = combobox.SelectedItem as string;
        if (!string.IsNullOrEmpty(selectedCity))
        {
            var results = people.Where(p => p.City == selectedCity);
            text.Text = string.Join("\n\n", results);
        }
    }
}

public class Person
{
    public string FullName { get; set; }
    public string City { get; set; }
    public int ExperienceYears { get; set; }
    public int Salary { get; set; }

    public override string ToString()
    {
        return $"Full name: {FullName}\n City: {City}\n Expirience years: {ExperienceYears} \n Salary: {Salary}";
    }
}