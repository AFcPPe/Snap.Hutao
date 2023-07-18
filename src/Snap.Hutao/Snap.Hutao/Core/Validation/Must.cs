﻿// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using System.Runtime.CompilerServices;

namespace Snap.Hutao.Core.Validation;

/// <summary>
/// 封装验证方法,简化微软验证
/// </summary>
[HighQuality]
internal static class Must
{
    /// <summary>
    /// Throws an <see cref="ArgumentException"/> if a condition does not evaluate to true.
    /// </summary>
    /// <param name="condition">The condition to check.</param>
    /// <param name="message">message</param>
    /// <param name="parameterName">The name of the parameter to blame in the exception, if thrown.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void Argument([DoesNotReturnIf(false)] bool condition, string? message, [CallerArgumentExpression(nameof(condition))] string? parameterName = null)
    {
        if (!condition)
        {
            throw new ArgumentException(message, parameterName);
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if a condition does not evaluate to true.
    /// </summary>
    /// <param name="condition">The condition to check.</param>
    /// <param name="message">message</param>
    /// <param name="parameterName">The name of the parameter to blame in the exception, if thrown.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void Range([DoesNotReturnIf(false)] bool condition, string? message, [CallerArgumentExpression(nameof(condition))] string? parameterName = null)
    {
        if (!condition)
        {
            throw new ArgumentOutOfRangeException(parameterName, message);
        }
    }

    /// <summary>
    /// Unconditionally throws an <see cref="NotSupportedException"/>.
    /// </summary>
    /// <param name="context">上下文</param>
    /// <returns>Nothing. This method always throws.</returns>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception NeverHappen(string? context = null)
    {
        throw new NotSupportedException(context);
    }
}
