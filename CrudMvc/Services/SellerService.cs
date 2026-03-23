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
        public async Task<List<Seller>> findAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task<Seller> findByIDAsync(int id) {
            return await _context.Seller.Include(b=>b.Departament).FirstOrDefaultAsync(X => X.Id == id);
          }

        public async Task RemoveAsync(int Id){//REMOVE OBJETO VENDEDOR de forma assyncrona agr
            try
            {
                var obj = _context.Seller.Find(Id);
                _context.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) {
                throw new IntegrityException(ex.Message);
            }
        }

        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();

        }
        
        public async Task UpdateAsync(Seller obj)
        { bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("id not found | ID não encontrado");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbException e) { throw new DbException(e.Message); }
           

        }
        
    }
}
