using FirebirdTest.Interfaces;
using FirebirdTest.Model;
using Prism.Commands;
using System;
using System.ComponentModel;


namespace FirebirdTest.ViewModel
{
    internal class AddViewModel : INotifyPropertyChanged, ICloseWindows
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string login;
        private string middle;
        private string first;
        private string last;

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
        private void Add()
        {
            repository = new UserRepository();
            user = new USERS
            {
                Login = login,
                First_Name = first,
                Middle_Name = middle,
                Last_Name = last
            };
            repository.Insert(user);

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

        public VMcommand AddButton
        {
            get { return new VMcommand((o) => { Add(); Close(); }); }
        }

        public VMcommand CancelButton
        {
            get { return new VMcommand((o) => { Close(); }); }
        }



    }
}
