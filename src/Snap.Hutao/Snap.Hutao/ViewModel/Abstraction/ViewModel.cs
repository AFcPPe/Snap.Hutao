﻿// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using CommunityToolkit.Mvvm.ComponentModel;
using Snap.Hutao.Core.ExceptionService;

namespace Snap.Hutao.ViewModel.Abstraction;

/// <summary>
/// 视图模型抽象类
/// </summary>
[HighQuality]
internal abstract partial class ViewModel : ObservableObject, IViewModel
{
    private bool isInitialized;

    /// <summary>
    /// 是否初始化完成
    /// </summary>
    public bool IsInitialized { get => isInitialized; set => SetProperty(ref isInitialized, value); }

    /// <inheritdoc/>
    public CancellationToken CancellationToken { get; set; }

    /// <inheritdoc/>
    public SemaphoreSlim DisposeLock { get; set; } = new(1);

    /// <inheritdoc/>
    public bool IsViewDisposed { get; set; }

    protected TaskCompletionSource<bool> Initialization { get; } = new();

    /// <summary>
    /// 异步初始化UI
    /// </summary>
    /// <returns>任务</returns>
    [Command("OpenUICommand")]
    protected virtual async Task OpenUIAsync()
    {
        // Set value on UI thread
        IsInitialized = await InitializeUIAsync().ConfigureAwait(true);
        Initialization.TrySetResult(IsInitialized);
    }

    /// <summary>
    /// 异步初始化界面数据
    /// </summary>
    /// <returns>初始化是否成功</returns>
    protected virtual ValueTask<bool> InitializeUIAsync()
    {
        return ValueTask.FromResult(true);
    }

    /// <summary>
    /// 保证 using scope 内的代码运行完成
    /// 防止 视图资源被回收
    /// </summary>
    /// <returns>解除执行限制</returns>
    protected async ValueTask<IDisposable> EnterCriticalExecutionAsync()
    {
        ThrowIfViewDisposed();
        IDisposable disposable = await DisposeLock.EnterAsync(CancellationToken).ConfigureAwait(false);
        ThrowIfViewDisposed();
        return disposable;
    }

    /// <summary>
    /// 当页面被释放后抛出异常
    /// </summary>
    /// <exception cref="OperationCanceledException">操作被用户取消</exception>
    private void ThrowIfViewDisposed()
    {
        if (IsViewDisposed)
        {
            ThrowHelper.OperationCanceled(SH.ViewModelViewDisposedOperationCancel);
        }
    }
}