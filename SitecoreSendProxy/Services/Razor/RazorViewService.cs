using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;

namespace SitecoreSendProxy.Services.Razor
{
    public class RazorViewService
    {
        private readonly ICompositeViewEngine _viewEngine;
        private readonly ILogger<RazorViewService> _razorViewService;

        public RazorViewService(ICompositeViewEngine viewEngine, ILogger<RazorViewService> razorViewService)
        {
            _viewEngine = viewEngine;
            _razorViewService = razorViewService;
        }

        public Task<string> RenderPartialViewToString(Controller controller, ViewResult viewResult)
        {
            return RenderPartialViewToString(controller, viewResult.ViewName, viewResult.Model);
        }

        public async Task<string> RenderPartialViewToString(Controller controller, string viewName, object model)
        {
            _razorViewService.LogInformation("Render Partial '{viewName}'", viewName);
            if (string.IsNullOrEmpty(viewName))
                viewName = controller.ControllerContext.ActionDescriptor.ActionName;

            controller.ViewData.Model = model;

            await using var writer = new StringWriter();
            var viewResult =
                _viewEngine.FindView(controller.ControllerContext, viewName, false);

            var viewContext = new ViewContext(
                controller.ControllerContext,
                viewResult.View,
                controller.ViewData,
                controller.TempData,
                writer,
                new HtmlHelperOptions()
            );

            await viewResult.View.RenderAsync(viewContext);

            return writer.GetStringBuilder().ToString();
        }
    }
}