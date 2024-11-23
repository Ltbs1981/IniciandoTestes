namespace IniciandoTestes.Servicos
{
    using System;
    public class Calculadora
    {
        public double SomarNumeros(double n1, double n2)
        {
            if (n1 < 0 || n2 < 0)
            {
                return -1;
            }

            return n1 + n2;
        }
    }
}
