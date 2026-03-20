namespace CrudMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email{ get; set; }
        public DateTime BirthDate { get; set; }

        public double BaseSalary { get; set; }

        public int DepartamentId { get; set; }
        public Departament? Departament { get; set; }//aparentemente é necessário pro eager loading
        public ICollection<SalesRecord>Sales { get; set; }=new List<SalesRecord>();

        public Seller()
        {


        }
        
        public void addSales(SalesRecord sr)
        {
            Sales.Add(sr);

        }
        public void removeSales(SalesRecord sr)
        {
            Sales.Remove(sr);

        }
       public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(x=>x.Date>=initial && x.Date<=final).Sum(sr=>sr.Amount) ;
        }
            

    }
}
