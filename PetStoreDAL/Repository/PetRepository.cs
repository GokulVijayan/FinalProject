using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetStoreDAL.Models;
using PetStoreDAL.DBContext;
using System.Data.Entity;

namespace PetStoreDAL.Repository
{
    public class PetRepository :IPetRepository
    {
        PetDbContext db = new PetDbContext();

        public IEnumerable<PetDetails> FindAll()
        {
            return db.petdetails.ToList();
        }

        public void SaveDetails(PetDetails pdd)
        {
            db.petdetails.Add(pdd);
            Save();
        }
        public void Save()
        {
            db.SaveChanges();
        }

        IEnumerable<Pet> IPetRepository.GetType()
        {
            return db.pet.ToList();
        }

        public IEnumerable<PetDetails> SortByPetType(string type)
        {
            return db.petdetails.Where(p =>p.pet.PetType==type).ToList();
        }

        public IEnumerable<PetDetails> SortByPrice(string type)
        {
            var price = Convert.ToInt32(type);
            return db.petdetails.Where(p => p.Price<=price).ToList();
        }

        public void DeletePetRecord(int id)
        {
            var pet = db.petdetails.Where(p => p.PetId==id).FirstOrDefault();
            db.petdetails.Remove(pet);
            Save();
        }

        public int GetPetId(string petName, string breedType)
        {
            var id = db.petdetails.Where(p => p.PetName == petName && p.BreedType == breedType).Select(p => p.PetId).FirstOrDefault();
            return id;

        }

        public PetDetails GetPetById(int id)
        {
            return db.petdetails.Find(id);
        }

        public void EditPet(PetDetails pd)
        {
            db.Entry(pd).State = EntityState.Modified;
            Save();
        }
    }
}
