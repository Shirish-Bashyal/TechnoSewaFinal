﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.User.Role
{
    public class CreateRoleDTO
    {
        [Required]
        public string RoleName { get; set; }
    }
}
