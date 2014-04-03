using NSubstitute;
using ToDo.Model;
using Xunit;

namespace ToDo.Tests.Model
{
    public class AssigmentCacheTests
    {
        private readonly AssigmentCache _assigmentCache;

        public AssigmentCacheTests()
        {
            _assigmentCache = new AssigmentCache();
        }

        [Fact]
        public void RaisesAssigmentAddedEventOnAssigmentAdded()
        {
            IAssigment actualAssigment = null;
            _assigmentCache.AssimentAdded += assigment => actualAssigment = assigment;

            var expectedAssigment = Substitute.For<IAssigment>();
            _assigmentCache.Add(expectedAssigment);


            Assert.Same(actualAssigment, expectedAssigment);
        }
    }
}