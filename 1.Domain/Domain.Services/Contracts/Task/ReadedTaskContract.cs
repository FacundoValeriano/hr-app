﻿using Domain.Services.Contracts.Consultant;
using Domain.Services.Contracts.TaskItem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Contracts.Task
{
    public class ReadedTaskContract
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsApprove { get; set; }
        public bool IsNew { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime EndDate { get; set; }

        public int ConsultantId { get; set; }
        public ReadedConsultantContract Consultant { get; set; }

        public ICollection<ReadedTaskItemContract> TaskItems { get; set; }
    }
}
