namespace WebApplication2.Data
{
    public class UnitofWork
    {
        private PersonRepository personRepository;
        public UnitofWork(PersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        public PersonRepository People
        {
            get
            {
                //if (personRepository == null)
                //    personRepository = new PersonRepository();
                return personRepository;
            }
        }
    }
}
