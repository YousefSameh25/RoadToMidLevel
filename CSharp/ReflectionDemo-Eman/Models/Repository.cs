using System;
using System.Collections.Generic;

namespace ReflectionProject.Models
{
    public class Repository<T>
    {
        private List<T> _items = new List<T>();
        
        public void Add(T item)
        {
            _items.Add(item);
        }
        
        public List<T> GetAll()
        {
            return _items;
        }
        
        public T GetById<TId>(TId id) where TId : IEquatable<TId>
        {
            return _items.Count > 0 ? _items[0] : default;
        }
        
        public void Clear()
        {
            _items.Clear();
        }
        
        public int Count => _items.Count;
    }
    public class PersonRepository : Repository<Person>
    {
        public List<Person> FindByLastName(string lastName)
        {
            var result = new List<Person>();
            foreach (var person in GetAll())
            {
                if (person.LastName == lastName)
                {
                    result.Add(person);
                }
            }
            return result;
        }
    }
}
