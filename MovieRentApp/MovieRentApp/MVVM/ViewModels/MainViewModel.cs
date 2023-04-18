using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieRentApp.Core;
using MovieRentApp.Repository;
using System;

namespace MovieRentApp.MVVM.ViewModels;

public class MainViewModel : ObservableObject
{
    public RelayCommand MovieViewCommand { get; set; }
    public RelayCommand ClientsViewCommand { get; set; }
    public MovieViewModel MovieViewModel { get; set; }
    public ClientViewModel ClientsViewModel { get; set; }

    private object? _currentView;

    public object? CurrentView
	{
		get { return _currentView; }
		set 
		{
            _currentView = value;
            OnPropertyChanged();
		}
	}

    public MainViewModel()
    {
        
    }

    public MainViewModel(IRepository repository)
    {
        MovieViewModel = new MovieViewModel(repository);
        ClientsViewModel = new ClientViewModel();
		CurrentView = MovieViewModel;

		MovieViewCommand = new RelayCommand(o =>
		{
			CurrentView = MovieViewModel;
		});

		ClientsViewCommand = new RelayCommand(o =>
		{
			CurrentView = ClientsViewModel;
		});
    }
}
