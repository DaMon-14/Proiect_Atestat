﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AttendanceAPI.EF;
using Newtonsoft.Json;
using AttendanceAPI.EF.DBO;

namespace WebApp.Pages.Admin.Cards
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7172"),
        };

        public IList<CardDBO> Cards { get;set; } = default!;

        [BindProperty]
        public bool SortActive { get; set; }=true;

        public async Task<IActionResult> OnGetAsync()
        {
            var reps = HttpContext.Session.TryGetValue("Admin", out _);
            if (HttpContext.Session.TryGetValue("Admin", out _))
            {
                httpClient.DefaultRequestHeaders.Add("UID", _configuration.GetValue<string>("UID"));
                using HttpResponseMessage response = await httpClient.GetAsync("Admin/allCards");
                Cards = JsonConvert.DeserializeObject<List<CardDBO>>(await response.Content.ReadAsStringAsync());
                if (Cards.Count() == 0)
                {
                    Cards = new List<CardDBO>();
                }
                if(SortActive)
                {
                    Cards = Cards.ExceptBy(Cards.Where(x => x.isActive == false), x => x).ToList();
                }
                return Page();
            }
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (HttpContext.Session.TryGetValue("Admin", out _))
            {
                httpClient.DefaultRequestHeaders.Add("UID", _configuration.GetValue<string>("UID"));
                using HttpResponseMessage response = await httpClient.GetAsync("Admin/allCards");
                Cards = JsonConvert.DeserializeObject<List<CardDBO>>(await response.Content.ReadAsStringAsync());
                if (Cards.Count() == 0)
                {
                    Cards = new List<CardDBO>();
                }
                if (SortActive)
                {
                    Cards = Cards.ExceptBy(Cards.Where(x => x.isActive == false), x => x).ToList();
                }
                return Page();
            }
            return RedirectToPage("/Index");
        }
    }    
}
