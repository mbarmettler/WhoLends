﻿// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
// 0108: suppress "Foo hides inherited member Foo. Use the new keyword if hiding was intended." when a controller and its abstract parent are both processed
// 0114: suppress "Foo.BarController.Baz()' hides inherited member 'Qux.BarController.Baz()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword." when an action (with an argument) overrides an action in a parent controller
#pragma warning disable 1591, 3008, 3009, 0108, 0114
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
public static partial class MVC
{
    public static WhoLends.Controllers.AccountController Account = new WhoLends.Controllers.T4MVC_AccountController();
    public static WhoLends.Controllers.HomeController Home = new WhoLends.Controllers.T4MVC_HomeController();
    public static WhoLends.Controllers.LendItemsController LendItems = new WhoLends.Controllers.T4MVC_LendItemsController();
    public static WhoLends.Controllers.LendsController Lends = new WhoLends.Controllers.T4MVC_LendsController();
    public static T4MVC.SharedController Shared = new T4MVC.SharedController();
}

namespace T4MVC
{
}

namespace T4MVC
{
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class Dummy
    {
        private Dummy() { }
        public static Dummy Instance = new Dummy();
    }
}

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal partial class T4MVC_System_Web_Mvc_ActionResult : System.Web.Mvc.ActionResult, IT4MVCActionResult
{
    public T4MVC_System_Web_Mvc_ActionResult(string area, string controller, string action, string protocol = null): base()
    {
        this.InitMVCT4Result(area, controller, action, protocol);
    }
     
    public override void ExecuteResult(System.Web.Mvc.ControllerContext context) { }
    
    public string Controller { get; set; }
    public string Action { get; set; }
    public string Protocol { get; set; }
    public RouteValueDictionary RouteValueDictionary { get; set; }
}



namespace Links
{
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static class Scripts {
        public const string UrlPath = "~/Scripts";
        public static string Url() { return T4MVCHelpers.ProcessVirtualPath(UrlPath); }
        public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(UrlPath + "/" + fileName); }
        public static readonly string ai_0_15_0_build58334_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/ai.0.15.0-build58334.min.js") ? Url("ai.0.15.0-build58334.min.js") : Url("ai.0.15.0-build58334.js");
        public static readonly string ai_0_15_0_build58334_min_js = Url("ai.0.15.0-build58334.min.js");
        public static readonly string bootstrap_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/bootstrap.min.js") ? Url("bootstrap.min.js") : Url("bootstrap.js");
        public static readonly string bootstrap_min_js = Url("bootstrap.min.js");
        public static readonly string jquery_1_9_1_intellisense_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/jquery-1.9.1.intellisense.min.js") ? Url("jquery-1.9.1.intellisense.min.js") : Url("jquery-1.9.1.intellisense.js");
        public static readonly string jquery_1_9_1_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/jquery-1.9.1.min.js") ? Url("jquery-1.9.1.min.js") : Url("jquery-1.9.1.js");
        public static readonly string jquery_1_9_1_min_js = Url("jquery-1.9.1.min.js");
        public static readonly string jquery_1_9_1_min_map = Url("jquery-1.9.1.min.map");
        public static readonly string modernizr_2_6_2_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/modernizr-2.6.2.min.js") ? Url("modernizr-2.6.2.min.js") : Url("modernizr-2.6.2.js");
        public static readonly string modernizr_2_6_2_min_js = Url("modernizr-2.6.2.min.js");
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static class Content {
        public const string UrlPath = "~/Content";
        public static string Url() { return T4MVCHelpers.ProcessVirtualPath(UrlPath); }
        public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(UrlPath + "/" + fileName); }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class bootstrap {
            public const string UrlPath = "~/Content/bootstrap";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(UrlPath); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(UrlPath + "/" + fileName); }
            public static readonly string bootstrap_theme_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/bootstrap-theme.min.css") ? Url("bootstrap-theme.min.css") : Url("bootstrap-theme.css");
            public static readonly string bootstrap_theme_css_map = Url("bootstrap-theme.css.map");
            public static readonly string bootstrap_theme_min_css = Url("bootstrap-theme.min.css");
            public static readonly string bootstrap_theme_min_css_map = Url("bootstrap-theme.min.css.map");
            public static readonly string bootstrap_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/bootstrap.min.css") ? Url("bootstrap.min.css") : Url("bootstrap.css");
            public static readonly string bootstrap_css_map = Url("bootstrap.css.map");
            public static readonly string bootstrap_min_css = Url("bootstrap.min.css");
            public static readonly string bootstrap_min_css_map = Url("bootstrap.min.css.map");
        }
    
        public static readonly string bootstrap_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/bootstrap.min.css") ? Url("bootstrap.min.css") : Url("bootstrap.css");
        public static readonly string bootstrap_min_css = Url("bootstrap.min.css");
        public static readonly string Site_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/Site.min.css") ? Url("Site.min.css") : Url("Site.css");
    }

    
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static partial class Bundles
    {
        public static partial class Scripts 
        {
            public static class Assets
            {
                public const string ai_0_15_0_build58334_js = "~/Scripts/ai.0.15.0-build58334.js"; 
                public const string ai_0_15_0_build58334_min_js = "~/Scripts/ai.0.15.0-build58334.min.js"; 
                public const string bootstrap_js = "~/Scripts/bootstrap.js"; 
                public const string bootstrap_min_js = "~/Scripts/bootstrap.min.js"; 
                public const string jquery_1_9_1_intellisense_js = "~/Scripts/jquery-1.9.1.intellisense.js"; 
                public const string jquery_1_9_1_js = "~/Scripts/jquery-1.9.1.js"; 
                public const string jquery_1_9_1_min_js = "~/Scripts/jquery-1.9.1.min.js"; 
                public const string modernizr_2_6_2_js = "~/Scripts/modernizr-2.6.2.js"; 
                public const string modernizr_2_6_2_min_js = "~/Scripts/modernizr-2.6.2.min.js"; 
            }
        }
        public static partial class Content 
        {
            public static partial class bootstrap 
            {
                public static class Assets
                {
                    public const string bootstrap_theme_css = "~/Content/bootstrap/bootstrap-theme.css";
                    public const string bootstrap_theme_min_css = "~/Content/bootstrap/bootstrap-theme.min.css";
                    public const string bootstrap_css = "~/Content/bootstrap/bootstrap.css";
                    public const string bootstrap_min_css = "~/Content/bootstrap/bootstrap.min.css";
                }
            }
            public static class Assets
            {
                public const string bootstrap_css = "~/Content/bootstrap.css";
                public const string bootstrap_min_css = "~/Content/bootstrap.min.css";
                public const string Site_css = "~/Content/Site.css";
            }
        }
    }
}

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal static class T4MVCHelpers {
    // You can change the ProcessVirtualPath method to modify the path that gets returned to the client.
    // e.g. you can prepend a domain, or append a query string:
    //      return "http://localhost" + path + "?foo=bar";
    private static string ProcessVirtualPathDefault(string virtualPath) {
        // The path that comes in starts with ~/ and must first be made absolute
        string path = VirtualPathUtility.ToAbsolute(virtualPath);
        
        // Add your own modifications here before returning the path
        return path;
    }

    // Calling ProcessVirtualPath through delegate to allow it to be replaced for unit testing
    public static Func<string, string> ProcessVirtualPath = ProcessVirtualPathDefault;

    // Calling T4Extension.TimestampString through delegate to allow it to be replaced for unit testing and other purposes
    public static Func<string, string> TimestampString = System.Web.Mvc.T4Extensions.TimestampString;

    // Logic to determine if the app is running in production or dev environment
    public static bool IsProduction() { 
        return (HttpContext.Current != null && !HttpContext.Current.IsDebuggingEnabled); 
    }
}





#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114


