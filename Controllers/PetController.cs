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
            var pets = db.Pets.OrderBy(p => p.Id);
            return pets.ToList();
        }

        [HttpGet("alive")]
        public List<Pet> ViewAlive()
        {
            var allPets = db.Pets.Where(p => p.IsDead == false);
            var alive = allPets.OrderBy(p => p.Id);
            return alive.ToList();
        }

        [HttpGet("{id}")]
        public Pet GetOnePet(int id)
        {
            var pet = db.Pets.FirstOrDefault(p => p.Id == id);
            InteractUpdate(pet);
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
            var suddendeath = SuddenDeath();
            if (suddendeath == true)
            {
                play.IsDead = true;
                play.DeathDate = DateTime.Now;
                db.SaveChanges();
            }
            else if (suddendeath == false)
            {
                play.HappinessLevel = play.HappinessLevel + 5;
                play.HungerLevel = play.HungerLevel + 3;
                db.SaveChanges();
            }
            InteractUpdate(play);
            return play;
        }

        [HttpPatch("{id}/feed")]
        public Pet FeedPet (int id)
        {
            var feed = db.Pets.FirstOrDefault(p => p.Id == id);
            var suddendeath = SuddenDeath();
            if (suddendeath == true)
            {
                feed.IsDead = true;
                feed.DeathDate = DateTime.Now;
                db.SaveChanges();
            }
            else if (suddendeath == false)
            {
                feed.HappinessLevel = feed.HappinessLevel + 3;
                feed.HungerLevel = feed.HungerLevel - 5;
                db.SaveChanges();
            }
            InteractUpdate(feed);
            return feed; 
        }

        [HttpPatch("{id}/scold")]
        public Pet ScoldPet (int id)
        {
            var scold = db.Pets.FirstOrDefault(p => p.Id == id);
            var suddendeath = SuddenDeath();
            if (suddendeath == true)
            {
                scold.IsDead = true;
                scold.DeathDate = DateTime.Now;
                db.SaveChanges();
            }
            else if (suddendeath == false)
            {
                scold.HappinessLevel = scold.HappinessLevel - 5;
                db.SaveChanges();
            }
            InteractUpdate(scold);
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
          public bool SuddenDeath()
        {
            var killed = false;
            var random = new Random();
            var dead = random.Next(1,101);
            if (dead <= 10)
            {
                killed = true;
            }
                return killed;
        }
        public void InteractUpdate(Pet pet)
        {
            pet.LastInteractedWithDate = DateTime.Now;
            db.SaveChanges();
        }
        
    }
}