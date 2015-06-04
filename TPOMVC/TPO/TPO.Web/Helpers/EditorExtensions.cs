using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace TPO.Web.Helpers
{
    /// <summary>
    /// Contains editor extensions to the System.Web.Mvc.Html.HtmlHelper class.
    /// </summary>
    public static class EditorExtensions
    {
        

        public static MvcHtmlString ConditionallyEnabledTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, bool enabled, string id = "", string cssClass = "") 
        {
            object viewData = null;
            if (string.IsNullOrEmpty(id))
            {
                id = html.NameFor(expression).ToString();
            }
            if (enabled)
                viewData = new { @id = id, @class = cssClass };
            else
                viewData = new { @id = id, @disabled = "disabled", @class = cssClass };
            return html.TextBoxFor(expression, viewData);
        }
        public static MvcHtmlString ConditionallyEnabledDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList, bool enabled, string optionLabel = "", string cssClass = "") 
        {
            object viewData = null;

            if (enabled)
            {
                viewData = new { @class = cssClass };
            }
            else 
            {
                viewData = new { @disabled = "disabled", @class = cssClass };
            }
            return html.DropDownListFor(expression, selectList, optionLabel, viewData);
        }
        public static MvcHtmlString ConditionallyEnabledTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, bool enabled, string id = "", string cssClass = "") 
        {
            object viewData = null;
            if (string.IsNullOrEmpty(id))
            {
                id = html.NameFor(expression).ToString();
            }
            if (enabled)
            {
                viewData = new { @class = cssClass };
            }
            else
            {
                viewData = new { @disabled = "disabled", @class = cssClass };
            }
            return html.TextAreaFor(expression, viewData);
        }
        public static MvcHtmlString ConditionallyEnabledCheckBoxFor<TModel>(this HtmlHelper<TModel> html, Expression<Func<TModel, bool>> expression, bool enabled, string id = "")
        {
            object viewData = null;
            if (string.IsNullOrEmpty(id)) 
            {
                id = html.NameFor(expression).ToString();
            }
            if (enabled)
                viewData = new { @id = id };
            else
                viewData = new { @id = id, @disabled = "disabled" };
            return html.CheckBoxFor(expression, viewData);
        }
    }
}