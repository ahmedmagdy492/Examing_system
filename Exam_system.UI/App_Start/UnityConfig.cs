using Exam_system.UI.Contracts;
using Exam_system.UI.Controllers;
using Exam_system.UI.Models;
using Exam_system.UI.Repository;
using System;

using Unity;
using Unity.Injection;

namespace Exam_system.UI
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IRepository<Exam>, MainRepository<Exam>>();
            container.RegisterType<IRepository<Subject>, MainRepository<Subject>>();
            container.RegisterType<IincludeNavigation<Answer>, AnswerRepository>();
            container.RegisterType<IRepository<Question>, MainRepository<Question>>();
            container.RegisterType<IPostsRepository, PostsRepository>();
            container.RegisterType<IExamCorrection, ExamCorrection>();

            container.RegisterType<IEnrollable, EnrollRepository>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());            
        }

    }
}