using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tamagotchi.Models;

namespace Tamagotchi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        public DatabaseContext db { get; set; } = new DatabaseContext();
         
        [HttpGet]
        public List<Pet> GetAllPets()
        {
            var pets = db.Pets.OrderBy(p => p.Name);
            return pets.ToList();
        }

        [HttpGet("{id}")]
        public Pet GetOnePet(int id)
        {
            var pet = db.Pets.FirstOrDefault(p => p.Id == id);
            return pet;
        }

        [HttpPost]
        public Pet AdoptPet(Pet newPet)
        {
            db.Pets.Add(newPet);
            db.SaveChanges();
            return newPet;
        }

        [HttpPatch("{id}/play")]
        public Pet PlayWithPet (int id)
        {
            var play = db.Pets.FirstOrDefault(p => p.Id == id);
            play.HappinessLevel = play.HappinessLevel + 5;
            play.HungerLevel = play.HungerLevel + 3;
            db.SaveChanges();
            return play;
        }

        [HttpPatch("{id}/feed")]
        public Pet FeedPet (int id)
        {
            var feed = db.Pets.FirstOrDefault(p => p.Id == id);
            feed.HappinessLevel = feed.HappinessLevel + 3;
            feed.HungerLevel = feed.HungerLevel - 5;
            db.SaveChanges();
            return feed; 
        }

        [HttpPatch("{id}/scold")]
        public Pet ScoldPet (int id)
        {
            var scold = db.Pets.FirstOrDefault(p => p.Id == id);
            scold.HappinessLevel = scold.HappinessLevel - 5;
            db.SaveChanges();
            return scold; 
        }

        [HttpDelete("{id}")]
        public ActionResult AbandonPet (int id)
        {
            var abandon = db.Pets.FirstOrDefault(p => p.Id == id);
            if (abandon == null)
            {
                return NotFound();
            }
            db.Pets.Remove(abandon);
            db.SaveChanges();
            return Ok(); 
        }
    }
}