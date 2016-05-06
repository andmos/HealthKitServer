namespace HealthKitServer
{
    public interface ISimpleContainer
    {
        void Register<TInterface, TClass>() where TClass : TInterface, new();
        void RegisterSingleton<TInterface>(object instance);
        TInterface Resolve<TInterface>() where TInterface : class;
        TInterface Singleton<TInterface>() where TInterface : class;
    }
}
