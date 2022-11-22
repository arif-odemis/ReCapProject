using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
	public class EfCustomerDal : EfEntityRepositoryBase<Customer, ReCapContext>, ICustomerDal
	{
		public List<CustomerDetailDto> GetCustomerDetails()
		{
			using (ReCapContext context = new ReCapContext())
			{
				var result = from customer in context.Customers
							 join user in context.Users
							 on customer.UserId equals user.UserId
							 select new CustomerDetailDto
							 {
								 CompanyName = customer.CompanyName,
								 CustomerFirstName = user.FirstName,
								 CustomerLastName = user.LastName,
								 CustomerEmail = user.Email
							 };
				return result.ToList();
			}
		}
	}
	}

