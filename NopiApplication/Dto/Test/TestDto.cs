using NpoiApplication.Attribute;
using System;

namespace NpoiApplication.Dto.Test
{
    public class TestDto
    {
        [Column("协议编号*")]
        public string Code { get; set; }

        [Column("协议类型*")]
        public string Type { get; set; }

        [Column("甲方代表*")]
        public string BaseEmployee { get; set; }

        [Column("服务商*")]
        public string Contractor { get; set; }

        [Column("开始日期*", Format = "yyyy-MM-dd")]
        public DateTime StartTime { get; set; }

        [Column("结束日期*", Format = "yyyy-MM-dd")]
        public DateTime EndTime { get; set; }

        [Column("数量")]
        public decimal? Count { get; set; }
    }
}
