using System.ComponentModel.DataAnnotations;

namespace CrudMvc.Models
{
    public class Departament
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List <Seller>Seller { get; set; }=new List<Seller>();




        public void addSeller(Seller seller)
        {
            Seller.Add(seller);
            
        }
        public double TotalSales(DateTime initial,DateTime final)
        {

            return Seller.Sum(Seller=>Seller.TotalSales(initial,final)) ;
        }
    }
}
