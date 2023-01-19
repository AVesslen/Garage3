using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage3.Core;
using Garage3.Data;
using Garage3.Views.Vehicles;
using Garage3.Data.Migrations;
using Garage3.ViewModels;
using AutoMapper;

namespace Garage3.Controllers
{
    public class ReceiptsController : Controller 
    {
        private readonly Garage3Context _context;

        public ReceiptsController(Garage3Context context)
        {
            _context = context;
        }

        // GET: Receipts
        public async Task<IActionResult> Index()
        {
            return _context.Receipt != null ?
                        View(await _context.Receipt.ToListAsync()) :
                        Problem("Entity set 'Garage3Context.Receipt'  is null.");
        }

        // GET: Receipts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Receipt == null)
            {
                return NotFound();
            }

            var receipt = await _context.Receipt
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receipt == null)
            {
                return NotFound();
            }

            return View(receipt);
        }

        public async Task<IActionResult> SearchReceipt(string RegNo, string VehicleType)
        {
            var viewModel = await _context.Receipt
                         .Where(r => (string.IsNullOrEmpty(RegNo) || r.RegNo.StartsWith(RegNo)) &&
                         (string.IsNullOrEmpty(VehicleType) || r.VehicleType.Type.Contains(VehicleType)))
                .Select(r => new Receipt
                {
                    Id = r.Id,
                    RegNo = r.RegNo,
                    VehicleType = r.VehicleType,
                    TimeEnter = r.TimeEnter,
                    TimeExit = r.TimeExit,
                    Price = r.Price,
                    PriceTotal = r.PriceTotal,

                }).ToListAsync();

            return View(nameof(Index), viewModel);
        }

        // GET: Receipts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Receipts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RegNo,TimeEnter,TimeExit,Price,PriceTotal")] Receipt receipt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receipt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(receipt);
        }

        // GET: Receipts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Receipt == null)
            {
                return NotFound();
            }

            var receipt = await _context.Receipt.FindAsync(id);
            if (receipt == null)
            {
                return NotFound();
            }
            return View(receipt);
        }

        // POST: Receipts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RegNo,TimeEnter,TimeExit,Price,PriceTotal")] Receipt receipt)
        {
            if (id != receipt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receipt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceiptExists(receipt.Id))
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
            return View(receipt);
        }

        // GET: Receipts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Receipt == null)
            {
                return NotFound();
            }

            var receipt = await _context.Receipt
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receipt == null)
            {
                return NotFound();
            }

            return View(receipt);
        }

        // POST: Receipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Receipt == null)
            {
                return Problem("Entity set 'Garage3Context.Receipt'  is null.");
            }
            var receipt = await _context.Receipt.FindAsync(id);
            if (receipt != null)
            {
                _context.Receipt.Remove(receipt);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

       
        public async Task<IActionResult> Receipt(int id)
        {
            if (_context.Vehicle == null)
            {
                return Problem("Entity set 'Garage3Context.Vehicle' is null.");
            }
            //var vehicle = await _context.Vehicle.FindAsync(id)


                 var vehicle = await _context.Vehicle
                .Include(v => v.Member)
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(m => m.Id == id);

            //Får vi tillbaks ett fordon att ta bort? Hanterar null

            if (vehicle is null)
            {
                return NotFound();
            }
            vehicle.IsParked = false;
            _context.Vehicle.Update(vehicle);
            TempData["AlertMessage"] = $"Fordon med regnr {vehicle.RegNo} har checkats ut.";

            var price = Price.GetPrice;

            DateTime timeExit = DateTime.Now;
            TimeSpan span = timeExit.Subtract(vehicle.ArrivalTime);
            var spanInMinutes = span.TotalMinutes;
            var totalPrice = spanInMinutes * price / 60;

            //Create model for receipt
            //Add information from vehicle to model
            //Send model to Receipt View
            var receipt = new Receipt
            {
                RegNo = vehicle.RegNo,
                VehicleType = vehicle.VehicleType,
                TimeEnter = vehicle.ArrivalTime,
                TimeExit = timeExit,
                Price = price,
                PriceTotal = (int)totalPrice,
                MemberId = vehicle.MemberID,
                VehicleId = vehicle.Id
            };

            _context.Add(receipt);
            await _context.SaveChangesAsync();
            TempData["AlertMessage"] = $"Fordon med regnr {vehicle.RegNo} har checkats ut.";
            
            var model = new ReceiptViewModel
            {
                Id = id,
                RegNo = vehicle.RegNo,
                VehicleType = vehicle.VehicleType,
                TimeEnter = vehicle.ArrivalTime,
                TimeExit = timeExit,
                Price = price,
                PriceTotal = (int)totalPrice,                
                Member = vehicle.Member
            };
            return View(model);
        }

        private bool ReceiptExists(int id)
        {
          return (_context.Receipt?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
