class PetServices
{
    /*
    Services Performed :
        - Get a list of All Pet in the system
        - Get a list of All Pets belonging to a Person
        - Get a list of ALL Records belonging to a Pet   
        - Add a Pet
        - Update a Pet
        - Get a Pet
    */

    PetRepo pr;

    public PetServices(PetRepo petRepo)
    {
        pr = petRepo; 
    }

    /***********************************************/
    /* Method Name - GetAllPet                     */
    /* Inputs      - No Input                      */
    /* Returns     - List of all Pet in system     */
    /***********************************************/
    public List<Pet> GetAllPet()
    {
        /* Get All Pet in system */
        List<Pet> allPets = pr.GetAllPets();

        return allPets; 
        
    }

    /***********************************************/
    /* Method Name - GetPersonsPets                */
    /* Inputs      - PersonId                      */
    /* Returns     - List of all Pets belonging to */
    /*               Person                        */
    /***********************************************/
    public List<Pet> GetPersonsPets(int id)
    {
        /* Get All Pets in system */
        List<Pet> allPets = pr.GetAllPets();

        /* Filter out only Pets belonging to Person */
        List<Pet> allPersonsPets = new();

        /* Filter out only Pets matching PersonId passed in */
        foreach (Pet pet in allPets)
        {
            if(pet.PersonId == id)
            {
                allPersonsPets.Add(pet);
            }
        }

        /* Return list of Pets belonging to Person */
        return allPersonsPets; 
    }

    /***********************************************/
    /* Method Name - GetPetsRecords                */
    /* Inputs      - PetId                         */
    /* Returns     - List of all records belonging */
    /*               to Pet                        */
    /***********************************************/
    public List<Pet> GetPetsRecords(int id)
    {
        /* Get All Pets in system */
        List<Pet> allPets = pr.GetAllPets();

        /* Filter out only records belonging to Pet */
        List<Pet> allPetsRecord = new();

        /* Filter out only Pets matching PetId passed in */
        foreach (Pet pet in allPets)
        {
            if(pet.PetId == id)
            {
                allPetsRecord.Add(pet);
            }
        }

        /* Return list of records belonging to Pet */
        return allPetsRecord; 
    }

    /***********************************************/
    /* Method Name - AddPet                        */
    /* Inputs      - Pet                           */
    /* Returns     - Newly Added pet               */
    /***********************************************/
    public Pet? AddPet(Pet p)
    {
        pr.AddPet(p); 
        pr.Save();
        return p;
    }

    /***********************************************/
    /* Method Name - GetPet                        */
    /* Inputs      - PetId                         */
    /* Returns     - Located Pet                   */
    /***********************************************/
    public Pet? GetPet(int id)
    {
        Pet getPet = pr.GetPet(id);
        return getPet; 
    }

    /***********************************************/
    /* Method Name - UpdatePet                     */
    /* Inputs      - PetId                         */
    /* Returns     - Located Pet                   */
    /***********************************************/
    public Pet? UpdatePet(Pet p)
    {
        pr.UpdatePet(p);
        pr.Save();
        return p; 
    }
}