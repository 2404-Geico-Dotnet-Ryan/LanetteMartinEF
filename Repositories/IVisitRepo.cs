interface IVisitRepo
{
    public void AddPetVisit(Visit vi);

    public List<Visit> GetAllPetVisits(int id);

    public List<Visit> GetAllParentVisits(int id);

    public void Save();
}