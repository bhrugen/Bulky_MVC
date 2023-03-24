using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IProductImageRepository : IRepository<ProductImage>
    {
        void Update(ProductImage obj);
    }
}
