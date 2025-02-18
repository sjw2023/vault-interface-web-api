using Autodesk.DataManagement.Client.Framework.Vault.Currency.Connections;
using ConsoleApp2.Model;

namespace ConsoleApp2.Services
{
    public interface IPropertyService<T> : IBaseService<T>
    {
        T GetPropertyValues(Connection connection);
        T GetPropertiesOfItem(Connection connection);
        T CheckUserPermission(Connection connection);
    }
}