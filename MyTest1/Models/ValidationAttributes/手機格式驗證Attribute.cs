using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace MyTest1.Models.ValidationAttributes
{
    public class 手機格式驗證Attribute : DataTypeAttribute
    {
        public 手機格式驗證Attribute() : base(DataType.Text)
        {

        }

        public override bool IsValid(object value)
        {
            return new Regex(@"\d{4}-\d{6}").IsMatch((string)value);
        }
    }
}