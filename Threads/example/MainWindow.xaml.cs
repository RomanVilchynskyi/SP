using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace example
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread thread = null;
        public MainWindow()
        {
            InitializeComponent();
        }

     
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //HardWork();//freez
            thread = new Thread(HardWork);
            thread.Start();

            thread2 = new Thread(ShowNumber);
            thread2.Start();
        }

        void ShowNumber()
        {

            for (int i = 0; i < 50; i++)
            {
                //Console.WriteLine(i);
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    list.Items.Add(i);
                }));
            }
        }

        private void HardWork()
        {
            bool isContinue = false;

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                if (progress.Value > 0)
                    progress.Value = progress.Minimum;
                isContinue = progress.Value < progress.Maximum;
            }));

            while (isContinue)
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    progress.Value++;
                    isContinue = progress.Value < progress.Maximum;
                }));
                Thread.Sleep(110);
                Tuple<int, int> tuple = new Tuple<int, int>(20, 30);
                
            }
        }
    }
}
