﻿using Domain.Model.Enum;
using Domain.Services.Contracts.CandidateSkill;
using Domain.Services.Contracts.Consultant;
using Domain.Services.Contracts.Role;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Contracts.Employee
{
    public class UpdateEmployeeContract
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int DNI { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string LinkedInProfile { get; set; }
        public string AdditionalInformation { get; set; }
        public EmployeeStatus Status { get; set; }
        public int RecruiterId { get; set; }
        public CreateConsultantContract Recruiter { get; set; }
        public int RoleId { get; set; }
        public CreateRoleContract Role { get; set; }
        public bool isReviewer { get; set; }
        public int? ReviewerId { get; set; }
        public CreateEmployeeContract Reviewer { get; set; }
    }
}
