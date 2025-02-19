using System;
using Autodesk.Connectivity.WebServices;
using Autodesk.DataManagement.Client.Framework.Vault.Currency.Connections;
using VDF = Autodesk.DataManagement.Client.Framework;

namespace ConsoleApp2.Services
{
    public class LogInDecoratorService<T> : IItemService<T>, IPropertyService<T>, IFileService<T>
    {
        private Connection _connection;
        private VDF.Vault.Results.LogInResult _result;
        private string _server;
        private string _vault;
        private string _user;
        private string _password;
        //TODO: Add AuthenticationFlags
        //TODO: Add ConnectionProperties
        //TODO: Add ConnectionOptions
        private readonly IItemService<T> _decoratedItem;
        private readonly IBaseService<T> _decorated;
        private readonly IPropertyService<T> _propertyService;
        private readonly IFileService<T> _fileService;

        public LogInDecoratorService(IBaseService<T> decorated)
        {
            _decorated = decorated;
            _decoratedItem = decorated as IItemService<T>;
            _propertyService = decorated as IPropertyService<T>;
            _fileService = decorated as IFileService<T>;
        }

        public void LogIn()
        {
            try
            {
                _result = VDF.Vault.Library.ConnectionManager.LogIn(
                        // "192.168.10.250", "WSA", "DTcenter", "1234"
                        "192.168.10.250", "DTcenter", "DTcenter", "1234"
                        //"192.168.10.250", "WSA", "joowon.suh@woosungautocon.com", "R-6qEbT#*nrJLZp"
                        , VDF.Vault.Currency.Connections.AuthenticationFlags.Standard, null);
                if (!_result.Success)
                {
                    foreach (var key in _result.ErrorMessages.Keys)
                    {
                        Console.WriteLine(_result.ErrorMessages[key]);
                    }
                    throw new Exception("Login Failed");
                }
                else
                {
                    Console.WriteLine("Login Success");
                }
                _connection = _result.Connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void LogOut()
        {
            VDF.Vault.Library.ConnectionManager.LogOut(_connection);
        }

        public void Add(T entity, Connection connection)
        {
            LogIn();
            _decorated.Add(entity, _connection);
            LogOut();
        }

        public void Delete(T entity, Connection connection)
        {
            LogIn();
            _decorated.Delete(entity, _connection);
            LogOut();
        }

        public void Update(T entity, Connection connection)
        {
            LogIn();
            _decorated.Update(entity, _connection);
            LogOut();
        }

        public T GetById(long id, Connection connection)
        {
            LogIn();
            var entity = _decorated.GetById(id, _connection);
            LogOut();
            return entity;
        }

        public T GetAll(long[] ids, Connection connection)
        {
            LogIn();
            var entities = _decorated.GetAll(ids, _connection);
            LogOut();
            return entities;
        }

        public T GetByName(string name, Connection connection)
        {
            LogIn();
            var entity = _decoratedItem.GetByName(name, _connection);
            LogOut();
            return entity;
        }

        public T GetBySchCond(SrchCond[] srchCond, SrchSort[] sortConditions, bool bRequestLatestOnly, ref string bookmark, out SrchStatus searchstatus, Connection connection)
        {
            LogIn();
            var entity = _decoratedItem.GetBySchCond(srchCond, sortConditions, bRequestLatestOnly, ref bookmark, out searchstatus, _connection);
            LogOut();
            return entity;
        }

        public T GetByDate(string date, VDF.Vault.Currency.Connections.Connection connection)
        {
            LogIn();
            var entity = _decoratedItem.GetByDate(date, _connection);
            LogOut();
            return entity;
        }

        public T UpdateItemName(long id, string name, Connection connection)
        {
            LogIn();
            var entity = _decoratedItem.UpdateItemName(id, name, _connection);
            LogOut();
            return entity;

        }

        public T GetPropertyValues(Connection connection)
        {
            LogIn();
            var entity = _propertyService.GetPropertyValues(_connection);
            LogOut();
            return entity;
        }

        public T GetPropertiesOfItem(Connection connection)
        {
            LogIn();
            var entity = _propertyService.GetPropertiesOfItem(_connection);
            LogOut();
            return entity;
        }

        public T CheckUserPermissions(Connection connection)
        {
            LogIn();
            var entity = _fileService.CheckUserPermissions(_connection);
            LogOut();
            return entity;
        }
    }
}
