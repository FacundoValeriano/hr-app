﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Services.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Core;
using Domain.Model;
using ApiServer.Contracts.Postulant;

namespace ApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostulantsController: BaseController<PostulantsController>
    {
        IPostulantService _postulantService;
        private IMapper _mapper;

        public PostulantsController(IPostulantService postulantService,
                                    ILog<PostulantsController>logger, IMapper mapper):
                                    base(logger)
        {
            _postulantService = postulantService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return ApiAction(() =>
            {
                var postulants = _postulantService.List();
                return Accepted(_mapper.Map<List<ReadedPostulantViewModel>>(postulants));
            });
        }
    }
}
