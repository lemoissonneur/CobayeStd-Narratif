using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Room", menuName = "Scenario/Room", order = 1)]
public class Situation : ScriptableObject
{
    public enum SituationType { TextOnly, TextInteractive };

    [TextArea]
    public string description;
    public string situationName;
    public Choice[] choices;

    public TextFormatSettings format;

    [Serializable]
    public struct TextFormatSettings
    {
        public string fontName;
        public int size;
        public Color textColor;
        public Color backColor;
        public FontStyle style;
    };

    // default values
    public static readonly uint newNodeCharSize = 1000;
    public static readonly TextFormatSettings defaultTextFormat = new TextFormatSettings()
    {
        fontName = "Arial.ttf",
        size = 14,
        textColor = Color.white,
        backColor = Color.black,
        style = FontStyle.Normal
    };
}