using System.Web;
using System.Web.Optimization;

namespace PhotoGallery.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // Dropzone
            bundles.Add(new ScriptBundle("~/bundles/dropzone").Include(
                        "~/Scripts/dropzone.js"));
            bundles.Add(new StyleBundle("~/Content/dropzone").Include(
                      "~/Content/dropzone.css"));

            // Bootstrap Typeahead
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-tagsinput").Include(
                "~/Scripts/bootstrap3-typeahead.js",
                "~/Scripts/bootstrap-tagsinput.js"));
            bundles.Add(new StyleBundle("~/Content/bootstrap-tagsinput").Include(
                "~/Content/bootstrap-tagsinput-typeahead.css",
                "~/Content/bootstrap-tagsinput.css"));

            // HandleBars
            bundles.Add(new ScriptBundle("~/bundles/handlebars").Include(
                "~/Scripts/handlebars-v4.0.10.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery.timeago").Include(
                "~/Scripts/jquery.timeago.js")); 

            // Photos -> Index
            bundles.Add(new ScriptBundle("~/bundles/photos-index").Include(
                "~/Scripts/photos-index.js"));
            
        }
    }
}
