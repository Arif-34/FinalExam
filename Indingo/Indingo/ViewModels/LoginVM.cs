﻿using System.ComponentModel.DataAnnotations;

namespace Indingo.ViewModels
{
    public class LoginVM
    {
        public string UsernameOrEmail { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsRememberMe { get; set; }
    }
}
