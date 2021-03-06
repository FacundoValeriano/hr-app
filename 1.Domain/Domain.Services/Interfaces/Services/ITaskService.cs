﻿using Domain.Services.Contracts.Task;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Interfaces.Services
{
    public interface ITaskService
    {
        CreatedTaskContract Create(CreateTaskContract contract);
        ReadedTaskContract Read(int id);
        IEnumerable<ReadedTaskContract> ListByConsultant(string consultantEmail);
        void Update(UpdateTaskContract contract);
        void Approve(int id);
        void Delete(int id);
        IEnumerable<ReadedTaskContract> List();
    }
}
