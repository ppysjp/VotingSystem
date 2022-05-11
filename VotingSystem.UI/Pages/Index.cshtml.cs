using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VotingSystem.Application;
using VotingSystem.Database;
using VotingSystem.Models;

namespace VotingSystem.UI.Pages
{
    public class IndexModel : PageModel
    {
        public VotingPoll Poll { get; set; }
        public ICollection<VotingPollVM> VotingPolls { get; set; }

        [BindProperty]
        public VotingPollFactory.Request Form { get; set; }

        public void OnGet([FromServices] AppDbContext ctx)
        {
            VotingPolls = ctx.VotingPolls
                .Select(x => new VotingPollVM {
                Id = EF.Property<int>(x, "Id"),
                Title = x.Title})
                .ToList();
        }

        public void OnPost([FromServices] VotingPollInteractor votingPollInteractor)
        {
            votingPollInteractor.CreateVotingPoll(Form);
        }

        public class VotingPollVM
        {
            public int Id { get; set; }
            public string Title { get; set; }
        }

    }
}
