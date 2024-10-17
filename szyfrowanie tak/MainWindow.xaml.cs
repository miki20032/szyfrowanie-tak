using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using szyfrowanie_tak.szyfrowanie;
using Microsoft.Win32;

namespace szyfrowanie_tak
{
    public partial class MainWindow : Window
    {
        private HashContext _hashContext = new HashContext();
        private string openedFilePath = null; // Przechowywanie ścieżki otwartego pliku

        public MainWindow()
        {
            InitializeComponent();
        }

        private void HashButton_Click(object sender, RoutedEventArgs e)
        {
            string password = PasswordTextBox.Text;

            // Sprawdzenie, czy wprowadzono hasło
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Proszę wprowadzić hasło.");
                return;
            }

            if (AlgorithmComboBox.SelectedItem == null)
            {
                MessageBox.Show("Wybierz algorytm hashujący.");
                return;
            }

            string selectedAlgorithm = (AlgorithmComboBox.SelectedItem as ComboBoxItem).Content.ToString();
            switch (selectedAlgorithm)
            {
                case "MD5":
                    _hashContext.SetHashStrategy(new Szyfrowanie1());
                    break;
                case "SHA-1":
                    _hashContext.SetHashStrategy(new Szyfrowanie2());
                    break;
                case "SHA-256":
                    _hashContext.SetHashStrategy(new Szyfrowanie3());
                    break;
                case "BCrypt":
                    _hashContext.SetHashStrategy(new Szyfrowanie4());
                    break;
                default:
                    MessageBox.Show("Wybierz algorytm hashujący.");
                    return;
            }

            string hashedPassword = _hashContext.Hash(password);
            HashedPasswordTextBox.Text = hashedPassword;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string password = PasswordTextBox.Text;
            string hashedPassword = HashedPasswordTextBox.Text;
            string selectedAlgorithm = (AlgorithmComboBox.SelectedItem as ComboBoxItem).Content.ToString();

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "ZaszyfrowaneHasło.txt",
                DefaultExt = ".txt",
                Filter = "Text files (.txt)|*.txt"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                string fileContent = $"Hasło: {password}\nZaszyfrowane hasło przez {selectedAlgorithm}: {hashedPassword}";
                File.WriteAllText(filePath, fileContent);
                MessageBox.Show($"Hasło zostało zapisane do pliku {filePath}");
            }
        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                DefaultExt = ".txt",
                Filter = "Text files (.txt)|*.txt"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                openedFilePath = openFileDialog.FileName;
                string fileContent = File.ReadAllText(openedFilePath);
                FileInfo fileInfo = new FileInfo(openedFilePath);
                FileNameText.Text = $"Nazwa pliku: {fileInfo.Name}";
                FilePathText.Text = $"Ścieżka: {fileInfo.DirectoryName}";
                FileSizeText.Text = $"Rozmiar: {fileInfo.Length / 1024.0:F2} KB";
                MessageBox.Show($"Zawartość pliku: \n{fileContent}");
            }
        }

        private void AddPasswordToFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(openedFilePath))
            {
                MessageBox.Show("Najpierw otwórz plik, aby dodać hasło.");
                return;
            }

            string password = PasswordTextBox.Text;
            string hashedPassword = HashedPasswordTextBox.Text;
            string selectedAlgorithm = (AlgorithmComboBox.SelectedItem as ComboBoxItem).Content.ToString();

            string newContent = $"\n\nHasło: {password}\nZaszyfrowane hasło przez {selectedAlgorithm}: {hashedPassword}";
            File.AppendAllText(openedFilePath, newContent);
            MessageBox.Show("Nowe hasło zostało dodane do pliku.");
        }
    }
}
