using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node
{
    // TYPES :
    public enum NodeType {PLAYERCHOICE, TEXT};

    [Serializable]
    public struct NodeDataStorage
    {
        public uint id;
        public string text;
        public uint previousNodeId;
        public List<uint> nextNodesId;
        public NodeType type;
        public TextFormatSettings format;
    };

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


    // FIELDS :
    public bool isEditable = false;
    public NodeDataStorage data;
    public Node previousNode = null;
    public List<Node> ChildsNodes = new List<Node>();

    // GameObjects & components
    private GameObject parentGObj;

    private GameObject panelGObj;
    private RectTransform panelRectTCp;
    private CanvasRenderer panelCanvasRCp;
    private Image panelImageCp;

    private GameObject textGObj;
    private RectTransform textRectTCp;
    private CanvasRenderer textCanvasCp;
    private Text textCp;
    private InputField textInputCp;



    // create new empty node
    public Node(GameObject parent, uint newid, bool editmode = false)
    {
        parentGObj = parent;

        isEditable = editmode;

        data = new NodeDataStorage()
        {
            id = newid,
            text = "empty",
            previousNodeId = 0,
            nextNodesId = new List<uint>(),
            type = NodeType.TEXT,
            format = Node.defaultTextFormat
        };
    }

    // create from data
    public Node(GameObject parent, NodeDataStorage newdata)
    {
        parentGObj = parent;

        data = newdata;
    }

    public void CreateGObj()
    {
        // create panel GameOBject
        panelGObj = new GameObject("Node: " + data.id.ToString() + " panel");
        panelGObj.transform.SetParent(parentGObj.transform);
        panelRectTCp = panelGObj.AddComponent<RectTransform>();
        panelCanvasRCp = panelGObj.AddComponent<CanvasRenderer>();
        panelImageCp = panelGObj.AddComponent<Image>();

        // create text GameOBject
        textGObj = new GameObject("Node: " + data.id.ToString() + " text");
        textGObj.transform.SetParent(panelGObj.transform);
        textRectTCp = textGObj.AddComponent<RectTransform>();
        textCanvasCp = textGObj.AddComponent<CanvasRenderer>();
        textCp = textGObj.AddComponent<Text>();

        // setup panel
        panelRectTCp.anchorMin = panelRectTCp.anchorMax = new Vector2(0, 1);
        panelRectTCp.pivot = new Vector2(0, 1);
        panelRectTCp.anchoredPosition = new Vector2(0, 0);

        // setup text
        textRectTCp.anchorMin = new Vector2(0, 0);
        textRectTCp.anchorMax = new Vector2(1, 1);
        textRectTCp.offsetMin = textRectTCp.offsetMax = new Vector2(0, 0);
        textCp.text = data.text;

        UpdateDisplay();

        if (isEditable)
        {
            textInputCp = textGObj.AddComponent<InputField>();
            textInputCp.textComponent = textCp;
            textInputCp.lineType = InputField.LineType.MultiLineNewline;
            textCp.supportRichText = false;
        }
    }

    public Vector2 AdjustSize(float Xmax)
    {
        float spaceNeeded;  // area needed based on number of character in text
        Vector2 panelSize = new Vector2();  

        if (isEditable) // for editor mode
            spaceNeeded = Mathf.Pow(textCp.fontSize + 2, 2) * newNodeCharSize;
        else
            spaceNeeded = Mathf.Pow(textCp.fontSize + 2, 2) * textCp.text.Length;

        // area to x&y size
        panelSize.x = Xmax;
        panelSize.y = spaceNeeded / panelSize.x;

        if (panelSize.y < (textCp.fontSize + 2))
            panelSize.y = textCp.fontSize + 2;

        Debug.Log(panelSize);

        panelRectTCp.sizeDelta = panelSize;

        return panelSize;

        /*
            parentGObj.GetComponent<RectTransform>().offsetMax += new Vector2(0, panelSize.y + newNodeOffset);
        */
    }

    public void UpdateDisplay()
    {
        textCp.font = GetFont(data.format.fontName);
        textCp.fontSize = data.format.size;
        textCp.fontStyle = data.format.style;
        textCp.color = data.format.textColor;
        panelImageCp.color = data.format.backColor;
    }

    private Font GetFont(string font)
    {
        return Resources.GetBuiltinResource<Font>(font);
    }
}
