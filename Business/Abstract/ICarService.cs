using Core.Utilities.Result;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService
    {
        IDataResult<List<Car>> GetAll();

        IResult Add(Car car);
        IDataResult<List<Car>> GetByBrand(int id);
        IResult Delete(Car car);
        IDataResult<List<CarDtoDetails>> GetByDto();
        IResult AddTransactionTest(Car car);
    }
}
