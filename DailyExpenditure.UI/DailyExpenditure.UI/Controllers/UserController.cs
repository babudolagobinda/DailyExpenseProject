using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DailyExpenditure.DAL.DTO;
using DailyExpenditure.DAL.Factory;
using DailyExpenditure.DAL.Entity;
using DailyExpenditure.DAL.ViewModel;
using System.IO;
using System.Web.Security;
using Newtonsoft.Json;

namespace DailyExpenditure.UI.Controllers
{
    public class UserController : Controller
    {
        UserDetailsFactory userDetailsFactory = new UserDetailsFactory();
        ExpenseMasterFactory expenseMasterFactory = new ExpenseMasterFactory();
        AdvanceSearchFactory advanceSearchFactory = new AdvanceSearchFactory();
        UserMessage userMessage = new UserMessage();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserDetailsDto userDetailsDto)
        {
            if (userDetailsDto.UserName != null && userDetailsDto.Password != null)
            {
                UserDetailsDto UserDetails = userDetailsFactory.UserSignIn(userDetailsDto);
                if (UserDetails != null)
                {
                    Session["UserId"] = UserDetails.UserId;
                    return RedirectToAction("DashBoard");
                }
                else
                {
                    ViewBag.ErrorMsg = userMessage.invalidUserMsg;
                }
            }
            else
            {
                ViewBag.ErrorMsg = userMessage.nullErrorMsg;
            }
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserDetailsDto userDetailsDto, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var chkUsername = userDetailsFactory.CheckUserName(userDetailsDto.UserName);
                if (chkUsername == null || chkUsername == "")
                {
                    if (file != null)
                    {
                        var allowedExtensions = new[] { ".JPG", ".jpg", ".png", ".PNG", ".JPEG", ".jpeg" };
                        var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                        var ext = Path.GetExtension(file.FileName);
                        if (allowedExtensions.Contains(ext.ToLower()))
                        {
                            var path = Path.Combine(Server.MapPath("~/UploadFiles"), fileName);
                            file.SaveAs(path);
                            userDetailsDto.UserImage = fileName;
                            int i = userDetailsFactory.UserSignUp(userDetailsDto);
                            ViewBag.SuccessMsg = userMessage.successMsg;
                            ModelState.Clear();
                        }
                        else
                        {
                            ViewBag.imgMessage = userMessage.imgMessage;
                        }
                    }
                    else
                    {
                        ViewBag.imgMessage = userMessage.nonImgMessage;
                    }
                }
                else
                {
                    ViewBag.userCheckMessage = userMessage.userCheckMessage;
                }
            }
            return View();
        }
        public ActionResult DashBoard()
        {
            int UserId = Convert.ToInt32(Session["UserId"]);
            if (UserId > 0)
            {
                UserInformationViewModel userInformationViewModel = new UserInformationViewModel();
                ViewBag.UserDetails = userDetailsFactory.GetUserDetails(UserId);
                ViewBag.expenseList = expenseMasterFactory.GetAllExpenseToShowAsChart(UserId);
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult UserSignOut()
        {
            HttpContext.Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
        public ActionResult ExpenseMaster()
        {
            int UserId = Convert.ToInt32(Session["UserId"]);
            if (UserId > 0)
            {
                ViewBag.UserDetails = userDetailsFactory.GetUserDetails(UserId);
                ViewBag.expenseTypeMasterDtoList = expenseMasterFactory.GetAllExpenseType(UserId);
                ViewBag.expenseMasterDtoList = expenseMasterFactory.GetAllExpenseMaster(UserId);
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult AddExpenseType(string ExpenseType)
        {
            int UserId = Convert.ToInt32(Session["UserId"]);
            int i = expenseMasterFactory.AddExpenseType(ExpenseType, UserId);
            return Json(i, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddPaymentType(string PaymentType)
        {
            int UserId = Convert.ToInt32(Session["UserId"]);
            int i = expenseMasterFactory.AddPaymentType(PaymentType, UserId);
            return Json(i, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetAllExpenseType()
        {
            int UserId = Convert.ToInt32(Session["UserId"]);
            List<ExpenseTypeMasterDto> expenseTypeMasterDtoList = expenseMasterFactory.GetAllExpenseType(UserId);
            return Json(expenseTypeMasterDtoList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetAllPaymentType()
        {
            int UserId = Convert.ToInt32(Session["UserId"]);
            List<PaymentTypeMasterDto> paymentTypeMasterDtoList = expenseMasterFactory.GetAllPaymentType(UserId);
            return Json(paymentTypeMasterDtoList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveExpenseMaster(ExpenseMasterDto expenseMasterDto)
        {
            expenseMasterDto.UserId = Convert.ToInt32(Session["UserId"]);
            int i = expenseMasterFactory.SaveExpenseMaster(expenseMasterDto);
            return Json(i, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EditExpense(int expenseId)
        {
            ExpenseMasterDto expenseMasterDto = expenseMasterFactory.EditExpense(expenseId);
            return Json(expenseMasterDto, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteExpense(int expenseId)
        {
            int UserId = Convert.ToInt32(Session["UserId"]);
            int i = expenseMasterFactory.DeleteExpense(expenseId, UserId);
            return Json(i, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SalaryMaster()
        {
            int UserId = Convert.ToInt32(Session["UserId"]);
            if (UserId > 0)
            {
                ViewBag.UserDetails = userDetailsFactory.GetUserDetails(UserId);
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult CheckUserEmailId(string EmailId)
        {
            int UserId = Convert.ToInt32(Session["UserId"]);
            int i = userDetailsFactory.CheckUserEmailId(UserId, EmailId);
            return Json(i, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AdvanceSearch()
        {
            int UserId = Convert.ToInt32(Session["UserId"]);
            if (UserId > 0)
            {
                ViewBag.UserDetails = userDetailsFactory.GetUserDetails(UserId);
                ViewBag.expenseList = expenseMasterFactory.GetAllExpenseToShowAsChart(UserId);
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult GetYearWiseExpense(int year)
        {
            int UserId = Convert.ToInt32(Session["UserId"]);
            List<ExpenseMasterDto> yearOfExpenseMasterDtoList = advanceSearchFactory.GetYearWiseExpense(year, UserId);
            return Json(yearOfExpenseMasterDtoList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetYearAndMonthWiseExpense(int year, int month)
        {
            int UserId = Convert.ToInt32(Session["UserId"]);
            List<ExpenseMasterDto> yearOfExpenseMasterDtoList = advanceSearchFactory.GetYearAndMonthWiseExpense(year, month, UserId);
            return Json(yearOfExpenseMasterDtoList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LiveChat()
        {
            int UserId = Convert.ToInt32(Session["UserId"]);
            if (UserId > 0)
            {
                ViewBag.UserDetails = userDetailsFactory.GetUserDetails(UserId);
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public Cricket CricketChartData()
        {
            Cricket objcricket = new Cricket();
            objcricket.Year = "2009,2010,2011,2012,2013,2014,2015,2016";
            objcricket.Century = "20,28,12,24,14,18,19,27";
            objcricket.HalfCentury = "42,72,22,12,11,34,13,29";
            return objcricket;
        }
    }
    public class DataPoint
    {
        public DataPoint(double x, double y)
        {
            this.x = x;
            this.Y = y;
        }
        //Explicitly setting the name to be used while serializing to JSON.
        public Nullable<double> x = null;
        //Explicitly setting the name to be used while serializing to JSON.
        public Nullable<double> Y = null;
    }
    public class CricketModel
    {
        public string CricketYear { get; set; }
        public string CenturyTitle { get; set; }
        public string HalfCenturyTitle { get; set; }
        public Cricket CricketData { get; set; }
    }
    public class Cricket
    {
        public string Year { get; set; }
        public string Century { get; set; }
        public string HalfCentury { get; set; }
    }
}