namespace PeopleManagementApi.Services
{
    public interface IPeopleRepository
    {
        Person Add(Person newPerson);
        IEnumerable<Person> GetAll();
        Person GetById(int id);
        void Delete(int id);
    }
}
