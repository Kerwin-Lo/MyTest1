using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using MyTest1.Models;


namespace MyTest1.Controllers
{
    
    public class 客戶資料Controller : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();
        // GET: 客戶資料
        public ActionResult Index()
        {
            var all = db.客戶資料.AsQueryable();
            var result = all.Where(c=>c.是否已刪除 == false).OrderByDescending(c => c.Id);
            return View(result);
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            string strKeyword = fc.Get("txtKeyword");
            var all = db.客戶資料.AsQueryable();
            var result = all.Where(c => c.是否已刪除 == false 
                & (
                    c.客戶名稱.Contains(strKeyword) 
                    | c.統一編號.Contains(strKeyword)
                    | c.電話.Contains(strKeyword)
                    | c.傳真.Contains(strKeyword)
                    | c.地址.Contains(strKeyword)
                    | c.Email.Contains(strKeyword)
                )
            )
                .OrderByDescending(c => c.Id);
            return View(result);
        }



        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(客戶資料 client)
        {
            if (ModelState.IsValid)
            {
                db.客戶資料.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }


        public ActionResult Edit(int id)
        {
            return View(db.客戶資料.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, 客戶資料 client)
        {
            if (ModelState.IsValid)
            {
                var item = db.客戶資料.Find(id);
                item.客戶名稱 = client.客戶名稱;
                item.統一編號 = client.統一編號;
                item.電話 = client.電話;
                item.傳真 = client.傳真;
                item.地址 = client.地址;
                item.Email = client.Email;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        public ActionResult Details(int id)
        {
            return View(db.客戶資料.Find(id));
        }

        public ActionResult Delete(int id)
        {

            var client = db.客戶資料.Find(id);
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