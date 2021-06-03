using Moq;
using NUnit.Framework;
using StationSearchCore.Domain.Interfaces;
using System.Collections.Generic;

namespace StationSearchCore.Service.Tests
{
    [TestFixture]
    public class LookupServiceTests
    {
        [Test]
        public void SearchForXShouldReturnExpectedResults()
        {
            // arrange
            var expected = new List<string>
            {
                "ZZZ"
            };
            var tree = new Mock<IPrefixTree>();
            tree.Setup(x => x.Find(It.IsAny<string>())).Returns(expected);
            var s = new LookupService(tree.Object);

            // act
            var r = s.GetAllStartingWith("Z");

            // assert
            Assert.That(r, Is.EqualTo(expected));
        }
    }
}
