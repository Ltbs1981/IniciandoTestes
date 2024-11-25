using Xunit;
using IniciandoTestes.Servicos;

namespace IniciandoTestes.Tests
{
    public class CalculadoraTest
    {
        [Fact]
        public void CalculadoraDeveRetornarNegativo()
        {
            //Arrange
            Calculadora sut = new Calculadora();

            //Act
            double result = sut.SomarNumeros(0, 0);

            //Assert
            Assert.False(result > 0);
        }

        [Theory]
        [InlineData(2, 3, 5)]
        [InlineData(4, 9, 13)]
        [InlineData(32, 45, 77)]
        [InlineData(12, 3, 15)]
        [InlineData(8, 16, 24)]
        public void SomarNumero_DeveCalcularComSucesso_QuandoNumerosPositivos(double n1,
                                                                              double n2,
                                                                              double expectedResult)
        {
            //Arrange
            Calculadora sut = new Calculadora();

            //Act
            double result = sut.SomarNumeros(n1, n2);

            //Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(1, 3, 4)]
        [InlineData(5, 10, 15)]
        [InlineData(20, 50, 70)]
        public void SomarNumeros_DeveFuncionar_QuandoN1MenorQueN2(double n1, double n2, double expectedResult)
        {
            //Arrange
            Calculadora sut = new Calculadora();

            //Act
            double result = sut.SomarNumeros(n1, n2);

            //Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(3, 1, 4)]
        [InlineData(10, 5, 15)]
        [InlineData(50, 20, 70)]
        public void SomarNumeros_DeveFuncionar_QuandoN1MaiorQueN2(double n1, double n2, double expectedResult)
        {
            //Arrange
            Calculadora sut = new Calculadora();

            //Act
            double result = sut.SomarNumeros(n1, n2);

            //Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
