using GameRev.Models;
using GameRev.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace GameRev.Controllers
{
    public class GoalsController : Controller
    {

        GoalsApiController apiController = new GoalsApiController();

        // GET: Goals
        public ActionResult Index()
        {          
            return View(apiController.Get());
        }

        // GET: Goals/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Goals/Create
        public ActionResult Create()
        {
            GoalModel model = new GoalModel();
            model.RULES.ToList().Add(new GoalRuleModel());
            return View("Create", model);
        }

        // POST: Goals/Create
        [HttpPost]
        public ActionResult Create(GoalModel goal)
        {
            try
            {
                apiController.Post(JsonConvert.SerializeObject(goal));                
                return View(goal);
            }
            catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return View();
            }
        }

        // GET: Goals/Edit/5
        public ActionResult Edit(int id)
        {
            GoalsApiController apiController = new GoalsApiController();

            return View(apiController.Get(id));
        }

        // POST: Goals/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Goals/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Goals/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
