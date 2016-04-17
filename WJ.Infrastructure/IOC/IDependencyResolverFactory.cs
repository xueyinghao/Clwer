namespace WJ.Infrastructure.IOC
{
    public interface IDependencyResolverFactory
    {
        IDependencyResolver CreateInstance();
    }
}