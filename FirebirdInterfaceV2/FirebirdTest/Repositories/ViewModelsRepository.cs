using NHibernate;
using System.Collections.Generic;

namespace FirebirdTest.Repositories
{
    internal class ViewModelsRepository
    {
        public void Insert(TUI_VIEW_MODELS viewmodel)
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
        public List<TUI_VIEW_MODELS> GetList()
        {
            using (ISession session = HibernateHelper.OpenSession())
            {
                List<TUI_VIEW_MODELS> list = new List<TUI_VIEW_MODELS>();
                var query = session.QueryOver<TUI_VIEW_MODELS>();
                list = (List<TUI_VIEW_MODELS>)query.List();
                return list;
            }
        }

        public TUI_VIEW_MODELS SelectViewModelByName(string name)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {
                var queryResult = session.QueryOver<TUI_VIEW_MODELS>().Where(x => x.Name == name).SingleOrDefault();
                return queryResult ?? new TUI_VIEW_MODELS();
            }
        }

        public void Update(TUI_VIEW_MODELS viewmodel)
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

        public void Delete(TUI_VIEW_MODELS viewmodel)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(viewmodel);
                    transaction.Commit();
                }
            }
        }
    }
}
