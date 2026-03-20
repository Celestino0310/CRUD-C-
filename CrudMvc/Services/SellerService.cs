using CrudMvc.Data;
using CrudMvc.Models;
using CrudMvc.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

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
        public Seller findByID(int id) {
            return _context.Seller.Include(b=>b.Departament).FirstOrDefault(X => X.Id == id);
          }

        public void Remove(int Id){//REMOVE OBJETO VENDEDOR
            var obj=_context.Seller.Find(Id);
            _context.Remove(obj);
            _context.SaveChanges();

        }

        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();

        }
        
        public void Update(Seller obj)
        {
            if (!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("id not found | ID não encontrado");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch(DbException e) { throw new DbException(e.Message); }
           

        }
        
    }
}
