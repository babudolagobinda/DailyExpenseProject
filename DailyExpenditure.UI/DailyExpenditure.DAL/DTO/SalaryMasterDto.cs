using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyExpenditure.DAL.DTO
{
    public class SalaryMasterDto
    {
        public int SalaryId { get; set; }
        public decimal Salary { get; set; }
        public decimal ProvidentFund { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal ExtraDeduct { get; set; }
        public decimal DeductWithReturn { get; set; }
        public decimal SalaryInHand { get; set; }
        public DateTime CreatedDate { get; set; }
        public int IsActive { get; set; }
        public int UserId { get; set; }
    }
}
