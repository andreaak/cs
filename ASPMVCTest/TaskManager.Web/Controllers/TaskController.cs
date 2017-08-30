using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Domain;
using TaskManager.Web.Infrastructure;
using WebMatrix.WebData;

namespace TaskManager.Web.Controllers
{
    [Authorize] // К контроллеру получают доступ только аутентифицированные пользователи.
    public class TaskController : Controller
    {
        // Dependency Injection
        // Данные поля будут хранит ссылки на реальные репозитории или на тестовые в соответствии с параметрами переданными в конструктор
        ITaskRepository _tasks;
        IUserProfileRepository _users;

        public TaskController()
            : this(new DALContext())
        {
        }

        public TaskController(IDALContext context)
        {
            _tasks = context.Tasks;
            _users = context.Users;
        }

        //
        // GET: /Task/

        public ActionResult Index()
        {
            return View(_users.CurrentUser.Tasks.ToList());
        }

        //
        // GET: /Task/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Task/

        [HttpPost]
        public ActionResult Create(Task task)
        {
            if (ModelState.IsValid)
            {
                task.Owner = _users.CurrentUser;
                _tasks.InsertOrUpdate(task);
                _tasks.Save();
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /Task/Edit/1

        public ActionResult Edit(int id)
        {
            return View(_tasks.All.FirstOrDefault(t => t.Id == id));
        }

        //
        // POST: /Task/Edit/

        [HttpPost]
        public ActionResult Edit(Task task)
        {
            if (ModelState.IsValid)
            {
                _tasks.InsertOrUpdate(task);
                _tasks.Save();
            }
            return RedirectToAction("Index");
        }

        //
        // Edit: /Task/Delete/1

        public ActionResult Delete(int id)
        {
            return View(_tasks.All.FirstOrDefault(t => t.Id == id));
        }

        //
        // POST: /Task/Delete/1

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _tasks.Remove(_tasks.All.FirstOrDefault(t => t.Id == id));
            _tasks.Save();
            return RedirectToAction("Index");
        }

    }
}
