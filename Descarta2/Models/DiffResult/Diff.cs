using System.Text;

namespace Descarta2.Models
{
    public class Diff
    {
        public Diff(int offset, int length)
        {
            this.offset = offset;
            this.length = length;
        }
        public String toString()
        {
            return new StringBuilder().Append("offset=").Append(offset)
                    .Append(" length=").Append(length).ToString();
        }

        public int offset { get; set; }
        public int length { get; set; }
    }
}
