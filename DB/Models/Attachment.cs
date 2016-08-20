using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class Attachment
    {
        public int x1 { get; set; }
        public int y1 { get; set; }
        public int x2 { get; set; }
        public int y2 { get; set; }

        public Attachment(int x1, int y1, int x2, int y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }

        public Attachment()
        {
            x1 = x2 = y1 = y2 = 0;
        }

        public Attachment(int x, int y)
        {
            x1 = x;
            y1 = y;
            x2 = 0;
            y2 = 0;

        }
    }
}
