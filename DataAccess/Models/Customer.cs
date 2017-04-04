using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class Customer
    {
        
        public Customer()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
