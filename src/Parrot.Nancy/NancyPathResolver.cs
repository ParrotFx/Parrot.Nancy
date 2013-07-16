namespace Parrot.Nancy
{
    using System;
    using System.IO;
    using Parrot.Renderers.Infrastructure;
    using global::Nancy.ViewEngines;

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