using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManager.Web.Controllers;
using TaskManager.Web.Tests.Infrastructure;
using System.Web.Mvc;
using System.Linq;
using TaskManager.Domain;
using System.Collections;

namespace TaskManager.Web.Tests.Controllers
{
    // Класс содержит тесты для проверки работы методов действий TaskController

    [TestClass]
    public class TaskControllerTest
    {
        // UnitTest проверяющий работу метода Index
        [TestMethod]
        public void IndexTestController()
        {
            TaskController controller = new TaskController(new TestDALContext());

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        // UnitTest проверяющий работу метода Create
        [TestMethod]
        public void CreateTestController()
        {
            // Создаем два тестовых репозитория для поверки работы метода действия Create 
            TestDALContext context = new TestDALContext();

            // Создаем контроллер указывя ссылки на тестовые репозитории
            TaskController controller = new TaskController(context);

            // Временная сущность, которая будет добавлена в тестовый репозиторий.
            Task task = new Task();
            task.Name = "Test Task";
            task.IsCompleted = false;
            task.Owner = context.Users.CurrentUser;

            // Вызов метода Create
            ViewResult result = controller.Create(task) as ViewResult;

            // Проверяем результат
            CollectionAssert.Contains(context.Tasks.All.ToList(), task);
        }
    }
}
