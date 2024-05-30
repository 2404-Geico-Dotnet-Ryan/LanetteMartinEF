class VisitServices
{
    /*
    Services Performed :
        - Get ALL Records belonging to a Pet
        - Get ALL Records belonging to a Parent 
        - Add a Pet Visit to historical log 
    */

    VisitRepo vr;

    public VisitServices(VisitRepo visitRepo)
    {
        vr = visitRepo; 
    }

    /***********************************************/
    /* Method Name - GetAllVisitsPet               */
    /* Input       - Pet Id                        */
    /* Returns     - List of all Vists for a Pet   */
    /*               in the system                 */
    /***********************************************/
    public List<Visit> GetAllVisitsPet(int id)
    {
        /* Get ALL Visit Records belonging to a Pet */
        List<Visit> allVisits = vr.GetAllPetVisits(id);

        return allVisits; 
    }

    /***********************************************/
    /* Method Name - GetAllVisitsParent            */
    /* Input       - Parent Id                     */
    /* Returns     - List of all Vists for all of  */
    /*               a Parents Pets in the system  */ 
    /***********************************************/
    public List<Visit> GetAllVisitsParent(int id)
    {
        /* Get ALL Visit Records belonging to a Pet Parent */
        List<Visit> allVisits = vr.GetAllParentVisits(id);

        return allVisits;  
    }

    /***********************************************/
    /* Method Name - AddPet                        */
    /* Inputs      - Pet                           */
    /* Returns     - Newly Added pet               */
    /***********************************************/
    public Visit? AddVisit(Visit v)
    {
       vr.AddPetVisit(v);
       vr.Save();
       return v;
    }
}