using BusinessRulesEngine.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
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
                    //var parameterlessCtor = x.GetConstructors().SingleOrDefault(c => c.GetParameters().Length == 0);
                    var constructors = x.GetConstructors().OrderByDescending(x => x.GetParameters().Length);
                    foreach (var constructor in constructors)
                    {
                        var satisfyAll = false;
                        var paramsInfo = constructor.GetParameters();
                        List<object> args = new List<object>();
                        foreach (var param in paramsInfo)
                        {
                            if (serviceProvider.GetService(param.ParameterType) != null)
                            {
                                args.Add(serviceProvider.GetService(param.ParameterType));
                                satisfyAll = true;
                            }
                            else
                            {
                                satisfyAll = false;
                            }
                        }

                        if (satisfyAll)
                        {
                            return Activator.CreateInstance(x, args.ToArray());
                        }
                    }

                    return Activator.CreateInstance(x);
                })
                .Cast<IBusinessRuleHandler>()
                .ToImmutableDictionary(x => x.HandlerName, x => x);
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
