using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class Attachment
    {
        public int upleft { get; set; }
        public int upright { get; set; }
        public int downleft { get; set; }
        public int downright { get; set; }

        public Attachment(int upright, int upleft, int downleft, int downright)
        {
            this.upleft = upleft;
            this.upright = upright;
            this.downleft = downleft;
            this.downright = downright;
        }

        public Attachment()
        {
            upleft = upright = downleft = downright = 0;
        }

        public Attachment(int x, int y)
        {
            upright = x;
            downleft = y;
            upleft = 0;
            downright = 0;

        }
    }
}
