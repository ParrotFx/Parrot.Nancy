using System;
using Parrot.Renderers.Infrastructure;

namespace Nancy.ViewEngines.Parrot
{
    class AttributeRenderer : IAttributeRenderer
    {
        private readonly IRenderContext _renderContext;

        public AttributeRenderer(IRenderContext renderContext)
        {
            _renderContext = renderContext;
        }

        public string PostRender(string key, object value)
        {
            if (value != null)
            {
                string temp = value.ToString();
                if (temp.StartsWith("~/") && !key.StartsWith("data-val", StringComparison.OrdinalIgnoreCase))
                {
                    //convert this to a server path
                    return this._renderContext.ParsePath(temp);
                }

                return temp;
            }

            return null;
        }
    }
}