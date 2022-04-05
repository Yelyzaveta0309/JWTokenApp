using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebApplication2.Data
{
    public class PersonRepository : IRepository<Person>
    {
        private WebApplication2Context _dbContext;

        public PersonRepository(WebApplication2Context context)
        {
            _dbContext = context;
        }

        public IEnumerable<Person> GetAll()
        {
            return _dbContext.People;
        }

        public Person Get(int id)
        {
            return _dbContext.People.Find(id);
        }

        public void Create(Person person)
        {
            _dbContext.People.Add(person);
            _dbContext.SaveChanges();
        }

        public void Update(Person person)
        {
            _dbContext.Entry(person).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            Person person = _dbContext.People.Find(id);
            if (person != null)
                _dbContext.People.Remove(person);
            _dbContext.SaveChanges();
        }
    }
}
