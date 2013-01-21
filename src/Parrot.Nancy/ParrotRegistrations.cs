namespace Parrot.Nancy
{
    using System.Collections.Generic;
    using Parrot.Renderers.Infrastructure;
    using global::Nancy.Bootstrapper;
    using global::Nancy.ViewEngines;

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