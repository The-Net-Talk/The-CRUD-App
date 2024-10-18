using Microsoft.EntityFrameworkCore;
using TheCrudApp.Database.Entities;
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
    
    public async Task<CarDto> GetCarById(Guid carId)
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
    public async Task<CarDto> CreateCar(string model, decimal price, int year)
    {
        var car = new Car
        {
            Id = Guid.NewGuid(),
            Model = model,
            Price = price,
            Year = year
        };

        await _databaseContext.Cars.AddAsync(car);
        await _databaseContext.SaveChangesAsync();

        var carDTO = new CarDto()
        {
            Id = car.Id,
            Model = car.Model,
            Price = car.Price,
            Year = car.Year
        };
        return carDTO;
    }
}

public interface ICarRepository
{
    Task<CarDto> GetCarById(Guid carId);
    Task<CarDto> CreateCar(string model, decimal price, int year);
}