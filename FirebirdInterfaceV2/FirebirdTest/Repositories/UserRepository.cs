using NHibernate;
using System.Collections.Generic;
using System.Windows;

namespace FirebirdTest
{
    internal class UserRepository
    {
        public void Insert(USERS user)
        {
            try
            {
                using (ISession session = HibernateHelper.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(user);
                        transaction.Commit();
                    }
                }
            } catch { MessageBox.Show("Проблема с добавлением нового пользователя\nвозможно поля пустые"); }

        }
        public List<USERS> GetList()
        {
            using (ISession session = HibernateHelper.OpenSession())
            {
                List<USERS> list = new List<USERS>();
                var query = session.QueryOver<USERS>();
                list = (List<USERS>)query.List();
                return list;
            }
        }

        public USERS SelectUserByName(string name)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {
                var queryResult = session.QueryOver<USERS>().Where(x => x.First_Name == name).SingleOrDefault();
                return queryResult ?? new USERS();
            }
        }

        public void Update(USERS user)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(user);
                    transaction.Commit();
                }
            }
        }

        public void Delete(USERS user)
        {
            try
            {
                using (ISession session = HibernateHelper.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Delete(user);
                        transaction.Commit();
                       
                    }
                }
            }
            catch { MessageBox.Show("Проблема с удалением\n возможно вы не выбрали пользователя"); }
        }

        public List<USERS> SelectToListByID(int UserID)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {

                List<USERS> permissions = (List< USERS>)
                  session.QueryOver<USERS>()
                    .Where(c => c.Id == UserID)
                     .List();
                return permissions;
            }
        }
        public List<USERS> SelectToListByLogin(string Login)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {

                List<USERS> permissions = (List<USERS>)
                  session.QueryOver<USERS>()
                    .Where(c => c.Login == Login)
                     .List();
                return permissions;
            }
        }
        public List<USERS> SelectToListByFirstName(string FirstName)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {

                List<USERS> permissions = (List<USERS>)
                  session.QueryOver<USERS>()
                    .Where(c => c.First_Name == FirstName)
                     .List();
                return permissions;
            }
        }
        public List<USERS> SelectToListByMiddleName(string MiddleName)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {

                List<USERS> permissions = (List<USERS>)
                  session.QueryOver<USERS>()
                    .Where(c => c.Middle_Name == MiddleName)
                     .List();
                return permissions;
            }
        }

        public List<USERS> SelectToListByLastName(string LastName)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {

                List<USERS> permissions = (List<USERS>)
                  session.QueryOver<USERS>()
                    .Where(c => c.Last_Name == LastName)
                     .List();
                return permissions;
            }
        }

    }
}
