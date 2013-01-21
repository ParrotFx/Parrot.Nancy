namespace Parrot.Nancy
{
    using Parrot.Infrastructure;
    using Parrot.Renderers.Infrastructure;

    /// <summary>
    /// Nancy host for Parrot
    /// </summary>
    public class NancyParrotHost : IHost
    {
        public NancyParrotHost(IModelValueProviderFactory modelValueProviderFactory, IPathResolver pathResolver)
        {
            ModelValueProviderFactory = modelValueProviderFactory;
            PathResolver = pathResolver;
            RendererFactory = new DefaultRendererFactory(this);
        }

        public IParrotWriter CreateWriter()
        {
            return new StandardWriter();
        }

        public IModelValueProviderFactory ModelValueProviderFactory { get; set; }
        public IRendererFactory RendererFactory { get; set; }
        public IPathResolver PathResolver { get; set; }
    }
}