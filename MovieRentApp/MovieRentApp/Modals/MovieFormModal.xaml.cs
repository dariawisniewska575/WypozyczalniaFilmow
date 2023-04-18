using MovieRentApp.Models;
using System;
using System.Windows;
using System.Windows.Input;

namespace MovieRentApp.Modals
{
    /// <summary>
    /// Interaction logic for MovieFormModal.xaml
    /// </summary>
    public partial class MovieFormModal : Window
    {
        public Movie? NewMovie { get; private set; }

        public MovieFormModal()
        {
            InitializeComponent();
            cmbCategory.ItemsSource = Enum.GetValues(typeof(Category));
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                NewMovie = new Movie
                {
                    Title = txtTitle.Text,
                    ReleaseYear = int.Parse(txtReleaseYear.Text),
                    Description = txtDescription.Text,
                    Category = (Category)cmbCategory.SelectedItem,
                    IsAvaiable = true
                };
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Wypełnij wszystkie pola!");
            }
        }

        private bool ValidateForm()
        {
            return !string.IsNullOrEmpty(txtTitle.Text)
                && !string.IsNullOrEmpty(txtReleaseYear.Text)
                && !string.IsNullOrEmpty(txtDescription.Text)
                && cmbCategory.SelectedItem != null;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out int _);
        }

        private void BorderMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
