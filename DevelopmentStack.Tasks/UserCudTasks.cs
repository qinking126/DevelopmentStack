using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevelopmentStack.Domain.Entities;
using DevelopmentStack.Tasks.ViewModels;
using SharpLite.Domain.DataInterfaces;

namespace DevelopmentStack.Tasks
{
    public class UserCudTasks : BaseEntityCudTasks<User, EditUserViewModel>
    {
        public UserCudTasks(IRepository<User> userRepository) : base(userRepository) { }

        protected override void TransferFormValuesTo(User toUpdate, User fromForm)
        {
            toUpdate.Name.FirstName = fromForm.Name.FirstName;
            toUpdate.Name.LastName = fromForm.Name.LastName;
            toUpdate.Avatar = fromForm.Avatar;
            toUpdate.Email = fromForm.Email;
            toUpdate.AccountTypeId = 2;
            toUpdate.OtherID = fromForm.OtherID;

            if (toUpdate.Id == 0)
            {
                toUpdate.CreateDate = DateTime.Now;
            }
        }
    }
}
