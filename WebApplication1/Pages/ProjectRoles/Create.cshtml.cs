﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PMS.Data.Entities.ProjectAggregate;
using WebApplication1.Data;

namespace PMS.Pages.ProjectRoles
{
    public class CreateModel : PageModel
    {
        private readonly WebApplication1.Data.ManageAppDbContext _context;

        public CreateModel(WebApplication1.Data.ManageAppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public ProjectRole ProjectRole { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ProjectRoles.Add(ProjectRole);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
