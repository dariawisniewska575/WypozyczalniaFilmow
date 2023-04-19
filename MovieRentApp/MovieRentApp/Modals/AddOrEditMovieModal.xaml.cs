using MovieRentApp.Models;
using System;
using System.Windows;
using System.Windows.Input;

namespace MovieRentApp.Modals
{
    /// <summary>
    /// Interaction logic for AddOrEditMovieModal.xaml
    /// </summary>
    public partial class AddOrEditMovieModal : Window
    {
        public AddOrEditMovieModal(Movie movie = null)
        {
            InitializeComponent();
            cmbCategory.ItemsSource = Enum.GetValues(typeof(Category));
            if (IsEditOperation(movie))
                SetFormValues();
        }

        public Movie Movie { get; private set; }

        private bool IsEditOperation(Movie movie)
        {
            if (movie is not null)
            {
                Movie = movie;
                return true;
            }
            return false;
        }

        private void SetFormValues()
        {
            txtTitle.Text = Movie.Title;
            txtDescription.Text = Movie.Description;
            txtReleaseYear.Text = Movie.ReleaseYear.ToString();
            cmbCategory.SelectedIndex = (int)Movie.Category;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                if (Movie != null)
                {
                    Movie.Title = txtTitle.Text;
                    Movie.ReleaseYear = int.Parse(txtReleaseYear.Text);
                    Movie.Description = txtDescription.Text;
                    Movie.Category = (Category)cmbCategory.SelectedItem;
                }else
                {
                    Movie = new Movie
                    {
                        Title = txtTitle.Text,
                        ReleaseYear = int.Parse(txtReleaseYear.Text),
                        Description = txtDescription.Text,
                        Category = (Category)cmbCategory.SelectedItem,
                        IsAvaiable = true
                    };
                }

                DialogResult = true;
                Close();
            }
            else
            {
                var messageBox = new WarningMessageBox();
                messageBox.ShowDialog();
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
