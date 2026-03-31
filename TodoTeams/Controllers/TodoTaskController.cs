using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TodoTeams.Controllers
{
    public class TodoTaskController : Controller
    {
        // GET: TodoTaskController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TodoTaskController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TodoTaskController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TodoTaskController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TodoTaskController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TodoTaskController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TodoTaskController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TodoTaskController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
