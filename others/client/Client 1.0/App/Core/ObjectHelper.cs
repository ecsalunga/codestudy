using System;

namespace Genting.Infrastructure.CommonServices.Client.Core
{
    public static class ObjectHelper
    {
        public static T CreateInstance<T>(string path)
        {
            Type type = Type.GetType(path);
            return (T)Activator.CreateInstance(type);
        }
    }
}