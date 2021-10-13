﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Areas.Admin.Models
{
    public class RoleUserVm
    {
        [Required]
        [DisplayName("Użytkownik")]
        public string UserId { get; set; }

        [Required]
        [DisplayName("Rola")]
        public string RoleId { get; set; }
    }
}