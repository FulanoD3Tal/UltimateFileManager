using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UltimateFileManagerCore;

namespace Demos
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void bt_selected_file_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tb_file_selected.Text = dialog.FileName;
            }
        }

        private void bt_renamed_Click(object sender, RoutedEventArgs e)
        {
                
            tb_output.Text = $"Rename a file:\ninput {tb_file_selected.Text}\noutput {tb_file_selected.Text.RenameFile(tb_new_name.Text)}";
        }

        private void bt_directory_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tb_new_directory.Text = dialog.SelectedPath;
            }
        }

        private void bt_change_directory_Click(object sender, RoutedEventArgs e)
        {
            tb_output.Text = $"Change directory of file:\ninput {tb_file_selected.Text}\noutput {UltimateFile.ChangeDirectory(tb_new_directory.Text,tb_file_selected.Text)}";
        }
    }
}
