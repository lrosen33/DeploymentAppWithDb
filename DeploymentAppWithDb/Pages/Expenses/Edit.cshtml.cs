using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeploymentAppWithDb.Data;
using DeploymentAppWithDb.Models;

namespace DeploymentAppWithDb.Pages.Expenses
{
    public class EditModel : PageModel
    {
        private readonly DeploymentAppWithDb.Data.ProjectContext _context;

        public EditModel(DeploymentAppWithDb.Data.ProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Expense Expense { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Expenses == null)
            {
                return NotFound();
            }

            var expense =  await _context.Expenses.Include(e => e.ExpenseType).FirstOrDefaultAsync(m => m.ID == id);

            if (expense == null)
            {
                return NotFound();
            }
            bool modelState=ModelState.IsValid;
            Expense = expense;

           ViewData["ExpenseTypeID"] = new SelectList(_context.ExpenseTypes, "ID", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            var expenseType = await _context.ExpenseTypes.FirstOrDefaultAsync(e => e.ID == Expense.ExpenseTypeID);
            if (expenseType==null)
            {
                return Page();
            }

            ModelState.ClearValidationState(nameof(Expense));
            Expense.ExpenseType = expenseType;
            bool isValid = TryValidateModel(Expense, nameof(Expense));

            if (!isValid)
            {
                return Page();
            }

            _context.Entry(Expense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(Expense.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ExpenseExists(int id)
        {
          return _context.Expenses.Any(e => e.ID == id);
        }
    }
}
