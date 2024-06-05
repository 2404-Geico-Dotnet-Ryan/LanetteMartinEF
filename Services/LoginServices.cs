class LoginServices
{
    /*
    Services Performed :
        - Add a New Person (Pet Parents only at this time)
        - Look Up a Pet Parent by Phone Number 
        - Get a list of All Person in the system

    */
    LoginRepo lg;

    public LoginServices(LoginRepo loginRepo)
    {
        lg = loginRepo; 
    }

    /***********************************************/
    /* Method Name - LoginUser                     */
    /* Inputs      - User Name and Password        */
    /* Returns     - Person Object found           */
    /***********************************************/
    public Login? LoginUser(string userName, string userPassword)
    {
        /* Look thru all users for a match to UserName and Password*/ 
        List<Login> allLogins = lg.GetAllLogins();

        foreach (Login login in allLogins)
        {
            if (login.UserName == userName)
            {
                if (login.UserPassword == userPassword)
                {
                    return login;
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
        Console.WriteLine("Login Credentials were not found in the system");
        return null; 
    }

    /***********************************************/
    /* Method Name - CheckUserName                 */
    /* Inputs      - Purposed new Username         */
    /* Returns     - True if Username is in use    */ 
    /*               False Username is free        */
    /***********************************************/
public bool CheckUserName(string userName)
    {
         /*Will not let you register a new person if the UserName is already in use */
        List<Login> allLogins = lg.GetAllLogins();

        foreach (Login login in allLogins)
        {
            if (login.UserName == userName)
            {
                Console.WriteLine();
                Console.WriteLine("User Name is already taken");
                return false; 
            }
        }      
        return true; 
    }
    /***********************************************/
    /* Method Name - AddNewLogin                   */
    /* Inputs      - Person Object containing data */
    /*               for new Person                */
    /* Returns     - Person Object containing data */
    /*               for Person justed added       */
    /***********************************************/
    public Login? AddNewLogin(Login l)
    {    
        /* If pass both check add the new user */
        lg.AddLogin(l);
        lg.Save();
        return l; 
    }
    
   
}