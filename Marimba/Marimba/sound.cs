namespace Marimba
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Media;
    using System.Text;

    class Sound
    {
        public static SoundPlayer Error = new SoundPlayer(Properties.Resources.error);
        public static SoundPlayer Click = new SoundPlayer(Properties.Resources.click);
        public static SoundPlayer Hover = new SoundPlayer(Properties.Resources.hover);
        public static SoundPlayer Success = new SoundPlayer(Properties.Resources.success);
        public static SoundPlayer Welcome = new SoundPlayer(Properties.Resources.welcome);
    }
}
