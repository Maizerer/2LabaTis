using _2LabaTis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2LabaTis.Controllers
{
    public class CarController : Controller
    {
        private ApplicationContext db;

        public CarController(ApplicationContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
           return Ok(await db.Cars.ToListAsync()) ;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Car car)
        {
            if (car.Name == null || car.Color == null || car.Price == 0)
            {
                return Content("Введены не все данные");
            }
            else
            {
                    var resultCar = db.Cars.Add(car);
                    await db.SaveChangesAsync();
                    return StatusCode(201, "Машина создана");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Edit(Car car)
        {
            if (car.Name == null || car.Color == null || car.Price == 0)
            {
                return Content("Введены не все данные");
            }
            else
            {
                db.Cars.Update(car);
                await db.SaveChangesAsync();
                return Ok(car);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Car car = new Car { Id = id.Value };
                db.Entry(car).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return Content("Машина успешно удалена");
            }
            return NotFound();
        }
    }
}
