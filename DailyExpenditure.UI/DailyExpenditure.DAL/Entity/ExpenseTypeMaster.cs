using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyExpenditure.DAL.Entity
{
    public class ExpenseTypeMaster
    {
        public int ExpenseTypeId { get; set; }
        public int UserId { get; set; }
        public string ExpenseType { get; set; }
        public virtual ICollection<ExpenseMaster> ExpenseMasters { get; set; }
        public virtual UserDetail UserDetails { get; set; }
    }
}
