using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class Tooltip : MonoBehaviour
{

    //This is the max characters. At least with W's. Can be more with lower case.
    string text = "WWWWWWWWWWW";

    string currentToolTipText = "";
    GUIStyle guiStyleFore;
    GUIStyle guiStyleBack;
    Texture2D BB_Box;

    void Start()
    {
        BB_Box = new Texture2D(1, 1);
        //BB_Box.SetPixel(0, 0, Color.black);
        guiStyleFore = new GUIStyle();
        guiStyleFore.normal.textColor = Color.white;
        guiStyleFore.alignment = TextAnchor.UpperCenter;
        guiStyleFore.wordWrap = true;
    }

    void OnMouseEnter()
    {
        currentToolTipText = text;
    }

    void OnMouseExit()
    {
        currentToolTipText = "";
    }

    void OnGUI()
    {
        if (currentToolTipText != "")
        {
            var x = Event.current.mousePosition.x;
            var y = Event.current.mousePosition.y;
            GUI.Box(new Rect(x - 70, y - 25, 140, 20), BB_Box);
            GUI.Box(new Rect(x - 70, y - 25, 140, 20), BB_Box);
            GUI.Label(new Rect(x - 70, y - 23, 140, 20), currentToolTipText, guiStyleFore);
        }
    }
}
