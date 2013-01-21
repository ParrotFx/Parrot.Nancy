namespace Parrot.Nancy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Parrot.Infrastructure;
    using Parrot.Nodes;
    using Parrot.Renderers;
    using Parrot.Renderers.Infrastructure;

    /// <summary>
    /// Layout renderer specific to Nancy
    /// </summary>
    public class LayoutRenderer : HtmlRenderer
    {
        private readonly IHost _host;
        private readonly ParrotViewLocator _parrotViewLocator;

        public override IEnumerable<string> Elements
        {
            get { yield return "layout"; }
        }

        public LayoutRenderer(IHost host, ParrotViewLocator parrotViewLocator) : base(host)
        {
            _host = host;
            _parrotViewLocator = parrotViewLocator;
        }

        public override void Render(IParrotWriter writer, IRendererFactory rendererFactory, Statement statement, IDictionary<string, object> documentHost, object model)
        {
            string layout = "";
            if (statement.Parameters != null && statement.Parameters.Any())
            {
                Type modelType = model != null ? model.GetType() : null;
                var modelValueProvider = Host.ModelValueProviderFactory.Get(modelType);

                var parameterLayout = GetLocalModelValue(documentHost, statement, modelValueProvider, model) ?? "_layout";

                //assume only the first is the path
                //second is the argument (model)
                layout = parameterLayout.ToString();
            }


            //ok...we need to load the view
            //then pass the model to it and
            //then return the result

            var result = _parrotViewLocator.LocateView(layout);

            if (result != null)
            {
                var parrotView = new ParrotView(_host, rendererFactory, _parrotViewLocator, result.Contents, writer);
                string contents = result.Contents().ReadToEnd();

                var document = parrotView.LoadDocument(contents);

                //Create a new DocumentView and DocumentHost
                if (!documentHost.ContainsKey("_LayoutChildren_"))
                {
                    documentHost.Add("_LayoutChildren_", new Queue<StatementList>());
                }
                (documentHost["_LayoutChildren_"] as Queue<StatementList>).Enqueue(statement.Children);

                DocumentView view = new DocumentView(_host, rendererFactory, documentHost, document);

                view.Render(writer);
            }
            else
            {
                throw new Exception(string.Format("Layout {0} could not be found", layout));
            }
        }
    }
}