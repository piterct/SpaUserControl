using SpaUserControl.Domain.Contracts.Services;
using SpaUserControl.Startup;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unity;

namespace SpaUserControl.Spammer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Idioma
            CultureInfo ci = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            var container = new UnityContainer();
            DependencyResolver.Resolve(container);

            var service = container.Resolve<IUserService>();

            try
            {
                service.Register("Michael Peter", "michael_piterct@hotmail.com", "michaelpeter", "michaelpeter");
                Console.WriteLine("Usuário cadastrado com sucesso!");
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
