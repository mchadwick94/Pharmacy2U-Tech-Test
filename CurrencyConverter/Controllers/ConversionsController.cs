using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CurrencyConverter.Data;
using CurrencyConverter.Models;

namespace CurrencyConverter.Controllers
{
    public class ConversionsController : Controller
    {
        private readonly CurrencyConverterContext _context;

        public ConversionsController(CurrencyConverterContext context)
        {
            _context = context;
        }


        public IActionResult ConversionLookUp()
            {
            return View();
            }
        public IQueryable<Conversion> GetFilteredConversionsBusiness(SearchModel searchModel)
            {
            var result = _context.Conversion.AsQueryable();
            if (searchModel != null)
                {
                if (searchModel.Search_Start_Date.HasValue)
                    result = result.Where(x => x.Conv_Date >= searchModel.Search_Start_Date);
                if (searchModel.Search_End_Date.HasValue)
                    result = result.Where(x => x.Conv_Date <= searchModel.Search_End_Date);
                }
            return result;
            }
        public ActionResult GetFilteredConversions(SearchModel searchModel)
            {
            var business = GetFilteredConversionsBusiness(searchModel);
            var model = business;
            return PartialView(model);
            }


        // GET: Conversions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Conversion.ToListAsync());
        }

        // GET: Conversions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversion = await _context.Conversion
                .FirstOrDefaultAsync(m => m.Conversion_ID == id);
            if (conversion == null)
            {
                return NotFound();
            }

            return View(conversion);
        }

        // GET: Conversions/Create
        public IActionResult Create()
        {
            List<SelectListItem> CurrenciesList = new List<SelectListItem>();
            foreach (var item in _context.Currency.ToList())
                {
                CurrenciesList.Add(
                    new SelectListItem()
                        {
                        Text = item.Currency_Name,
                        Value = item.Currency_Name.ToString()
                        });
                }
            ViewBag.Currencies = CurrenciesList;
            return View();
        }

        // POST: Conversions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Conversion_ID,Conv_Input_Value,Conv_Input_Currency,Conv_Output_Value,Conv_Output_Currency,Conv_Date")] Conversion conversion)
        {
            if (ModelState.IsValid)
            {
                double convertToValue = _context.Currency.Where(c => c.Currency_Name == conversion.Conv_Output_Currency).Select(c => c.Currency_Value).FirstOrDefault();
                double convertFromValue = _context.Currency.Where(c => c.Currency_Name == conversion.Conv_Input_Currency).Select(c => c.Currency_Value).FirstOrDefault();

                conversion.Conv_Output_Value = Math.Round((conversion.Conv_Input_Value / convertFromValue) * convertToValue, 2);
                conversion.Conv_Date = System.DateTime.Now;
                _context.Add(conversion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ConversionLookUp));
                }
            return View(conversion);
        }

        // GET: Conversions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversion = await _context.Conversion.FindAsync(id);
            if (conversion == null)
            {
                return NotFound();
            }
            return View(conversion);
        }

        // POST: Conversions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Conversion_ID,Conv_Input_Value,Conv_Input_Currency,Conv_Output_Value,Conv_Output_Currency,Conv_Date")] Conversion conversion)
        {
            if (id != conversion.Conversion_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conversion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConversionExists(conversion.Conversion_ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ConversionLookUp));
                }
            return View(conversion);
        }

        // GET: Conversions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversion = await _context.Conversion
                .FirstOrDefaultAsync(m => m.Conversion_ID == id);
            if (conversion == null)
            {
                return NotFound();
            }

            return View(conversion);
        }

        // POST: Conversions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conversion = await _context.Conversion.FindAsync(id);
            _context.Conversion.Remove(conversion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ConversionLookUp));
            }

        private bool ConversionExists(int id)
        {
            return _context.Conversion.Any(e => e.Conversion_ID == id);
        }
    }
}
