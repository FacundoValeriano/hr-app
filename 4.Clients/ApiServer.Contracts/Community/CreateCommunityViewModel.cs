﻿using ApiServer.Contracts.Consultant;
using System;
using System.Collections.Generic;
using System.Text;
using ApiServer.Contracts.CandidateProfile;

namespace ApiServer.Contracts.Community
{
    public class CreateCommunityViewModel
    {
        public string Name   { get; set; }
        public string Description { get; set; }
        public int ProfileId { get; set; }
        public CreateCandidateProfileViewModel Profile { get; set; }
    }
}
