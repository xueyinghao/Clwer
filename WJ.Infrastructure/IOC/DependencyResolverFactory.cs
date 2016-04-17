namespace WJ.Infrastructure.IOC
{
    using System;
    using WJ.Infrastructure.Core;

    public class DependencyResolverFactory : IDependencyResolverFactory
    {
        private readonly Type _resolverType;

        public DependencyResolverFactory(string resolverTypeName)
        {
            Check.Argument.IsNotEmpty(resolverTypeName, "resolverTypeName");

            _resolverType = Type.GetType(resolverTypeName, true, true);
        }

        public DependencyResolverFactory()
            : this(Util.ConfigUtil.GetConfig("dependencyResolverTypeName"))
        {
        }

        public IDependencyResolver CreateInstance()
        {
            return Activator.CreateInstance(_resolverType) as IDependencyResolver;
        }
    }
}