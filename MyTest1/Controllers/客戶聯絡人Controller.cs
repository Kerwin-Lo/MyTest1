using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using MyTest1.Models;

namespace MyTest1.Controllers
{
    public class 客戶聯絡人Controller : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();
        // GET: 客戶聯絡人
        public ActionResult Index()
        {
            var all = db.客戶聯絡人.AsQueryable();
            var result = all.Where(c => c.是否已刪除 == false).OrderByDescending(c => c.Id).Take(10);
            return View(result);
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            string strKeyword = fc.Get("txtKeyword");
            var all = db.客戶聯絡人.AsQueryable();
            var result = all.Where(c => c.是否已刪除 == false
                & (
                    c.姓名.Contains(strKeyword)
                    | c.職稱.ToString().Contains(strKeyword)
                    | c.Email.ToString().Contains(strKeyword)
                    | c.手機.Contains(strKeyword)
                    | c.電話.Contains(strKeyword)
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
        public ActionResult Create(客戶聯絡人 contrct, FormCollection fc)
        {
            var all = db.客戶資料.AsQueryable();
            var client = all.Where(c => c.是否已刪除 == false).OrderByDescending(c => c.Id);
            SelectList sl = new SelectList(client, "ID", "客戶名稱");
            ViewBag.CategoryItems = sl;
            if (ModelState.IsValid)
            {
                contrct.客戶Id =Convert.ToInt32(fc.Get("ddlClient"));
                db.客戶聯絡人.Add(contrct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contrct);
        }

        public ActionResult Edit(int id)
        {
            var all = db.客戶資料.AsQueryable();
            var client = all.Where(c => c.是否已刪除 == false).OrderByDescending(c => c.Id);
            SelectList sl = new SelectList(client, "ID", "客戶名稱");
            ViewBag.CategoryItems = sl;
            var result = db.客戶聯絡人.Find(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(int id, 客戶聯絡人 contrct, FormCollection fc)
        {
            var all = db.客戶資料.AsQueryable();
            var client = all.Where(c => c.是否已刪除 == false).OrderByDescending(c => c.Id);
            SelectList sl = new SelectList(client, "ID", "客戶名稱");
            ViewBag.CategoryItems = sl;

            if (ModelState.IsValid)
            {
                var c = db.客戶聯絡人.Find(id);
                c.客戶Id = Convert.ToInt32(fc.Get("ddlClient"));
                c.職稱 = contrct.職稱;
                c.姓名 = contrct.姓名;
                c.Email = contrct.Email;
                c.手機 = contrct.手機;
                c.電話 = contrct.電話;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contrct);
        }

        public ActionResult Details(int id)
        {
            return View(db.客戶聯絡人.Find(id));
        }

        public ActionResult Delete(int id)
        {

            var client = db.客戶聯絡人.Find(id);
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