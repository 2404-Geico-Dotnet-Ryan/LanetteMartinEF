public class Person
{
    public int PersonId { get; set; } 
    public int PersonType { get; set; }  /* 1 - Employee  2 - Pet Parent  used to determine screen to display*/ 
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNum { get; set; }
    public string? JobTitle { get; set; } /* Vet, Tech, PetParent*/

    // This establishes the "one to one" relationship 
    // One Person to One Login  
    public Login Login { get; set; }

    // This establishes the "one to many" relationship 
    // One Person to Many Pets 
    public ICollection<Pet> Pets { get; set; }
    
    /* NO Argurments Constructor*/
    public Person()
    {

    }

    /* FULL Argurments Constructor */
    public Person(int personId, int personType, string firstName, string lastName, string phoneNum, string jobTitle)
    {
    PersonId = personId;
    PersonType = personType;
    FirstName = firstName; 
    LastName = lastName;  
    PhoneNum = phoneNum;
    JobTitle= jobTitle;
    }

    /* Person ToString */
    public override string ToString() 
    {
        string newString = "";
        newString += "{PersonId " + PersonId;
        newString += ", Person Type " + PersonType;
        newString += ", First Name " + FirstName;
        newString += ", Last Name " + LastName;
        newString += ", Phone Number " + PhoneNum;
        newString += ", Job Title " + JobTitle;
        newString += "}";
        return newString;
    }
}