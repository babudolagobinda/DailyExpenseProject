using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyExpenditure.DAL.DTO
{
    public class ExpenseTypeMasterDto
    {
        public int ExpenseTypeId { get; set; }
        public string ExpenseType { get; set; }
        public int UserId { get; set; }
    }
}
