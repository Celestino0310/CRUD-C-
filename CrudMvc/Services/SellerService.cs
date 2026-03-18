using CrudMvc.Data;
using CrudMvc.Models;

namespace CrudMvc.Services
{
    public class SellerService
    {
        private readonly CrudMvcContext _context;

        public SellerService(CrudMvcContext context) { 
        
            _context = context;
        }
        public List<Seller> findAll() {
            return _context.Seller.ToList();
        }

    }
}
