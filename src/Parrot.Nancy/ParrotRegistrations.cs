using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy.Bootstrapper;
using Parrot.Infrastructure;
using Parrot.Renderers.Infrastructure;

namespace Nancy.ViewEngines.Parrot
{
    /// <summary>
    /// Register the dependencies with Nancy
    /// </summary>
    public class ParrotRegistrations : IApplicationRegistrations
    {
        public IEnumerable<TypeRegistration> TypeRegistrations
        {
            get
            {
                yield return new TypeRegistration(typeof(IRendererFactory), typeof(DefaultRendererFactory));
                yield return new TypeRegistration(typeof(IViewEngine), typeof(ParrotViewEngine));
            }
        }
        public IEnumerable<CollectionTypeRegistration> CollectionTypeRegistrations { get { return null; } }
        public IEnumerable<InstanceRegistration> InstanceRegistrations { get { return null; } }
    }
}