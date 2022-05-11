using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VotingSystem.Application;
using VotingSystem.Database;
using VotingSystem.Models;

namespace VotingSystem.UI.Pages
{
    public class IndexModel : PageModel
    {
        public VotingPoll Poll { get; set; }

        [BindProperty]
        public VotingPollFactory.Request Form { get; set; }

        public void OnGet([FromServices] AppDbContext ctx)
        {
        }

        public void OnPost([FromServices] VotingPollInteractor votingPollInteractor)
        {
            votingPollInteractor.CreateVotingPoll(Form);
        }

    }
}
