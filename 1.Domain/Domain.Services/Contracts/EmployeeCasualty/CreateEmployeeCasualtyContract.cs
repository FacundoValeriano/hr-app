﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Contracts.EmployeeCasualty
{
    public class CreateEmployeeCasualtyContract
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int Value { get; set; }
    }
}
