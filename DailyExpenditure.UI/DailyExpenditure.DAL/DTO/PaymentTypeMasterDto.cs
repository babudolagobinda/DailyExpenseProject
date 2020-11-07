using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyExpenditure.DAL.DTO
{
    public class PaymentTypeMasterDto
    {
        public int PaymentTypeId { get; set; }
        public string PaymentType { get; set; }
        public int UserId { get; set; }
    }
}
