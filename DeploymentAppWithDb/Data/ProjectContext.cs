using Microsoft.EntityFrameworkCore;
using DeploymentAppWithDb.Models;

namespace DeploymentAppWithDb.Data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options)
            : base(options)
        {
        }

        public DbSet<DeploymentAppWithDb.Models.Expense> Expenses { get; set; }
        public DbSet<DeploymentAppWithDb.Models.ExpenseType> ExpenseTypes { get; set; }

    }

    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ProjectContext(
                serviceProvider.GetRequiredService<DbContextOptions<ProjectContext>>()))
            {
                if (context == null || context.ExpenseTypes == null)
                {
                    throw new ArgumentNullException("Null ProjectContext");
                }

                if (!context.ExpenseTypes.Any())
                {
                    context.ExpenseTypes.AddRange(
                         new ExpenseType
                         {
                             Name = "Advertising"
                         },
                         new ExpenseType
                         {
                             Name = "Gas"
                         },
                         new ExpenseType
                         {
                             Name = "Repairs & Maintenance"
                         },
                         new ExpenseType
                         {
                             Name = "Meals & Entertainment"
                         },
                         new ExpenseType
                         {
                             Name = "Supplies"
                         },
                         new ExpenseType
                         {
                             Name = "Office Expenses"
                         },
                         new ExpenseType
                         {
                             Name = "Rent"
                         },
                         new ExpenseType
                         {
                             Name = "Travel"
                         },
                         new ExpenseType
                         {
                             Name = "Telephone & Utilities"
                         },
                         new ExpenseType
                         {
                             Name = "Licenses, Dues, Memberships"
                         },
                         new ExpenseType
                         {
                             Name = "Salaries, Wages and Benefits"
                         },
                         new ExpenseType
                         {
                             Name = "Misc"
                         }
                     );

                    context.SaveChanges();

                    // Create sample expense data

                    // get date information to add expenses to last month
                    var lastMonthTemp = DateTime.Now.AddMonths(-1);
                    int numDaysLastMonth = DateTime.DaysInMonth(lastMonthTemp.Year, lastMonthTemp.Month);
                    var lastMonthLastDay = new DateTime(lastMonthTemp.Year, lastMonthTemp.Month, numDaysLastMonth);
                    numDaysLastMonth *= -1; // make the number negative to use in creating random dates

                    int userId = 123;
                    Random randomInt = new Random();

                    var mealType = context.ExpenseTypes
                            .Where(t => t.Name == "Meals & Entertainment").FirstOrDefault();

                    context.Expenses.AddRange(
                        new Expense
                        {
                            Description = "Lunch",
                            Location = "Fried Chicken Hut",
                            Price = 12.55,
                            ExpenseTypeID = mealType.ID,
                            UserID = userId,
                            DateIncurred = lastMonthLastDay.AddDays(randomInt.Next(numDaysLastMonth - 2, -1))
                        },
                        new Expense
                        {
                            Description = "Dinner",
                            Location = "Seafood Palace",
                            Price = 20.75,
                            ExpenseTypeID = mealType.ID,
                            UserID = userId,
                            DateIncurred = lastMonthLastDay.AddDays(randomInt.Next(numDaysLastMonth - 2, -1))
                        },
                        new Expense
                        {
                            Description = "Dinner",
                            Location = "Veggie Delight",
                            Price = 18.22,
                            ExpenseTypeID = mealType.ID,
                            UserID = userId,
                            DateIncurred = lastMonthLastDay.AddDays(randomInt.Next(numDaysLastMonth - 2, -1))
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
