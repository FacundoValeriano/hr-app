﻿using System;
using System.Collections.Generic;
using System.Text;
using ApiServer.Contracts.Consultant;
using Domain.Model.Enum;

namespace ApiServer.Contracts.Stage
{
    public class ReadedTechnicalStageViewModel : ReadedStageViewModel
    {
        public Seniority Seniority { get; set; }
        public Seniority Seniority1 { get; set; }
        public string Client { get; set; }
    }
}
