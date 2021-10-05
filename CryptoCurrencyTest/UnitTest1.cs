using CryptoCurrency;
using System;
using Xunit;

namespace CryptoCurrencyTest
{
    public class UnitTest1
    {
        [Fact]
        public void Can_Convert_Currencies()
        {
            // Arrange 
            var sut = new Converter();
            sut.SetPricePerUnit("Bitcoin", 873.27);
            sut.SetPricePerUnit("Litecoin", 743.79);

            // Act
            var actual = sut.Convert("Bitcoin", "Litecoin", 2.2).ToString().Substring(0, 4);

            // Assert
            Assert.Equal("2,58", actual);
        }


        [Fact]
        public void Price_Below_Zero_Throws_Exception()
        {
            // Arrange 
            var sut = new Converter();
            sut.SetPricePerUnit("ValidCurrency", 1);
            sut.SetPricePerUnit("InvalidCurrency", -1);

            // Act
            Action act = () => sut.Convert("ValidCurrency", "InvalidCurrency", 1);
            
            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void Old_Price_Should_Be_Overwritten_By_New_Price()
        {
            // Arrange 
            var sut = new Converter();
            sut.SetPricePerUnit("Peercoin", 751);
            sut.SetPricePerUnit("Peercoin", 641.09);
            sut.SetPricePerUnit("Litecoin", 743.79);

            // Act
            var actual = sut.Convert("Peercoin", "Litecoin", 1).ToString().Substring(0, 4);

            // Assert
            Assert.Equal("0,86", actual);
        }
    }
}
