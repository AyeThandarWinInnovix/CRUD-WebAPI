namespace WebApi.Data
{
    public interface IDataAccess
    {
        Task<IEnumerable<T>> GetData<T, P>(string query, P parameters);

        Task SaveData<P>(string query, P parameters);
    }
}
