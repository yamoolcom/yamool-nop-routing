using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Core.Domain.Localization;
using Nop.Core.Infrastructure;
using Nop.Web.Framework.Mvc.Routing;

namespace Yamool.Nop.Plugin.Routing
{
    public sealed class NopRouteProvider : IRouteProvider
    {
        public int Priority => 100;

        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            var lang = string.Empty;
            var localizationSettings = EngineContext.Current.Resolve<LocalizationSettings>();
            if (localizationSettings.SeoFriendlyUrlsForLanguagesEnabled)
            {
                var code = "en";
                lang = $"{{{NopPathRouteDefaults.LanguageRouteValue}:maxlength(2):{NopPathRouteDefaults.LanguageParameterTransformer}={code}}}";
            }

            // Custom product details URL with ID parameter that used for non-english
            // {lang}/item/{product-id}
            endpointRouteBuilder.MapControllerRoute(
                name: "ProductItem",
                pattern: $"{lang}/item/{{ProductId}}",
                defaults: new { controller = "Product", action = "ProductDetails" });

            // Product
            // {lang}/products/{product-name}
            var productPattern = $"{lang}/products/{{SeName}}";
            endpointRouteBuilder.MapDynamicControllerRoute<SlugRouteTransformer>(productPattern);
            endpointRouteBuilder.MapControllerRoute(
                name: "Product",
                pattern: productPattern,
                defaults: new { controller = "Product", action = "ProductDetails" });

            // Catalog
            // {lang}/collections/{category-name}
            var catalogPattern = $"{lang}/collections/{{SeName}}";
            endpointRouteBuilder.MapDynamicControllerRoute<SlugRouteTransformer>(catalogPattern);
            endpointRouteBuilder.MapControllerRoute(
                name: "Category",
                pattern: catalogPattern,
                defaults: new { controller = "Catalog", action = "Category" });

            // Pages/Topic
            // {lang}/pages/{topic-name}
            var pagePattern = $"{lang}/pages/{{SeName}}";
            endpointRouteBuilder.MapDynamicControllerRoute<SlugRouteTransformer>(pagePattern);
            endpointRouteBuilder.MapControllerRoute(
                name: "Topic",
                pattern: pagePattern,
                defaults: new { controller = "Topic", action = "TopicDetails" });

            // Blog

            // {lang}/blog/{blog-post-name}
            var blogPostPattern = $"{lang}/blog/{{SeName}}";
            endpointRouteBuilder.MapDynamicControllerRoute<SlugRouteTransformer>(blogPostPattern);
            endpointRouteBuilder.MapControllerRoute(
                name: "BlogPost",
                pattern: blogPostPattern,
                defaults: new { controller = "Blog", action = "BlogPost" });

            // News
            var newsItemPattern = $"{lang}/news/{{SeName}}";
            endpointRouteBuilder.MapDynamicControllerRoute<SlugRouteTransformer>(newsItemPattern);
            endpointRouteBuilder.MapControllerRoute(
                name: "NewsItem",
                pattern: newsItemPattern,
                defaults: new { controller = "News", action = "NewsItem" });
        }
    }
}