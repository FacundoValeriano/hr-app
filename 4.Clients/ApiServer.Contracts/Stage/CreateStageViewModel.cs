﻿using Domain.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiServer.Contracts.Stage
{
    public abstract class CreateStageViewModel
    {
        public int Id { get; set; }
        public int ProcessId { get; set; }

        public DateTime? Date { get; set; }

        public StageStatus Status { get; set; }

        public string Feedback { get; set; }

        public int? ConsultantOwnerId { get; set; }

        public int? ConsultantDelegateId { get; set; }
        public string RejectionReason { get; set; }
    }
}
