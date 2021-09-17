using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EntityRepositoryBase<Car, RentalContext>, ICarDal
    {
        public List<CarDtoDetails> GetByDto()
        {
            using (RentalContext context=new RentalContext())
            {
                var result = from brand in context.Brands
                             join car in context.Cars on brand.BrandId equals car.BrandId
                             join color in context.Colors on car.ColorId equals color.ColorId
                             select new CarDtoDetails
                             {
                                  BrandName=brand.BrandName,
                                  ColorName=color.ColorName,
                                  ModelYear=car.ModelYear,
                                  DailyPrice=car.DailyPrice,
                                  Id=car.Id
                             };
                return result.ToList();
            }
            
        }
    }
}
