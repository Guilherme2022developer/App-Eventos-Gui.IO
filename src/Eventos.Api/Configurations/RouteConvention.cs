using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Eventos.Api.Configurations;

public class RouteConvention : IApplicationModelConvention
{
    private readonly AttributeRouteModel _centralPrefix;


    public RouteConvention(IRouteTemplateProvider RouteTemplateProvider)
    {
        _centralPrefix  = new AttributeRouteModel(RouteTemplateProvider);
    }

    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
        {
            var matchedSelectors = controller.Selectors.Where(x => x.AttributeRouteModel != null).ToList();
            if (matchedSelectors.Any())
            {
                foreach (var selectModel in matchedSelectors)
                {
                    selectModel.AttributeRouteModel =
                        AttributeRouteModel.CombineAttributeRouteModel(_centralPrefix, selectModel.AttributeRouteModel);
                }
            }

            var unmatchedSelectors = controller.Selectors.Where(x => x.AttributeRouteModel == null).ToList();
            if (unmatchedSelectors.Any())
            {
                foreach (var selectModel in unmatchedSelectors)
                {
                    selectModel.AttributeRouteModel = _centralPrefix;
                }
            }
        }


    }
}

public static class MvcOptionsExtensions
{
    public static void UseCentralRoutePrefix(this MvcOptions opts, IRouteTemplateProvider routeAttribute)
    {
        opts.Conventions.Insert(0,new RouteConvention(routeAttribute));
    }
}