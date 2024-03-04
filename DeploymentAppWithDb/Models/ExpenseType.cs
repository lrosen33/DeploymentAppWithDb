using System.ComponentModel.DataAnnotations;

namespace DeploymentAppWithDb.Models
{
    public class ExpenseType
    {
        public int ID { get; set; }

        [Display(Name = "Expense Type")]
        public string Name { get; set; }

    }
}
