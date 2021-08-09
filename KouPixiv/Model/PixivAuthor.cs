using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Koubot.SDK.Interface;
using Koubot.SDK.Models.System;
using Koubot.SDK.Protocol.AutoModel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KouFunctionPlugin.Pixiv
{
    [KouAutoModelTable("author", new[] {nameof(KouPixiv)}, Name = "作者列表")]
    [Table("plugin_pixiv_author")]
    public class PixivAuthor : KouFullAutoModel<PixivAuthor>
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 作者 uid
        /// </summary>
        public long Uid { get; set; }

        /// <summary>
        /// 作者名（入库时，并过滤掉 @ 及其后内容）
        /// </summary>
        [KouAutoModelField(ActivateKeyword = "作者")]
        public string Name { get; set; }
        /// <summary>
        /// 作者相关的作品
        /// </summary>
        [InverseProperty(nameof(PixivWork.Author))]
        public virtual ICollection<PixivWork> Works { get; set; }
    
        public override Action<EntityTypeBuilder<PixivAuthor>> ModelSetup()
        {
            return builder =>
            {
                builder.HasIndex(p => p.Uid);
                builder.HasIndex(p => p.Name);
            };
        }

        public override bool Equals(object? obj)
        {
            if (obj is PixivAuthor author)
                return author.Uid == Uid;
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Uid.GetHashCode();
        }

        public override string ToString(FormatType formatType, object supplement = null, KouCommand command = null)
        {
            return $"{ID}.{Name}「{Uid}」";
        }
    }
}