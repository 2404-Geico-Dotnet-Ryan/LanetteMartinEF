interface IPetRepo
{
    public void AddPet(Pet pe);

    public Pet? GetPet(int id);

    public List<Pet> GetAllPets();
    
    public void UpdatePet(Pet updateP);

    public void Save();

}