using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName ="TextFormatSettings", menuName ="TextFormatSettings", order = 2)]
public class TextFormatSettings : ScriptableObject
{
    public Font font;
    public int size;
    public Color textColor;
    public Color backColor;
    public FontStyle style;
}