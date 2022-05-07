﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Models;

namespace VotingSystem.UI.Controllers
{
    // controller
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly IVotingPollFactory _pollFactory;

        public HomeController(IVotingPollFactory pollFactory)
        {
            _pollFactory = pollFactory;
        }


        [HttpGet]
        public IActionResult Index() 
        {
            return View(); 
        }

        [HttpPost]
        public VotingPoll Create(VotingPollFactory.Request request) 
        {
            return _pollFactory.Create(request);
        }

    }
}
