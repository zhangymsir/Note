using NPOI.XWPF.UserModel;

namespace NpoiApplication.Dto.Word
{
    public class ParagraphSettingInput
    {
        /// <summary>
        /// 文档对象
        /// </summary>
        public XWPFDocument Document { get; set; }

        /// <summary>
        /// 段落第一个文本对象填充的内容
        /// </summary>
        public string FillContent { get; set; }

        /// <summary>
        /// 是否加粗
        /// </summary>
        public bool IsBold { get; set; }

        /// <summary>
        /// 字体大小
        /// </summary>
        public int FontSize { get; set; }

        /// <summary>
        /// 字体
        /// </summary>
        public string FontFamily { get; set; }

        /// <summary>
        /// 段落排列（左对齐，居中，右对齐）
        /// </summary>
        public ParagraphAlignment paragraphAlign { get; set; }

        /// <summary>
        /// 是否在同一段落创建第二个文本对象（解决同一段落里面需要填充两个或者多个文本值的情况，多个文本需要自己拓展，现在最多支持两个）
        /// </summary>
        public bool IsStatement { get; set; } = false;

        /// <summary>
        /// 第二次声明的文本对象填充的内容，样式与第一次的一致
        /// </summary>
        public string SecondFillContent { get; set; } = "";

        /// <summary>
        /// 字体颜色
        /// </summary>
        public string FontColor { get; set; } = "000000";

        /// <summary>
        /// 是否设置斜体（字体倾斜）
        /// </summary>
        public bool IsItalic { get; set; } = false;
    }
}
