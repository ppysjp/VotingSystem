using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public VotingPoll Create() 
        {
            return _pollFactory.Create();
        }

    }
}
