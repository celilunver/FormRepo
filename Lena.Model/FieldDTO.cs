using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lena.Model
{
    public enum DataType
    {
        STRING,
        NUMBER
    }
    public class FieldDTO
    {
        public int Id { get; set; }
        public int FormId { get; set; }
        public string Name { get; set; }
        public string DataType { get; set; }
        public bool IsActive { get; set; } = true;
        public bool Required { get; set; }
    }
}
