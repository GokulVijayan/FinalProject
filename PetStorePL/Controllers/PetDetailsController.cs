using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetStoreBL.Services;
using PetStorePL.ViewModel;
using Common;
using System.Drawing;
using System.Net;

namespace PetStorePL.Controllers
{
    public class PetDetailsController : Controller
    {
        private readonly IPetService transRepos;
        public static int petId;
        public PetDetailsController()
        {

        }
        public PetDetailsController(IPetService transactionRepo)
        {
            transRepos = transactionRepo;
        }
        public ActionResult Create()
        {
            
            var xx = GetPetType();
            var petType = new SelectList(xx, "TypeId", "PetType");
            ViewData["pettype"] = petType;
            return View();
        }
        public IEnumerable<PetViewModel> GetPetType()
        {
            IEnumerable<PetDto> petdetails = transRepos.GetType();
            var x = from g in petdetails
                    select new PetViewModel
                    {
                        PetType = g.PetType,
                        TypeId = g.TypeId
                    };
            return x.ToList();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "Age,ImagePath,BreedType,PetName,Price,PetType")] PetDetailsViewModel petdetails, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/") + file.FileName);
                    var filepath = HttpContext.Server.MapPath("~/Images/") + file.FileName;
                    PetDetailsDto pet = new PetDetailsDto
                    {
                        Age = petdetails.Age,
                        ImagePath = file.FileName,
                        BreedType = petdetails.BreedType,
                        PetName = petdetails.PetName,
                        Price = petdetails.Price,
                        PetType = petdetails.PetType
                    };

                    transRepos.Save(pet);

                }
            }
            var xx = GetPetType();
            var petType = new SelectList(xx, "TypeId", "PetType");
            ViewData["pettype"] = petType;
            return View(petdetails);
        }
        public ActionResult Index()
        {
            return View(ViewDetails());
        }
        public IEnumerable<PetDetailsViewModel> ViewDetails()
        {
            IEnumerable<PetDetailsDto> petdetails = transRepos.FindAll();
            Image image = null;
            var pet = GetAllPets(petdetails);
            return pet;
        }
        public ActionResult Search(String option, String search)
        {
            if (option == null)
            {
                return View(ViewDetails());
            }
            else if (option == "PetType")
            {
                if (!string.IsNullOrEmpty(search))
                    return View(SortByPetType(search));
                else
                {
                    ModelState.AddModelError("Search", "Please enter pet type");
                    return View(ViewDetails());
                }
            }
            else if(option== "Price")
            {
                if (!string.IsNullOrEmpty(search))
                    return View(SortByPrice(search));
                else
                {
                    ModelState.AddModelError("Search", "Please enter Price");
                    return View(ViewDetails());
                }

            }
            else if(option=="All")
            {
                return View(ViewDetails());
            }
            return View();

        }
        public IEnumerable<PetDetailsViewModel> SortByPrice(string type)
        {
            IEnumerable<PetDetailsDto> petdetails = transRepos.SortByPrice(type);
            var pet = GetAllPets(petdetails);
            return pet;
        }
        public IEnumerable<PetDetailsViewModel>GetAllPets(IEnumerable<PetDetailsDto> petdetails)
        {
            IEnumerable<PetDetailsViewModel> pt = from g in petdetails
                                                  select new PetDetailsViewModel
                                                  {
                                                      Age = g.Age,
                                                      BreedType = g.BreedType,
                                                      PetName = g.PetName,
                                                      ImagePath = g.ImagePath,
                                                      Price = g.Price,
                                                      PetType = g.PetType
                                                  };
            return pt.ToList();
        }

        public IEnumerable<PetDetailsViewModel> SortByPetType(string type)
        {
            IEnumerable<PetDetailsDto> petdetails = transRepos.SortByPetType(type);
            var pet = GetAllPets(petdetails);
            return pet;

        }
        public ActionResult Find(string petName, string breedType, string operation)
        {
            int? id = transRepos.GetPetId(petName, breedType);
            if (id == null)
            {
                return RedirectToAction("Index", "PetDetails");
            }
            else if (operation == "Delete")
                return RedirectToAction("Delete", "PetDetails", new { id = id });
            else if (operation == "Edit")
                return RedirectToAction("Edit", "PetDetails", new { id = id });
            else
                return View();
        }
        public ActionResult Delete(int id)
        {
            transRepos.DeletePetRecord(id);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                PetDetailsViewModel petview = GetPetById(id);
                var xx = GetPetType();
                petId = id;
                var petType = new SelectList(xx, "TypeId", "PetType", petview.PetType);
                ViewData["pettype"] = petType;
                //return RedirectToAction("Edit","PetDetails",new { petview = petview, id=id});
                return View(petview);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( PetDetailsViewModel petdetails,HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/") + file.FileName);
                    var filepath = HttpContext.Server.MapPath("~/Images/") + file.FileName;

                    PetDetailsDto pet = new PetDetailsDto
                    {
                        Age = petdetails.Age,
                        ImagePath = file.FileName,
                        BreedType = petdetails.BreedType,
                        PetName = petdetails.PetName,
                        Price = petdetails.Price,
                        PetType = petdetails.PetType
                    };
                    transRepos.EditPet(pet, petId);
                    return RedirectToAction("Index");

                }
                else
                {
                    ModelState.AddModelError("ImagePath", "Please select a file");
                    return RedirectToAction("Edit", "PetDetails", new { id = petId });
                }

                    
            }
            var xx = GetPetType();
            var petType = new SelectList(xx, "TypeId", "PetType",petdetails.PetType);
            ViewData["pettype"] = petType;
            return View(petdetails);
        }
        public PetDetailsViewModel GetPetById(int id)
        {
            PetDetailsDto pet = transRepos.GetPetById(id);
            PetDetailsViewModel pt =new PetDetailsViewModel
                                                  {
                                                      Age = pet.Age,
                                                      BreedType = pet.BreedType,
                                                      PetName = pet.PetName,
                                                      ImagePath = pet.ImagePath,
                                                      Price = pet.Price,
                                                      PetType = pet.PetType
                                                  };
            return pt;

        }
    }
}
