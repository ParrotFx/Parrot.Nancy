namespace Parrot.Nancy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::Nancy.ViewEngines;

    public class ParrotViewLocator
    {
        private readonly IEnumerable<ViewLocationResult> _viewLocationResults;

        public ParrotViewLocator(IEnumerable<ViewLocationResult> viewLocationResults)
        {
            _viewLocationResults = viewLocationResults;
        }

        public ViewLocationResult LocateView(string viewName)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                return null;
            }

            //i need some help here
            var matches = _viewLocationResults.Where(f => f.Name.Equals(viewName, StringComparison.OrdinalIgnoreCase)).ToList();
            if (matches.Any())
            {
                //look in directory first
                //return the first for now
                return matches.First();
            }

            return null;
        }
    }
}
