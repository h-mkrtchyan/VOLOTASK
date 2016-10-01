using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataAccessLayer.Models
{
    class ValidateImageAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
           
            string[] extensions = new string[] { ".jpg", ".png", ".svg",
                ".bmp", ".gif", ".bpg", ".WebP" };

            var file = value as HttpPostedFileBase;

            if (file != null && !extensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
            {
                ErrorMessage = "Please upload Your Image of type: " + string.Join(", ", extensions);
                return false;
            }
            else
                return true;
        }
    }
}
