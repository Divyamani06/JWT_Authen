﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT_Authen.Model
{
    public class UserCrud
    {
        public int Id { get; set; }      
        public string Email { get; set; }
        public string Password { get; set; } 
    }
}
