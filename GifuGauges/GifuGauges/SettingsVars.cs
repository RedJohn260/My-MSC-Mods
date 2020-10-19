using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSCLoader;
using UnityEngine;

namespace GifuGauges
{
    class SettingsVars
    {
        //Background Color Settings
        public static Settings BGcolorR = new Settings("BG_ColorR", "Background Color Red", 1f, new Action(GifuGauges.BackgroundColors));
        public static Settings BGcolorG = new Settings("BG_ColorG", "Background Color Green", 1f, new Action(GifuGauges.BackgroundColors));
        public static Settings BGcolorB = new Settings("BG_ColorB", "Background Color Blue", 1f, new Action(GifuGauges.BackgroundColors));

        //NeedleColorSettings
        public static Settings NcolorR = new Settings("N_ColorR", "Needle Color Red", 1f, new Action(GifuGauges.NeedleColors));
        public static Settings NcolorG = new Settings("N_ColorG", "Needle Color Green", 1f, new Action(GifuGauges.NeedleColors));
        public static Settings NcolorB = new Settings("N_ColorB", "Needle Color Blue", 1f, new Action(GifuGauges.NeedleColors));
    }
}
