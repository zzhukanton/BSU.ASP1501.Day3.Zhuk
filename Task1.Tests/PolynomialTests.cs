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
        [TestCase(new double[] { 0, 5, 0}, Result = "+5x^1")]
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

        [TestCase(new double[] {1, 0, -4}, new double[] { 1.6, -5.7, 9, 2}, Result = "+2,6x^0-5,7x^1+5x^2+2x^3")]
        public string OperatorPlusTest(double[] coefs1, double[] coefs2)
        {
            Polynomial p1 = new Polynomial(coefs1);
            Polynomial p2 = new Polynomial(coefs2);

            return (p1 + p2).ToString();
        }

        [TestCase(new double[] { 1, 0, -4 }, new double[] { 1.6, -5.7, 9, 2 }, Result = "-0,6x^0+5,7x^1-13x^2-2x^3")]
        public string OperatorMinusTest(double[] coefs1, double[] coefs2)
        {
            Polynomial p1 = new Polynomial(coefs1);
            Polynomial p2 = new Polynomial(coefs2);

            return (p1 - p2).ToString();
        }

        [TestCase(new double[] {2, -5, 8}, 1.5, Result = "+3x^0-7,5x^1+12x^2")]
        public string OperatorMultiplyTest(double[] coefs, double k)
        {
            Polynomial p = new Polynomial(coefs);

            return (p * k).ToString();
        }

        [TestCase(new double[] {1, -1, 6.3}, new double[] {1, -1, 6.3}, Result = true)]
        [TestCase(new double[] { 1, -1, 6.3 }, new double[] { 1, -1, 6.4 }, Result = false)]
        public bool OperatorEqualsTest(double[] coefs1, double[] coefs2)
        {
            Polynomial p1 = new Polynomial(coefs1);
            Polynomial p2 = new Polynomial(coefs2);

            return p1 == p2;
        }

        [TestCase(new double[] { 1, -1, 6.3 }, new double[] { 1, -1, 6.3 }, Result = false)]
        [TestCase(new double[] { 1, -1, 6.3 }, new double[] { 1, -1, 6.4 }, Result = true)]
        public bool OperatorNotEqualsTest(double[] coefs1, double[] coefs2)
        {
            Polynomial p1 = new Polynomial(coefs1);
            Polynomial p2 = new Polynomial(coefs2);

            return p1 != p2;
        }

        [TestCase(new double[] {1, 1, 1}, Result = "+7x^0+7x^1+7x^2")]
        public string AddMethod_SameAsOperatorPlus(double[] coefs)
        {
            Polynomial p = new Polynomial(coefs);

            return p.Add(new Polynomial(6, 6, 6)).ToString();
        }

        [TestCase(new double[] { 1, 1, 1 }, Result = "-5x^0-5x^1-5x^2")]
        public string SubtractMethod_SameAsOperatorMinus(double[] coefs)
        {
            Polynomial p = new Polynomial(coefs);

            return p.Subtract(new Polynomial(6, 6, 6)).ToString();
        }

        [TestCase(new double[] {5, 2, 1.8}, 2, Result = 16.2)]
        [TestCase(new double[] { 0, 2, 1.8 }, 0, Result = 0)]
        public double GetValueTest(double[] coefs, double value)
        {
            Polynomial p = new Polynomial(coefs);

            return p.GetValue(value);
        }

        [TestCase(new double[] { 2, 0, -7, 9 }, 9, Result = "+2x^0-7x^2+9x^3+9x^4")]
        public string AddItemTest(double[] coefs, double item)
        {
            Polynomial p = new Polynomial(coefs);

            p.AddItem(item);

            return p.ToString();
        }

        [TestCase(new double[] { 2, 0, -7, 9 }, new double[] { 2, 0, -7, 9 }, Result = true)]
        [TestCase(new double[] { 2, 0, -7, 9 }, new double[] { 2, 0, -7, 8 }, Result = false)]
        public bool EqualsWithPolynomTest(double[] coefs1, double[] coefs2)
        {
            Polynomial p1 = new Polynomial(coefs1);
            Polynomial p2 = new Polynomial(coefs2);

            return p1.Equals(p2);
        }

        [TestCase()]
        public void EqualsWithObjectTest()
        {
            object c = new object();
            Polynomial p = new Polynomial(1);
            object sameRef = p;

            Assert.AreEqual(false, p.Equals(c));
            Assert.AreEqual(false, p.Equals(null));
            Assert.AreEqual(true, p.Equals(sameRef));
        }

        [TestCase(new double[] { 2, 0, -7, 9 }, new double[] { 2, 0, -7, 9 }, Result = true)]
        [TestCase(new double[] { 5.3, 9 }, new double[] { 5.3, 9 }, Result = true)]
        public bool GetHashCodeEqualityTest(double[] coefs1, double[] coefs2)
        {
            Polynomial p1 = new Polynomial(coefs1);
            Polynomial p2 = new Polynomial(coefs2);

            return p1.GetHashCode() == p2.GetHashCode();
        }
    }
}
