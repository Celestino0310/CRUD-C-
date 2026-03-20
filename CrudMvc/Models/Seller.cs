using System.ComponentModel.DataAnnotations;

namespace CrudMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required (ErrorMessage ="{0} Required")]
        [StringLength(50,MinimumLength =3, ErrorMessage ="o nome: {0} precisa ter pelo menos 3 Caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} Required")]
        [DataType(DataType.EmailAddress)]//que legal isso deixa o email clicavel com 
        public string Email{ get; set; }

        [Required(ErrorMessage = "{0} Required")]
        [Display(Name ="Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]//formata a data para esse estilo
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} Required")]
        [Range(100.0,30000.0,ErrorMessage ="{0} Most Be Between{1} and {2}")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        public double BaseSalary { get; set; }

        [Required]
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
