using System;
using UnityEngine;
using Verse;

namespace NOQ_CoordReadout
{
    public class Settings : ModSettings
    {
        public static bool ShowCoords = true;
        public static float xPosUI = 300f;
        public static float yPosUI = 50f;
        public static float textSize = 18f;
        public static Color textColor = Color.white;

        private const float DefaultX = 300f;
        private const float DefaultY = 50f;
        private const float DefaultTextSize = 18f;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref ShowCoords, "ShowCoords", true);
            Scribe_Values.Look(ref xPosUI, "xPosUI", DefaultX);
            Scribe_Values.Look(ref yPosUI, "yPosUI", DefaultY);
            Scribe_Values.Look(ref textSize, "textSize", DefaultTextSize);
            Scribe_Values.Look(ref textColor, "textColor", Color.white);
            base.ExposeData();
        }

        public static void DoSettingsWindowContents(Rect rect)
        {
            Listing_Standard list = new Listing_Standard();
            list.Begin(rect);
            // Show/hide checkbox
            list.CheckboxLabeled("CoordRead.ShowCoords".Translate(), ref ShowCoords,
                                  "CoordRead.ShowCoordsTip".Translate());
            list.Gap();
            // Text size slider
            list.Label("CoordRead.TextSize".Translate() + ": " + textSize);
            textSize = Mathf.Round(list.Slider(textSize, 8f, 64f));
            list.Gap();

            // Color preview and sliders
            list.Label("CoordRead.TextColor".Translate());

            Rect previewRect = list.GetRect(28f);
            Widgets.DrawBoxSolidWithOutline(previewRect, textColor, Color.gray, 1);

            list.Gap(4f);

            list.Label("CoordRead.Red".Translate());
            textColor.r = list.Slider(textColor.r, 0f, 1f);

            list.Label("CoordRead.Green".Translate());
            textColor.g = list.Slider(textColor.g, 0f, 1f);

            list.Label("CoordRead.Blue".Translate());
            textColor.b = list.Slider(textColor.b, 0f, 1f);

            list.Label("CoordRead.Opacity".Translate());
            textColor.a = list.Slider(textColor.a, 0f, 1f);

            // Determine the actual text width/height for max slider
            GUIStyle style = new GUIStyle();
            style.fontSize = (int)textSize;
            // Simulate the actual text that will appear in-game
            IntVec3 exampleCell = new IntVec3(999, 0, 999); // worst-case coordinates
            string actualText = exampleCell.ToString();
            Vector2 textSizeVec = style.CalcSize(new GUIContent(actualText));
            float maxX = Mathf.Max(0f, UI.screenWidth - textSizeVec.x);
            float maxY = Mathf.Max(0f, UI.screenHeight - textSizeVec.y);
            // X position slider
            list.Label("CoordRead.xPosUI".Translate() + ": " + xPosUI);
            xPosUI = Mathf.Round(list.Slider(xPosUI, 0f, maxX));
            // Y position slider
            list.Label("CoordRead.yPosUI".Translate() + ": " + yPosUI);
            yPosUI = Mathf.Round(list.Slider(yPosUI, 0f, maxY));
            list.Gap(12f);
            // Reset button
            if (list.ButtonText("CoordRead.ResetDefaults".Translate()))
            {
                ShowCoords = true;
                xPosUI = DefaultX;
                yPosUI = DefaultY;
                textSize = DefaultTextSize;
                textColor = Color.white;
            }
            list.End();
        }
    }
}