using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Class
{
    public class Customer : IEquatable<Customer>, IComparable<Customer>
    {
        #region Fields
        private int _id;

        private string _name;

        private string _lastName;
        #endregion

        #region Constructors
        public Customer(int id, string name, string lastName)
        {
            if (id <= 0) {
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

            set
            {
                if (value > 0)
                {
                    this._id = value;
                }
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }

            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this._name = value;
                }
            }
        }

        public string LastName
        {
            get
            {
                return this._lastName;
            }

            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this._lastName = value;
                }
            }
        }
        #endregion

        #region Public methods

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Customer);
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
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }
            return this._id == other._id;
        }

        public bool EqualsBySelector<TKey>(Customer other, Func<Customer, TKey> keySelector) where TKey : IEquatable<TKey>
        {
            ValidateCustomer(other, nameof(other));
            if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            return EqualsCustormersBySelector(this, other, keySelector);
        }

        public int CompareTo(Customer other)
        {
            ValidateCustomer(other, nameof(other));
            return CompareCustomersBySelector(this, other, c => c.Name);
        }

        public int CompareBySelector<TKey>(Customer other, Func<Customer, TKey> keySelector) where TKey : IComparable<TKey>
        {
            ValidateCustomer(other, nameof(other));
            if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            return CompareCustomersBySelector(this, other, keySelector);
        }

        #region Operators
        public static bool operator ==(Customer first, Customer second)
        {
            ValidateCustomer(first, nameof(first));
            ValidateCustomer(second, nameof(second));
            return first.Equals(second);
        }

        public static bool operator !=(Customer first, Customer second)
        {
            return !first.Equals(second);
        }

        public static bool operator <(Customer first, Customer second)
        {
            ValidateCustomer(first, nameof(first));
            ValidateCustomer(second, nameof(second));
            return CompareCustomersBySelector(first, second, c => c.Name) < 0;
        }

        public static bool operator <=(Customer first, Customer second)
        {
            ValidateCustomer(first, nameof(first));
            ValidateCustomer(second, nameof(second));
            return CompareCustomersBySelector(first, second, c => c.Name) <= 0;
        }

        public static bool operator >(Customer first, Customer second)
        {
            ValidateCustomer(first, nameof(first));
            ValidateCustomer(second, nameof(second));
            return CompareCustomersBySelector(first, second, c => c.Name) > 0;
        }

        public static bool operator >=(Customer first, Customer second)
        {
            ValidateCustomer(first, nameof(first));
            ValidateCustomer(second, nameof(second));
            return CompareCustomersBySelector(first, second, c => c.Name) >= 0;
        }
        #endregion

        #endregion

        #region Private methods
        private static void ValidateCustomer(Customer customer, string argumentName)
        {
            if (object.ReferenceEquals(customer, null))
            {
                throw new ArgumentNullException(argumentName);
            }
        }

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
