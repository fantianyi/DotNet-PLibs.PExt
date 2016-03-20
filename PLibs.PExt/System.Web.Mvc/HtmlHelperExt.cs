using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;
using System.Web.Mvc.Html;
using System.ComponentModel;
using System.Reflection;

namespace System.Web.Mvc
{
    public static class HtmlHelperExt
    {
        public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression) where TModel : class
        {
            string inputName = GetInputName(expression);
            var value = htmlHelper.ViewData.Model == null
                ? default(TProperty)
                : expression.Compile()(htmlHelper.ViewData.Model);

            return htmlHelper.DropDownList(inputName, ToSelectList(typeof(TProperty), value.ToString()));
        }
        private static string GetInputName<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            if (expression.Body.NodeType == ExpressionType.Call)
            {
                MethodCallExpression methodCallExpression = (MethodCallExpression)expression.Body;
                string name = GetInputName(methodCallExpression);
                return name.Substring(expression.Parameters[0].Name.Length + 1);

            }
            return expression.Body.ToString().Substring(expression.Parameters[0].Name.Length + 1);
        }
        private static string GetInputName(MethodCallExpression expression)
        {
            // p => p.Foo.Bar().Baz.ToString() => p.Foo OR throw...
            MethodCallExpression methodCallExpression = expression.Object as MethodCallExpression;
            if (methodCallExpression != null)
            {
                return GetInputName(methodCallExpression);
            }
            return expression.Object.ToString();
        }
        private static SelectList ToSelectList(Type enumType, string selectedItem)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in Enum.GetValues(enumType))
            {
                FieldInfo fi = enumType.GetField(item.ToString());
                var attribute = fi.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault();
                var title = attribute == null ? item.ToString() : ((DescriptionAttribute)attribute).Description;
                var listItem = new SelectListItem
                {
                    Value = ((int)item).ToString(),
                    Text = title,
                    Selected = selectedItem == ((int)item).ToString()
                };
                items.Add(listItem);
            }

            return new SelectList(items, "Value", "Text");
        }        
    }

    public static class HtmlHelperEnumExt
    {

        public static MvcHtmlString DropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {
            return DropDownListFor(htmlHelper, expression, null, null);
        }

        public static MvcHtmlString DropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, string optionLabel)
        {
            return DropDownListFor(htmlHelper, expression, optionLabel, null);
        }

        public static MvcHtmlString DropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, object htmlAttributes)
        {
            return DropDownListFor(htmlHelper, expression, null, htmlAttributes);
        }

        public static MvcHtmlString DropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, string optionLabel, object htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            Type enumType = Nullable.GetUnderlyingType(typeof(TEnum)) ?? typeof(TEnum);
            IEnumerable<TEnum> enumValues = Enum.GetValues(enumType).Cast<TEnum>();
            IEnumerable<SelectListItem> items = enumValues.Select(e => new SelectListItem { Text = e.ToString(), Value = e.ToString(), Selected = e.Equals(metadata.Model) });
            if (optionLabel != null)
            {
                new[] { new SelectListItem { Text = optionLabel } }.Concat(items);
            }
            return htmlHelper.DropDownListFor(expression, items, optionLabel, htmlAttributes);
        }
    }
}