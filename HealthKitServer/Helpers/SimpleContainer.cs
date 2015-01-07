using System;
using System.Collections.Generic;

namespace HealthKitServer
{
    public class SimpleContainer : ISimpleContainer
    {
        private readonly Dictionary<Type, Func<object>> m_services;
        private readonly Dictionary<Type, Lazy<object>> m_singletons; 

        public SimpleContainer()
        {
            m_services = new Dictionary<Type, Func<object>>();
            m_singletons = new Dictionary<Type, Lazy<object>>();
        }

        public void Register<TInterface, TClass>() where TClass : TInterface, new()
        {
            var type = typeof(TInterface);
            Func<object> ctor = () => new TClass();
            if (m_services.ContainsKey(type))
            {
                m_services[type] = ctor;
                return;
            }

            m_services.Add(type, ctor);
        }

        public void RegisterSingleton<TInterface>(object instance)
        {
            m_singletons.Add(typeof(TInterface), new Lazy<object>(() => instance));
        }

        public TInterface Resolve<TInterface>() where TInterface : class
        {
            return (TInterface)m_services[typeof(TInterface)]();
        }

        public TInterface Singleton<TInterface>() where TInterface : class
        {
            var type = typeof(TInterface);
            if (!m_singletons.ContainsKey(type))
            {
                m_singletons.Add(type, new Lazy<object>(m_services[typeof(TInterface)]));
            }

            return (TInterface)m_singletons[typeof(TInterface)].Value;
        }
    }
}
