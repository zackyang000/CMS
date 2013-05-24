using System;
using System.Collections.Generic;
using System.Web.Mvc;
using YangKai.BlogEngine.Web.Mvc.Filters;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class AboutController : Controller
    {
        public ActionResult Index()
        {
            return View(Technology.Instance);
        }
    }
}

public class  Technology
{
    public static IList<Technology> Instance 
    {
        get
        {
            return new List<Technology>
                {
                    new Technology
                        {
                            Name = "Domain-Driven Design",
                            Image = "ddd.png",
                            Link = "http://en.wikipedia.org/wiki/Domain-driven_design",
                            Wide = true,
                        },
                    new Technology
                        {
                            Name = "ASP.NET MVC Framework",
                            Image = "mvc.png",
                            Link = "http://www.asp.net/mvc",
                            Wide = true,
                        },
                    new Technology
                        {
                            Name = "ADO.NET Entity Framework",
                            Image = "entityframework.png",
                            Link = "http://entityframework.codeplex.com/",
                            Wide = true,
                        },
                         new Technology
                        {
                            Name = ".NET Framework",
                            Image = "net.jpg",
                            Link = "http://www.microsoft.com/net",
                        },
                    new Technology
                        {
                            Name = "Bootstrapper",
                            Image = "bootstrapper.png",
                            Link = "http://bootstrapper.codeplex.com/",
                        },
                    new Technology
                        {
                            Name = "CKEditor",
                            Image = "CKEditor.png",
                            Link = "http://ckeditor.com/",
                        },
                    new Technology
                        {
                            Name = "CKFinder",
                            Image = "CKFinder.png",
                            Link = "http://ckfinder.com/",
                        },
                    new Technology
                        {
                            Name = "RSS.NET",
                            Image = "none.png",
                            Link = "http://www.rssdotnet.com/",
                        },
                    new Technology
                        {
                            Name = "Elmah",
                            Image = "none.png",
                            Link = "http://code.google.com/p/elmah/",
                        },
                    new Technology
                        {
                            Name = "QrCode.Net",
                            Image = "none.png",
                            Link = "http://qrcodenet.codeplex.com/",
                        },
                    new Technology
                        {
                            Name = "NPOI",
                            Image = "NPOI.jpg",
                            Link = "http://npoi.codeplex.com/",
                        },
                    new Technology
                        {
                            Name = "HTML5",
                            Image = "html5.png",
                            Link = "https://zh.wikipedia.org/wiki/HTML5",
                            Wide = true,
                        },
                    new Technology
                        {
                            Name = "CSS3",
                            Image = "css3.png",
                            Link = "https://zh.wikipedia.org/wiki/CSS3",
                        },
                    new Technology
                        {
                            Name = "Metro UI",
                            Image = "metro.jpg",
                            Link = "http://zh.wikipedia.org/wiki/Metro_UI",
                        },
                    new Technology
                        {
                            Name = "Knockout.js",
                            Image = "konckout.png",
                            Link = "http://knockoutjs.com/",
                            Wide = true,
                        },
                    new Technology
                        {
                            Name = "Bootstrap",
                            Image = "Bootstrap.png",
                            Link = "http://twitter.github.io/bootstrap/",
                            Wide = true,
                        },
                    new Technology
                        {
                            Name = "jQuery",
                            Image = "jquery.jpg",
                            Link = "http://jquery.com/",
                        },
                    new Technology
                        {
                            Name = "modernizr",
                            Image = "modernizr.png",
                            Link = "http://modernizr.com/",
                        },
                    new Technology
                        {
                            Name = "messenger",
                            Image = "none.png",
                            Link = "http://github.hubspot.com/messenger/",
                        },
                    new Technology
                        {
                            Name = "Knockout.js External Template Engine",
                            Image = "none.png",
                            Link = "https://github.com/ifandelse/Knockout.js-External-Template-Engine",
                            Wide = true,
                        },
                    new Technology
                        {
                            Name = "ko.pager",
                            Image = "none.png",
                            Link = "https://github.com/remcoros/ko.pager",
                        },
                    new Technology
                        {
                            Name = "sammy",
                            Image = "sammy.png",
                            Link = "https://github.com/quirkey/sammy",
                        },
                    new Technology
                        {
                            Name = "CoffeeScript",
                            Image = "coffeescript.jpg",
                            Link = "http://coffeescript.org/",
                        },
                };
        }
    }

public    string Name { get; set; }
public string Image { get; set; }
public string Link { get; set; }
public bool Wide { get; set; }
}