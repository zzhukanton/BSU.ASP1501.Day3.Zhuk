using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Task1;

namespace Task1.Tests
{
    [TestFixture]
    public class PolynomialTests
    {
        [TestCase(new double[] {1, 4, 6}, Result = "+1x^0+4x^1+6x^2")]
        [TestCase(new double[] { 1, -4, -6.01 }, Result = "+1x^0-4x^1-6,01x^2")]
        [TestCase(null, ExpectedException = typeof(NullReferenceException))]
        public string ToStringTest(double[] c)
        {
            Polynomial p = new Polynomial(c);
            return p.ToString();
        }

        [TestCase(new double[] {1, 4, 7}, 2, Result = 7)]
        [TestCase(new double[] {1, 4, 7}, 4, ExpectedException = typeof(IndexOutOfRangeException))]
        public double IndexatorTest(double[] c, int i)
        {
            Polynomial p = new Polynomial(c);
            return p[i];
        }


    }
}
