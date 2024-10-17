using Microsoft.EntityFrameworkCore;
using TheCrudApp.Exceptions;
using TheCrudApp.Models.Dto;

namespace TheCrudApp.Database.Repositories;

public class CarRepository : ICarRepository
{
    private readonly DatabaseContext _databaseContext;

    public CarRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public async Task<CarDto> GetCarByIdAsync(Guid carId)
    {
        var car = await _databaseContext.Cars.FirstOrDefaultAsync(c => c.Id == carId);

        if (car is null)
        {
            throw new NotFoundException("car not found");
        }
        
        return new CarDto
        {
            Id = car.Id,
            Model = car.Model,
            Price = car.Price,
            Year = car.Year
        };
    }
}

public interface ICarRepository
{
    Task<CarDto> GetCarByIdAsync(Guid carId);
}