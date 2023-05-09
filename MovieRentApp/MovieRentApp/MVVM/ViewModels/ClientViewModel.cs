using MovieRentApp.Core;
using MovieRentApp.MVVM.Models;
using MovieRentApp.Repository;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MovieRentApp.MVVM.ViewModels;

public class ClientViewModel : ObservableObject
{
    private readonly IRepository _repository;

    private ObservableCollection<Client> _clients;
    public ObservableCollection<Client> Clients
    {
        get { return _clients; }
        set 
        { 
            _clients = value;
            OnPropertyChanged();
        }
    }

    private bool _isEditOrDeleteButtonEnabled;
    public bool IsEditOrDeleteButtonEnabled
    {
        get { return _isEditOrDeleteButtonEnabled; }
        set
        {
            _isEditOrDeleteButtonEnabled = value;
            OnPropertyChanged(nameof(IsEditOrDeleteButtonEnabled));
        }
    }

    private Client _selectedItem;
    public Client SelectedItem
    {
        get { return _selectedItem; }
        set
        {
            _selectedItem = value;
            IsEditOrDeleteButtonEnabled = _selectedItem != null;
        }
    }

    public ClientViewModel()
    {
        
    }

    public ClientViewModel(IRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        LoadClients();
    }

    public void RemoveClient(Client client)
        => Task.Run(() => _repository.DeleteEntityAsync(client));

    public Client AddClient(Client newClient)
        => Task.Run(() => _repository.AddEntityAsync(newClient)).Result;

    public void EditClient(Client clientToEdit)
        => Task.Run(() => _repository.EditEntityAsync(clientToEdit));

    public async Task LoadClientsAsync()
    {
        var clients = await _repository.GetEntitiesAsync<Client>();
        Clients = new ObservableCollection<Client>(clients);
    }

    private void LoadClients()
    {
        var clients = _repository.GetEntities<Client>();
        Clients = new ObservableCollection<Client>(clients);
    }
}
