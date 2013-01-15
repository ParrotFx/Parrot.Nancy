using Parrot.Infrastructure;
using Parrot.Renderers;
using Parrot.Renderers.Infrastructure;

namespace Nancy.ViewEngines.Parrot
{
    /// <summary>
    /// Default renderer factory for Nancy
    /// Includes Nancy specific implementations for
    /// content and layout
    /// </summary>
    public class DefaultRendererFactory : RendererFactory
    {
        public DefaultRendererFactory(IHost host): base(new IRenderer[]
                {
                    new HtmlRenderer(host),
                    new StringLiteralRenderer(host),
                    new DocTypeRenderer(host),
                    new ContentRenderer(host),
                    new ForeachRenderer(host),
                    new InputRenderer(host),
                    new ConditionalRenderer(host),
                    new ListRenderer(host),
                    new SelfClosingRenderer(host)
                }) { }
    }
}