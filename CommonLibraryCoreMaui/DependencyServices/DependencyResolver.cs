﻿using SimpleInjector;
using System;

namespace CommonLibraryCoreMaui
{
    public class DependencyResolver
    {
        public static Container Container { get; private set; }

        public static void SetupContainer(Container container)
        {
            Container = container;
        }

        public static T Get<T>() where T : class
        {
            if (Container == null) throw new InvalidOperationException("Cannot resolve dependencies before the container has been initialized.");
            var producer = Container.GetRegistration(typeof(T));
            if (producer != null)
            {
                return Container.GetInstance<T>();
            }
            return null;
        }
    }
}
