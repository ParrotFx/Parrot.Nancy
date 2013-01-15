using System;
using System.Collections.Generic;
using System.IO;
using Parrot.Infrastructure;
using Parrot.Nodes;
using Parrot.Parser;
using Parrot.Renderers;
using Parrot.Renderers.Infrastructure;

namespace Nancy.ViewEngines.Parrot
{
    /// <summary>
    /// Parrot view for Nancy
    /// </summary>
    public class ParrotView
    {
        private readonly IHost _host;
        private readonly Func<TextReader> _contents;
        private readonly IParrotWriter _writer;
        private readonly IRendererFactory _rendererFactory;
        private readonly ParrotViewLocator _parrotViewLocator;

        public ParrotView(IHost host, IRendererFactory rendererFactory, ParrotViewLocator parrotViewLocator, Func<TextReader> contents, IParrotWriter writer)
        {
            _host = host;
            _rendererFactory = rendererFactory;
            _parrotViewLocator = parrotViewLocator;
            _contents = contents;
            _writer = writer;

            _rendererFactory.RegisterFactory(new LayoutRenderer(_host, _parrotViewLocator));
        }

        public void Render(dynamic model, IParrotWriter writer)
        {
            //View contents
            using (var stream = _contents())
            {
                var contents = stream.ReadToEnd();
                contents = Parse(model, contents);

                string output = contents;

                writer.Write(output);
            }
        }

        public void Render(dynamic model, StreamWriter writer)
        {
            var _writer = _host.CreateWriter();

            Render(model, _writer);

            writer.Write(_writer.Result());
        }

        internal Document LoadDocument(string template)
        {
            Parser parser = new Parser();
            Document document;

            if (parser.Parse(template, out document))
            {
                return document;
            }

            throw new Exception("Unable to parse: ");
        }

        private string Parse(object model, string template)
        {
            var document = LoadDocument(template);
            var documentHost = CreateDocumentHost(model);
            
            var view = new DocumentView(_host, _rendererFactory, documentHost, document);

            view.Render(_writer);

            return _writer.Result();
        }

        private static Dictionary<string, object> CreateDocumentHost(object model)
        {
            var documentHost = new Dictionary<string, object>();
            documentHost.Add("Model", model);
            return documentHost;
        }

    }
}