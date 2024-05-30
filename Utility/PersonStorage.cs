class PersonStorage
{
    public Dictionary<int, Person> persons;
    public int IdCounter = 1;

    public PersonStorage()
    {
        /* Create Vet Employees */
        Person person1 = new Person(IdCounter++, 1, "Dr. Charlie", "Ho", "123-555-1515", "Head Vet", "drvet", "123456", 1);
        Person person2 = new Person(IdCounter++, 1, "Lorraine", "Martin", "123-555-1515", "Vet Tech", "techvet", "123456", 1);
        Person person3 = new Person(IdCounter++, 1, "Sally", "Smith", "123-555-1515", "Groomer", "groomer", "123456", 2);

        /* Create Pet Parents */
        Person person4 = new Person(IdCounter++, 2, "Lilcow", "Moo", "123-555-1010", "PetParent", "parent1", "123456", 2);
        Person person5 = new Person(IdCounter++, 2, "TMouse", "Rat", "123-555-2020", "PetParent", "parent2", "123456", 2);

        /* Add the new Person Objects to the dictionary */
        persons = [];
        persons.Add(person1.PersonId, person1);
        persons.Add(person2.PersonId, person2);
        persons.Add(person3.PersonId, person3);
        persons.Add(person4.PersonId, person4);
        persons.Add(person5.PersonId, person5);
    }

}