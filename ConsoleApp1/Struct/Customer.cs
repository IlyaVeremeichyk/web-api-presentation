using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Struct
{
    public struct Customer
    {
        #region Fields

        private readonly int _id;
        private readonly string _name;
        private readonly string _lastName;
        #endregion

        #region Constructors

        public Customer(int id, string name, string lastName)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id can't be 0 or less");
            }
            this._id = id;

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name can't be null, empty or white spaces");
            }
            this._name = name;

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Last name can't be null, empty or white spaces");
            }
            this._lastName = lastName;
        }
        #endregion

        #region Properties

        public int Id
        {
            get
            {
                return this._id;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
        }

        public string LastName
        {
            get
            {
                return this._lastName;
            }
        }
        #endregion

        #region Public methods


        public override bool Equals(object obj)
        {
            if (!(obj is Customer || obj == null)) {
                return false;
            }
            return this.Equals((Customer)obj);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        public override string ToString()
        {
            return $"Customer {this._name} {this._lastName} with id {this._id}";
        }

        public bool Equals(Customer other)
        {
            return this._id == other._id;
        }

        public bool EqualsBySelector<TKey>(Customer other, Func<Customer, TKey> keySelector) where TKey : IEquatable<TKey>
        {
            if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            return EqualsCustormersBySelector(this, other, keySelector);
        }

        public int CompareTo(Customer other)
        {
            return CompareCustomersBySelector(this, other, c => c.Name);
        }

        public int CompareBySelector<TKey>(Customer other, Func<Customer, TKey> keySelector) where TKey : IComparable<TKey>
        {
            if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            return CompareCustomersBySelector(this, other, keySelector);
        }

        #region Operators
        public static bool operator ==(Customer first, Customer second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Customer first, Customer second)
        {
            return !first.Equals(second);
        }

        public static bool operator <(Customer first, Customer second)
        {
            return CompareCustomersBySelector(first, second, c => c.Name) < 0;
        }

        public static bool operator <=(Customer first, Customer second)
        {
            return CompareCustomersBySelector(first, second, c => c.Name) <= 0;
        }

        public static bool operator >(Customer first, Customer second)
        {
            return CompareCustomersBySelector(first, second, c => c.Name) > 0;
        }

        public static bool operator >=(Customer first, Customer second)
        {
            return CompareCustomersBySelector(first, second, c => c.Name) >= 0;
        }
        #endregion
        #endregion

        #region Private methods
        
        private static int CompareCustomersBySelector<TKey>(Customer first, Customer second, Func<Customer, TKey> keySelector) where TKey : IComparable<TKey>
        {
            return keySelector(first).CompareTo(keySelector(second));
        }

        private static bool EqualsCustormersBySelector<TKey>(Customer first, Customer second, Func<Customer, TKey> keySelector) where TKey : IEquatable<TKey>
        {
            return keySelector(first).Equals(keySelector(second));
        }
        #endregion
    }
}
