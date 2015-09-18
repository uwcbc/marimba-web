using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;

namespace Marimba
{
    class sound
    {
        
        public static SoundPlayer error = new SoundPlayer(Properties.Resources.error);
        public static SoundPlayer click = new SoundPlayer(Properties.Resources.click);
        public static SoundPlayer hover = new SoundPlayer(Properties.Resources.hover);
        public static SoundPlayer success = new SoundPlayer(Properties.Resources.success);
        public static SoundPlayer welcome = new SoundPlayer(Properties.Resources.welcome);
    }
}
