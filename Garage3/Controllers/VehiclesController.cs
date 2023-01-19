using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage3.Core;
using Garage3.Data;
using AutoMapper;
using Garage3.ViewModels;

namespace Garage3.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly Garage3Context _context;
        private readonly IMapper mapper;

        public VehiclesController(Garage3Context context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        //// GET: Vehicles
        //public async Task<IActionResult> Index()  // Default
        //{
        //    var garage3Context = _context.Vehicle.Include(v => v.Member).Include(v => v.VehicleType);
        //    return View(await garage3Context.ToListAsync());
        //}


        // GET: Vehicles
        public async Task<IActionResult> Index()  // Med AutoMapper
        {
            var viewModel = await mapper.ProjectTo<VehicleIndexViewModel>(_context.Vehicle)              
           .OrderByDescending(s => s.Id)
           .ToListAsync();
            return View(viewModel);
        }

        //public async Task<IActionResult> Search(string RegNo, string Member, string VehicleType)
        //{
        //    var viewModel = await _context.Vehicle
        //        .Where(v => (string.IsNullOrEmpty(RegNo) || v.RegNo.StartsWith(RegNo)) &&
        //                (string.IsNullOrEmpty(VehicleType) || v.VehicleType.Type.Contains(VehicleType)))
        //        .Select(v => new VehicleIndexViewModel
        //        {
        //            Id = v.Id,
        //            RegNo = v.RegNo,
        //            VehicleType = v.VehicleType,                      

        //        }).ToListAsync();

        //    return View(nameof(Index), viewModel);
        //}

        public async Task<IActionResult> Search(string RegNo, string Member, string VehicleType)
        {
            var viewModel = await mapper.ProjectTo<VehicleIndexViewModel>(_context.Vehicle)
                 .Where(v => (string.IsNullOrEmpty(RegNo) || v.RegNo.StartsWith(RegNo)) &&
                 (string.IsNullOrEmpty(VehicleType) || v.VehicleType.Type.Contains(VehicleType)))
                 .ToListAsync();

            return View(nameof(Index), viewModel);
        }


        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await mapper.ProjectTo<VehicleDetailsViewModel>(_context.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            ViewData["MemberID"] = new SelectList(_context.Member, "Id", "FirstName");
            ViewData["VehicleTypeID"] = new SelectList(_context.Set<VehicleType>(), "Id", "Type");
            return View();
        } 

        // POST: Vehicles/Create      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleCreateViewModel viewModel)
        {
            var vehicle = mapper.Map<Vehicle>(viewModel);
            vehicle.ArrivalTime = DateTime.Now;
            _context.Add(vehicle);
            await _context.SaveChangesAsync();
            TempData["AlertMessage"] = $"Fordon med regnr {vehicle.RegNo} har registrerats.";
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Park(int? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle.FindAsync(id);        
           
            vehicle.IsParked = true;
            vehicle.ArrivalTime = DateTime.Now;                        

            _context.Update(vehicle);
            await _context.SaveChangesAsync();
            TempData["AlertMessage"] = $"Fordon med regnr {vehicle.RegNo} har parkerats.";

            return RedirectToAction(nameof(Index));          
        }


        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewData["MemberID"] = new SelectList(_context.Member, "Id", "FirstName", vehicle.MemberID);
            ViewData["VehicleTypeID"] = new SelectList(_context.Set<VehicleType>(), "Id", "Type", vehicle.VehicleTypeID);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RegNo,Brand,VehicleModel,Color,ArrivalTime,IsParked,MemberID,VehicleTypeID")] Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                    TempData["AlertMessage"] = $"Fordon med regnr {vehicle.RegNo} har ändrats.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id))
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
            ViewData["MemberID"] = new SelectList(_context.Member, "Id", "FirstName", vehicle.MemberID);
            ViewData["VehicleTypeID"] = new SelectList(_context.Set<VehicleType>(), "Id", "Type", vehicle.VehicleTypeID);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .Include(v => v.Member)
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vehicle == null)
            {
                return Problem("Entity set 'Garage3Context.Vehicle'  is null.");
            }
            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicle.Remove(vehicle);
                TempData["AlertMessage"] = $"Fordon med regnr {vehicle.RegNo} har avregistrerats.";
            }
            
            await _context.SaveChangesAsync();            
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
          return (_context.Vehicle?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
