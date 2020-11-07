using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyExpenditure.DAL.Entity
{
    public class UserDetail
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string PhoneNo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserImage { get; set; }
        public DateTime CreatedDate { get; set; }
        public int IsActive { get; set; }
        public virtual ICollection<ExpenseMaster> ExpenseMasters { get; set; }
        public virtual ICollection<ExpenseTypeMaster> ExpenseTypeMasters { get; set; }
        public virtual ICollection<PaymentTypeMaster> PaymentTypeMasters { get; set; }
        public virtual ICollection<SalaryMaster> SalaryMasters { get; set; }
    }
}
