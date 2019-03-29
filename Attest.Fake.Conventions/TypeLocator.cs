﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Solid.Common;
using Solid.Practices.Composition;

namespace Attest.Fake.Conventions
{
    /// <summary>
    /// Conventions helper
    /// </summary>
    public static class TypeLocator
    {
        /// <summary>
        /// Find matches between providers' contracts and their respective builders
        /// </summary>
        /// <returns></returns>
        public static Dictionary<Type, Type> FindContractToBuilderMatches()
        {
            var assembliesProvider = new CustomAssemblySourceProvider(PlatformProvider.Current.GetRootPath(), null,
                new[] { ConventionsStore.ContractsAssemblyEnding(), ConventionsStore.BuildersAssemblyEnding() });
            var assemblies = assembliesProvider.Assemblies.ToArray();
            var contractTypes = assemblies.FindContractTypes();
            var contractToBuilderMatches = FindContractToBuilderMatchesImpl(assemblies, contractTypes);
            return contractToBuilderMatches;
        }

        /// <summary>
        /// Find matches between providers' simulators and their respective builders
        /// </summary>
        /// <returns></returns>
        public static Dictionary<Type, Type> FindSimulatorToBuilderMatches()
        {
            var assembliesProvider = new CustomAssemblySourceProvider(PlatformProvider.Current.GetRootPath(), null,
                new[] { ConventionsStore.SimulatorsAssemblyEnding(), ConventionsStore.BuildersAssemblyEnding() });
            var assemblies = assembliesProvider.Assemblies.ToArray();
            var contractTypes = assemblies.FindSimulatorsTypes();
            var contractToBuilderMatches = FindSimulatorToBuilderMatchesImpl(assemblies, contractTypes);
            return contractToBuilderMatches;
        }

        private static Dictionary<Type, Type> FindContractToBuilderMatchesImpl(
            this IEnumerable<Assembly> assemblies,
            Type[] contractTypes)
        {
            var buildersTypes = assemblies.FindBuildersTypes();
            var contractToBuilderMatches = new Dictionary<Type, Type>();
            foreach (var builderType in buildersTypes)
            {
                var contractType =
                    contractTypes.FirstOrDefault(
                        t => t.Name == "I" + builderType.Name.Replace(ConventionsStore.BuilderEnding(), string.Empty));
                if (contractType != null)
                {
                    contractToBuilderMatches.Add(contractType, builderType);
                }
            }
            return contractToBuilderMatches;
        }

        private static Dictionary<Type, Type> FindSimulatorToBuilderMatchesImpl(
            this IEnumerable<Assembly> assemblies,
            Type[] simulatorTypes)
        {
            var buildersTypes = assemblies.FindBuildersTypes();
            var simulatorToBuilderMatches = new Dictionary<Type, Type>();
            foreach (var builderType in buildersTypes)
            {
                var simulatorType =
                    simulatorTypes.FirstOrDefault(
                        t => t.Name == "I" + builderType.Name.Replace(ConventionsStore.BuilderEnding(), string.Empty)
                                 .Replace(ConventionsStore.ProviderEnding(), ConventionsStore.SimulatorEnding()));
                if (simulatorType != null)
                {
                    simulatorToBuilderMatches.Add(simulatorType, builderType);
                }
            }
            return simulatorToBuilderMatches;
        }
       
        internal static Type[] FindContractTypes(this IEnumerable<Assembly> assemblies) => assemblies.FindTypes(
            ConventionsStore.ContractsAssemblyEnding(),
            t => t.InterfaceEndsWith(ConventionsStore.ProviderEnding()));

        internal static Type[] FindFakeTypes(this IEnumerable<Assembly> assemblies) => assemblies.FindTypes(
            ConventionsStore.FakeAssemblyEnding(),
            t => t.ClassEndsWith(ConventionsStore.ProviderEnding()));

        internal static Type[] FindBuildersTypes(this IEnumerable<Assembly> assemblies)
        {
            var buildersAssemblies = assemblies.Where(t => t.GetName().Name.EndsWith(ConventionsStore.BuildersAssemblyEnding()));
            var buildersTypes = buildersAssemblies.SelectMany(k => k.DefinedTypes
                .Where(t => t.ClassEndsWith(ConventionsStore.BuilderEnding()))
                .Select(t => t.AsType())).ToArray();
            return buildersTypes;
        }

        internal static Type[] FindSimulatorsTypes(this IEnumerable<Assembly> assemblies) => assemblies.FindTypes(
            ConventionsStore.SimulatorsAssemblyEnding(),
            t => t.InterfaceEndsWith(ConventionsStore.SimulatorEnding()));

        private static Type[] FindTypes(this IEnumerable<Assembly> assemblies, string assemblyEnding,
            Func<TypeInfo, bool> criterion) => assemblies.Where(t => t.GetName().Name.EndsWith(assemblyEnding))
            .SelectMany(k => k.DefinedTypes
                .Where(criterion)
                .Select(t => t.AsType())).ToArray();
    }
}