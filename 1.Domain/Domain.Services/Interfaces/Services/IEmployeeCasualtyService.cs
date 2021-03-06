﻿using Domain.Services.Contracts.EmployeeCasualty;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Interfaces.Services
{
    public interface IEmployeeCasualtyService
    {
        CreatedEmployeeCasualtyContract Create(CreateEmployeeCasualtyContract contract);
        ReadedEmployeeCasualtyContract Read(int id);
        void Update(UpdateEmployeeCasualtyContract contract);
        void Delete(int id);
        IEnumerable<ReadedEmployeeCasualtyContract> List();
    }
}
