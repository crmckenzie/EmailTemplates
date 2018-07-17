using System;
using System.Dynamic;
using System.IO;
using RazorLight;

namespace console
{
    class Program
    {
        static void Main(string[] args)
        {
            var absolutePath = new DirectoryInfo(Environment.CurrentDirectory).FullName;

            var engine = new RazorLightEngineBuilder()
                .UseFilesystemProject(absolutePath)
                .UseMemoryCachingProvider()
              .Build();

            var model = new EmailTemplateViewModel
            {
                ContentTitle = "Content Title",
                ContentImage = "some-path",
                Text = "Some text"
            };

            dynamic viewbag = new ExpandoObject();
            viewbag.Title = "This is the Viewbag Title";
            viewbag.TitleImageSource = "http://www.google.com";

            string result = engine.CompileRenderAsync("Body.cshtml", model, viewbag).Result;
            Console.WriteLine(result);
            Console.Read();

        }
    }

    public class EmailTemplateViewModel
    {
        public string ContentTitle { get; set; }
        public string ContentImage { get; set; }
        public string Text { get; set; }
    }
}
