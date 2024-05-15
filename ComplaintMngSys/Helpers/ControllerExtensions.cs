using ComplaintMngSys.Models.CommonViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using static ComplaintMngSys.Helpers.StaticData;

namespace ComplaintMngSys.Helpers
{
    public static class ControllerExtensions
    {
        public static string GetConnectionString(IConfiguration Configuration)
        {
            var _ApplicationInfo = Configuration.GetSection("ApplicationInfo").Get<ApplicationInfo>();
            string _GetConnStringName = String.Empty;
            if (_ApplicationInfo.DBConnectionStringName == ConnectionStrings.connMSSQLNoCred)
            {
                _GetConnStringName = Configuration.GetConnectionString(ConnectionStrings.connMSSQLNoCred);
            }
            else if (_ApplicationInfo.DBConnectionStringName == ConnectionStrings.connMSSQL)
            {
                _GetConnStringName = Configuration.GetConnectionString(ConnectionStrings.connMSSQL);
            }
            else if (_ApplicationInfo.DBConnectionStringName == ConnectionStrings.connPostgreSQL)
            {
                _GetConnStringName = Configuration.GetConnectionString(ConnectionStrings.connPostgreSQL);
            }
            else if (_ApplicationInfo.DBConnectionStringName == ConnectionStrings.connMySQL)
            {
                _GetConnStringName = Configuration.GetConnectionString(ConnectionStrings.connMySQL);
            }
            else if (_ApplicationInfo.DBConnectionStringName == ConnectionStrings.connDockerBase)
            {
                _GetConnStringName = Configuration.GetConnectionString(ConnectionStrings.connDockerBase);
            }
            else if (_ApplicationInfo.DBConnectionStringName == ConnectionStrings.connMSSQLProd)
            {
                _GetConnStringName = Configuration.GetConnectionString(ConnectionStrings.connMSSQLProd);
            }
            else if (_ApplicationInfo.DBConnectionStringName == ConnectionStrings.connOthers)
            {
                _GetConnStringName = Configuration.GetConnectionString(ConnectionStrings.connOthers);
            }

            return _GetConnStringName;
        }
        public static async Task<string> RenderViewAsync<TModel>(this Controller controller, string viewName, TModel model, bool partial = false)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = controller.ControllerContext.ActionDescriptor.ActionName;
            }
            controller.ViewData.Model = model;
            using (var writer = new StringWriter())
            {
                IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult viewResult = viewEngine.FindView(controller.ControllerContext, viewName, !partial);
                if (viewResult.Success == false)
                {
                    return $"A view with the name {viewName} could not be found";
                }
                ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, writer, new HtmlHelperOptions());
                await viewResult.View.RenderAsync(viewContext);
                return writer.GetStringBuilder().ToString();
            }
        }
    }
}
