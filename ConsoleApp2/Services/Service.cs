using Autodesk.DataManagement.Client.Framework.Vault.Currency.Connections;
using ConsoleApp2.Services;

namespace ConsoleApp2.Results
{
    public class Service<T> : IBaseService<T>
    {
        public void Add(T entity, Connection connection)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity, Connection connection)
        {
            throw new NotImplementedException();
        }

        public T GetAll(long[] ids, Connection connection)
        {
            throw new NotImplementedException();
        }

        public T GetById(long id, Connection connection)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity, Connection connection)
        {
            throw new NotImplementedException();
        }
    }
}
