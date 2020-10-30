using System;
using ClassLibraryForYa.Contracts;
using ClassLibraryForYa.Exceptions;
using ClassLibraryForYa.Services;
using NUnit.Framework;

namespace ClassLibraryForYa.Tests
{
    public class GetNumbersServiceTests
    {
        private IGetNumbersService _tested;

        [SetUp]
        public void Setup()
        {
            _tested = new GetNumbersService();
        }

        [Test]
        [TestCase(1, 2)]
        [TestCase(100, 200)]
        [TestCase(-3, -1)]
        public void CreateRange_WhenRangeValuesCorrect_ReturnRangeId(int minValue, int maxValue)
        {
            Assert.DoesNotThrow(() => _tested.CreateRange( minValue, maxValue ));
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(1, 0)]
        public void CreateRange_WhenRangeValuesInCorrect_ThrowsArgumentException( int minValue, int maxValue )
        {
            Assert.Throws<ArgumentException>(() => _tested.CreateRange(minValue, maxValue));
        }

        [Test]
        [TestCase( 1, 2 )]
        public void GetNumber_WhenRangeCreatedAndValueAllowed_ReturnNumber(int minValue, int maxValue )
        {
            var rangeId = _tested.CreateRange( minValue, maxValue );

            var value = _tested.GetNumber( rangeId );

            Assert.IsTrue( value >= minValue && value <= maxValue );
        }

        [Test]
        [TestCase(1, 2)]
        public void GetNumber_WhenRangeCreatedAndValueAllowed_ReturnDifferentNumbers(int minValue, int maxValue)
        {
            var rangeId = _tested.CreateRange(minValue, maxValue);

            var value1 = _tested.GetNumber(rangeId);
            var value2 = _tested.GetNumber(rangeId);

            Assert.IsTrue(value1 != value2);
        }

        [Test]
        public void GetNumber_WhenRangeDoesNotCreated_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>( () => _tested.GetNumber(0));
        }

        [Test]
        public void GetNumber_WhenRangeDoesNotContainAvailableNumbers_ThrowsNotAvailableValuesInRangeException()
        {
            var rangeId = _tested.CreateRange( 1, 2 );

            _tested.GetNumber( rangeId );
            _tested.GetNumber( rangeId );

            Assert.Throws<NotAvailableValuesInRangeException>( () => _tested.GetNumber( rangeId ) );
        }

        [Test]
        [TestCase(1, 4)]
        public void FreeNumber_WhenArgumentsCorrect_DoesNotThrow( int minValue, int maxValue )
        {
            var rangeId = _tested.CreateRange( minValue, maxValue );

            var number = _tested.GetNumber( rangeId );

            Assert.DoesNotThrow( () => _tested.FreeNumber( number, rangeId ) );
        }

        [Test]
        [TestCase(1, 4, 2)]
        public void FreeNumber_WhenNumberAllowed_ThrowsArgumentException(int minValue, int maxValue, int number)
        {
            var rangeId = _tested.CreateRange(minValue, maxValue);

            Assert.Throws<ArgumentException>(() => _tested.FreeNumber(number, rangeId));
        }

        [Test]
        [TestCase(1, 4)]
        public void FreeNumber_WhenRangeIdIncorrect_ThrowsArgumentException(int minValue, int maxValue)
        {
            var rangeId = _tested.CreateRange(minValue, maxValue);

            var number = _tested.GetNumber( rangeId );

            Assert.Throws<ArgumentException>(() => _tested.FreeNumber(number, rangeId + 1));
        }
    }
}