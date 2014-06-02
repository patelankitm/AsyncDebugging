using System;
using System.Collections.Generic;
using System.Linq;
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
using System.IO;

namespace AsyncDebugging
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    
        //Asynchronous method that passes some variables to
        //the other async method that will write the file
        //You wait for the async operation to be completed by using
        //the await operator. This method cannot be awaited itself
        //because it returns void.
        private async void WriteFile()
        {
            string filePath = @"C:\temp\testFile.txt";
            string text = "Visual Studio 2013 Succinctly\r\n";
    
            await WriteTextAsync(filePath, text);
        }
    
        
        //Asynchronous method that writes some text into a file
        //Marked with "async"
        private async Task WriteTextAsync(string filePath, string text)
        {
            byte[] encodedText = Encoding.Unicode.GetBytes(text);
    
            using (FileStream sourceStream = new FileStream(filePath,
                FileMode.Append, FileAccess.Write, FileShare.None,
                bufferSize: 4096, useAsync: true))
            {
                //new APIs since .NET 4.5 offer async methods to read
                //and write files
                //you use "await" to wait for the async operation to be
                //completed and to get the result
                await sourceStream.WriteAsync(encodedText, 0,
                      encodedText.Length);
            };
        }
    
        private void FileButton_Click(object sender, RoutedEventArgs e)
        {
            //Place a breakpoint here...
            WriteFile();
        }
    }

}
