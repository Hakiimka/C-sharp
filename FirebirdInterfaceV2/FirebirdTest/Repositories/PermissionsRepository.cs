using NHibernate;
using System.Collections.Generic;
using System.Windows;

namespace FirebirdTest.Repositories
{
    internal class PermissionsRepository
    {
        public void Insert(TUI_PERMISSIONS viewmodel)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(viewmodel);
                    transaction.Commit();
                }
            }
        }
        public List<TUI_PERMISSIONS> GetList()
        {
            using (ISession session = HibernateHelper.OpenSession())
            {
                List<TUI_PERMISSIONS> list = new List<TUI_PERMISSIONS>();
                var query = session.QueryOver<TUI_PERMISSIONS>();
                list = (List<TUI_PERMISSIONS>)query.List();
                return list;

            }
        }

        public List<TUI_PERMISSIONS> SelectToListByID(int UserID)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {

                List<TUI_PERMISSIONS> permissions = (List<TUI_PERMISSIONS>)
                  session.QueryOver<TUI_PERMISSIONS>()
                    .Where(c => c.User_Id == UserID)
                     .List();
                return permissions;
            }
        }

        public TUI_PERMISSIONS SelectViewModelByUserId(int id)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {
                var queryResult = session.QueryOver<TUI_PERMISSIONS>().Where(x => x.User_Id == id).SingleOrDefault();
                return queryResult ?? new TUI_PERMISSIONS();
            }
        }

        public void Update(TUI_PERMISSIONS viewmodel)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(viewmodel);
                    transaction.Commit();
                }
            }
        }

        public void Delete(TUI_PERMISSIONS viewmodel)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(viewmodel);
                        transaction.Commit();
                        MessageBox.Show("Успешно удалено");
                    }
                    catch { MessageBox.Show("Проблема с удалением\nвозможно вы не выбрали права"); }

                }
            }
        }
    }
}

