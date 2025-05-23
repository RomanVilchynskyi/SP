﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _02_Tread;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    Thread th = null;
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        th = new Thread(Hardwork);
        th.Start();
        //Hardwork();
    }
    private void Hardwork()
    {
        bool flag = false;
        Application.Current.Dispatcher.Invoke(() =>
        {
            if (progress.Value > 0)
            {
                progress.Value = progress.Minimum;
            }
            flag = progress.Value < progress.Minimum;

        });

        while (flag)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                progress.Value++;
                flag = progress.Value < progress.Maximum;
            });
                Thread.Sleep(100);
        }
    }
}