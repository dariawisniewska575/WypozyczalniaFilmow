using MovieRentApp.Models;
using MovieRentApp.MVVM.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace MovieRentApp.MVVM.Views
{
    /// <summary>
    /// Interaction logic for MovieView.xaml
    /// </summary>
    public partial class MovieView : UserControl
    {
        public MovieView()
        {
            InitializeComponent();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = (MovieViewModel)DataContext;
            if (MovieDataGrid.SelectedItem == null) return;

            var selectedRow = MovieDataGrid.SelectedItem as Movie;
            if (selectedRow is null) return;

            var primaryKey = selectedRow.Id;
            vm.RemoveMovie(primaryKey);
            vm.Movies.Remove(selectedRow);
        }
    }
}
