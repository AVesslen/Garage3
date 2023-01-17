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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Receipt(int id)
        {
            if (_context.Vehicle == null)
            {
                return Problem("Entity set 'Garage3Context.Vehicle' is null.");
            }
            var Vehicle = await _context.Vehicle.FindAsync(id);

            //Får vi tillbaks ett fordon att ta bort? Hanterar null

            if (Vehicle is null)
            {
                return NotFound();
            }

            _context.Vehicle.Remove(Vehicle);
            await _context.SaveChangesAsync();

            var price = Price.GetPrice;

            DateTime timeExit = DateTime.Now;

            TimeSpan span = timeExit.Subtract(Vehicle.ArrivalTime);
            var spanInMinutes = span.TotalMinutes;
            var totalPrice = spanInMinutes * price / 60;

            //Create model for receipt
            //Add information from vehicle to model
            //Send model to Receipt View
            var model = new ReceiptViewModel
            {
                Id = id,
                RegNo = Vehicle.RegNo,
                VehicleType = Vehicle.VehicleType,
                TimeEnter = Vehicle.ArrivalTime,
                TimeExit = timeExit,
                Price = price,
                PriceTotal = (int)totalPrice,

            };

            return View(model);
        }

        private bool ReceiptExists(int id)
        {
          return (_context.Receipt?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
