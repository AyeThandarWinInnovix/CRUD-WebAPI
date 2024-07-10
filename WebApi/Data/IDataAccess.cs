namespace WebApi.Data
{
    public interface IDataAccess
    {
        Task<IEnumerable<T>> GetData<T, P>(string query, P parameters);
        Task<IEnumerable<T>> GetData<T,P>(string spName, P parameters, string connection);
        Task SaveData<P>(string query, P parameters);
    }
}
