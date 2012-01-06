using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;
using DevelopmentStack.Domain.Entities;
using SharpLite.Domain.DataInterfaces;

namespace DevelopmentStack.Web.Binders
{
    public class UserModelBinder : IModelBinder
    {
        private readonly IRepository<User> _userRepository;

        public UserModelBinder(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var email = HttpContext.Current.User.Identity.Name;

            var user = _userRepository.GetAll().Where(u => u.Email == email.ToString()).FirstOrDefault();

            return user;
        }
    }
}