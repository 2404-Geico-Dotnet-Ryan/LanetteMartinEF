using Microsoft.Data.SqlClient;
class LoginRepo : ILoginRepo
{
    private readonly AppDbContext _context;
    
    /* Dependency Injection - Constructor Injection example */ 
    public LoginRepo(AppDbContext context)
    {
        _context = context;
    }

    /***********************************************/
    /* Method Name - GetAllLogins                  */
    /* Input       - No Input                      */
    /* Returns     - List of all Logins in system  */
    /***********************************************/
    public List<Login> GetAllLogins()
    {
        return _context.Logins.ToList(); 
    }

    /***********************************************/
    /* Method Name - AddLogin                     */
    /* Input       - Login Object                 */
    /* Returns     - Login Object                 */
    /***********************************************/
    public void AddLogin(Login lg)
    {
        _context.Logins.Add(lg);
    }

    /***********************************************/
    /* Method Name - Save                          */
    /* Input       - No Input                      */
    /* Returns     - No Return                     */
    /* This method just Saves data to the database */
    /***********************************************/
    public void Save()
    {
        _context.SaveChanges();
    }
}