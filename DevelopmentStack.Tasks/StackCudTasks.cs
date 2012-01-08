using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevelopmentStack.Domain.Entities;
using DevelopmentStack.Tasks.ViewModels;
using SharpLite.Domain.DataInterfaces;

namespace DevelopmentStack.Tasks
{
    using System.Web;

    public class StackCudTasks : BaseEntityCudTasks<Stack, EditStackViewModel>
    {
        private readonly IRepository<User> _userRepository;

        public StackCudTasks(IRepository<Stack> stackRepository, IRepository<User> userRepository) : base(stackRepository)
        {
            _userRepository = userRepository;
        }

        public override EditStackViewModel CreateEditViewModel()
        {
            EditStackViewModel editStackViewModel = base.CreateEditViewModel();
            User currentUser = _userRepository.GetAll()
                .Where(u => u.Email == HttpContext.Current.User.Identity.Name).FirstOrDefault();
            editStackViewModel.Stack.PostBy = currentUser;
            return editStackViewModel;
        }

        protected override void TransferFormValuesTo(Stack toUpdate, Stack fromForm)
        {
            toUpdate.Title = fromForm.Title;
            toUpdate.Description = fromForm.Title;

            if (toUpdate.Id == 0)
            {
                toUpdate.PostDate = DateTime.Now;
            }

            toUpdate.PostBy = fromForm.PostBy;
            //toUpdate.Tags

        }
    }
}
