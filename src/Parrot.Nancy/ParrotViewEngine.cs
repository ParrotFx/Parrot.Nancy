namespace Parrot.Nancy
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Parrot.Renderers.Infrastructure;
    using global::Nancy;
    using global::Nancy.Responses;
    using global::Nancy.ViewEngines;

    public class ParrotViewEngine : IViewEngine
    {
        private readonly IModelValueProviderFactory _modelValueProviderFactory;
        private ViewEngineStartupContext _viewEngineStartupContext;
        private ParrotViewLocator _parrotViewLocator;

        public IEnumerable<string> Extensions
        {
            get { return new[] { "parrot" }; }
        }

        public ParrotViewEngine(IModelValueProviderFactory modelValueProviderFactory)
        {
            _modelValueProviderFactory = modelValueProviderFactory;
        }

        public void Initialize(ViewEngineStartupContext viewEngineStartupContext)
        {
            _viewEngineStartupContext = viewEngineStartupContext;
            _parrotViewLocator = new ParrotViewLocator(_viewEngineStartupContext.ViewLocationResults);
        }

        public Response RenderView(ViewLocationResult viewLocationResult, dynamic model, IRenderContext renderContext)
        {
            //we already have a view but we still need a view provider
            return new HtmlResponse(contents: stream =>
                {
                    var host = new NancyParrotHost(_modelValueProviderFactory, new NancyPathResolver(renderContext));
                    var parrotWriter = host.CreateWriter();
                    var rendererFactory = host.RendererFactory;
                    var view = new ParrotView(host, rendererFactory, _parrotViewLocator, viewLocationResult.Contents, parrotWriter);
                    var writer = new StreamWriter(stream);
                    
                    view.Render(model, writer);

                    writer.Flush();
                });
        }
    }

    public class NancyPathResolver : IPathResolver
    {
        private readonly IRenderContext _renderContext;

        public NancyPathResolver(IRenderContext renderContext)
        {
            _renderContext = renderContext;
        }

        public Stream OpenFile(string path)
        {
            throw new System.NotImplementedException();
        }

        public bool FileExists(string path)
        {
            throw new System.NotImplementedException();
        }

        public string ResolvePath(string path)
        {
            throw new System.NotImplementedException();
        }

        public string ResolveAttributeRelativePath(string key, object value)
        {
            if (value != null)
            {
                string temp = value.ToString();
                if (temp.StartsWith("~/") && !key.StartsWith("data-val", StringComparison.OrdinalIgnoreCase))
                {
                    //convert this to a server path

                    return _renderContext.ParsePath(temp);

                }
                return temp;
            }

            return null;

        }
    }
}