using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DeploymentAppWithDb.Data;
using DeploymentAppWithDb.Models;
using Microsoft.EntityFrameworkCore;

namespace DeploymentAppWithDb.Pages.Expenses
{
    public class CreateModel : PageModel
    {
        private readonly DeploymentAppWithDb.Data.ProjectContext _context;

        public CreateModel(DeploymentAppWithDb.Data.ProjectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["ExpenseTypeID"] = new SelectList(_context.ExpenseTypes, "ID", "Name");
            return Page();
        }

        [BindProperty]
        public Expense Expense { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var expenseType = await _context.ExpenseTypes.FirstOrDefaultAsync(e => e.ID == Expense.ExpenseTypeID);
            if (expenseType == null)
            {
                return Page();
            }

            ModelState.ClearValidationState(nameof(Expense));
            Expense.ExpenseType = expenseType;
            bool isValid = TryValidateModel(Expense, nameof(Expense));

            if (isValid)
            {
                return Page();
            }

            _context.Expenses.Add(Expense);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
