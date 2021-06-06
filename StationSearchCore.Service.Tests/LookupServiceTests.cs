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
            var repo = new Mock<IStationRepository>();
            repo.Setup(x => x.GetAll()).Returns(expected);
            var s = new LookupService(repo.Object);

            // act
            var r = s.GetAllStartingWith("Z");

            // assert
            Assert.That(r, Is.EqualTo(expected));
        }
    }
}
