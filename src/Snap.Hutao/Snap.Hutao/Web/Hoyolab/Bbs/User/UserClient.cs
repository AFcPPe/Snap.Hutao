﻿// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using Snap.Hutao.Core.DependencyInjection.Annotation.HttpClient;
using Snap.Hutao.Web.Hoyolab.Annotation;
using Snap.Hutao.Web.Hoyolab.DynamicSecret;
using Snap.Hutao.Web.Response;
using System.Net.Http;

namespace Snap.Hutao.Web.Hoyolab.Bbs.User;

/// <summary>
/// 用户信息客户端 DS版
/// </summary>
[HighQuality]
[UseDynamicSecret]
[ConstructorGenerated(ResolveHttpClient = true)]
[HttpClient(HttpClientConfiguration.XRpc)]
internal sealed partial class UserClient : IUserClient
{
    private readonly JsonSerializerOptions options;
    private readonly ILogger<UserClient> logger;
    private readonly HttpClient httpClient;

    /// <summary>
    /// 获取当前用户详细信息
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="token">取消令牌</param>
    /// <returns>详细信息</returns>
    [ApiInformation(Cookie = CookieType.SToken, Salt = SaltType.K2)]
    public async ValueTask<Response<UserFullInfoWrapper>> GetUserFullInfoAsync(Model.Entity.User user, CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(user.Aid);
        Response<UserFullInfoWrapper>? resp = await httpClient

            // .SetUser(user, CookieType.SToken)
            .SetReferer(ApiEndpoints.BbsReferer)

            // .UseDynamicSecret(DynamicSecretVersion.Gen1, SaltType.K2, true)
            .TryCatchGetFromJsonAsync<Response<UserFullInfoWrapper>>(ApiEndpoints.UserFullInfoQuery(user.Aid), options, logger, token)
            .ConfigureAwait(false);

        return Response.Response.DefaultIfNull(resp);
    }
}