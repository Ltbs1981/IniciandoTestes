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

        //Idepotente - Repetir uma a��o inumeras vezes e sempre terei o mesmo resultado
        //A.C.I.D

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
    }
}
