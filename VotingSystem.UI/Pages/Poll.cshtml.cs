using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VotingSystem.Application;

namespace VotingSystem.UI.Pages
{
    public class PollModel : PageModel
    {
        public void OnGet(int id, [FromServices] StatisticsInteractor interactor)
        {
            var statistics = interactor.GetStatistics(id); 
        }
    }
}
