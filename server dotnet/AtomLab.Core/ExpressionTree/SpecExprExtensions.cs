using System;
using System.Linq.Expressions;
using AtomLab.Core.ExpressionTree;

public static class SpecExprExtensions
{
    public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> one)
    {
        var candidateExpr = one.Parameters[0];
        var body = Expression.Not(one.Body);

        return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
    }

    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> one, Expression<Func<T, bool>> another)
    {
        var candidateExpr = Expression.Parameter(typeof(T), "candidate");
        var parameterReplacer = new ParameterReplacer(candidateExpr);

        var left = parameterReplacer.Replace(one.Body);
        var right = parameterReplacer.Replace(another.Body);
        var body = Expression.And(left, right);
        
        return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
    }

    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> one, Expression<Func<T, bool>> another)
    {
        var candidateExpr = Expression.Parameter(typeof(T), "candidate");
        var parameterReplacer = new ParameterReplacer(candidateExpr);

        var left = parameterReplacer.Replace(one.Body);
        var right = parameterReplacer.Replace(another.Body);
        var body = Expression.Or(left, right);

        return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
    }
}