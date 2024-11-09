using System;

namespace Infrastructure.Util
{
    public static class ServiceLocator
    {
        private static IServiceProvider _serviceProvider;

        public static void SetLocatorProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public static T GetService<T>() where T : class
        {
            if (_serviceProvider == null)
            {
                throw new NullReferenceException();
            }

            return _serviceProvider.GetService(typeof(T)) as T;
        }
    }
}
