﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmBasics.Model
{
    public class AccountOwner
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public DateTime AccountCreatedDate { get; set; }
    }
}
