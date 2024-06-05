using System;
using System.Linq.Expressions;

class Program
{
    /*
    This program will track pets seen by the staff of the Kitty City Vet Office.  

    Office Staff - will be able to create new Pet Records, Update Pet Records and Close Out Pet Records
    Pet Owners   - will be able to view their pet's records and reqeust a call back from the Vet staff
    */
    static LoginServices lgs;
    static PersonServices prs;
    static PetServices pet;
    static VisitServices vis;
    static Person? loggedInUser = null;

    static string parentPhoneNum = "";

    static void Main(string[] args)
    {
        /********************************************/
        /* Getting connected to the KittyCityVet DB */
        /********************************************/
        using AppDbContext context = new();

        /* Set up all Repos to have access to the KittyCity database */
        LoginRepo lgr = new(context);
        lgs = new(lgr);

        PersonRepo prr = new(context);
        prs = new(prr);

        PetRepo ptr = new(context);
        pet = new(ptr);

        VisitRepo vir = new(context);
        vis = new(vir);

        /* Call to display the Systems Home Screen */
        HomeScreen();
    }

    public static void HomeScreen()
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("------------------------------");
        Console.WriteLine("  /\\     /\\     WELCOME TO  ");
        Console.WriteLine(" {  `---'  }       THE        ");
        Console.WriteLine(" {  O   O  }    KITTY CITY    ");
        Console.WriteLine(" ~~>  V  <~~    VET SYSTEM    ");
        Console.WriteLine("  \\  \\|/  /                 ");
        Console.WriteLine("   `-----'____                ");
        Console.WriteLine("   /     \\    \\_            ");
        Console.WriteLine("  {       }\\  )_\\_   _      ");
        Console.WriteLine("  |  \\_/  |/ /  \\_\\_/ )    ");
        Console.WriteLine("   \\__/  /(_/     \\__/      ");
        Console.WriteLine("     (__/                     ");
        Console.WriteLine("------------------------------");

        /* If Users is sucessfully logged in their 'PersonType'         */
        /* found on the DB is used to determine which screen to display */
        int typeOfUser = VetLogin();

        switch (typeOfUser)
        {
            case 1:
                {
                    VetStaffOptions();
                    break;
                }
            case 2:
                {
                    PetOwnerOptions();
                    break;
                }
            case 3:
                {
                    Console.WriteLine();
                    Console.WriteLine("Your have failed to login please please contact vet staff to have password reset");
                    Console.WriteLine();
                    break;
                }
        }
    }

    /*********************************************************************/
    /* The following section of code logs user into the system           */
    /* If user is successfully logged in their 'PersonType' is returned  */
    /*********************************************************************/
    public static int VetLogin()
    {
        /* User can attemp 3 times to log into the system */
        /* before we send them back to the home screen    */
        int counter = 0;
        int attemptsLeft = 2;
        while (counter < 3)
        {
            string systemUserId = "";
            while (systemUserId == "")
            {
                Console.WriteLine();
                Console.WriteLine("Please Enter System UserId :");
                systemUserId = Console.ReadLine().TrimEnd() ?? "";
            }

            string systemPassword = "";
            while (systemPassword == "")
            {
                Console.WriteLine();
                Console.WriteLine("Please Enter System Password :");
                systemPassword = Console.ReadLine().TrimEnd() ?? "";
            }

            Login loggedIn = lgs.LoginUser(systemUserId, systemPassword);

            if (loggedIn != null)
            {
                /* Pull the Login in uers Person data for later use by system*/
                Person personLogedIn = prs.GetPerson(loggedIn.PersonId);

                /* Set the loggedInUser to hold the Person you just got logged */
                /* in for later usage of the data                              */
                loggedInUser = personLogedIn;

                /* Returning ONLY the Person Type here so it can be used to   */
                /* determine which screen to show to the logged in user       */
                return personLogedIn.PersonType;
            }
            else if (attemptsLeft > 0)
            {
                Console.WriteLine();
                Console.WriteLine("You entered invalid credentials please try again");
                Console.WriteLine("You have " + attemptsLeft + " attempts left");
                counter++;
                attemptsLeft--;
            }
            else
            {
                counter++;
            }
        }
        return counter;
    }

    /***********************************************************/
    /* The following section of code runs Vet Staff Functions  */
    /***********************************************************/
    public static void VetStaffOptions()
    {
        bool keepWorking = true;

        while (keepWorking)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine();
            Console.WriteLine("                       /)            ");
            Console.WriteLine("              /\\___/\\ ((           ");
            Console.WriteLine("              \\`@_@'/  ))           ");
            Console.WriteLine("              {_:Y:.}_//             ");
            Console.WriteLine("--------------{_}^-'{_}--------------");
            Console.WriteLine("- Welcome to the Pet Records System -");
            Console.WriteLine("-        Vet Staff Options          -");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Type '1' To Set Up a New Pet Family  ");
            Console.WriteLine("Type '2' To Set Up a New Pet Record  ");
            Console.WriteLine("Type '3' To View All of a Parent's Pets");
            Console.WriteLine("Type '4' To Update a Pet Record      ");
            Console.WriteLine("Type '5' To Close Out a Pet Record   ");
            Console.WriteLine("Type '6' To View List of All People in System ");
            Console.WriteLine("Type '7' To View List of All Pets in System ");
            Console.WriteLine("Type '8' Exit System                 ");
            Console.WriteLine();

            int staffSelection = int.Parse(Console.ReadLine().TrimEnd() ?? "0");

            int taskToRun = ValidateTask(staffSelection, 8);

            switch (taskToRun)
            {
                case 1:
                    {
                        NewPetFamilyRecord();
                        break;
                    }
                case 2:
                    {
                        NewPetRecord();
                        break;
                    }
                case 3:
                    {
                        ViewPetRecordVet();
                        break;
                    }
                case 4:
                    {
                        UpdatePetRecord();
                        break;
                    }
                case 5:
                    {
                        CloseOutPetRecord();
                        break;
                    }
                case 6:
                    {
                        ViewAllPerson();
                        break;
                    }
                case 7:
                    {
                        ViewAllPet();
                        break;
                    }
                case 8:
                    {
                        Console.WriteLine("Have a PURRfectly great remaninder of your work day!");
                        Console.WriteLine();
                        keepWorking = false;
                        break;
                    }
            }
        }
    }

    /***********************************************************/
    /* The following section of code runs Pet Parent Functions */
    /***********************************************************/
    public static void PetOwnerOptions()
    {
        bool keepParenting = true;

        while (keepParenting)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine();
            Console.WriteLine("      /^--^\\     /^--^\\     /^--^\\    ");
            Console.WriteLine("      \\____/     \\____/     \\____/    ");
            Console.WriteLine("     /      \\   /      \\   /      \\    ");
            Console.WriteLine("    |        | |        | |        |     ");
            Console.WriteLine("     \\__  __/   \\__  __/   \\__  __/   ");
            Console.WriteLine("|^|^|^|^\\ \\^|^|^|^/ /^|^|^|^|^\\ \\^|^|^|^|");
            Console.WriteLine("| | | | |\\ \\| | |/ /| | | | | |\\ \\| | | |");
            Console.WriteLine("########/ /######\\ \\###########/ /#######");
            Console.WriteLine("| | | | \\/| | | | \\/| | | | | |\\/ | | | |");
            Console.WriteLine("|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|");
            Console.WriteLine("-   Welcome to the Pet Records System   -");
            Console.WriteLine("-          Pet Parent Options           -");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Type '1' To View All Your Pets Records");
            Console.WriteLine("Type '2' To Request A Call Back       ");
            Console.WriteLine("Type '3' Exit System                  ");
            Console.WriteLine();

            int parentSelection = int.Parse(Console.ReadLine().TrimEnd() ?? "0");

            int taskToRun = ValidateTask(parentSelection, 3);
            switch (taskToRun)
            {
                case 1:
                    {
                        ViewPetRecordParent();
                        break;
                    }
                case 2:
                    {
                        RequstCallBack();
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Thank you for trusting us with your Kitty!");
                        Console.WriteLine();
                        keepParenting = false;
                        break;
                    }
            }
        }
    }

    /***********************************************/
    /* Method Name - NewPetFamilyRecord            */
    /* Inputs      - Console Input                 */
    /* Returns     - VOID (No Data Returned)       */
    /***********************************************/
    private static void NewPetFamilyRecord()
    {
        Console.WriteLine();
        Console.WriteLine("  Please Enter Pet Parent Information   ");
        Console.WriteLine(" to check if they are already in system ");
        Console.WriteLine("----------------------------------------");

        Person? lookUp = LookUpParent();

        if (lookUp != null)
        {
            Console.WriteLine();
            Console.WriteLine("Pet Parent was located in the system please use option 2 to add in their new pet");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("We did not find the Pet Parent in our system lets get them and their kitty added");

        string personFirstName = "";
        while (personFirstName == "")
        {
            Console.WriteLine();
            Console.WriteLine("Pet Parent First Name :");
            personFirstName = Console.ReadLine().TrimEnd() ?? "";
        }

        string personLastName = "";
        while (personLastName == "")
        {
            Console.WriteLine();
            Console.WriteLine("Pet Parent Last Name :");
            personLastName = Console.ReadLine().TrimEnd() ?? "";
        }

        string personUserName = "";
        while (personUserName == "")
        {
            Console.WriteLine();
            Console.WriteLine("Pet Parent Account User Name :");
            personUserName = Console.ReadLine().TrimEnd() ?? "";
        }

        string personPassword = "";
        while (personPassword == "")
        {
            Console.WriteLine();
            Console.WriteLine("Pet Parent Account Password :");
            personPassword = Console.ReadLine().TrimEnd() ?? "";
        }

        /* Hard Coded Values as for now only new Pet Parents can be added to system*/
        string personTitle = "Pet Parent";

        /* Check if personUserName in use PRIOR to creating a new Person */
        /* If TRUE is returned we will create new records */
        if (lgs.CheckUserName(personUserName))
        {
            /* Create a new Pet Parent (Person) */
            Person? newPerson = new Person(0, 2, personFirstName, personLastName, parentPhoneNum, personTitle);
            Person? addedPerson = prs?.AddNewPerson(newPerson);

            /* Create the new Pet Parents Login record (Login) */
            Login newLogin = new Login(0, newPerson.PersonId, personUserName, personPassword, 2);
            Login addedLogin = lgs.AddNewLogin(newLogin);

            Console.WriteLine();
            Console.WriteLine("Newly Added Pet Parent - " + addedPerson);

            Console.WriteLine();
            Console.WriteLine("Newly Added Pet Parent Login Data - " + addedLogin);

            Pet? newPet = NewPet(addedPerson.PersonId);

            Console.WriteLine();
            Console.WriteLine("Newly Added Pet - " + newPet);
        }
    }

    /***********************************************/
    /* Method Name - NewPetRecord                  */
    /* Inputs      - Console Input                 */
    /* Returns     - VOID (No Data Returned)       */
    /***********************************************/
    private static void NewPetRecord()
    {
        Console.WriteLine();
        Console.WriteLine(" Please Enter Pet Parent Account Information ");
        Console.WriteLine("---------------------------------------------");

        Person? lookUp = LookUpParent();

        Pet? newPet = NewPet(lookUp.PersonId);

        Console.WriteLine("Newly Added Pet - " + newPet);
    }

    /***********************************************/
    /* Method Name - NewPet                        */
    /* Inputs      - Pet Parent's personId         */
    /* Returns     - Newly created Pet             */
    /***********************************************/
    private static Pet? NewPet(int personId)
    {
        Console.WriteLine();
        Console.WriteLine("Please Enter Pet Information");
        Console.WriteLine("----------------------------");

        string petName = "";
        while (petName == "")
        {
            Console.WriteLine("Pet Name :");
            petName = Console.ReadLine().TrimEnd() ?? "";
        }

        string petColor = "";
        while (petColor == "")
        {
            Console.WriteLine();
            Console.WriteLine("Fur Color :");
            petColor = Console.ReadLine().TrimEnd() ?? "";
        }

        string petFurType = "";
        while (petFurType == "")
        {
            Console.WriteLine();
            Console.WriteLine("Fur Type :");
            petFurType = Console.ReadLine().TrimEnd() ?? "";
        }

        string petGender = "";
        while (petGender == "")
        {
            Console.WriteLine();
            Console.WriteLine("Gender :");
            petGender = Console.ReadLine().TrimEnd() ?? "";
        }

        int petAge = 0;
        while (petAge == 0)
        {
            Console.WriteLine();
            Console.WriteLine("Age :");
            petAge = int.Parse(Console.ReadLine().TrimEnd() ?? "0");
        }

        int petWeight = 0;
        while (petWeight == 0)
        {
            Console.WriteLine();
            Console.WriteLine("Weight :");
            petWeight = int.Parse(Console.ReadLine().TrimEnd() ?? "");
        }

        string petInside = "";
        while (petInside == "")
        {
            Console.WriteLine();
            Console.WriteLine("Inside Pet - True or False");
            petInside = Console.ReadLine().TrimEnd() ?? "";
        }

        bool inSide = true;

        if (petInside != null)
        {
            if (petInside.ToUpper() == "TRUE")
            {
                inSide = true;
            }
            else inSide = false;
        }

        DateTime appointmentDate = DateTime.Now;

        /* SeenBy set based on Vet employee who is logged into the system */
        /* This is using the 'loggedInUser' data we stored when we logged the person into the system */
        string petSeenBy = loggedInUser.FirstName + " " + loggedInUser.LastName;

        /* Creates a new Pet */
        Pet newPet = new Pet(0, personId, petName, petColor, petFurType, petGender, petWeight, petAge, inSide, appointmentDate, petSeenBy, "0");

        /* Adds the new Pet to the database */
        Pet addPet = pet.AddPet(newPet);

        /* Creates a new Visit */
        Visit newVisit = new Visit(0, addPet.PetId, personId, petWeight, petAge, inSide, appointmentDate, petSeenBy);

        /* Adds the Pets first visit to the database */
        vis.AddVisit(newVisit);

        /* Returns the new Pet */
        return addPet;
    }

    /***********************************************/
    /* Method Name - UpdatePetRecord               */
    /* Inputs      - VetRepo Object, Console Input */
    /* Returns     - VOID (No Data Returned)       */
    /***********************************************/
    private static void UpdatePetRecord()
    {
        Console.WriteLine();
        Console.WriteLine("Please Enter Updated Pet Information");
        Console.WriteLine("       Based On Todays Visit        ");
        Console.WriteLine("------------------------------------");

        // We are making the assumption that vet employee knowns IDs that will work
        // as the have already looked up the Parent's Pets 
        Pet? updatePet = PromotForId();

        Console.WriteLine();
        Console.WriteLine("Current Pet Age is " + updatePet.Age);
        int newAge = 0;
        while (newAge == 0)
        {
            Console.WriteLine("New Pet Age this appointment");
            newAge = int.Parse(Console.ReadLine().TrimEnd() ?? "");
        }
        updatePet.Age = newAge;

        Console.WriteLine();
        Console.WriteLine("Current Pet Weight is " + updatePet.Weight);
        int newWeight = 0;
        while (newWeight == 0)
        {
            Console.WriteLine("New Pet Weight this appointment");
            newWeight = int.Parse(Console.ReadLine().TrimEnd() ?? "");
        }
        updatePet.Weight = newWeight;

        string petInside = "";
        while (petInside == "")
        {
            Console.WriteLine();
            Console.WriteLine("Inside Pet - True or False");
            petInside = Console.ReadLine().TrimEnd() ?? "";
        }

        if (petInside.ToUpper() == "TRUE")
        {
            updatePet.InSidePet = true;
        }
        else updatePet.InSidePet = false;

        /* Setting to one day in the future on purpose here so I can SEE the update better       */
        /* versus the new date being just a few minutes different than the last Appointment date */
        updatePet.AppointmentDate = DateTime.Now.AddDays(+1);

        /* SeenBy set based on Vet employee who is logged into the system */
        /* This is using the 'loggedInUser' data we stored when we logged the person into the system */
        updatePet.SeenBy = loggedInUser.FirstName + " " + loggedInUser.LastName;

        /* Update the Pet record in the database with Pets most current data */
        updatePet = pet.UpdatePet(updatePet);

        /* Create a new Visit */
        Visit newVisit = new Visit(0, updatePet.PetId, updatePet.PersonId, updatePet.Weight, updatePet.Age, updatePet.InSidePet, updatePet.AppointmentDate, updatePet.SeenBy);

        /* Adds the Pets current visit to the database */
        vis.AddVisit(newVisit);

        /* After Pet updated display its new information */
        Console.WriteLine();
        Console.WriteLine("Pet was updated as follows - " + updatePet);
    }

    /***********************************************/
    /* Method Name - ViewAllPerson                 */
    /* Input       - No Input                      */
    /* Returns     - List of all Person in the     */
    /*               system                        */
    /***********************************************/
    private static void ViewAllPerson()
    {
        Console.WriteLine();
        Console.WriteLine("List of all Vet Employees and Pet Parents ");
        Console.WriteLine("              in the system               ");
        Console.WriteLine("------------------------------------------");
        /* Get list of all Person in the system */
        List<Person> persons = prs.GetAllPerson();

        /* Write the list to the Console */
        foreach (Person person in persons)
        {
            Console.WriteLine(person);
        }
    }

    /***********************************************/
    /* Method Name - ViewAllPet                    */
    /* Input       - No Input                      */
    /* Returns     - List of all Pet in the        */
    /*               system                        */
    /***********************************************/
    private static void ViewAllPet()
    {
        Console.WriteLine();
        Console.WriteLine(" List of all Kitty Cats ");
        Console.WriteLine("     in the system      ");
        Console.WriteLine("------------------------");
        /* Get list of all Person in the system */
        List<Pet> pets = pet.GetAllPet();

        /* Write the list to the Console */
        foreach (Pet pet in pets)
        {
            Console.WriteLine(pet);
            Console.WriteLine("");
        }
    }

    /***********************************************/
    /* Method Name - ViewPetRecordVet              */
    /* Input       - VetRepo Object                */
    /* Returns     - VOID (No Data Returned)       */
    /***********************************************/
    private static void ViewPetRecordVet()
    {
        Person lookUpParent = LookUpParent();

        List<Pet> allPersonsPets = new();
        List<Visit> allPersonsVisits = new();

        try
        {
            allPersonsPets = pet.GetPersonsPets(lookUpParent.PersonId);
            allPersonsVisits = vis.GetAllVisitsParent(lookUpParent.PersonId);
        }
        catch (NullReferenceException)
        {
            Console.WriteLine("Pet Parent was not located in the system");
        }

        /* After Pets retrieved display its information*/
        foreach (Pet pet in allPersonsPets)
        {
            Console.WriteLine();
            Console.WriteLine("Pets Most Recent Vet Vist Information");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine(pet);
            Console.WriteLine();
            Console.WriteLine("Pets Historical Vet Vist Information");
            Console.WriteLine("------------------------------------");

            foreach (Visit vist in allPersonsVisits)
                if (vist.PetId == pet.PetId)
                {
                    Console.WriteLine(vist);
                }
        }
    }

    /***********************************************/
    /* Method Name - ViewPetRecordParent           */
    /* Input       - VetRepo Object                */
    /* Returns     - VOID (No Data Returned)       */
    /***********************************************/
    private static void ViewPetRecordParent()
    {
        List<Pet> allPersonsPets = new();
        List<Visit> allPersonsVisits = new();

        try
        {
            allPersonsPets = pet.GetPersonsPets(loggedInUser.PersonId);
            allPersonsVisits = vis.GetAllVisitsParent(loggedInUser.PersonId);
        }
        catch (NullReferenceException)
        {
            Console.WriteLine("Pet Parent was not located in the system");
        }

        /* After Pets retrieved display its information*/
        foreach (Pet pet in allPersonsPets)
        {
            Console.WriteLine();
            Console.WriteLine("Pets Most Recent Vet Vist Information");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine(pet);
            Console.WriteLine();
            Console.WriteLine("Pets Historical Vet Vist Information");
            Console.WriteLine("------------------------------------");

            foreach (Visit vist in allPersonsVisits)
                if (vist.PetId == pet.PetId)
                {
                    Console.WriteLine(vist);
                }
        }
    }

    /***********************************************/
    /* Method Name - CloseOutPetRecord             */
    /* Input       - VetRepo Object, Console Input */
    /* Returns     - VOID (No Data Returned)       */
    /***********************************************/
    private static void CloseOutPetRecord()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine();
        Console.WriteLine("              ___           ");
        Console.WriteLine("             (___)          ");
        Console.WriteLine("      ____                  ");
        Console.WriteLine("    _\\___ \\  |\\_/|          ");
        Console.WriteLine("   \\     \\ \\/ , , \\ ___     ");
        Console.WriteLine("    \\__   \\ \\ ='= //|||\\    ");
        Console.WriteLine("     |===  \\/____)_)||||    ");
        Console.WriteLine("     \\______|    | |||||    ");
        Console.WriteLine("         _/_|  | | =====    ");
        Console.WriteLine("        (_/  \\_)_)          ");
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("  Please Enter Date Kitty   ");
        Console.WriteLine(" Crossed the Rainbow Bridge ");
        Console.WriteLine("----------------------------");

        // We are making the assumption that user knowns IDs that will work 
        Pet? closePet = PromotForId();

        Console.WriteLine();
        Console.WriteLine("Pet to close out - " + closePet);

        string petsRainbowBridgeDate = "";
        while (petsRainbowBridgeDate == "")
        {
            Console.WriteLine();
            Console.WriteLine("What day did Kitty cross the Rainbow Bridge:");
            petsRainbowBridgeDate = Console.ReadLine().TrimEnd() ?? "";
        }

        /* SeenBy set based on Vet employee who is logged into the system */
        /* This is using the 'loggedInUser' data we stored when we logged the person into the system */
        closePet.SeenBy = loggedInUser.FirstName + " " + loggedInUser.LastName;

        /* Setting to one day in the future on purpose here so I can SEE the update better       */
        /* versus the new date being just a few minutes different than the last Appointment date */
        closePet.AppointmentDate = DateTime.Now.AddDays(+1);

        closePet.RainbowBridgeDate = petsRainbowBridgeDate;

        /* Update the Pet in the collection */
        closePet = pet.UpdatePet(closePet);

        /* After Pet updated display its new information */
        Console.WriteLine();
        Console.WriteLine("Pet Record has been closed out - " + closePet);
    }

    /***********************************************/
    /* Method Name - RequstCallBack                */
    /* Input       - Console Input                 */
    /* Returns     - VOID (No Data Returned)       */
    /***********************************************/
    public static void RequstCallBack()
    {
        string parentName = "";
        while (parentName == "")
        {
            Console.WriteLine();
            Console.WriteLine("Call Back Name : ");
            parentName = Console.ReadLine().TrimEnd() ?? "0";
        }

        string parentNumber = "";
        while (parentNumber == "")
        {
            Console.WriteLine();
            Console.WriteLine("Call Back Number : ");
            parentNumber = Console.ReadLine().TrimEnd() ?? "0";
        }

        string petName = "";
        while (petName == "")
        {
            Console.WriteLine();
            Console.WriteLine("Name of Pet Calling about : ");
            petName = Console.ReadLine().TrimEnd() ?? "0";
        }

        string filepath = "KittyCityCallBackLog.txt";
        WriteToFile(filepath, parentName, parentNumber, petName);
    }

    /***********************************************/
    /* Method Name - WriteToFile                   */
    /* Inputs      - filepath, parentName,         */
    /*               parentNumber, petName         */
    /* Returns     - VOID (No Data Returned)       */
    /***********************************************/
    public static void WriteToFile(string filepath, string parentName, string parentNumber, string petName)
    {
        using (StreamWriter writer = new StreamWriter(filepath, true))
        {
            /* Write data out to the file */
            writer.WriteLine("Pet Parent to Call Back :" + parentName);
            writer.WriteLine("Pet Called About        :" + petName);
            writer.WriteLine("Call Back Number        :" + parentNumber);

            /* Display on screen message after file was written to */
            Console.WriteLine("The vet staff has been messaged to call you back. ");
            Console.WriteLine();
        }
    }

    /***********************************************/
    /* Method Name - ValidateTask                  */
    /* Inputs      - Task number keyed in by user  */
    /* Returns     - Validated Task Number         */
    /***********************************************/
    public static int ValidateTask(int task, int maxOption)
    {
        while (task < 0 || task > maxOption)
        {
            Console.WriteLine("Invalid Option - Please enter an option number between 1 " + maxOption);
            task = int.Parse(Console.ReadLine().TrimEnd() ?? "0");
        }


        return task;
    }

    /***********************************************/
    /* Method Name - LookUpParent                  */
    /* Inputs      - Console Input                 */
    /* Returns     - Person matching to inputs     */
    /***********************************************/
    private static Person? LookUpParent()
    {

        parentPhoneNum = "";
        while (parentPhoneNum == "")
        {
            Console.WriteLine("Pet Parent Phone Number :");
            parentPhoneNum = Console.ReadLine().TrimEnd() ?? "";
        }

        Person? lookUp = prs.LookUpPetParent(parentPhoneNum);

        return lookUp;
    }

    /***********************************************/
    /* Method Name - PromotForId                   */
    /* Input       - VetRepo Object, Console Input */
    /* Returns     - Pet Object                    */
    /***********************************************/
    public static Pet? PromotForId()
    {
        /* Loop asking for valid ID until one is entered by User*/
        int inputId = 0;
        Pet? locatedPet = new();

        while (inputId == 0)
        {
            Console.WriteLine();
            Console.WriteLine("Please enter a Pet ID");
            string? userInput = Console.ReadLine().TrimEnd();

            if (userInput != null && userInput != "")
            {
                try
                {
                    inputId = int.Parse(userInput);
                    if (inputId < 0)
                    {
                        inputId = 0;
                    }
                }
                catch (Exception)
                {
                    inputId = 0;
                }

                if (inputId != 0)
                {
                    try
                    {
                        locatedPet = pet.GetPet(inputId);
                        if (locatedPet == null)
                        {
                            inputId = 0;
                        }
                    }
                    catch (Exception)
                    {
                        inputId = 0;
                    }
                }
            }
        }

        return locatedPet;
    }
}
