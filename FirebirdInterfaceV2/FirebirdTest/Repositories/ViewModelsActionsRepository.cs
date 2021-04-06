using NHibernate;
using System.Collections.Generic;
using System.Windows;

namespace FirebirdTest.Repositories
{
    internal class ViewModelsActionsRepository
    {
        public void Insert(TUI_VIEW_MODELS_ACTIONS viewmodel)
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
        public List<TUI_VIEW_MODELS_ACTIONS> GetList()
        {
            using (ISession session = HibernateHelper.OpenSession())
            {
                List<TUI_VIEW_MODELS_ACTIONS> list = new List<TUI_VIEW_MODELS_ACTIONS>();
                var query = session.QueryOver<TUI_VIEW_MODELS_ACTIONS>();
                list = (List<TUI_VIEW_MODELS_ACTIONS>)query.List();
                return list;
            }
        }

        public List<TUI_VIEW_MODELS_ACTIONS> SelectToListByID(int ViewModelAction)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {

                List<TUI_VIEW_MODELS_ACTIONS> permissions = (List<TUI_VIEW_MODELS_ACTIONS>)
  session.QueryOver<TUI_VIEW_MODELS_ACTIONS>()
      .Where(c => c.View_Model == ViewModelAction)
      .List();
                return permissions;
            }
        }

        public TUI_VIEW_MODELS_ACTIONS SelectViewModelByNote(string name)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {
                var queryResult = session.QueryOver<TUI_VIEW_MODELS_ACTIONS>().Where(x => x.Note == name).SingleOrDefault();
                return queryResult ?? new TUI_VIEW_MODELS_ACTIONS();
            }
        }

        public void Update(TUI_VIEW_MODELS_ACTIONS viewmodel)
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

        public void Delete(TUI_VIEW_MODELS_ACTIONS viewmodel)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(viewmodel);
                        transaction.Commit();
                    }
                    catch { MessageBox.Show("Delete Problem"); }

                }
            }
        }
    }
}
