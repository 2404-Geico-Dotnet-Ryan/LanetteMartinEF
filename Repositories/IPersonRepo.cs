interface IPersonRepo
{
public void AddPerson(Person pr);

public Person? GetPerson(int id);

public List<Person> GetAllPersons();

public void Save();
}