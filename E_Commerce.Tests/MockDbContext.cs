using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Tests
{
    public class MockDbContext<T> where T : DbContext
    {
        public DbSet<TObject> GetMockDbSet<TObject>(IEnumerable<TObject> list) where TObject : class
        {
            var queryableList = list.AsQueryable();
            var mockSet = new Mock<DbSet<TObject>>();
            mockSet.As<IQueryable<TObject>>().Setup(m => m.Provider).Returns(queryableList.Provider);
            mockSet.As<IQueryable<TObject>>().Setup(m => m.Expression).Returns(queryableList.Expression);
            mockSet.As<IQueryable<TObject>>().Setup(m => m.ElementType).Returns(queryableList.ElementType);
            mockSet.As<IQueryable<TObject>>().Setup(m => m.GetEnumerator()).Returns(() => queryableList.GetEnumerator());

            return mockSet.Object;
        }

        public Mock<T> GetMockContext()
        {
            return new Mock<T>();
        }
    }
}
