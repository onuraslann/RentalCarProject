using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EntityRepositoryBase<User, RentalContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (RentalContext context  = new RentalContext())
            {
                var result = from operationclaim in context.OperationClaims
                             join useroperationClaim in context.UserOperationClaims
                         on operationclaim.Id equals useroperationClaim.OperationClaimId
                             join users in context.Users
                             on useroperationClaim.UserId equals users.Id
                             select new OperationClaim
                             {
                                 Id = operationclaim.Id,
                                 Name = operationclaim.Name

                             };
                return result.ToList();

            }

        }
    }
}
