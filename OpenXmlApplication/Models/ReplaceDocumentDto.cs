using OpenXmlApplication.Utils;
using System.Collections.Generic;

namespace OpenXmlApplication.Models
{
    public class ReplaceDocumentDto
    {
        /// <summary>
        /// 书签类型
        /// </summary>
        public BookmarkType BookmarkType { get; set; }

        /// <summary>
        /// 书签名称
        /// </summary>
        public string BookmarkName { get; set; }

        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// 表格
        /// </summary>
        public List<List<string>> TableData { get; set; }
    }
}
