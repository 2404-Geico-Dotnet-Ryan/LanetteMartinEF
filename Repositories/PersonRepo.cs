using Microsoft.Data.SqlClient;
class PersonRepo : IPersonRepo
{
    private readonly AppDbContext _context;
    
    /* Dependency Injection - Constructor Injection example */ 
    public PersonRepo(AppDbContext context)
    {
        _context = context;
    }
    
    /***********************************************/
    /* Method Name - AddPerson                     */
    /* Input       - Person Object                 */
    /* Returns     - Person Object                 */
    /***********************************************/
    public void AddPerson(Person pr)
    {
        _context.Persons.Add(pr);
    }

    /***********************************************/
    /* Method Name - GetPerson                     */
    /* Input       - PersonId                      */
    /* Returns     - Person that matched to the    */ 
    /*               passed in PersonId            */
    /***********************************************/
    public Person? GetPerson(int id)
    {
        return _context.Persons.Find(id);
    }

    /***********************************************/
    /* Method Name - GetAllPersons                 */
    /* Input       - No Input                      */
    /* Returns     - List of all Pets in system    */
    /***********************************************/
    public List<Person> GetAllPersons()
    {
        return _context.Persons.ToList(); 
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