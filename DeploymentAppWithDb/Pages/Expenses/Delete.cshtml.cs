using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DeploymentAppWithDb.Data;
using DeploymentAppWithDb.Models;

namespace DeploymentAppWithDb.Pages.Expenses
{
    public class DeleteModel : PageModel
    {
        private readonly DeploymentAppWithDb.Data.ProjectContext _context;

        public DeleteModel(DeploymentAppWithDb.Data.ProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Expense Expense { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Expenses == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses.FirstOrDefaultAsync(m => m.ID == id);

            if (expense == null)
            {
                return NotFound();
            }
            else 
            {
                Expense = expense;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Expenses == null)
            {
                return NotFound();
            }
            var expense = await _context.Expenses.FindAsync(id);

            if (expense != null)
            {
                Expense = expense;
                _context.Expenses.Remove(Expense);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
