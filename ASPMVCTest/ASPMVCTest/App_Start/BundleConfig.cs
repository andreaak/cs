using System.Web.Optimization;

namespace _01_ASPMVCTest
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Создание бандла ~/bundles/jquery в который войдут файлы jquery-* 
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            /* 
            modernizr-* - такая запись подгрузит все скрипты, которые начинаются с modernizr-
            это позволяет избежать необходимости пересобрать проект после замены библиотеки modernizr на новую версию
            */
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/javaScripts")
                        .Include("~/Scripts/jquery*")
                        .Include("~/Scripts/knockout-2.1.0.js"));

            /*
            StyleBundle - бандл для стилей. Минимизирует и объединяет несколько CSS файлов.
            Для того что бы не нарушить пути к ресурсам определенным в CSS файлах, 
            название папки такое же как и папки с исходными стилями.
            */
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            // Данный StyleBundle будет работать не правильно из-за пусти ~/bundles/css
            //bundles.Add(new StyleBundle("~/bundles/css")
            //    .Include("~/Content/Site.css")



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
        }
    }
}