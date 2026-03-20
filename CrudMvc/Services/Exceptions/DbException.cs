namespace CrudMvc.Services.Exceptions
{
    public class DbException: ApplicationException
    {

        public DbException(string message) : base(message) { }
        
    }
}
