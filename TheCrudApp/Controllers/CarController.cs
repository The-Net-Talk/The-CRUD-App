using Microsoft.AspNetCore.Mvc;
using TheCrudApp.Database.Repositories;
using TheCrudApp.Models.Dto;

namespace TheCrudApp.Controllers;

[ApiController]
[Route("api/v{verion:apiVersion}/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class CarController : ControllerBase
{
    private readonly ICarRepository _carRepository;

    public CarController(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }
    
    /// <summary>
    /// Get a car by Id
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CarDto))]
    [HttpGet("{id:guid}", Name = "GetCarById")]
    public async Task<IActionResult> GetCarById(Guid id)
    {
       var car = await _carRepository.GetCarById(id);
        
        return Ok(car);
    }
}