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
    public class ExpenseMasterFactory
    {
        DataContext dataContext = new DataContext();
        public int AddExpenseType(string ExpenseType,int UserId)
        {
            ExpenseTypeMaster expenseTypeMaster = new ExpenseTypeMaster();
            expenseTypeMaster.ExpenseType = ExpenseType;
            expenseTypeMaster.UserId = UserId;
            dataContext.ExpenseTypeMaster.Add(expenseTypeMaster);
            int i = dataContext.SaveChanges();
            return i;
        }
        public int AddPaymentType(string PaymentType, int UserId)
        {
            PaymentTypeMaster paymentTypeMaster = new PaymentTypeMaster();
            paymentTypeMaster.PaymentType = PaymentType;
            paymentTypeMaster.UserId = UserId;
            dataContext.PaymentTypeMaster.Add(paymentTypeMaster);
            int i = dataContext.SaveChanges();
            return i;
        }
        public List<ExpenseTypeMasterDto> GetAllExpenseType(int UserId)
        {
            List<ExpenseTypeMasterDto> expenseTypeMasterDtoList = this.dataContext.ExpenseTypeMaster  
                .Where(p=>p.UserId== UserId)
                .Select(p => new ExpenseTypeMasterDto()
                {
                    ExpenseTypeId = p.ExpenseTypeId,
                    ExpenseType = p.ExpenseType
                }).ToList();
            return expenseTypeMasterDtoList;
        }
        public List<PaymentTypeMasterDto> GetAllPaymentType(int UserId)
        {
            List<PaymentTypeMasterDto> paymentTypeMasterDtoList = this.dataContext.PaymentTypeMaster
                .Where(p => p.UserId == UserId)
                .Select(p => new PaymentTypeMasterDto()
                {
                    PaymentTypeId = p.PaymentTypeId,
                    PaymentType = p.PaymentType
                }).ToList();
            return paymentTypeMasterDtoList;
        }
        public int SaveExpenseMaster(ExpenseMasterDto expenseMasterDto)
        {
            ExpenseMaster expenseMaster = new ExpenseMaster();
            if (expenseMasterDto.ExpenseId > 0)
            {
                ExpenseMaster expenseId = dataContext.ExpenseMaster.Where(e => e.ExpenseId == expenseMasterDto.ExpenseId).SingleOrDefault();
                expenseId.UserId = expenseMasterDto.UserId;
                expenseId.NameOfExpense = expenseMasterDto.NameOfExpense;
                expenseId.Amount = expenseMasterDto.Amount;
                expenseId.Date = expenseMasterDto.Date;
                expenseId.ExpenseTypeId = expenseMasterDto.ExpenseTypeId;
                expenseId.PaymentTypeId = expenseMasterDto.PaymentTypeId;
                expenseId.Note = expenseMasterDto.Note;
                expenseId.CreatedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                expenseId.IsActive = 1;
                dataContext.SaveChanges();
                return expenseMasterDto.ExpenseId;
            }
            else
            {
                expenseMaster.UserId = expenseMasterDto.UserId;
                expenseMaster.NameOfExpense = expenseMasterDto.NameOfExpense;
                expenseMaster.Amount = expenseMasterDto.Amount;
                expenseMaster.Date = expenseMasterDto.Date;
                expenseMaster.ExpenseTypeId = expenseMasterDto.ExpenseTypeId;
                expenseMaster.PaymentTypeId = expenseMasterDto.PaymentTypeId;
                expenseMaster.Note = expenseMasterDto.Note;
                expenseMaster.CreatedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                expenseMaster.IsActive = 1;
                dataContext.ExpenseMaster.Add(expenseMaster);
                int i = dataContext.SaveChanges();
                return i;
            }
        }
        public List<ExpenseMasterDto> GetAllExpenseMaster(int UserId)
        {
            var query = dataContext.ExpenseMaster
                .Join(dataContext.ExpenseTypeMaster, r => r.ExpenseTypeId, p => p.ExpenseTypeId, (r, p) => new { r, p })
                .Join(dataContext.PaymentTypeMaster, q => q.r.PaymentTypeId, s => s.PaymentTypeId, (q, s) => new { q, s })
               .Where(x => x.q.r.IsActive == 1 && x.q.r.UserId== UserId)
                .Select(m => new ExpenseMasterDto
                {
                    ExpenseId = m.q.r.ExpenseId,
                    UserId = m.q.r.UserId,
                    NameOfExpense = m.q.r.NameOfExpense,
                    Amount = m.q.r.Amount,
                    Date = m.q.r.Date,
                    ExpenseTypeId = m.q.r.ExpenseTypeId,
                    ExpenseType = m.q.p.ExpenseType,
                    PaymentTypeId = m.q.r.PaymentTypeId,
                    PaymentType = m.s.PaymentType,
                    Note = m.q.r.Note,
                    CreatedDate = m.q.r.CreatedDate,
                    IsActive = m.q.r.IsActive
                });
            List<ExpenseMasterDto> expenseMasterList = query.ToList();
            return expenseMasterList;
        }
        public ExpenseMasterDto EditExpense(int expenseId)
        {
            var query = dataContext.ExpenseMaster
                .Join(dataContext.ExpenseTypeMaster, r => r.ExpenseTypeId, p => p.ExpenseTypeId, (r, p) => new { r, p })
                .Join(dataContext.PaymentTypeMaster, q => q.r.PaymentTypeId, s => s.PaymentTypeId, (q, s) => new { q, s })
               .Where(x => x.q.r.IsActive == 1 && x.q.r.ExpenseId == expenseId)
                .Select(m => new ExpenseMasterDto
                {
                    ExpenseId = m.q.r.ExpenseId,
                    UserId = m.q.r.UserId,
                    NameOfExpense = m.q.r.NameOfExpense,
                    Amount = m.q.r.Amount,
                    Date = m.q.r.Date,
                    ExpenseTypeId = m.q.r.ExpenseTypeId,
                    ExpenseType = m.q.p.ExpenseType,
                    PaymentTypeId = m.q.r.PaymentTypeId,
                    PaymentType = m.s.PaymentType,
                    Note = m.q.r.Note,
                    CreatedDate = m.q.r.CreatedDate,
                    IsActive = m.q.r.IsActive
                });
            ExpenseMasterDto expenseMaster = query.FirstOrDefault();
            return expenseMaster;
        }
        public List<ExpenseMasterDto> GetAllExpenseToShowAsChart(int UserId)
        {
            var query = dataContext.ExpenseMaster
                .Where(q => q.Date.Month == DateTime.Now.Month && q.IsActive == 1 && q.UserId== UserId)
                .GroupBy(p => p.Date).Select(x => new ExpenseMasterDto
            { Date = x.Key, Amount = x.Sum(y => y.Amount) });
            List<ExpenseMasterDto> expenseMasterList = query.ToList();
            return expenseMasterList;
        }
        public int DeleteExpense(int expenseId,int UserId)
        {
            ExpenseMaster expenseMaster = dataContext.ExpenseMaster
                .Where(p => p.ExpenseId == expenseId && p.UserId== UserId).FirstOrDefault();
            expenseMaster.IsActive = 0;
            int i = dataContext.SaveChanges();
            return i;
        }
    }
}
