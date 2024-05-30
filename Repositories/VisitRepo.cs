using Microsoft.Data.SqlClient;
class VisitRepo
{
    private readonly AppDbContext _context;
    
    /* Dependency Injection - Constructor Injection example */ 
    public VisitRepo(AppDbContext context)
    {
        _context = context;
    }
    /***********************************************/
    /* Method Name - AddPetVisit                   */
    /* Input       - Visit Object                  */
    /* Returns     - Visit Object                  */
    /***********************************************/
    public void AddPetVisit(Visit vi)
    {
         _context.Visits.Add(vi);
    }

    /***********************************************/
    /* Method Name - GetAllPetVisits               */
    /* Input       - No Input                      */
    /* Returns     - List of all Vists for a Pet   */
    /*               in the system                 */
    /***********************************************/
    public List<Visit> GetAllPetVisits(int id)
    {
        return _context.Visits.ToList();
    }

    /***********************************************/
    /* Method Name - GetAllParentVisits            */
    /* Input       - No Input                      */
    /* Returns     - List of all Vists for all of  */
    /*               a Parents Pets in the system  */ 
    /***********************************************/
    public List<Visit> GetAllParentVisits(int id)
    {
        return _context.Visits.ToList();
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