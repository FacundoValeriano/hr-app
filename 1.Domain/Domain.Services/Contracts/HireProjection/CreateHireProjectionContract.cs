﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Contracts.HireProjection
{
    public class CreateHireProjectionContract
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int Value { get; set; }
    }
}
