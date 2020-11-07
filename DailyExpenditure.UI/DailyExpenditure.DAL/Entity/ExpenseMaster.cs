using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyExpenditure.DAL.Entity
{
    public class ExpenseMaster
    {
        public int ExpenseId { get; set; }
        public int UserId { get; set; }
        public string NameOfExpense { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int ExpenseTypeId { get; set; }
        public int PaymentTypeId { get; set; }
        public string Note { get; set; }
        public DateTime CreatedDate { get; set; }
        public int IsActive { get; set; }

        public virtual UserDetail UserDetails { get; set; }
        public virtual ExpenseTypeMaster ExpenseTypeMasters { get; set; }
        public virtual PaymentTypeMaster PaymentTypeMasters { get; set; }
    }
}
