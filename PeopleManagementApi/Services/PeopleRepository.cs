namespace PeopleManagementApi.Services
{
    public record Person(int Id, string FirstName, string LastName, string Email);
    public class PeopleRepository : IPeopleRepository
    {
        private List<Person> People { get; } = new();
        public Person Add(Person newPerson) 
        { 
            People.Add(newPerson); 
            return newPerson; 
        }

        public IEnumerable<Person> GetAll() => People;

        public Person GetById(int id) => People.FirstOrDefault(p => p.Id == id);

        public void Delete(int id)
        {
            var personToDelete = GetById(id);
            if (personToDelete == null)
            {
                throw new ArgumentException("Invalid person");
            }
            People.Remove(personToDelete);
        }
    }
}
