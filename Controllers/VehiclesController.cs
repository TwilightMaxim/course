using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PassengerTransportationAPI.Models;

namespace PassengerTransportationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehiclesController : Controller
    {
        private readonly ApplicationContext _context;
        public VehiclesController(ApplicationContext CityMove)
        {
            _context = CityMove;
        }
        /// <summary>
        /// Получения списка транспорта
        /// </summary>
        /// <returns></returns>
        [HttpGet("VehicleList")]
        public async Task<IActionResult> GetVehicle()
        {
            var vehicles = await _context.Vehicles.ToListAsync();
            return Ok(JsonConvert.SerializeObject(vehicles));
        }
        /// <summary>
        /// Метод получения информации о трансопрте по ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet("VehicleInfoID")]
        public async Task<IActionResult> GetInfoVehicleID(int ID)
        {
            Vehicle? vehicle = _context.Vehicles.SingleOrDefault(p => p.VehicleID == ID);
            return Ok(JsonConvert.SerializeObject(vehicle));
        }
        /// <summary>
        /// Добавление транспорта в базу данных
        /// </summary>
        /// <param name="vehicles"></param>
        /// <returns></returns>
        [HttpPost("VehicleAdd")]
        public async Task<IActionResult> PatientRegistration(Vehicle vehicles)
        {
            Vehicle? vehicle = _context.Vehicles.SingleOrDefault(p => p.RegistrationNumber == vehicles.RegistrationNumber);
            if (vehicle != null)
            {
                return StatusCode(409);
            }
            else
            {
                _context.Vehicles.Add(vehicles);
                await _context.SaveChangesAsync();
                return Ok();
            }
        }
        /// <summary>
        /// Редактирование информации о транспортном средстве
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        [HttpPut("EditVehicle")]
        public async Task<IActionResult> EditVehicle(int id, Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var existingVehicle = _context.Vehicles.FirstOrDefault(i => i.VehicleID == id);
            if (existingVehicle == null)
            {
                return NotFound();
            }

            existingVehicle.Photo = vehicle.Photo;
            existingVehicle.Model = vehicle.Model;
            existingVehicle.RegistrationNumber = vehicle.RegistrationNumber;
            existingVehicle.Capacity = vehicle.Capacity;
            await _context.SaveChangesAsync();
            return Ok();
        }
        /// <summary>
        /// Удаление транспортного средства
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteVehicle")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            Vehicle? vehicles = _context.Vehicles.FirstOrDefault(p => p.VehicleID == id);
            if (vehicles == null)
            {
                return NotFound();
            }
            _context.Vehicles.Remove(vehicles);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
