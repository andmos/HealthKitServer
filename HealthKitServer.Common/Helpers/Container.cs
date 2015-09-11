namespace HealthKitServer
{
    public static class Container
    {
        public static ISimpleContainer Instance { private get; set; }

        public static TInterface Resolve<TInterface>() where TInterface : class
        {
            return Instance.Resolve<TInterface>();
        }

        public static TInterface Singleton<TInterface>() where TInterface : class
        {
            return Instance.Singleton<TInterface>();
        }
    }
}
