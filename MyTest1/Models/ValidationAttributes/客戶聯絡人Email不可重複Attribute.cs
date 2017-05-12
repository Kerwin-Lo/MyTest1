using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyTest1.Models.ValidationAttributes
{
    public class 客戶聯絡人Email不可重複Attribute : DataTypeAttribute
    {
        private 客戶資料Entities db = new 客戶資料Entities();
        public 客戶聯絡人Email不可重複Attribute() : base(DataType.Text)
        {

        }

        public override bool IsValid(object value)
        {
            var strValue = (string)value;
            var all = db.客戶聯絡人.AsQueryable();
            var result = from c in all
                         where c.Email.ToLower() == strValue.ToLower()
                         select c;
            return result.Count()==0;
        }
    }
}