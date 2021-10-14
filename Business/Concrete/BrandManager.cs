using Business.Abstract;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Caching;
using Core.Utilities.Business;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }
        [SecuredOperation("admin")]
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Add(Brand brand)
        {
            IResult result = BusinessRules.Run(CheckIfNameExist(brand.BrandName));
            if (result != null)
            {
                return result;
            }
            _brandDal.Add(brand);
            return new SuccessResult(Messages.BrandAdded);
        }

        public IResult Delete(Brand brand)
        {
            _brandDal.Delete(brand);
            return new SuccessResult(Messages.BrandAdded);
        }

        [CacheAspect]
        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(), Messages.BrandList);
        }

        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Update(Brand brand)
        {
            _brandDal.Update(brand);
            return new SuccessResult();
        }

        private IResult CheckIfNameExist(string brandName)
        {
            var result = _brandDal.GetAll(x => x.BrandName == brandName).Any();
            if (result)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
    }
}
