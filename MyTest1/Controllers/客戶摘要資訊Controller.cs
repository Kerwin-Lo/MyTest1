using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyTest1.Models;

namespace MyTest1.Controllers
{
    public class 客戶摘要資訊Controller : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();
        // GET: 客戶摘要資訊
        public ActionResult Index()
        {
            //http://blog.miniasp.com/post/2013/11/07/Entity-Framework-and-Primary-Keys-on-Views.aspx
            var all = db.客戶摘要資訊.AsQueryable();
            var result = all.OrderByDescending(c => c.Id).Take(10);
            return View(result);
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            string strKeyword = fc.Get("txtKeyword");
            var all = db.客戶摘要資訊.AsQueryable();
            var result = all.Where(c => c.客戶名稱.Contains(strKeyword))
                .OrderByDescending(c => c.Id);
            return View(result);
        }
    }
}