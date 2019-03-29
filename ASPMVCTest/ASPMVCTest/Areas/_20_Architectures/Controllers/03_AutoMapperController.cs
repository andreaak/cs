using _01_ASPMVCTest.Areas._20_Architectures._03_AutoMapper;
using AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._20_Architectures.Controllers
{
    public class _03_AutoMapperController : Controller
    {
        //
        // GET: /_20_Architectures/03_AutoMapper/

        IRepository<User> repo;
        public _03_AutoMapperController(IRepository<User> r)
        {
            repo = r;
        }

        public ActionResult Index()
        {
            // Настройка AutoMapper
            Mapper.CreateMap<User, IndexUserViewModel>();
            // сопоставление
            var users = Mapper.Map<IEnumerable<User>, List<IndexUserViewModel>>(repo.GetAll());
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
