﻿using Microsoft.AspNetCore.Mvc;
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
        /// ПОлучение списка маршрутов
        /// </summary>
        /// <returns></returns>
        [HttpGet("RoutesList")]
        public async Task<IActionResult> GetRoutes()
        {
            var routes = await _context.TransportRoute.ToListAsync();
            return Ok(JsonConvert.SerializeObject(routes));
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
