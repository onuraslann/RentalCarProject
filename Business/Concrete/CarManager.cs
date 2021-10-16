using Business.Abstract;
using Business.BusinessAspects;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Core.Aspects.Transaction;
using Core.Aspects.Validation;
using Core.Utilities.Business;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }
        //[SecuredOperation("admin,editör")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
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
        [TransactionScopeAspect]
        public IResult AddTransactionOperation(Car car)
        {
            _carDal.Update(car);
            _carDal.Add(car);
            return new SuccessResult(Messages.TransactionsCars);
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }

         [CacheAspect]
         [PerformanceAspect(interval:5)]
        public IDataResult<List<Car>> GetAll()
        {
            Thread.Sleep(6000);
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

        public IResult Update(Car car)
        {
            _carDal.Update(car);

            return new SuccessResult();
        }

        private IResult CheckIfBrandIdCount(int brandId)
        {
            var result = _carDal.GetAll(x => x.BrandId == brandId).Count;
            if (result > 10)
            {
                return new ErrorResult(Messages.CheckIfBrandIdCount);
            }
            return new SuccessResult(Messages.CarUpdated);
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
