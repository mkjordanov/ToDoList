using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.Services.Contracts;
using ToDoList.Services;

namespace ToDoList.Web.App_Start.NinjectModules
{
    public class ServicesModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IToDoListModelService>().To<ToDoListModelService>();
            this.Bind<IUserService>().To<UserService>();
        }
    }
}