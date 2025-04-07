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
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace _06_ht;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private List<Author> authors = new List<Author>();

    public MainWindow()
    {
        InitializeComponent();
        InitializeDatabase();
        LoadAuthorsAsync();
    }

    private void InitializeDatabase()
    {
        using (var context = new LibraryDb())
        {
            context.Database.EnsureDeleted(); 
            context.Database.EnsureCreated();
        }
    }
    private async void LoadAuthorsAsync()
    {
        authors = await Task.Run(() =>
        {
            using var context = new LibraryDb();
            return context.Authors.ToList();
        });

        comboBoxAuthors.ItemsSource = authors;
    }

    private async void SearchBtn(object sender, RoutedEventArgs e)
    {
        var selectedAuthor = comboBoxAuthors.SelectedItem as Author;
        string searchText = textBoxSearch.Text;

        if (selectedAuthor == null)
        {
            MessageBox.Show("Select Author");
            return;
        }

        List<Book> books = await Task.Run(() =>
        {
            using var context = new LibraryDb();

            var query = context.Books.Where(b => b.AuthorId == selectedAuthor.Id);

            if (!string.IsNullOrWhiteSpace(searchText) && searchText.Length >= 3)
            {
                query = query.Where(b => b.Title.Contains(searchText));
            }

            return query.ToList(); 
        });

        listBoxBooks.Items.Clear();
        foreach (var book in books)
        {
            listBoxBooks.Items.Add(book);
        }
    }
}