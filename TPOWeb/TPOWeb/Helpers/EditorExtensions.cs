using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace TPOWeb.Helpers
{
    /// <summary>
    /// Contains editor extensions to the System.Web.Mvc.Html.HtmlHelper class.
    /// </summary>
    public static class EditorExtensions
    {
        /// <summary>
        /// Returns an HTML input element for each property in the specified expression, 
        /// with a boolean value determining whether the inputs will be enabled or not.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html"></param>
        /// <param name="expression">An expression that identifies the object that contains the properties to display.</param>
        /// <param name="enabled">A boolean value indicating whether the input returned should be enabled or not.</param>
        /// <returns>An HTML input element, conditionally enabled.</returns>
        public static MvcHtmlString ConditionallyEnabledEditorFor<TModel, TValue>(this HtmlHelper<TModel> html, string id, Expression<Func<TModel, TValue>> expression, bool enabled) 
        {
            object viewData = null;
            if (enabled)
                viewData = new { @id = id };
            else
                viewData = new { @id = id, @disabled = "disabled" };
            return html.EditorFor(expression, viewData);
        }

        public static MvcHtmlString ConditionallyEnabledTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, string id, Expression<Func<TModel, TValue>> expression, bool enabled) 
        {
            object viewData = null;
            if (enabled)
                viewData = new { @id = id };
            else
                viewData = new { @id = id, @disabled = "disabled" };
            return html.TextBoxFor(expression, viewData);
        }
        public static MvcHtmlString ConditionallyEnabledCheckBoxFor<TModel>(this HtmlHelper<TModel> html, string id, Expression<Func<TModel, bool>> expression, bool enabled)
        {
            object viewData = null;
            if (enabled)
                viewData = new { @id = id };
            else
                viewData = new { @id = id, @disabled = "disabled" };
            return html.CheckBoxFor(expression, viewData);
        }
    }
}