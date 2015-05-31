using BubbleTea.DataAccess;
using BubbleTea.Models;
using ClientManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClientManager.ViewModel
{
    public class CustomerEditorViewModel : ViewModelBase
    {
        private const string ADD = "Add";
        private const string EDIT = "Edit";
        private IDataRepository service = null;
        private ObservableCollection<User> _users = null;

        public CustomerEditorViewModel(IDataRepository _service)
        {
            service = _service;
            GetAllUsers();
            SelectedUser = new User();
            AddEdit = ADD;
        }

        private void GetAllUsers()
        {
            Users = new ObservableCollection<User>(service.GetAllUsers());
        }

        private User _selectedUser;
        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                if (_selectedUser != null && _selectedUser.Id > 0)
                {
                    AddEdit = EDIT;
                }
                else
                {
                    AddEdit = ADD;
                }
                IsDisabled = true;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<User> Users
        {
            get
            { return _users; }
            set { _users = value; OnPropertyChanged(); }
        }

        private string _addEdit;
        public string AddEdit { get { return _addEdit; } set { _addEdit = value; OnPropertyChanged(); } }

        private bool _isDisabled;
        public bool IsDisabled { get { return _isDisabled; }
            set { _isDisabled = value; OnPropertyChanged(); }
        }

        public ICommand EditCommand
        {
            get
            {
                return new RelayCommand((s) => { IsDisabled = false; } );
            }
        }

        public ICommand SaveCommand { get { return new RelayCommand((s) => Save(), (p) => { return !IsDisabled; }); } }

        private void Save()
        {
            if (SelectedUser != null && SelectedUser.Id > 0)
            {
                service.UpdateUser(SelectedUser);
            }
            else
            {
                service.AddUser(SelectedUser);
            }
            GetAllUsers();
            //SelectedUser = new User();
        }

        public ICommand CancelCommand
        {
            get
            {
                return new RelayCommand((s) =>
                {
                    SelectedUser = null;
                });
            }
        }
    }
}
