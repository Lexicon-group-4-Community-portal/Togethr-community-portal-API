﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UserClasses
{
    public class Guest : User
    {
        public int UserExperience { get; set; } = 0; //based on activity

    }
}
