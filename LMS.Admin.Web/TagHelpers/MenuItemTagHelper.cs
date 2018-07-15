using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace LMS.Admin.Web.TagHelpers
{
    [HtmlTargetElement("menu-item")]
    public class MenuItemTagHelper : TagHelper
    {
        private readonly Random idGenerator;
        private readonly IHtmlGenerator htmlGenerator;

        public MenuItemTagHelper(IHtmlGenerator generator)
        {
            idGenerator = new Random();
            htmlGenerator = generator;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { set; get; }

        public string Controller { get; set; }
        public string Action { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "li";
            output.TagMode = TagMode.StartTagAndEndTag;
            var builder = new HtmlContentBuilder();

            var childContent = await output.GetChildContentAsync();
            if (childContent.IsEmptyOrWhiteSpace)
            {
                if (string.IsNullOrEmpty(Action))
                {
                    var span = new TagBuilder("span");
                    AppendMenuItemContent(span.InnerHtml);
                    builder.AppendHtml(span);
                }
                else
                {
                    var actionAnchor = htmlGenerator.GenerateActionLink(
                        ViewContext,
                        linkText: "",
                        actionName: Action,
                        controllerName: Controller ?? ViewContext.RouteData.Values["controller"]?.ToString(),
                        fragment: null,
                        hostname: null,
                        htmlAttributes: null,
                        protocol: null,
                        routeValues: null
                    );
                    actionAnchor.AddCssClass("nav-link");
                    AppendMenuItemContent(actionAnchor.InnerHtml);
                    builder.AppendHtml(actionAnchor);
                }
            }
            else
            {
                var collapseId = "collapseComponents" + idGenerator.Next();

                var anchor = new TagBuilder("a");
                anchor.AddCssClass("nav-link");
                anchor.AddCssClass("nav-link-collapse");
                anchor.AddCssClass("collapsed");
                anchor.Attributes.Add("href", "#" + collapseId);
                anchor.Attributes.Add("data-toggle", "collapse");
                AppendMenuItemContent(anchor.InnerHtml);
                builder.AppendHtml(anchor);

                var childrenUl = new TagBuilder("ul");
                childrenUl.AddCssClass("sidenav-second-level");
                childrenUl.AddCssClass("collapse");
                childrenUl.Attributes.Add("id", collapseId);
                childrenUl.InnerHtml.AppendHtml(childContent);
                builder.AppendHtml(childrenUl);
            }
            output.Content.SetHtmlContent(builder);
            output.Attributes.Add("class", "nav-item");
            output.Attributes.Add("data-toggle", "toolip");
            output.Attributes.Add("data-placement", "right");
            output.Attributes.Add("title", Title);
        }

        private void AppendMenuItemContent(IHtmlContentBuilder builder)
        {
            if (string.IsNullOrWhiteSpace(Icon))
            {
                builder.Append(Title);
            }
            else
            {
                builder.AppendHtml(
$@"<i class='fa fa-fw {Icon}'></i>
<span class='nav-link-text'>{Title}</span>");
            }
        }
    }
}
