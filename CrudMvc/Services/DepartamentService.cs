using CrudMvc.Data;
using CrudMvc.Models;



namespace CrudMvc.Services
{
    public class DepartamentService
    {

        private readonly CrudMvcContext _context;

        public DepartamentService(CrudMvcContext context)
        {

            _context = context;
        }
        public List<Departament> findAll()
        {
            return _context.Departament.OrderBy(x => x.Name).ToList();
        }
       
    }
}


