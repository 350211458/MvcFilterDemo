using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcFilterDemo.Models
{
    public class User
    {
        [MinLength(5,ErrorMessage ="用户名长度不能小于5")]
        [MaxLength(16,ErrorMessage ="用户名长度不能大于16")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [MinLength(5,ErrorMessage ="密码长度不能小于5")]
        [MaxLength(16,ErrorMessage ="密码长度不能大于16")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}