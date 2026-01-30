using HarmonyLib;
using Verse;
using RimWorld;
using UnityEngine;

namespace NOQ_CoordReadout
{
    [HarmonyPatch(typeof(MouseoverReadout), "MouseoverReadoutOnGUI")]
    static class CoordinateReadout
    {
        static void Postfix()
        {
            if (!Settings.ShowCoords)
                return;

            if (Event.current.type != EventType.Repaint)
                return;

            IntVec3 mouseCell = UI.MouseCell();
            if (!mouseCell.InBounds(Find.CurrentMap))
                return;

            // Create a GUIStyle with the configured text size
            GUIStyle style = new GUIStyle();
            style.fontSize = (int)Settings.textSize;
            style.normal.textColor = new Color(1f, 1f, 1f, 0.8f);

            // Calculate text size
            Vector2 textSize = style.CalcSize(new GUIContent(mouseCell.ToString()));

            // Ensure position does not go offscreen (optional safety)
            float x = Mathf.Clamp(Settings.xPosUI, 0f, UI.screenWidth - textSize.x);
            float y = Mathf.Clamp(UI.screenHeight - Settings.yPosUI - textSize.y, 0f, UI.screenHeight - textSize.y);

            Rect labelRect = new Rect(x, y, textSize.x, textSize.y);

            // Draw the text using Unity's GUI.Label directly
            GUI.Label(labelRect, mouseCell.ToString(), style);
        }
    }
}
