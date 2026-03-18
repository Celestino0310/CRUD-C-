using CrudMvc.Models;

namespace CrudMvc.Data
{
    public class SeedingService
    {
        private CrudMvcContext _context;

        public SeedingService(CrudMvcContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Departament.Any())
            {
                return; // Já tem dados, não faz nada
            }
            else { 
                Departament d1 = new Departament { Name = "Tecnologia" };
                Departament d2 = new Departament { Name = "Recursos Humanos" };
                Departament d3 = new Departament { Name = "Financeiro" };
                Departament d4 = new Departament { Name = "Marketing" };

                _context.Departament.AddRange(d1, d2, d3, d4);
                _context.SaveChanges();
            }
            if (_context.Seller.Any()) {
                return;
            }
            else
            {
                
               /* Seller s1 = new Seller { Id = 1,Name="Gabriel", Email="gabrielcelestino@gmail.com", BirthDate=2003/10/20,De };
                _context.SaveChanges();*/
            }
        }
    }
}