using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Situation", menuName = "Scenario/Situation", order = 1)]
public class Situation : ScriptableObject
{
    public enum SituationType { TextOnly, TextInteractive };

    public SituationType Type;
    [TextArea]
    public string Description;
    public Situation NextSituation;
    public Choice[] Choices;

    public TextFormatSettings Format;

    [Serializable]
    public struct TextFormatSettings
    {
        public Font font;
        public int size;
        public Color textColor;
        public Color backColor;
        public FontStyle style;
    };

    // default values
    public static readonly uint NewNodeCharSize = 1000;
    internal static readonly TextFormatSettings defaultTextFormat = new TextFormatSettings()
    {
        //fontName = new Fon"Arial.ttf",
        size = 14,
        textColor = Color.white,
        backColor = Color.black,
        style = FontStyle.Normal
    };

    internal void AddChoice()
    {
        throw new NotImplementedException();
    }
}