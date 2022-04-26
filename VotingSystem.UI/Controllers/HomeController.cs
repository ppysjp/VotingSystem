using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingSystem.UI.Controllers
{
    // controller
    [Route("home")]
    public class HomeController : Controller
    {
        // action
        public string Index() 
        {
            return "Hello Index Page";
        }

        [HttpGet("/about-page")]
        public string About() 
        {
            return "About Page"; 
        }
        
    }
}
