using CrudMvc.Data;
using CrudMvc.Models;

namespace CrudMvc.Services
{
    public class SellerService
    {
        private readonly CrudMvcContext _context;

        public SellerService(CrudMvcContext context)
        {

            _context = context;
        }
        public List<Seller> findAll()
        {
            return _context.Seller.ToList();
        }
        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();

        }
        public void Delete(Seller obj)
        {
            _context.Remove(obj);
            _context.SaveChanges();

        }
    }
}
