using FirebirdTest.Model;
using FirebirdTest.Repositories;
using FirebirdTest.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace FirebirdTest
{
    public class UsersViewModel : INotifyPropertyChanged
    {
        #region
        public event PropertyChangedEventHandler PropertyChanged;
        public static USERS user_temp;
        private bool ButtonFlag = true;

        private UserRepository UserRep = new UserRepository();
        public ObservableCollection<USERS> collection { get; set; }
        private USERS user;
        public USERS user_search { get; set; }
        private ObservableCollection<USERS> collection_search;

        private PermissionsRepository permissionsRepository = new PermissionsRepository();
        private ObservableCollection<TUI_PERMISSIONS> PermCollection;
        private TUI_PERMISSIONS permission;

        private bool LoginRB, FirstNameRB, MiddleNameRB, LastNameRB;
        private string text;
        private int count;
        private bool IdRB = true;
        #endregion  // perem

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private void DataRefresh()
        {
            collection.Clear(); UserRep.GetList().ForEach(x => collection.Add(x));
        }
        private int Delete()
        {
            if (User == null)
            {
                MessageBox.Show("Проблема с удалением\nвозможно вы не выбрали пользователя");
                return 0;
            }
            else
            {
                UserRep.Delete(User);
                collection.Remove(User);
                return 0;
            }
        }
        private void AddUser()
        {
            AddWindow addwindow = new AddWindow();
            addwindow.ShowDialog();
            refresh();
        }

        private int Edit()
        {
            if (User == null)
            {
                MessageBox.Show("Проблема с изменением\nвозможно вы не выбрали пользователя");
                return 0;
            }
            else
            {
                EditWindow editwindow = new EditWindow();
                editwindow.ShowDialog();
                DataRefresh();
                return 0;
            }
        }

        private int AddPermission()
        {
            if (User == null)
            {
                MessageBox.Show("Проблема с изменением\nвозможно вы не выбрали пользователя");
                return 0;
            }
            else
            {
                AddPermissionWindow permissionwindow = new AddPermissionWindow();
                permissionwindow.ShowDialog();
                DataRefresh();
                return 0;
            }

        }
        private void DeletePermission()
        {
            permissionsRepository.Delete(permission);
        }
        private void refresh()
        {

            UserRep.GetList().ForEach(x => collection.Add(x));
            PermCollection = new ObservableCollection<TUI_PERMISSIONS>(permissionsRepository.GetList());

        }

        private int DoSearch()
        {
            if (collection_search == null)
                collection_search = new ObservableCollection<USERS>();


            collection_search.Clear();
            if (IdRB == true)
            {
                UserRep.SelectToListByID(Convert.ToInt32(text)).ForEach(x => collection_search.Add(x));
            }
            else if (LoginRB == true)
            {
                UserRep.SelectToListByLogin(text).ForEach(x => collection_search.Add(x));
            }
            else if (FirstNameRB == true)
            {
                UserRep.SelectToListByFirstName(text).ForEach(x => collection_search.Add(x));
            }
            else if (MiddleNameRB == true)
            {
                UserRep.SelectToListByMiddleName(text).ForEach(x => collection_search.Add(x));
            }
            else if (LastNameRB == true)
            {
                UserRep.SelectToListByLastName(text).ForEach(x => collection_search.Add(x));
            }
            if (collection_search.Count == 0)
                return 0;


            user_search = collection_search[0];
            MessageBox.Show(user_search.First_Name + user_search.Middle_Name + user_search.Last_Name);
            // user = user_search;
            // user_search = user;
            count = collection_search.Count;
            return count;
        }
        public bool IsButtonEnabled
        {
            get { return ButtonFlag; }
            set { ButtonFlag = value; OnPropertyChanged("IsButtonEnabled"); }
        }
        public USERS User
        {
            get { user_temp = user; if (user_search != null) return user_search; else return user; }
            set { user_temp = user; if (user_search != null) user_search = value; else user = value; OnPropertyChanged("User"); }
        }


        public ObservableCollection<USERS> UsersCollection
        {
            get { if (collection == null) collection = new ObservableCollection<USERS>(); collection.Clear(); UserRep.GetList().ForEach(x => collection.Add(x)); return collection; }
            set { collection = value; OnPropertyChanged("UsersCollection"); }
        }


        public VMcommand DeleteButton
        {
            get { return new VMcommand((o) => { Delete(); DataRefresh(); }); }
        }
        public VMcommand AddButton
        {
            get { return new VMcommand((o) => { AddUser(); DataRefresh(); }); }
        }
        public VMcommand EditButton
        {
            get { return new VMcommand((o) => { Edit(); DataRefresh(); }); }
        }
        public VMcommand EditPermissionButton
        {
            get { return new VMcommand((o) => { AddPermission(); DataRefresh(); }); }
        }
        public VMcommand RefreshButton
        {
            get { return new VMcommand((o) => { collection.Clear(); UserRep.GetList().ForEach(x => collection.Add(x)); }); }

        }

        public USERS UserSearch
        {
            get { return user_search; }
            set
            {
                user_search = value; OnPropertyChanged("UserSearch");
            }
        }

        public string Text
        {
            get { return text; }
            set { text = value; OnPropertyChanged("Text"); }
        }

        public bool IDRB
        {
            get { return IdRB; }
            set { IdRB = value; OnPropertyChanged("IDRB"); }
        }

        public bool LOGINRB
        {
            get { return LoginRB; }
            set { LoginRB = value; OnPropertyChanged("LOGINRB"); }
        }
        public bool FIRSTNAMERB
        {
            get { return FirstNameRB; }
            set { FirstNameRB = value; OnPropertyChanged("FirstNameRB"); }
        }
        public bool MIDDLENAMERB
        {
            get { return MiddleNameRB; }
            set { MiddleNameRB = value; OnPropertyChanged("MIDDLENAMERB"); }
        }
        public bool LASTNAMERB
        {
            get { return LastNameRB; }
            set { LastNameRB = value; OnPropertyChanged("LASTNAMERB"); }
        }

        public VMcommand SearchButton
        {
            get { return new VMcommand((o) => { DoSearch(); MessageBox.Show($"Найдено {count} пользователей"); }); }

        }




    }
}
