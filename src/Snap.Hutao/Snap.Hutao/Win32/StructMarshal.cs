﻿// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using System.Buffers.Binary;
using System.Numerics;
using System.Runtime.CompilerServices;
using Windows.Graphics;
using Windows.Win32.UI.WindowsAndMessaging;

namespace Snap.Hutao.Win32;

/// <summary>
/// 结构体封送
/// </summary>
[HighQuality]
internal static class StructMarshal
{
    /// <summary>
    /// 构造一个新的 <see cref="Windows.Win32.UI.WindowsAndMessaging.WINDOWPLACEMENT"/>
    /// </summary>
    /// <returns>新的实例</returns>
    public static unsafe WINDOWPLACEMENT WINDOWPLACEMENT()
    {
        return new() { length = SizeOf<WINDOWPLACEMENT>() };
    }

    /// <summary>
    /// 获取结构的大小
    /// </summary>
    /// <typeparam name="TStruct">结构类型</typeparam>
    /// <returns>结构的大小</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe uint SizeOf<TStruct>()
        where TStruct : unmanaged
    {
        return unchecked((uint)sizeof(TStruct));
    }

    /// <summary>
    /// 使用四字节颜色代码初始化一个新的颜色
    /// </summary>
    /// <param name="value">颜色代码</param>
    /// <returns>对应的颜色</returns>
    public static unsafe Windows.UI.Color Color(uint value)
    {
        Unsafe.SkipInit(out Windows.UI.Color color);
        *(uint*)&color = BinaryPrimitives.ReverseEndianness(value);
        return color;
    }

    /// <summary>
    /// 从 0,0 点构造一个新的<see cref="Windows.Graphics.RectInt32"/>
    /// </summary>
    /// <param name="size">尺寸</param>
    /// <returns>新的实例</returns>
    public static RectInt32 RectInt32(SizeInt32 size)
    {
        return new(0, 0, size.Width, size.Height);
    }

    /// <summary>
    /// 构造一个新的<see cref="Windows.Graphics.RectInt32"/>
    /// </summary>
    /// <param name="point">左上角位置</param>
    /// <param name="size">尺寸</param>
    /// <returns>新的实例</returns>
    public static RectInt32 RectInt32(PointInt32 point, Vector2 size)
    {
        return RectInt32(point.X, point.Y, size);
    }

    /// <summary>
    /// 构造一个新的<see cref="Windows.Graphics.RectInt32"/>
    /// </summary>
    /// <param name="x">X</param>
    /// <param name="y">Y</param>
    /// <param name="size">尺寸</param>
    /// <returns>新的实例</returns>
    public static RectInt32 RectInt32(int x, int y, Vector2 size)
    {
        return new(x, y, (int)size.X, (int)size.Y);
    }

    /// <summary>
    /// 构造一个新的<see cref="Windows.Graphics.RectInt32"/>
    /// </summary>
    /// <param name="point">左上角位置</param>
    /// <param name="size">尺寸</param>
    /// <returns>新的实例</returns>
    public static RectInt32 RectInt32(PointInt32 point, SizeInt32 size)
    {
        return new(point.X, point.Y, size.Width, size.Height);
    }
}