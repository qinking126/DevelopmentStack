using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SharpLite.Domain.DataInterfaces;
using DevelopmentStack.Domain.Entities;
using DevelopmentStack.Tasks;
using DevelopmentStack.Domain;

namespace DevelopmentStack.Web.Controllers
{
    public class StackController : Controller
    {
        private readonly IRepository<Stack> _stackRepository;
        private readonly IRepository<User> _userRepository;
        private readonly StackCudTasks _stackCudTasks;

        public StackController(IRepository<Stack> stackRepository, IRepository<User> userRepository, StackCudTasks stackCudTasks)
        {
            _stackRepository = stackRepository;
            _userRepository = userRepository;
            _stackCudTasks = stackCudTasks;

        }

        public ActionResult Index()
        {
            return View(_stackRepository.GetAll()); //.OrderByDescending(s=>s.Title));
        }

        [Authorize]
        public ActionResult Add()
        {
            return View("Add", _stackCudTasks.CreateEditViewModel());
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Stack stack)
        {
            if (ModelState.IsValid)
            {   
                stack.PostDate = DateTime.Now;
                

                ActionConfirmation<Stack> confirmation = _stackCudTasks.SaveOrUpdate(stack);

                if (confirmation.WasSuccessful)
                {
                    TempData["message"] = confirmation.Message;
                    return RedirectToAction("Index");
                }

                ViewBag["message"] = confirmation.Message;
            }

            return View(_stackCudTasks.CreateEditViewModel(stack));

        }
    }
}
