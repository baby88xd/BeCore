using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeCore.Core
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]//必须的
        [DataType(DataType.Password)]//内容检查是否为密码
        public string Password { get; set; }
    }
}
