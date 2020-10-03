using System;
using MSCLoader;

namespace ColorfulGauges3
{
    class SettingsVars
    {
        //Background Color Settings
        public static Settings BGcolorR = new Settings("BG_ColorR", "Background Color Red", 1f, new Action(ColorfulGauges3.BackgroundColors));
        public static Settings BGcolorG = new Settings("BG_ColorG", "Background Color Green", 1f, new Action(ColorfulGauges3.BackgroundColors));
        public static Settings BGcolorB = new Settings("BG_ColorB", "Background Color Blue", 1f, new Action(ColorfulGauges3.BackgroundColors));

        //NeedleColorSettings
        public static Settings NcolorR = new Settings("N_ColorR", "Needle Color Red", 1f, new Action(ColorfulGauges3.NeedleColors));
        public static Settings NcolorG = new Settings("N_ColorG", "Needle Color Green", 1f, new Action(ColorfulGauges3.NeedleColors));
        public static Settings NcolorB = new Settings("N_ColorB", "Needle Color Blue", 1f, new Action(ColorfulGauges3.NeedleColors));

        //Ambient Lights Enable/Disable
        public static Settings ambient = new Settings("Ambient", "Ambient Lights", true, new Action(ColorfulGauges3.AmbientLights));
    }
}
