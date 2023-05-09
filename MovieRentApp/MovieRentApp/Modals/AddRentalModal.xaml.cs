using MovieRentApp.MVVM.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MovieRentApp.Modals
{
    /// <summary>
    /// Interaction logic for AddRentalModal.xaml
    /// </summary>
    public partial class AddRentalModal : Window
    {
        private ObservableCollection<Client> _clients;
        private ObservableCollection<Movie> _movies;
        public AddRentalModal(ObservableCollection<Client> clients, ObservableCollection<Movie> movies)
        {
            InitializeComponent();
            cmbMovie.ItemsSource = movies.Select(x => x.Title);
            var firstNames = clients.Select(x => x.FirstName).ToList();
            var lastNames = clients.Select(x => x.LastName).ToList();

            _clients = clients;
            _movies = movies;

            cmbUser.ItemsSource = firstNames.Join(
                lastNames,
                firstName => firstNames.IndexOf(firstName),
                lastName => lastNames.IndexOf(lastName),
                (firstName, lastName) => firstName + " " + lastName);
        }

        public Rental Rental { get; private set; }
        public Movie Movie { get; private set; }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            if (ValidateForm())
            {
                var clientName = cmbUser.SelectedItem.ToString()!.Split(" ");
                var client = _clients.FirstOrDefault(x => x.FirstName == clientName[0] && x.LastName == clientName[1]);
                var movie = _movies.FirstOrDefault(x => x.Title == cmbMovie.SelectedItem.ToString());
                var rental = new Rental
                {
                    ClientId = client!.Id,
                    MovieId = movie!.Id
                };

                Rental = (Rental)rental.Clone();
                movie.IsAvaiable = false;
                Movie = movie;
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
            return cmbMovie.SelectedItem != null &&
                cmbUser.SelectedItem != null;
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
