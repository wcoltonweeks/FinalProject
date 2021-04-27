using System.Web.Optimization;

namespace FinalProject.UI.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/css/_site-blocks.css",
                    "~/Content/fonts/icomoon/style.css",
                    "~/Content/css/bootstrap.min.css",
                    "~/Content/css/jquery-ui.css",
                    "~/Content/css/owl.carousel.min.css",
                    "~/Content/css/owl.theme.default.min.css",
                    "~/Content/css/jquery.fancybox.min.css",
                    "~/Content/css/magnific-popup.css",
                    "~/Content/css/mediaelementplayer.css",
                    "~/Content/fonts/flaticon/font/flaticon.css",
                    "~/Content/fonts/flaticon-covid/font/flaticon.css",
                    "~/Content/css/aos.css",
                    "~/Content/css/style.css",
                    "~/Content/css/custom.css"
                ));

            bundles.Add(new ScriptBundle("~/Content/js").Include("~/Content/js/jquery-3.3.1.min.js",
                    "~/Content/js/jquery-ui.js",
                    "~/Content/js/bootstrap.min.js",
                    "~/Content/js/owl.carousel.min.js",
                    "~/Content/js/jquery.countdown.min.js",
                    "~/Content/js/jquery.easing.1.3.js",
                    "~/Content/js/aos.js",
                    "~/Content/js/jquery.fancybox.min.js",
                    "~/Content/js/jquery.sticky.js",
                    "~/Content/js/isotope.pkgd.min.js",
                    "~/Content/js/main.js"
                ));
        }
    }
}
