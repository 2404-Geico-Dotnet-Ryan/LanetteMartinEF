class PersonServices
{
    /*
    Services Performed :
        - Add a New Person (Pet Parents only at this time)
        - Look Up a Pet Parent by Phone Number 
        - Login a User 
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
         /*Will not let you register a new person if the UserName is already in use */
        List<Person> allPersons = pr.GetAllPersons();

        foreach (Person person in allPersons)
        {
            if (person.UserName == p.UserName)
            {
                Console.WriteLine("User Name is already taken");
                return null; 
            }
        }      

        /* If pass both check add the new user */
        pr.AddPerson(p);
        return LookUpPetParent(p.PhoneNum);
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
    /* Method Name - LoginUser                     */
    /* Inputs      - User Name and Password        */
    /* Returns     - Person Object found           */
    /***********************************************/
    public Person? LoginUser(string userName, string userPassword)
    {
        /* Look thru all users for a match to UserName and Password*/ 
        List<Person> allPersons = pr.GetAllPersons();

        foreach (Person person in allPersons)
        {
            if (person.UserName == userName)
            {
                if (person.UserPassword == userPassword)
                {
                    return person;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Password was invalid");
                    return null; 
                }
            }
        }  

        /* If loop found no match means we never found a match */
        Console.WriteLine();
        Console.WriteLine("Person was not found in the system");
        return null; 
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