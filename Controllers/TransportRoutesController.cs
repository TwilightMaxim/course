using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PassengerTransportationAPI.Models;

namespace PassengerTransportationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransportRoutesController : Controller
    {
        private readonly ApplicationContext _context;
        public TransportRoutesController(ApplicationContext CityMove)
        {
            _context = CityMove;
        }
        /// <summary>
        /// Поиск маршрутов по заданным параметрам
        /// </summary>
        /// <returns></returns>
        [HttpGet("RoutesList")]
        public async Task<IActionResult> SearchOffers(string departureCity, string arrivalCity, DateOnly date)
        {
            try
            {
                // Выполнить поиск предложений по заданным параметрам
                var offers = await _context.TransportRoute
                    .Where(r => r.CityDeparture == departureCity && r.CityArrival == arrivalCity && r.Date == date)
                    .ToListAsync();

                if (offers.Count == 0)
                {
                    return NotFound("Предложения не найдены");
                }

                return Ok(offers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Внутренняя ошибка сервера: {ex.Message}");
            }
        }
        /// <summary>
        /// Добавление маршрута
        /// </summary>
        /// <param name="transport"></param>
        /// <returns></returns>
        [HttpPost("RoutesAdd")]
        public async Task<IActionResult> AddRoutes(TransportRoutes routes)
        {
            _context.TransportRoute.Add(routes);
            await _context.SaveChangesAsync();
            return Ok();
        }
        /// <summary>
        /// Редактирование информации о маршруте
        /// </summary>
        /// <param name="id"></param>
        /// <param name="routes"></param>
        /// <returns></returns>
        [HttpPut("EditRoutes")]
        public async Task<IActionResult> EditRoutes(int id, TransportRoutes routes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var existingRoutes = _context.TransportRoute.FirstOrDefault(i => i.Id == id);
            if (existingRoutes == null)
            {
                return NotFound();
            }

            existingRoutes.Date = routes.Date;
            existingRoutes.ArrivalTime = routes.ArrivalTime;
            existingRoutes.DepartureTime = routes.DepartureTime;
            existingRoutes.PlaceArrival = routes.PlaceArrival;
            existingRoutes.PlaceDeparture = routes.PlaceDeparture;
            existingRoutes.CityArrival = routes.CityArrival;
            existingRoutes.CityDeparture = routes.CityDeparture;
            existingRoutes.Cost = routes.Cost;
            existingRoutes.NumberSeats = routes.NumberSeats;
            existingRoutes.VehicleId = routes.VehicleId;
            await _context.SaveChangesAsync();
            return Ok();
        }
        /// <summary>
        /// Удаление маршрута
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteRoutes")]
        public async Task<IActionResult> DeleteRoutes(int id)
        {
            TransportRoutes? routes = _context.TransportRoute.FirstOrDefault(p => p.Id == id);
            if (routes == null)
            {
                return NotFound();
            }
            _context.TransportRoute.Remove(routes);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
