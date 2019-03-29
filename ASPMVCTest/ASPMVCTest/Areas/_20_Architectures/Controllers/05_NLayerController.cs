using _01_ASPMVCTest.Areas._20_Architectures._05_NLayer.NLayerApp.BLL.DTO;
using _01_ASPMVCTest.Areas._20_Architectures._05_NLayer.NLayerApp.BLL.Infrastructure;
using _01_ASPMVCTest.Areas._20_Architectures._05_NLayer.NLayerApp.BLL.Interfaces;
using _01_ASPMVCTest.Areas._20_Architectures._05_NLayer.NLayerApp.WEB.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._20_Architectures.Controllers
{
    public class _05_NLayerController : Controller
    {
        //
        // GET: /_20_Architectures/05_NLayer/

        IOrderService orderService;
        public _05_NLayerController(IOrderService serv)
        {
            orderService = serv;
        }

        public ActionResult Index()
        {
            Mapper.CreateMap<PhoneDTO, PhoneViewModel>();
            var phones = Mapper.Map<IEnumerable<PhoneDTO>, List<PhoneViewModel>>(orderService.GetPhones());
            return View(phones);
        }

        public ActionResult MakeOrder(int? id)
        {
            try
            {
                Mapper.CreateMap<PhoneDTO, OrderViewModel>()
                .ForMember("PhoneId", opt => opt.MapFrom(src => src.Id));
                var order = Mapper.Map<PhoneDTO, OrderViewModel>(orderService.GetPhone(id));
                return View(order);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult MakeOrder(OrderViewModel order)
        {
            try
            {
                Mapper.CreateMap<OrderViewModel, OrderDTO>();
                var orderDto = Mapper.Map<OrderViewModel, OrderDTO>(order);
                orderService.MakeOrder(orderDto);
                return Content("<h2>Ваш заказ успешно оформлен</h2>");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(order);
        }

        protected override void Dispose(bool disposing)
        {
            orderService.Dispose();
            base.Dispose(disposing);
        }
    }
}
