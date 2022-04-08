using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Linq.Expressions;

namespace WebHelpers.Mvc5
{
    public static class StringExtensions
    {
        private static readonly ModelExpressionProvider ModelExpressionProvider = new(new EmptyModelMetadataProvider());

        public static string GetExpressionText<TEntity, TProperty>(this Expression<Func<TEntity, TProperty>> expression)
        {
            return ModelExpressionProvider.GetExpressionText(expression);
        }
    }
}
