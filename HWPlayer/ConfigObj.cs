// 2023-07-01, Bruce

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWPlayer
{
    internal class ConfigObj
    {
        public int win_left { get; set; }
        public int win_top { get; set; }
        public int win_width { get; set; }
        public int win_height { get; set; }
        public string? open_url { get; set; }
    }
}
