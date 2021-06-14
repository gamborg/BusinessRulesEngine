using BusinessRulesEngine.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BusinessRulesEngine.Handlers
{
    public class BusinessRulesFactory
    {
        private readonly IReadOnlyDictionary<string, IBusinessRuleHandler> _notificationProviders;
        public BusinessRulesFactory(IServiceProvider serviceProvider)
        {
            var businessRulesProviderType = typeof(IBusinessRuleHandler);
            _notificationProviders = businessRulesProviderType.Assembly.ExportedTypes
                .Where(x => businessRulesProviderType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x =>
                {
                    var constructorWithMostParams = x.GetConstructors().OrderByDescending(x => x.GetParameters().Length).First();
                    var requiredServices = GetRequiredServices(constructorWithMostParams, serviceProvider);

                    if (requiredServices.IsValid)
                    {
                        return Activator.CreateInstance(x, requiredServices.Args.ToArray());
                    }

                    return Activator.CreateInstance(x);
                })
                .Cast<IBusinessRuleHandler>()
                .ToImmutableDictionary(x => x.HandlerName, x => x);
        }

        internal (bool IsValid, List<object> Args) GetRequiredServices(ConstructorInfo constructor, IServiceProvider serviceProvider)
        {
            var paramsInfo = constructor.GetParameters();
            List<object> args = new List<object>();
            foreach (var param in paramsInfo)
            {
                var service = serviceProvider.GetService(param.ParameterType);

                if (service == null)
                {
                    break;
                }

                args.Add(service);
            }
            return ((args.Count == paramsInfo.Count() && paramsInfo.Count() > 0), args);
        }

        public IBusinessRuleHandler GetProviderByName(string businessRuleName)
        {
            var provider = _notificationProviders.GetValueOrDefault(businessRuleName);
            return provider ?? DefaultNotificationProvidersProvider();
        }

        private IBusinessRuleHandler DefaultNotificationProvidersProvider()
        {
            throw new NotImplementedException("Service not found!");
        }

        public bool IsValidService(string name)
        {
            return _notificationProviders.ContainsKey(name);
        }
    }
}
