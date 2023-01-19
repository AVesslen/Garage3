﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage3.Core;
using Garage3.Data;
using Garage3.ViewModels;
using AutoMapper;
using Bogus;
using Bogus.DataSets;

namespace Garage3.Controllers
{
    public class MembersController : Controller
    {
        private readonly Garage3Context _context;
        private readonly IMapper mapper;

        public MembersController(Garage3Context context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: Members  
        public async Task<IActionResult> Index()
        {           

            var viewModel = await mapper.ProjectTo<MemberIndexViewModel>(_context.Member)  // Med AutoMapper                                               
                .ToListAsync();

            return View(viewModel);
        }

        //public async Task<IActionResult> Search(string personalNo, string firstName, string lastName)
        //{
        //    var viewModel = await _context.Member
        //        .Where(m => (string.IsNullOrEmpty(personalNo) || m.PersonalNo.StartsWith(personalNo)) &&
        //                (string.IsNullOrEmpty(firstName) || m.FirstName.StartsWith(firstName)) &&
        //                (string.IsNullOrEmpty(lastName) || m.LastName.StartsWith(lastName)))
        //        .Select(m => new MemberIndexViewModel
        //        {
        //            Id = m.Id,
        //            FirstName = m.FirstName,
        //            LastName = m.LastName,                
        //            //PersonalNo = m.PersonalNo,

        //        }).ToListAsync();

        //    return View(nameof(Index), viewModel);
        //}


        public async Task<IActionResult> Search(string personalNo, string firstName, string lastName)
        {
            var viewModel = await mapper.ProjectTo<MemberIndexViewModel>(_context.Member)
                .Where(m => 
                        (string.IsNullOrEmpty(firstName) || m.FirstName.StartsWith(firstName)) &&
                        (string.IsNullOrEmpty(lastName) || m.LastName.StartsWith(lastName)))                
               .ToListAsync();

            return View(nameof(Index), viewModel);
        }



        // GET: Members/Details/5   //Med AutoMapper
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Member == null)
            {
                return NotFound();
            }

            var member = await mapper.ProjectTo<MemberDetailsViewModel>(_context.Member)                
                .FirstOrDefaultAsync(m => m.Id == id);

            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }


        // GET: Members/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Members/Create       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,PersonalNo")] Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = $"{member.FirstName} har registrerats som medlem.";
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Member == null)
            {
                return NotFound();
            }

            var member = await _context.Member.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,PersonalNo")] Member member)
        {
            if (id != member.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                    TempData["AlertMessage"] = "Dina ändringar har sparats.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Member == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Member == null)
            {
                return Problem("Entity set 'Garage3Context.Member'  is null.");
            }

            var member = await _context.Member.FindAsync(id);
            if (member != null)
            {               
                _context.Member.Remove(member);
                TempData["AlertMessage"] = $"{member.FirstName} har raderats ur registret.";
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
          return (_context.Member?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
