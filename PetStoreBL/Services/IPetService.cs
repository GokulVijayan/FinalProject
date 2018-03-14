using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetStoreDAL.Models;

namespace PetStoreBL.Services
{
    public interface IPetService
    {
        IEnumerable<PetDetailsDto>FindAll();
        void Save(PetDetailsDto pdd);
        IEnumerable<PetDto> GetType();
        IEnumerable<PetDetailsDto> SortByPetType(string type);
        IEnumerable<PetDetailsDto> SortByPrice(string type);
        void DeletePetRecord(int id);
        int GetPetId(string petName, string breedType);
        PetDetailsDto GetPetById(int id);
        void EditPet(PetDetailsDto pd,int petId);
    }
}
