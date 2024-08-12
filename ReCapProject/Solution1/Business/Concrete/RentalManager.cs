﻿using Business.Abstract;
using Core.Utilities;
using DataAccess.Abstract;
using DataAccess.Console.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }
        public IResult Add(Rental rental)
        {
            _rentalDal.Add(rental);
            return new SuccessResult("Yuklenme Tamamlandi");
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult("Silinme Tamamlandi");
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new DataResult<List<Rental>>(_rentalDal.GetAll(), true, "Brandlarin Siyahisi");
        }


        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);

            return new SuccessResult("Mehsul Ugurla Deyisdirildi");
        }

        IDataResult<Rental> IRentalService.GetById(int rentalId)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r=> r.RentalId == rentalId));
        }
    }

}
