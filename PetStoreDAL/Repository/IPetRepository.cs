using PetStoreDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStoreDAL.Repository
{
    public interface IPetRepository
    {
        void SaveDetails(PetDetails pdd);
        IEnumerable<PetDetails> FindAll();
        void Save();
        IEnumerable<Pet> GetType();
        IEnumerable<PetDetails> SortByPetType(string type);
        IEnumerable<PetDetails> SortByPrice(string type);
        void DeletePetRecord(int id);
        int GetPetId(string petName, string breedType);
        PetDetails GetPetById(int id);
        void EditPet(PetDetails pd);
    }
}
