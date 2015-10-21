using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    /// <summary>
    /// Represents polynomial of one variable
    /// </summary>
    public class Polynomial : IEquatable<Polynomial>
    {
        #region PrivateFields
        /// <summary>
        /// Coefficients of polynom
        /// </summary>
        private double[] _coefficients;

        /// <summary>
        /// The accuracy for checking coefficients equality
        /// </summary>
        private readonly double _difference = 0.00001;
        #endregion

        #region ProperiesAndConstructers

        /// <summary>
        /// To get coefficient by index in internal array
        /// </summary>
        /// <param name="index">Index of coefficient</param>
        /// <returns>Coefficient</returns>
        public double this[int index]
        {
            get
            {
                if (index > _coefficients.Length - 1)
                    throw new IndexOutOfRangeException();

                return _coefficients[index];
            }
            set
            {
                _coefficients[index] = value;
            }
        }

        /// <summary>
        /// Length of polynomial
        /// </summary>
        public int Length
        {
            get { return _coefficients.Length; }
        }

        /// <summary>
        /// Constructor of epmty polynomial
        /// </summary>
        public Polynomial()
        {
            _coefficients = new double[0];
        }

        /// <summary>
        /// Creates polinomial with union of coefficients
        /// </summary>
        /// <param name="coefs">Coeffficients</param>
        public Polynomial(params double[] coefs)
        {
            if (coefs == null)
                throw new NullReferenceException();
            else
            {
                _coefficients = new double[coefs.Length];
                Array.Copy(coefs, _coefficients, coefs.Length);
            }
        }
        #endregion

        /// <summary>
        /// String representation of object
        /// </summary>
        /// <returns>Polynom in string</returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < Length; i++)
            {
                if (_coefficients[i] == 0)
                    continue;
                if (_coefficients[i] > 0)
                    result.Append(string.Format("+{0}x^{1}", this[i], i));
                else
                    result.Append(string.Format("{0}x^{1}", this[i], i));
            }
            return result.ToString();
        }

        #region Operators

        public static Polynomial operator +(Polynomial p1, Polynomial p2)
        {
            if (p1.Length > p2.Length)
            {
                for (int i = 0; i < p2.Length; i++)
                    p1[i] += p2[i];
                return p1;
            }
            else
            {
                for (int i = 0; i < p1.Length; i++)
                    p2[i] += p1[i];
                return p2;
            }
        }

        public static Polynomial operator -(Polynomial p1, Polynomial p2)
        {
            if (p1.Length > p2.Length)
            {
                for (int i = 0; i < p2.Length; i++)
                    p1[i] -= p2[i];
                return p1;
            }
            else
            {
                for (int i = 0; i < p1.Length; i++)
                    p1[i] -= p2[i];
                for (int i = p1.Length; i < p2.Length; i++)
                {
                    p1.AddItem(-p2[i]);
                }
                return p1;
            }
        }

        public static Polynomial operator *(Polynomial p1, double coef)
        {
            for (int i = 0; i < p1.Length; i++)
            {
                p1[i] = p1[i] * coef;
            }
            return p1;
        }

        public static bool operator ==(Polynomial left, Polynomial right)
        {
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return ReferenceEquals(left, right);
            }
            else
            {
                return left.Equals(right);
            }
        }

        public static bool operator !=(Polynomial left, Polynomial right)
        {
            if (left == null || right == null)
                return ReferenceEquals(left, right);
            else
                return !left.Equals(right);
        }
        #endregion

        #region SomeUsefulMethods
        /// <summary>
        /// Method for addition of two polynomials
        /// </summary>
        /// <param name="p">Other polynomial</param>
        /// <returns>Sum polynomial</returns>
        public Polynomial Add(Polynomial p)
        {
            return (this + p);
        }

        /// <summary>
        /// Method for subtraction of two polynomials
        /// </summary>
        /// <param name="p">Other polynomial</param>
        /// <returns>Subtract polynomial</returns>
        public Polynomial Subtract(Polynomial p)
        {
            return (this - p);
        }

        /// <summary>
        /// Calculate polynomials numerical value
        /// </summary>
        /// <param name="value">Value of polynomal's variable</param>
        /// <returns>Value of polynomial</returns>
        public double GetValue(double value)
        {
            double sum = 0;
            for (int i = 0; i < Length; i++)
            {
                sum += _coefficients[i] * Math.Pow(value, i);
            }
            return sum;
        }

        /// <summary>
        /// Adds member of polynomial with the higest degree
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(double item)
        {
            Array.Resize(ref _coefficients, Length + 1);
            _coefficients[Length - 1] = item;
        }
        #endregion

        /// <summary>
        /// Overriding version of equals
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>Equal or not</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is Polynomial)
                return Equals((Polynomial)obj);

            return false;
        }

        /// <summary>
        /// Checks polynomial's equality
        /// </summary>
        /// <param name="other">Polynomial to compare</param>
        /// <returns>Equal or not</returns>
        public bool Equals(Polynomial other)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(other, this))
                return true;

            if (this.Length != other.Length)
                return false;

            for (int i = 0; i < Length; i++)
            {
                double coefDifference = Math.Abs(this[i] - other[i]);
                if (coefDifference > _difference)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Overriding version of calculating hash code for polynomial objects
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            uint hashCode = 0;
            foreach (double coef in _coefficients)
                hashCode += (uint)coef.GetHashCode();

            return (int)hashCode % 127;
        }
    }
}