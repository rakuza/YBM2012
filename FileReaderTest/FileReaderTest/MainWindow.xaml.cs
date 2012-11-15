using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.IO;
using Microsoft.Win32;

namespace FileReaderTest
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

        FileStream fs;

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            fs = File.Open(new Uri(ofd.FileName).LocalPath, FileMode.Open);
            int length, offset;
            length = int.Parse(tbxlength.Text);
            offset = int.Parse(tbxoffset.Text);
            byte[] buffer = new byte[offset];
            fs.Read(buffer,0,offset);

            TextRange tr = new TextRange(tbxPrevious.Document.ContentStart, tbxPrevious.Document.ContentEnd);
            using(MemoryStream ms = new MemoryStream(buffer))
            {
                tr.Load(ms, DataFormats.Rtf);
            }

            buffer = new byte[length];
            fs.Read(buffer, 0, length);
            tr = new TextRange(tbxTarget.Document.ContentStart, tbxTarget.Document.ContentEnd);
            using (MemoryStream ms = new MemoryStream(buffer))
            {
                tr.Load(ms, DataFormats.Rtf);
            }

            buffer = new byte[fs.Length - fs.Position];
            fs.Read(buffer, 0, buffer.Length);
            tr = new TextRange(tbxNext.Document.ContentStart, tbxNext.Document.ContentEnd);
            using (MemoryStream ms = new MemoryStream(buffer))
            {
                tr.Load(ms, DataFormats.Rtf);
            }

            fs.Close();
            

        }

    }
}
