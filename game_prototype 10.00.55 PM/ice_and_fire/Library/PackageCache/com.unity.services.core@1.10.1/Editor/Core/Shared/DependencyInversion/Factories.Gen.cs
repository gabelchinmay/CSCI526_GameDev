// WARNING: Auto generated code. Modifications will be lost!
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Unity.Services.Core.Editor.Shared.DependencyInversion
{
    static class Factories
    {
        const string k_InitMethod = "Initialize";

        public static T Default<T>(IServiceProvider sp)
        {
            return Default<T, T>(sp);
        }

        public static TInterface Default<TInterface, TImplementation>(IServiceProvider sp) where TImplementation : TInterface
        {
            var ctr = typeof(TImplementation).GetConstructors().SingleOrDefault(p => p.IsPublic);
            if (ctr == null)
            {
                ctr = typeof(TImplementation)
                    .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
                    .SingleOrDefault(p => p.IsAssembly);
                if (ctr == null)
                {
                    throw new ConstructorNotFoundException(typeof(TImplementation));
                }
            }

            var parameters = ctr.GetParameters();
            var types = parameters.Select(t => sp.GetService(t.ParameterType));
            return (TInterface)ctr.Invoke(types.ToArray());
        }

        public static void InitializeInstance(IServiceProvider sp, object instance)
        {
            var init = instance.GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .SingleOrDefault(p => p.IsPublic && p.Name == k_InitMethod);
            if (init == null)
            {
                throw new MethodNotFoundException(instance.GetType(), k_InitMethod);
            }

            var parameters = init.GetParameters();
            var types = parameters.Select(t =>
            {
                try
                {
                    return sp.GetService(t.ParameterType);
                }
                catch (Exception)
                {
                    Debug.Log(t.ParameterType);
                    throw;
                }
            });
            init.Invoke(instance, types.ToArray());
        }
    }
}
