using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyExpenditure.DAL.DTO;
using DailyExpenditure.DAL.Entity;
using DailyExpenditure.DAL.DataConnection;

namespace DailyExpenditure.DAL.Factory
{
    public class AdvanceSearchFactory
    {
        DataContext dataContext = new DataContext();
        public List<ExpenseMasterDto> GetYearWiseExpense(int year,int UserId)
        {
            List<ExpenseMasterDto> expenseMasterDtoList = this.dataContext.ExpenseMaster
                .Where(p => p.Date.Year == year && p.UserId== UserId)
                .GroupBy(p => new { p.Date.Year, p.Date.Month })
               .Select(p => new ExpenseMasterDto()
               {
                   Date = p.Select(n => n.Date).FirstOrDefault(),
                   Amount = p.Sum(d => d.Amount),
               }).ToList();
            return expenseMasterDtoList;
        }
        public List<ExpenseMasterDto> GetYearAndMonthWiseExpense(int year, int month,int UserId)
        {
            List<ExpenseMasterDto> expenseMasterDtoList = this.dataContext.ExpenseMaster
                .Where(p => p.Date.Year == year && p.Date.Month == month && p.UserId== UserId)
                .GroupBy(p => p.Date)
               .Select(p => new ExpenseMasterDto()
               {
                   Date = p.Key,
                   Amount = p.Sum(d => d.Amount),
               }).ToList();
            return expenseMasterDtoList;
        }
    }
}
