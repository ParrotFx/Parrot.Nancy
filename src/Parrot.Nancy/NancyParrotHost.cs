using System;
using System.Reflection;
using System.Text;
using Parrot.Infrastructure;
using Parrot.Renderers;
using Parrot.Renderers.Infrastructure;

namespace Nancy.ViewEngines.Parrot
{
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