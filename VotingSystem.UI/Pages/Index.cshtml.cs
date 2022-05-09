using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VotingSystem.Models;

namespace VotingSystem.UI.Pages
{
    public class IndexModel : PageModel
    {
        public VotingPoll Poll { get; set; }

        public void OnGet([FromServices] IVotingPollFactory pollFactory)
        {
            var request = new VotingPollFactory.Request
            {
                Title = "title",
                Description = "desc",
                Names = new[] { "one", "two" }
            };

            Poll = pollFactory.Create(request);

        }
    }
}
