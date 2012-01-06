using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevelopmentStack.Domain.Entities;
using DevelopmentStack.Tasks.ViewModels;
using SharpLite.Domain.DataInterfaces;

namespace DevelopmentStack.Tasks
{
    public class StackCudTasks : BaseEntityCudTasks<Stack, EditStackViewModel>
    {
        public StackCudTasks(IRepository<Stack> stackRepository) : base(stackRepository) { }



        protected override void TransferFormValuesTo(Stack toUpdate, Stack fromForm)
        {
            toUpdate.Title = fromForm.Title;
            toUpdate.Description = fromForm.Title;

            if (toUpdate.Id == 0)
            {
                toUpdate.PostDate = DateTime.Now;
            }

            //toUpdate.PostBy = fromForm.PostBy;
            //toUpdate.Tags

        }
    }
}
