using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using MyTest1.Models;
namespace MyTest1.Controllers
{
    public class 客戶銀行資訊Controller : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();
        // GET: 客戶銀行資訊
        public ActionResult Index()
        {
            var all = db.客戶銀行資訊.AsQueryable();
            var result = all.Where(c => c.是否已刪除 == false).OrderByDescending(c => c.Id).Take(10);
            return View(result);
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            string strKeyword = fc.Get("txtKeyword");
            var all = db.客戶銀行資訊.AsQueryable();
            var result = all.Where(c => c.是否已刪除 == false
                & (
                    c.銀行名稱.Contains(strKeyword)
                    | c.銀行代碼.ToString().Contains(strKeyword)
                    | c.分行代碼.ToString().Contains(strKeyword)
                    | c.帳戶號碼.Contains(strKeyword)
                    | c.帳戶名稱.Contains(strKeyword)
                    | c.客戶資料.客戶名稱.Contains(strKeyword)
                )
            )
                .OrderByDescending(c => c.Id);
            return View(result);
        }

        public ActionResult Create()
        {
            var all = db.客戶資料.AsQueryable();
            var client = all.Where(c => c.是否已刪除 == false).OrderByDescending(c => c.Id);
            SelectList sl = new SelectList(client, "ID", "客戶名稱");
            ViewBag.CategoryItems = sl;

            return View();
        }

        [HttpPost]
        public ActionResult Create(客戶銀行資訊 bank, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                bank.客戶Id = Convert.ToInt32(fc.Get("ddlClient"));
                db.客戶銀行資訊.Add(bank);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bank);
        }

        public ActionResult Edit(int id)
        {
            var all = db.客戶資料.AsQueryable();
            var client = all.Where(c => c.是否已刪除 == false).OrderByDescending(c => c.Id);
            SelectList sl = new SelectList(client, "ID", "客戶名稱");
            ViewBag.CategoryItems = sl;
            return View(db.客戶銀行資訊.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, 客戶銀行資訊 bank, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                var b = db.客戶銀行資訊.Find(id);
                b.客戶Id = Convert.ToInt32(fc.Get("ddlClient"));
                b.帳戶名稱 = bank.帳戶名稱;
                b.銀行代碼 = bank.銀行代碼;
                b.分行代碼 = bank.分行代碼;
                b.帳戶號碼 = bank.帳戶號碼;
                b.帳戶名稱= bank.帳戶名稱;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bank);
        }

        public ActionResult Details(int id)
        {
            return View(db.客戶銀行資訊.Find(id));
        }

        public ActionResult Delete(int id)
        {

            var client = db.客戶銀行資訊.Find(id);
            client.是否已刪除 = true;
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

    }
}