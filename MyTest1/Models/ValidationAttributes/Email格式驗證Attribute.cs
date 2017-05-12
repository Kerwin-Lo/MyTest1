using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace MyTest1.Models.ValidationAttributes
{
    public class Email格式驗證Attribute : DataTypeAttribute
    {
        public Email格式驗證Attribute() : base(DataType.Text)
        {

        }

        public override bool IsValid(object value)
        {
            return new Regex(@"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$").IsMatch((string)value);
        }
    }
}