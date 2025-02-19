using Autodesk.DataManagement.Client.Framework.Vault.Currency.Connections;

namespace ConsoleApp2.Services
{
    public interface IFileService<T>: IBaseService<T>
    {
        T CheckUserPermissions( Connection connection );
        // T ChekOutFile( Connection connection );
    }
}