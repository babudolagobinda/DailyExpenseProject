using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyExpenditure.DAL.Entity
{
    public class SalaryMaster
    {
        public int SalaryId { get; set; }
        public int UserId { get; set; }
        public decimal Salary { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
        public decimal ProvidentFund { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal ExtraDeduct { get; set; }
        public decimal DeductWithReturn { get; set; }
        public decimal SalaryInHand { get; set; }
        public DateTime CreatedDate { get; set; }
        public int IsActive { get; set; }
        public virtual UserDetail UserDetails { get; set; }

    }
}
