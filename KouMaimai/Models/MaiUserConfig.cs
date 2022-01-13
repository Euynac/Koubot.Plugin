﻿using Koubot.Shared.Models;
using Koubot.Tool.Extensions;
using Koubot.Tool.String;
using System;
using System.ComponentModel;
using System.Linq;

namespace KouGamePlugin.Maimai.Models;

[Flags]
public enum SongVersion
{
    None,
    [KouEnumName("maimai和maimai PLUS", "真")]
    真系 = maimai | maimaiPLUS,
    [KouEnumName("无印至FiNALE为止", "舞")]
    舞系 = 霸者,
    [KouEnumName("无印至FiNALE为止", "霸者")]
    霸者 = maimaiPLUS | maimaiFiNALE | MiLKPLUS | maimaiGreeN | maimaiGreeNPLUS | maimaiMiLK | maimaiMURASAKi | maimaiMURASAKiPLUS | maimaiORANGE | maimaiORANGEPLUS | maimaiPiNK | maimaiFiNALE | maimaiPiNKPLUS,
    [KouEnumName("maimai")] 
    maimai = 1 << 0,
    [KouEnumName("maimai PLUS")]
    maimaiPLUS = 1 << 1,

    [KouEnumName("maimai GreeN", "超")] 
    maimaiGreeN = 1 << 2,

    [KouEnumName("maimai GreeN PLUS", "檄")] 
    maimaiGreeNPLUS = 1 << 3,

    [KouEnumName("maimai ORANGE", "橙")]
    maimaiORANGE = 1 << 4,

    [KouEnumName("maimai ORANGE PLUS", "晓")]
    maimaiORANGEPLUS = 1 << 5,

    [KouEnumName("maimai PiNK", "桃")]
    maimaiPiNK = 1 << 6,

    [KouEnumName("maimai PiNK PLUS", "樱")]
    maimaiPiNKPLUS = 1 <<7,

    [KouEnumName("maimai MURASAKi", "紫")]
    maimaiMURASAKi = 1 << 8,

    [KouEnumName("maimai MURASAKi PLUS", "堇")]
    maimaiMURASAKiPLUS = 1 <<9,

    [KouEnumName("maimai MiLK", "白")]
    maimaiMiLK = 1 << 10,

    [KouEnumName("MiLK PLUS", "雪")]
    MiLKPLUS = 1 << 11,

    [KouEnumName("maimai FiNALE", "辉")]
    maimaiFiNALE = 1 <<12,

    [KouEnumName("maimai でらっくす", "熊")]
    maimaiでらっくす = 1 << 13,

    [KouEnumName("maimai でらっくす PLUS", "华")]
    maimaiでらっくすPLUS = 1 << 14,

    [KouEnumName("maimai でらっくす Splash", "爽")]
    maimaiでらっくすSplash = 1 << 15,

    [KouEnumName("maimai でらっくす Splash PLUS", "煌")]
    maimaiでらっくすSplashPLUS = 1 << 16,
}

public enum PlateType
{
    /// <summary>
    /// 各版本所收录乐曲的BASIC～MASTER谱面FULL COMBO达成
    /// </summary>
    [KouEnumName("极")]
    [Description("所收录乐曲的BASIC～MASTER谱面FULL COMBO达成")]
    極,
    /// <summary>
    /// 各版本所收录乐曲的BASIC～MASTER谱面SSS达成
    /// </summary>
    [Description("收录乐曲的BASIC～MASTER谱面SSS达成")]
    将,
    /// <summary>
    /// 各版本所收录乐曲的BASIC～MASTER谱面ALL PERFECT达成
    /// </summary>
    [Description("收录乐曲的BASIC～MASTER谱面ALL PERFECT达成")]
    神,
    /// <summary>
    /// 各版本所收录乐曲的BASIC～MASTER谱面FULL SYNC DX达成（旧框体为100% SYNC，舞萌DX为FDX）
    /// </summary>
    [Description("收录乐曲的BASIC～MASTER谱面FULL SYNC DX达成（旧框体为100% SYNC，舞萌DX为FDX）")]
    舞舞,
    /// <summary>
    /// 通关无印至FiNALE为止的所有BASIC～Re:MASTER标准谱面（前前前世为对象外，Splash收录的旧曲Re:MASTER也为对象外曲）
    /// </summary>
    [KouEnumName("霸者", "富者", "财神")]
    [Description("所有BASIC～Re:MASTER标准谱面通关（达成率80%以上）（前前前世为对象外，Splash收录的旧曲Re:MASTER也为对象外曲）")]
    覇者
}

/// <summary>
/// maimai插件用户配置
/// </summary>
public class MaiUserConfig : PluginUserConfig
{
    /// <summary>
    /// Diving-fish 用户名
    /// </summary>
    public string Username { get; set; }
    /// <summary>
    /// Diving-fish 密码
    /// </summary>
    public string Password { get; set; }
    /// <summary>
    /// 登录Token
    /// </summary>
    public string LoginTokenValue { get; set; }
    /// <summary>
    /// 牌子
    /// </summary>
    public string Plate { get; set; }
    /// <summary>
    /// 昵称
    /// </summary>
    public string Nickname { get; set; }
    /// <summary>
    /// 段位分
    /// </summary>
    public int AdditionalRating { get; set; }
    /// <summary>
    /// 上一次获取成绩时间
    /// </summary>
    public DateTime GetRecordsTime { get; set; }
    /// <summary>
    /// 获取用户当前资料
    /// </summary>
    /// <returns></returns>
    public string? ToUserProfileString(UserAccount user)
    {
        if (Username == null) return null;
        return $"{Nickname}" +
               Plate.BeIfNotEmpty($"【{Plate}】") + $"(段位分{AdditionalRating})" +
               $"\nB40 Rating：{SongRecord.GetB40Charts(user).Sum(p => p.Rating)}" +
               $"\n上次成绩刷新时间：{GetRecordsTime}" +
               $"\nDiving-Fish用户名：{Username}";
    }
}