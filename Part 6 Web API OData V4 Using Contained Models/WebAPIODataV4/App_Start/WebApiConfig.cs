using System.Linq;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using Microsoft.OData.Edm;
using Unity.AspNet.WebApi;
using WebAPIODataV4.Models;

namespace WebAPIODataV4
{
    public static class WebApiConfig
    {
        public static HttpConfiguration Register()
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            config.DependencyResolver = new UnityDependencyResolver(UnityConfig.Container);
            RegisterFilterProviders(config);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Count().Filter().OrderBy().Expand().Select().MaxTop(null);
            config.MapODataServiceRoute("odata", "odata", model: GetModel());

            return config;
        }

        public static IEdmModel GetModel()
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder();

            builder.EntitySet<Address>("Address");
            builder.EntitySet<AddressType>("AddressType");
            builder.EntitySet<BusinessEntity>("BusinessEntity");
            builder.EntitySet<BusinessEntityAddress>("BusinessEntityAddress");
            builder.EntitySet<BusinessEntityContact>("BusinessEntityContact");
            builder.EntitySet<ContactType>("ContactType");
            builder.EntitySet<CountryRegion>("CountryRegion");
            builder.EntitySet<EmailAddress>("EmailAddress");
            builder.EntitySet<Password>("Password");
            builder.EntitySet<Person>("Person");
            builder.EntitySet<PersonPhone>("PersonPhone");
            builder.EntitySet<PhoneNumberType>("PhoneNumberType");
            builder.EntitySet<StateProvince>("StateProvince");

            EntitySetConfiguration<EntityWithEnum> entitesWithEnum
                = builder.EntitySet<EntityWithEnum>("EntityWithEnum");

            FunctionConfiguration functionEntitesWithEnum
                = entitesWithEnum.EntityType.Collection.Function("PersonSearchPerPhoneType");

            functionEntitesWithEnum.Parameter<PhoneNumberTypeEnum>("PhoneNumberTypeEnum");
            functionEntitesWithEnum.ReturnsCollectionFromEntitySet<EntityWithEnum>("EntityWithEnum");

            EntitySetConfiguration<ContactType> contactType = builder.EntitySet<ContactType>("ContactType");
            var actionY = contactType.EntityType.Action("ChangePersonStatus");
            actionY.Parameter<string>("Level");
            actionY.Returns<bool>();

            var changePersonStatusAction = contactType.EntityType.Collection.Action("ChangePersonStatus");
            changePersonStatusAction.Parameter<string>("Level");
            changePersonStatusAction.Returns<bool>();

            EntitySetConfiguration<Person> persons = builder.EntitySet<Person>("Person");

            FunctionConfiguration myFirstFunction = persons.EntityType.Collection.Function("MyFirstFunction");
            myFirstFunction.ReturnsCollectionFromEntitySet<Person>("Person");

            builder.EntitySet<AnimalType>("AnimalType");
            builder.EntitySet<EventData>("EventData");

            builder.EntitySet<Player>("Player");
            builder.EntityType<PlayerStats>();

            return builder.GetEdmModel();
        }

        private static void RegisterFilterProviders(HttpConfiguration config)
        {
            var providers = config.Services.GetFilterProviders().ToList();
            config.Services.Add(typeof(System.Web.Http.Filters.IFilterProvider), new WebApiUnityActionFilterProvider(UnityConfig.Container));
            var defaultprovider = providers.First(p => p is ActionDescriptorFilterProvider);
            config.Services.Remove(typeof(System.Web.Http.Filters.IFilterProvider), defaultprovider);
        }
    }
}
