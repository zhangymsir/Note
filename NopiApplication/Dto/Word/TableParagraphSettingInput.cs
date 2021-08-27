using NPOI.XWPF.UserModel;

namespace NpoiApplication.Dto.Word
{
    public class TableParagraphSettingInput
    {
        /// <summary>
        /// document文档对象
        /// </summary>
        public XWPFDocument Document { get; set; }

        /// <summary>
        /// 表格对象
        /// </summary>
        public XWPFTable Table { get; set; }

        /// <summary>
        /// 要填充的文字
        /// </summary>
        public string FillContent { get; set; }

        /// <summary>
        /// 段落排列（左对齐，居中，右对齐）
        /// </summary>
        public ParagraphAlignment ParagraphAlign { get; set; }

        /// <summary>
        /// 设置文本位置（设置两行之间的行间,从而实现表格文字垂直居中的效果），从而实现table的高度设置效果
        /// </summary>
        public int TextPosition { get; set; } = 24;

        /// <summary>
        /// 是否加粗（true加粗，false不加粗）
        /// </summary>
        public bool IsBold { get; set; } = false;

        /// <summary>
        /// 字体大小
        /// </summary>
        public int FontSize { get; set; } = 10;

        /// <summary>
        /// 字体颜色--十六进制
        /// </summary>
        public string FontColor { get; set; } = "000000";

        /// <summary>
        /// 是否设置斜体（字体倾斜）
        /// </summary>
        public bool IsItalic { get; set; } = false;
    }
}
