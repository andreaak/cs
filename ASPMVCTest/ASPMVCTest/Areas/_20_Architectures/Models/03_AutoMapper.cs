using _01_ASPMVCTest.Areas._20_Architectures.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._20_Architectures.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }

    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }

    public class UserRepository : IRepository<User>
    {
        UserContext db;
        public UserRepository()
        {
            db = new UserContext();
        }
        public IEnumerable<User> GetAll()
        {
            return db.Users.ToList();

        }
        public User Get(int id)
        {
            return db.Users.Find(id);
        }
        public void Create(User item)
        {
            db.Users.Add(item);
        }
        public void Update(User item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
        }
        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public interface IRepository<T> : IDisposable
    where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
    }

    public class IndexUserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class CreateUserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Login { get; set; }
    }

    public class EditUserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Login { get; set; }
    }

    namespace AutoMapperApp.Controllers
    {
        public class AutoMapperController : Controller
        {
            IRepository<User> repo;
            public AutoMapperController(IRepository<User> r)
            {
                repo = r;
            }

            public ActionResult Index()
            {
                // Настройка AutoMapper
                Mapper.CreateMap<User, IndexUserViewModel>();
                // сопоставление
                var users =
                Mapper.Map<IEnumerable<User>, List<IndexUserViewModel>>(repo.GetAll());
                return View(users);
            }

            public ActionResult Create()
            {
                return View();
            }

            [HttpPost]
            public ActionResult Create(CreateUserViewModel model)
            {
                if (ModelState.IsValid)
                {
                    // Настройка AutoMapper
                    Mapper.CreateMap<CreateUserViewModel, User>()
                    .ForMember("Name", opt => opt.MapFrom(c => c.FirstName + " " + c.LastName))
                    .ForMember("Email", opt => opt.MapFrom(src => src.Login));
                    // Выполняем сопоставление
                    User user = Mapper.Map<CreateUserViewModel, User>(model);
                    repo.Create(user);
                    repo.Save();

                    return RedirectToAction("Index");
                }
                return View(model);
            }

            public ActionResult Edit(int? id)
            {
                if (id == null)
                    return HttpNotFound();
                // Настройка AutoMapper
                Mapper.CreateMap<User, EditUserViewModel>()
                .ForMember("Login", opt => opt.MapFrom(src => src.Email));
                // Выполняем сопоставление
                EditUserViewModel user = Mapper.Map<User, EditUserViewModel>(repo.Get(id.Value));
                return View(user);
            }

            [HttpPost]
            public ActionResult Edit(EditUserViewModel model)
            {
                if (ModelState.IsValid)
                {
                    // Настройка AutoMapper
                    Mapper.CreateMap<EditUserViewModel, User>()
                    .ForMember("Email", opt => opt.MapFrom(src => src.Login));
                    // Выполняем сопоставление
                    User user = Mapper.Map<EditUserViewModel, User>(model);
                    repo.Update(user);
                    repo.Save();
                    return RedirectToAction("Index");

                }
                return View(model);
            }
        }
    }


}

namespace AutoMapperApp
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<User, IndexUserViewModel>();
            Mapper.CreateMap<CreateUserViewModel, User>()
            .ForMember("Name", opt => opt.MapFrom(c => c.FirstName + " " + c.LastName))
            .ForMember("Email", opt => opt.MapFrom(src => src.Login));
            Mapper.CreateMap<User, EditUserViewModel>()
            .ForMember("Login", opt => opt.MapFrom(src => src.Email));
        }
    }

    //protected void Application_Start()
    //{
    //    AutoMapperConfig.RegisterMappings();

    //    // остальной код
    //}
}