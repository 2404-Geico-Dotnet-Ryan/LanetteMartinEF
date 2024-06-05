class PersonServices
{
    /*
    Services Performed :
        - Add a New Person (Pet Parents only at this time)
        - Look Up a Pet Parent by Phone Number 
        - Look Up a Pet Parent by PersonId
        - Get a list of All Person in the system

    */
    PersonRepo pr;

    public PersonServices(PersonRepo personRepo)
    {
        pr = personRepo; 
    }

    /***********************************************/
    /* Method Name - AddNewPerson                  */
    /* Inputs      - Person Object containing data */
    /*               for new Person                */
    /* Returns     - Person Object containing data */
    /*               for Person justed added        */
    /***********************************************/
    public Person? AddNewPerson(Person p)
    {
        pr.AddPerson(p);
        pr.Save();
        Person newlyAdded = LookUpPetParent(p.PhoneNum);
        return newlyAdded;
    }
    
    /***********************************************/
    /* Method Name - LookUpPetParent               */
    /* Inputs      - Phone Number                  */
    /* Returns     - Person Object found           */
    /***********************************************/
    public Person? LookUpPetParent(string phoneNumber)
    {
        /* Look thru all users for a match to UserName or PhoneNumber*/ 
        List<Person> allPersons = pr.GetAllPersons();

        foreach (Person person in allPersons)
        {
            if (person.PhoneNum == phoneNumber)
            {
                return person;
            }
        } 

        return null; 
    }

    /***********************************************/
    /* Method Name - GetPerson                     */
    /* Inputs      - PersonId                      */
    /* Returns     - Located Person                */
    /***********************************************/
    public Person? GetPerson(int id)
    {
        Person getPerson = pr.GetPerson(id);
        return getPerson; 
    }

    /***********************************************/
    /* Method Name - GetAllPerson                  */
    /* Inputs      - No Input                      */
    /* Returns     - List of all Person in system  */
    /***********************************************/
    public List<Person> GetAllPerson()
    {
        /* Get All Person in system */
        List<Person> allPerson = pr.GetAllPersons();

        return allPerson;
    }

   
}