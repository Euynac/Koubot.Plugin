﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Koubot.SDK.AutoModel;
using Koubot.SDK.System;
using Koubot.SDK.Tool;
using Koubot.Shared.Interface;
using Koubot.Shared.Protocol;
using Koubot.Shared.Protocol.Attribute;
using Koubot.Shared.Protocol.Enums;
using Koubot.Tool.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace KouGamePlugin.Maimai.Models
{
    [KouAutoModelTable]
    public partial class SongInfo : KouFullAutoModel<SongInfo>
    {
        public override Action<EntityTypeBuilder<SongInfo>> ModelSetup()
        {
            return entity =>
            {
                entity.HasKey(e => e.SongId)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.SongId);

                entity.HasIndex(e => e.SongTitle);
                entity.HasMany(p => p.Aliases).WithOne(p => p.CorrespondingSong)
                    .HasForeignKey(p => p.SongKanaId)
                    .HasPrincipalKey(p => p.SongTitleKaNa);
            };
        }

        public override string ToString(FormatType formatType, object supplement = null, KouCommand command = null)
        {
            var chartInfo = supplement as SongChart;
            if (chartInfo == null)
            {
                return formatType switch
                {
                    FormatType.Brief => $"{SongId}.{SongTitle}",
                    FormatType.Detail => $"{SongId}.{SongTitle}",
                    _ => throw new NotImplementedException(),
                };
            }

            var id = chartInfo.ChartId;
            var songType = chartInfo.SongType;
            var constantData = chartInfo.ToConstantString();
            switch (formatType)
            {
                case FormatType.Brief:
                    return
                        $"{id}.{SongTitle}({songType}) {(constantData is null ? $"*[{chartInfo.ToRatingString()}]" : $"[{constantData}]")}";
                case FormatType.Detail:
                    string difficultData = $"{chartInfo.ToRatingString()?.Be("\n难度：{0}", true)}" +
                                           $"{constantData?.Be("\n定数：{0}", true)}" +
                                           $"{chartInfo.ToOldRatingString()?.Be("\n旧难度：{0}", true)}" +
                                           $"{chartInfo.ToOldConstantString()?.Be("\n旧定数：{0}", true)}";
                    return
                        $"{JacketUrl?.Be(new KouImage(JacketUrl, chartInfo).ToKouResourceString())}" + //BUG 需要解决翻页可能会使得图片资源字符串裂开的问题
                        $"{id}.{SongTitle} [{songType}]" +
                        difficultData +
                        SongGenre?.Be($"\n分类：{SongGenre}") +
                        Version?.Be($"\n版本：{Version}") + SongArtist?.Be($"\n曲师：{SongArtist}") +
                        SongBpm?.Be($"\nBPM：{SongBpm}") + SongLength?.Be($"\n歌曲长度：{SongLength}") +
                        Remark?.Be($"\n注：{Remark}") +
                        Aliases?.ToSetString(FormatType.Customize1, "，", false)?.Be("\n别名：{0}", true);
                default:
                    return null;
            }
        }
    }
}
