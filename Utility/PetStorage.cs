class PetStorage
{
    public Dictionary<int, Pet> pets;
    public int IdCounter = 1;

    public PetStorage()
    {
            /* Create Pets */
            Pet pet1 = new Pet (IdCounter++, 3, "Jay", "Orange", "Short", "Male", 13, 7, false, DateTime.Now, "0", "0");
            Pet pet2 = new Pet (IdCounter++, 3, "Turtle", "Tuxcedo", "Short", "Male", 8, 1, true, DateTime.Now, "0", "0");  
            Pet pet3 = new Pet (IdCounter++, 4, "DJMeow", "Calico", "Long", "Male", 9, 3, true, DateTime.Now, "0", "0");
            Pet pet4 = new Pet (IdCounter++, 4, "DJScratchCat", "Black", "Short", "Female", 9, 3, true, DateTime.Now, "0", "0");

            /* Add the new Pet Objects to the dictionary */
            pets =  [];
            pets.Add(pet1.PetId,pet1);
            pets.Add(pet2.PetId,pet2);
            pets.Add(pet3.PetId,pet3);
            pets.Add(pet4.PetId,pet4);
    }
}