using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ReflectionProject.Models
{
    /// <summary>
    /// Sample Person class used for reflection demonstrations
    /// </summary>
    [Serializable]
    [Description("Represents a person in the system")]
    public class Person
    {
        // Private fields
        private Guid _id;
        private string _firstName;
        private string _lastName;
        private int _age;
        private static int _instanceCount = 0;
        
        // Properties
        public Guid Id => _id;
        
        [Required]
        [MaxLength(50)]
        public string FirstName 
        { 
            get { return _firstName; }
            set 
            { 
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        
        [Required]
        [MaxLength(50)]
        public string LastName 
        { 
            get { return _lastName; }
            set 
            { 
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        
        [Range(0, 120)]
        public int Age 
        { 
            get { return _age; }
            set 
            { 
                _age = value;
                OnPropertyChanged(nameof(Age));
            }
        }
        
        public string FullName => $"{FirstName} {LastName}";
        
        public static int InstanceCount => _instanceCount;
        
        // Constructors
        public Person()
        {
            _id = Guid.NewGuid();
            _instanceCount++;
        }
        
        public Person(string firstName, string lastName) : this()
        {
            FirstName = firstName;
            LastName = lastName;
        }
        
        public Person(string firstName, string lastName, int age) : this(firstName, lastName)
        {
            Age = age;
        }
        
        // Methods
        public void Introduce()
        {
            Console.WriteLine($"Hello, my name is {FullName} and I am {Age} years old.");
        }
        
        [Obsolete("Use CalculateAgeInMonths instead")]
        public int CalculateAgeInDays()
        {
            return Age * 365;
        }
        
        public int CalculateAgeInMonths()
        {
            return Age * 12;
        }
        
        public int CalculateRetirementAge(int retirementAge)
        {
            return retirementAge - Age;
        }
        
        public static Person CreateAdult(string firstName, string lastName)
        {
            return new Person(firstName, lastName, 18);
        }
        
        // Generic method
        public T ConvertAge<T>()
        {
            if (typeof(T) == typeof(string))
                return (T)(object)Age.ToString();
            else if (typeof(T) == typeof(double))
                return (T)(object)(double)Age;
            else
                return default;
        }
        
        // Event
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        // Nested type
        public class Address
        {
            public string Street { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
            
            public override string ToString()
            {
                return $"{Street}, {City}, {Country}";
            }
        }
    }
}
