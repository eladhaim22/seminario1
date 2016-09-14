using System.Web;
using System.Web.Optimization;

namespace Seminario.Web
{
	public class BundleConfig
	{
		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
						"~/Scripts/jquery-ui-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.unobtrusive*",
						"~/Scripts/jquery.validate*"));

			bundles.Add(new ScriptBundle("~/bundles/angular").Include(
					 "~/App/Controllers/*.js",
					 "~/App/Directive/*.js",
					 "~/App/Services/*.js"
					));
			bundles.Add(new ScriptBundle("~/bundles/Scripts").Include(
			   "~/Scripts/angular.js",
			   "~/Scripts/angular-route.js",
			   "~/Scripts/angular-sanitize.js",
			   "~/Scripts/bootstrap.js",
			   "~/Scripts/moment.js",
			   "~/Scripts/ui-grid.js",
			   "~/Scripts/angular-ui/ui-bootstrap-tpls-2.0.2.min.js",
			   "~/Scripts/lodash.js"
			));
			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
				"~/Content/site.css",
				"~/Content/bootstrap.css",
				"~/Content/ui-bootstrap-csp.css",
				"~/Content/font-awesome.css",
				"~/Content/sb-admin-2.css",
				"~/Content/timeline.css",
				"~/Content/DataTables/css/buttons.dataTables.css",
				"~/Content/DataTables/css/dataTables.editor.css",
				"~/Content/bootstrap-datetimepicker.less",
				"~/Content/ui-grid.css"));

			bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
						"~/Content/themes/base/jquery.ui.core.css",
						"~/Content/themes/base/jquery.ui.resizable.css",
						"~/Content/themes/base/jquery.ui.selectable.css",
						"~/Content/themes/base/jquery.ui.accordion.css",
						"~/Content/themes/base/jquery.ui.autocomplete.css",
						"~/Content/themes/base/jquery.ui.button.css",
						"~/Content/themes/base/jquery.ui.dialog.css",
						"~/Content/themes/base/jquery.ui.slider.css",
						"~/Content/themes/base/jquery.ui.tabs.css",
						"~/Content/themes/base/jquery.ui.datepicker.css",
						"~/Content/themes/base/jquery.ui.progressbar.css",
						"~/Content/themes/base/jquery.ui.theme.css"));
			bundles.Add(new StyleBundle("~/bundles/Styles").Include(
				"~/Scripts/*.css"
			));
		}
	}
}
