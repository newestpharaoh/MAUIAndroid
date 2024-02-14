using AndroidPatientAppMaui.ApiProviders;
using Autofac;
using CommunityToolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.BusinessCode
{
    public class AppSetup
    {
        public Autofac.IContainer CreateContainer()
        {
            ContainerBuilder cb = new ContainerBuilder();
            RegisterDepenencies(cb);
            return cb.Build();
        }

        protected virtual void RegisterDepenencies(ContainerBuilder cb)
        {
            Ioc.Default.ConfigureServices(
   new ServiceCollection()
    .AddSingleton<IApiProvider, ApiProvider>()
    .AddSingleton<IBusinessCode, BuisnessCode>()
   //.AddSingleton<IAlertService, AlertService>()
   //.AddSingleton<ITelephoneService, TelephoneService>()
   //.AddSingleton<IMediaService, MediaService>()
   .BuildServiceProvider());
        }
    }
}
