using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Linq;
using System.Threading.Tasks;
using ApiServer.Contracts.DaysOff;
using AutoMapper;
using Core;
using Domain.Model;
using Domain.Services.Contracts.DaysOff;
using Domain.Services.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class EmailSenderController : BaseController<EmailSenderController>
    {

        private readonly IEmailSenderService _emailSenderService;
        private readonly IMapper _mapper;
        public EmailSenderController(IEmailSenderService emailSenderService, ILog<EmailSenderController> logger, IMapper mapper)
            : base(logger)
        {
            _emailSenderService = emailSenderService;
            _mapper = mapper;
        }

        [HttpPost("DaysOff")]
        public IActionResult Post([FromBody]CreateDaysOffViewModel vm)
        {
            return ApiAction(() =>
            {
                var contract = _mapper.Map<CreateDaysOffContract>(vm);
                var daysoff = _mapper.Map<DaysOff>(contract);
                var email = _emailSenderService.ComposeDaysOffMail(daysoff);
                _emailSenderService.SendMail(email);

                return Accepted();
            });
        }

    }
}
