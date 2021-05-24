using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace StationSearchCore.Service.Tests
{
    [TestFixture]
    public class LookupServiceTests
    {
        [Test]
        public void SearchForXShouldReturnExpectedResults()
        {
            // arrange
            var s = new LookupService();
            var expected = new List<string>
            {
                "ZZZ"
            };

            // act
            var r = s.GetAllStartingWith("Z");

            // assert
            Assert.That(r, Is.EqualTo(expected));
        }
    }
}
