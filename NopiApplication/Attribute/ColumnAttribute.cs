using System.ComponentModel.DataAnnotations;

namespace NpoiApplication.Attribute
{
    public class ColumnAttribute : ValidationAttribute
    {
        public ColumnAttribute()
        {

        }

        public ColumnAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public string Format { get; set; }
    }
}
