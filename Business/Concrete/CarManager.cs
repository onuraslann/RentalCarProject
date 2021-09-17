using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Validation;
using Core.Utilities.Business;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }
        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            IResult result = BusinessRules.Run(CheckIfBrandIdCount(car.BrandId), CheckIfColorCount(car.ColorId));
            if (result != null)
            {
                return result;
            }
            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll());
        }

        public IDataResult<List<Car>> GetByBrand(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(x=>x.BrandId==id));
        }

        public IDataResult<List<CarDtoDetails>> GetByDto()
        {
            return new SuccessDataResult<List<CarDtoDetails>>(_carDal.GetByDto());
        }
        private IResult CheckIfBrandIdCount(int brandId)
        {
            var result = _carDal.GetAll(x => x.BrandId == brandId).Count;
            if (result > 10)
            {
                return new ErrorResult(Messages.CheckIfBrandIdCount);
            }
            return new SuccessResult();
        }
        private IResult CheckIfColorCount(int colorId)
        {
            var result = _carDal.GetAll(x => x.BrandId == colorId).Count;
            if (result > 10)
            {
                return new ErrorResult(Messages.CheckIfColorCount);
            }
            return new SuccessResult();
        }
    }
}
