using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;

namespace Parsing_ulmart
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());

            string source = "https://www.ulmart.ru/goods/4534822";

            var document = await context.OpenAsync(source);

            var breadcrumbs = document.QuerySelectorAll("nav").Where(x => x.ClassName.Contains("b-crumbs b-crumbs_theme_normal")).First();
            List<string> breadcrumbsTitle = breadcrumbs.QuerySelectorAll("span[itemprop='title']").Select(x => x.Text()).ToList();
            string name = document.QuerySelectorAll("h1").Where(x => x.ClassName.Contains("main-h1 main-h1_bold js-reload")).First().Text().Trim('\n', ' ');
            string num = document.QuerySelector("span.b-art__num").Text();
            string oldPrice = document.QuerySelector("div.b-product-card__price").QuerySelectorAll("span").First().Text();
            string newPrice = document.QuerySelector("span.b-price__num").Text();
            string currency = document.QuerySelector("span.b-price__sign").Text();
            string availability = document.QuerySelector("div#p-stat").QuerySelector("div").Text().Trim('\n', ' ');
            string imageUrl = document.QuerySelector("div#product_card_img").QuerySelector("img").GetAttribute("src");

            Console.Write("Категории: ");
            foreach (var item in breadcrumbsTitle)
            {
                if (item == breadcrumbsTitle.Last()) {
                    Console.Write(item);
                    break;
                }
                Console.Write(item + ", ");
            }
            Console.WriteLine("\nНазвание: " + name);
            Console.WriteLine("Артикул: " + num);
            Console.WriteLine(String.Format("Старая цена: {0} {1}", oldPrice, currency));
            Console.WriteLine(String.Format("Новая цена: {0} {1}", newPrice, currency));
            Console.WriteLine("Наличие: " + availability);
            Console.WriteLine("Ссылка на картинку: " + imageUrl);
            Console.ReadLine();
        }
    }
}
