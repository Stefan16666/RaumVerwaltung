using System.Web;
using System.Web.Optimization;

namespace Innovation4Austria.web
{
    public class BundleConfig
    {
        // Weitere Informationen zu Bundling finden Sie unter "http://go.microsoft.com/fwlink/?LinkId=301862"
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                      "~/Scripts/jquery-{version}.js",
                      "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                        "~/Scripts/toastr.js"));

            bundles.Add(new ScriptBundle("~/bundles/datepickerJS").Include(
                    "~/Scripts/moment.js", "~/Scripts/daterangepicker.js"));

            //bundles.Add(new ScriptBundle("~/bundles/datepickerJS").Include(
            //       "~/Scripts/daterangepicker.js"));


            // Verwenden Sie die Entwicklungsversion von Modernizr zum Entwickeln und Erweitern Ihrer Kenntnisse. Wenn Sie dann
            // für die Produktion bereit sind, verwenden Sie das Buildtool unter "http://modernizr.com", um nur die benötigten Tests auszuwählen.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/style.css"));

            bundles.Add(new StyleBundle("~/Content/toastr").Include(
                "~/Content/toastr.css"));

            bundles.Add(new StyleBundle("~/bundles/datepicker").Include(
          "~/Content/daterangepicker.css"));

        }
    }
}
