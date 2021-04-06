using FirebirdTest.Interfaces;
using FirebirdTest.Model;
using Prism.Commands;
using System;
using System.ComponentModel;

namespace FirebirdTest.ViewModel
{
    internal class EditViewModel : INotifyPropertyChanged, ICloseWindows
    {
        public event PropertyChangedEventHandler PropertyChanged;

        
        string login = UsersViewModel.user_temp.Login;
        string middle = UsersViewModel.user_temp.Middle_Name;
        string first = UsersViewModel.user_temp.First_Name;
        string last = UsersViewModel.user_temp.Last_Name; 

        private USERS user;
        private UserRepository repository;
        public Action Close { get; set; }
        private DelegateCommand _closeCommand;
        private DelegateCommand CloseCommand =>
            _closeCommand ?? (_closeCommand = new DelegateCommand(CloseWindow));


        private void CloseWindow()
        {
            Close?.Invoke();
        }
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public string Login
        {
            get { return login; }
            set { login = value; OnPropertyChanged("Login"); }
        }
        public string Middle
        {
            get { return middle; }
            set { middle = value; OnPropertyChanged("Middle"); }
        }
        public string First
        {
            get { return first; }
            set { first = value; OnPropertyChanged("First"); }
        }

        public string Last
        {
            get { return last; }
            set { last = value; OnPropertyChanged("Last"); }
        }
        private void Update()
        {
            repository = new UserRepository();
            user = new USERS
            {
                Id = UsersViewModel.user_temp.Id,
                Login = login,
                First_Name = first,
                Middle_Name = middle,
                Last_Name = last
            };
            repository.Update(user);
            //repository.Insert(user);
            Close();
        }

        public VMcommand CancelButton
        {
            get { return new VMcommand((o) => { Close(); }); }
        }
        public VMcommand EditButton
        {
            get { return new VMcommand((o) => { Update(); }); }
        }

    }
}
