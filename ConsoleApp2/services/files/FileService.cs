using System;
using Autodesk.DataManagement.Client.Framework.Vault.Currency.Connections;
using ConsoleApp2.dtos;

namespace ConsoleApp2.Services
{
    public class FileService<T> : IFileService<T> where T : IFileDto
    {
        public void Add(T entity, Connection connection)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(T entity, Connection connection)
        {
            throw new System.NotImplementedException();
        }

        public void Update(T entity, Connection connection)
        {
            throw new System.NotImplementedException();
        }

        public T GetById(long id, Connection connection)
        {
            throw new System.NotImplementedException();
        }

        public T GetAll(long[] ids, Connection connection)
        {
            throw new System.NotImplementedException();
        }

        public T CheckUserPermissions( Connection connection )
        {
            var toRet = default(T);
            var availability = connection.WebServiceManager.DocumentService.CheckRolePermissions(new[]{"CheckOutFile"});
            Console.WriteLine(availability);
            return (T)toRet;
        }
    }
}