using Microsoft.Data.SqlClient;
class PetRepo : IPetRepo
{
    private readonly AppDbContext _context;
    
    /* Dependency Injection - Constructor Injection example */ 
    public PetRepo(AppDbContext context)
    {
        _context = context;
    }

    /***********************************************/
    /* Method Name - AddPet                        */
    /* Input       - Pet Object                    */
    /* Returns     - Pet Object                    */
    /***********************************************/
    public void AddPet(Pet pe)
    {
        _context.Pets.Add(pe);
    }

    /***********************************************/
    /* Method Name - GetPet                        */
    /* Input       - PetId                         */
    /* Returns     - Pet that matched to the passed*/ 
    /*               in PetId                      */
    /***********************************************/
    public Pet? GetPet(int id)
    {
        return _context.Pets.Find(id);
    }

    /***********************************************/
    /* Method Name - GetAllPets                    */
    /* Input       - No Input                      */
    /* Returns     - List of all Pets in system    */
    /***********************************************/
    public List<Pet> GetAllPets()
    {
        return _context.Pets.ToList(); 
    }

    /***********************************************/
    /* Method Name - UpdatePet                     */
    /* Input       - Pet Object                    */
    /* Returns     - Updated Pet Object            */
    /***********************************************/
    public void UpdatePet(Pet updateP)
    {
        _context.Pets.Update(updateP);
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