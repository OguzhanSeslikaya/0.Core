using _0.Core.Exceptions;
using _0.Core.Extensions;
using _0.Core.Test.ExtensionsTests.Dtos;

namespace _0.Core.Test.ExtensionsTests
{
    public class IQueryableExtensionsTests
    {
        [Fact]
        public void ApplyOrderBy_ShouldSortAscending_WhenValidPropertyGiven()
        {
            var data = new List<IQueryableExtensionsApplyOrderByTestClass>
            {
                new IQueryableExtensionsApplyOrderByTestClass { Id = 3, Name = "C", CreatedDate = DateTime.UtcNow },
                new IQueryableExtensionsApplyOrderByTestClass { Id = 1, Name = "A", CreatedDate = DateTime.UtcNow.AddDays(-1) },
                new IQueryableExtensionsApplyOrderByTestClass { Id = 2, Name = "B", CreatedDate = DateTime.UtcNow.AddDays(-2) }
            }.AsQueryable();

            var result = data.ApplyOrderBy("Id", ascending: true).ToList();

            Assert.Equal(1, result[0].Id);
            Assert.Equal(2, result[1].Id);
            Assert.Equal(3, result[2].Id);
        }

        [Fact]
        public void ApplyOrderBy_ShouldSortDescending_WhenValidPropertyGiven()
        {
            var data = new List<IQueryableExtensionsApplyOrderByTestClass>
            {
                new IQueryableExtensionsApplyOrderByTestClass { Id = 3, Name = "C", CreatedDate = DateTime.UtcNow },
                new IQueryableExtensionsApplyOrderByTestClass { Id = 1, Name = "A", CreatedDate = DateTime.UtcNow.AddDays(-1) },
                new IQueryableExtensionsApplyOrderByTestClass { Id = 2, Name = "B", CreatedDate = DateTime.UtcNow.AddDays(-2) }
            }.AsQueryable();

            var result = data.ApplyOrderBy("Id", ascending: false).ToList();

            Assert.Equal(3, result[0].Id);
            Assert.Equal(2, result[1].Id);
            Assert.Equal(1, result[2].Id);
        }

        [Fact]
        public void ApplyOrderBy_ShouldThrowException_WhenInvalidPropertyGiven()
        {
            var data = new List<IQueryableExtensionsApplyOrderByTestClass>
            {
                new IQueryableExtensionsApplyOrderByTestClass { Id = 1, Name = "A", CreatedDate = DateTime.UtcNow }
            }.AsQueryable();

            Assert.Throws<HttpStatusCodeException>(() => data.ApplyOrderBy("dddd", true).ToList());
        }

        [Fact]
        public void ApplyOrderBy_ShouldReturnSameList_WhenPropertyNameIsNullOrEmpty()
        {
            var data = new List<IQueryableExtensionsApplyOrderByTestClass>
            {
                new IQueryableExtensionsApplyOrderByTestClass { Id = 2, Name = "B", CreatedDate = DateTime.UtcNow },
                new IQueryableExtensionsApplyOrderByTestClass { Id = 1, Name = "A", CreatedDate = DateTime.UtcNow.AddDays(-1) }
            }.AsQueryable();

            var result = data.ApplyOrderBy("", true).ToList();

            Assert.Equal(data.ToList(), result);
        }
    }
}
