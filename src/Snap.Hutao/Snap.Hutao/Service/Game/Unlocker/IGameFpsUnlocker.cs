﻿// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

namespace Snap.Hutao.Service.Game.Unlocker;

/// <summary>
/// 游戏帧率解锁器
/// </summary>
[HighQuality]
internal interface IGameFpsUnlocker
{
    /// <summary>
    /// 异步的解锁帧数限制
    /// </summary>
    /// <param name="options">选项</param>
    /// <param name="progress">进度</param>
    /// <param name="token">取消令牌</param>
    /// <returns>解锁的结果</returns>
    ValueTask UnlockAsync(UnlockTimingOptions options, IProgress<UnlockerStatus> progress, CancellationToken token = default);
}