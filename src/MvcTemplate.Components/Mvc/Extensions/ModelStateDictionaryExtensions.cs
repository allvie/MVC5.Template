﻿using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace MvcTemplate.Components.Mvc
{
    public static class ModelStateDictionaryExtensions
    {
        public static void AddModelError<TModel>(this ModelStateDictionary modelState, Expression<Func<TModel, Object>> expression, Exception exception)
        {
            modelState.AddModelError(GetExpressionText(expression), exception);
        }

        public static void AddModelError<TModel>(this ModelStateDictionary modelState, Expression<Func<TModel, Object>> expression, String message)
        {
            modelState.AddModelError(GetExpressionText(expression), message);
        }
        public static void AddModelError<TModel>(this ModelStateDictionary modelState, Expression<Func<TModel, Object>> expression, String format, params Object[] args)
        {
            modelState.AddModelError(GetExpressionText(expression), String.Format(format, args));
        }

        private static String GetExpressionText(LambdaExpression expression)
        {
            UnaryExpression unary = expression.Body as UnaryExpression;
            if (unary?.Operand is MemberExpression)
                return ExpressionHelper.GetExpressionText(Expression.Lambda(unary.Operand, expression.Parameters[0]));

            return ExpressionHelper.GetExpressionText(expression);
        }
    }
}
