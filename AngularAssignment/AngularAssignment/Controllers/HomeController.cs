using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AngularAssignment.Controllers
{
    public class HomeController : Controller
    {
        private PeopleContext db = new PeopleContext();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        /* to creat new person to add it to the list
        take the input of FirstName,LastName and Country from the form and save it as Person class
        then check if its valid and if it is save it in database
        */
        public ActionResult NewPerson(string Firstname, string Lastname, string country, Person person)
        {
            if (ModelState.IsValid)
            {

                db.People.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return Content("success");
        }

        /* we will use Ajax to return json values, in this method the list of people will be returned 
         the people list will be extracted from database and ajax will put it in Json and make it ready 
         to send if any request get sent to it 
         */
        public JsonResult AjaxThatReturnsJson()
        {
            var myInfo = db.People.ToList();
            return Json(myInfo, JsonRequestBehavior.AllowGet);
        }

        /* Use ajax to send the countries as json values. The countries have been saved as Enum so in order to get them out we have to 
         * make a variable that saves all the countries from our enum in this case it is countryEnum.
         * we have to creat a list to save these enum inside it to be able to use them later, the list called countries
         * later we used a foreach to loop in our enum variable and take each item out and add it to our list
         * finally ajax will convert it as json let it be ready to get by request
         */
        public JsonResult ReturnCountries()
        {
            var countryEnum = Enum.GetValues(typeof(Countries));
            List<string> countries = new List<string>();
            foreach(var item in countryEnum)
            {
                countries.Add(item.ToString());
            }            
            return Json(countries, JsonRequestBehavior.AllowGet);
        }

        /* to return the information of a single person in the list, this method takes the id of that porson and 
         * search for it in database and returns it to an object. later ajax convert it to Jason
         */
        public JsonResult AjaxThatReturnsJsonPerson(int? ID)
        {
            object myInfo = null;

            myInfo = db.People.Single(p => p.ID == ID);

            if (myInfo == null)
            {
                myInfo = new { ID = 0, Firstname = "Not", Lastname = "Found" };
            }

            return Json(myInfo, JsonRequestBehavior.AllowGet);
        }

        /*it takes an Id, search for it in database and then remove it from there, this method is 
         * a post, it alos reuqired a get method to find the inforamtoin of that id and show it, which we do it 
         * with the  AjaxThatReturnsJsonPerson method
         */
        public ActionResult DeleteConfirmed(int? ID)
        {
            Person person = db.People.Find(ID);
            db.People.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /* this edit method is act as post for editing, we need a get method to recieve the ID for the person
         * that needs to be edited, the AjaxThatReturnsJsonPerson method do this.
         * in this method the id of the person and its edited data will be recived and basedon the id
         * the information of that person will be modified 
         */
        public ActionResult Edit([Bind(Include = "ID,Firstname,Lastname,country")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}