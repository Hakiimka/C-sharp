using FirebirdTest.Interfaces;
using FirebirdTest.Model;
using FirebirdTest.Repositories;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace FirebirdTest.ViewModel
{
    internal class PermissionAddViewModel : INotifyPropertyChanged, ICloseWindows
    {
        #region
        public Action Close { get; set; }
        private DelegateCommand _closeCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        private UserRepository UserRep = new UserRepository();
        private ViewModelsActionsRepository ActionRep = new ViewModelsActionsRepository();

        private ObservableCollection<USERS> usersCollection;
        private USERS user;

        private TUI_PERMISSIONS permission;
        private PermissionsRepository PermissionRep;
        private ObservableCollection<TUI_PERMISSIONS> AddedPermissions;


        private ObservableCollection<TUI_VIEW_MODELS_ACTIONS> actionsCollection;
        private TUI_VIEW_MODELS_ACTIONS action;
        private DateTime dateexpire = DateTime.Today;
        private DelegateCommand CloseCommand =>
            _closeCommand ?? (_closeCommand = new DelegateCommand(CloseWindow));
        #endregion
        private void CloseWindow()
        {
            Close?.Invoke();
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private ObservableCollection<TUI_PERMISSIONS> GetAddedPermissions()
        {
            PermissionsRepository permissions = new PermissionsRepository();
            ObservableCollection<TUI_PERMISSIONS> permissions_list = new ObservableCollection<TUI_PERMISSIONS>(permissions.SelectToListByID(UsersViewModel.user_temp.Id));
            return permissions_list;
        }

        private int GivePermission()
        {
            try
            {
                if (UsersViewModel.user_temp == null || VMAction == null)
                {
                    MessageBox.Show("Проблема с добавлением\nвозможно какое-то из полей пустое");
                    return 0;
                }
                PermissionRep = new PermissionsRepository();
                permission = new TUI_PERMISSIONS
                {
                    User_Id = UsersViewModel.user_temp.Id,
                    View_Model_Action = VMAction.View_Model,
                    Date_Expire = dateexpire
                };
                PermissionRep.Insert(Permission);
                AddedPermissions.Add(Permission);
                MessageBox.Show(ActionsCollection[0].ToString());
                
                ActionsCollection.RemoveAt(0);
                return 0;
            }
            catch { MessageBox.Show("Проблема с добавлением\nвозможно какое-то из полей пустое"); return 0; }
        }



        public DateTime DateExpire
        {
            get { return dateexpire; }
            set { dateexpire = value; OnPropertyChanged("DateExpire"); }
        }

        public USERS StaticUser
        {
            get { return UsersViewModel.user_temp; }
        }
        public USERS User
        {
            get { return user; }
            set { user = value; OnPropertyChanged("User"); }
        }
        public TUI_PERMISSIONS Permission
        {
            get { return permission; }
            set { permission = value; OnPropertyChanged("Permission"); }
        }
        public TUI_VIEW_MODELS_ACTIONS VMAction
        {
            get { return action; }
            set { action = value; OnPropertyChanged("VMAction"); }
        }
        public ObservableCollection<USERS> UsersCollection
        {
            get { return new ObservableCollection<USERS>(UserRep.GetList()); }
            set { usersCollection = value; OnPropertyChanged("UsersCollection"); }
        }
        public ObservableCollection<TUI_VIEW_MODELS_ACTIONS> ActionsCollection
        {
            get { return new ObservableCollection<TUI_VIEW_MODELS_ACTIONS>(ActionRep.GetList()); }
            set { actionsCollection = value; OnPropertyChanged("ActionsCollection"); }
        }

        public ObservableCollection<TUI_PERMISSIONS> AlreadyAddedPermissions
        {
            get { if(AddedPermissions==null) AddedPermissions = GetAddedPermissions(); return AddedPermissions; }
            set { AddedPermissions = value; OnPropertyChanged("AlreadyAddedPermissions"); }
        }

        public VMcommand CancelButton
        {
            get { return new VMcommand((o) => { Close(); }); }
        }
        public VMcommand GiveButton
        {
            get { return new VMcommand((o) => { GivePermission(); if (User != null) MessageBox.Show("Права выданы"); Close(); }); }
        }
        public VMcommand DeleteButton
        {
            get
            {
                return new VMcommand((o) =>
                {
                    if (UsersViewModel.user_temp == null || Permission == null)
                    {
                        MessageBox.Show("Проблема с удалением\nвозможно какое-то из полей пустое");
                    }
                    else
                        AddedPermissions.Remove(Permission);
                        
                });
            }
        }

        public VMcommand AddButton
        {
            get { return new VMcommand((o) => { GivePermission(); }); }
        }
    }
}
