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
    [Route("[controller]/[action]")]
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
            var info = new Info { Message = "Hello World" };
            return View(info); 
        }


        [HttpGet]
        public IActionResult About() 
        {
            return View("Index"); 
        }

        [HttpPost]
        public VotingPoll Create(VotingPollFactory.Request request) 
        {
            return _pollFactory.Create(request);
        }

    }

    public class Info 
    {
        public string Message { get; set; }
    }

}
