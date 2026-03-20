using CrudMvc.Data;
using CrudMvc.Models;
using Microsoft.EntityFrameworkCore;



namespace CrudMvc.Services
{
    public class DepartamentService
    {

        private readonly CrudMvcContext _context;

        public DepartamentService(CrudMvcContext context)
        {

            _context = context;
        }
        public async Task<List<Departament>> findAllAsync()
        {
            return await _context.Departament.OrderBy(x => x.Name).ToListAsync();
        }
       
    }
}


