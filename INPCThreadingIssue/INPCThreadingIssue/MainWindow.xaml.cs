using System;
using System.Threading;
using System.Windows;

namespace INPCThreadingIssue
{
    public partial class MainWindow
    {
        Model model;

        public MainWindow()
        {
            InitializeComponent();
            model = new Model();
            DataContext = model;
        }


        void RunThreadedINPC(object sender, RoutedEventArgs e)
        {
			new Thread(() =>
			               {
			                   model.Property = "Hello";
			               }).Start();
        }

        void RunThreadedCode(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                ThreadedCodeTextBox.Text = "Hello";
            }).Start();
        }

        void RunThreadedMarshalledCode(object sender, RoutedEventArgs e)
        {
            new Thread(() => Dispatcher.BeginInvoke(new Action(() => MarshalledThreadedCodeTextBox.Text = "Hello"))).Start();
        }
    }
}
