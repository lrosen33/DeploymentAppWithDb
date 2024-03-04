using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DeploymentAppWithDb.Models
{
    public class Expense
    {
        public int ID { get; set; }
        public string Description { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime DateIncurred { get; set; }

        public string Location { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public double Price { get; set; }

        // Expense Type ID (foreign key)
        [Display(Name = "Expense Type")]
        public int ExpenseTypeID { get; set; }

        // Expense Type
        public ExpenseType ExpenseType { get; set; }

        // User ID
        public int UserID { get; set; }

    }
}
