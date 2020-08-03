using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.Cobaye.ScenarioSystem
{
    public class Node
    {
        // TYPES :
        public enum NodeType { PLAYERCHOICE, TEXT };

        [Serializable]
        public struct NodeDataStorage
        {
            public uint id;
            public string text;
            public List<uint> previousNodeId;
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
        public List<Node> FatherNode = new List<Node>();
        public List<Node> ChildsNodes = new List<Node>();

        // GameObjects & components
        private GameObject parentGObj = null;

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
        public Node(uint newid, bool editmode = false)
        {
            isEditable = editmode;

            data = new NodeDataStorage()
            {
                id = newid,
                text = "empty",
                previousNodeId = new List<uint>(),
                nextNodesId = new List<uint>(),
                type = NodeType.TEXT,
                format = Node.defaultTextFormat
            };
        }

        // create from data
        public Node(NodeDataStorage newdata)
        {
            data = newdata;
        }

        public void CreateGObj(GameObject parent, Vector2 position = new Vector2(), Vector2 size = new Vector2())
        {
            parentGObj = parent;

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
            panelRectTCp.localScale = Vector3.one;

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
                textInputCp.text = data.text;
                textInputCp.lineType = InputField.LineType.MultiLineNewline;
                textCp.supportRichText = false;
                textInputCp.onValueChanged.AddListener(EditText);
            }

            panelRectTCp.offsetMin = new Vector2(position.x, -position.y - size.y);
            panelRectTCp.offsetMax = new Vector2(position.x + size.x, -position.y);

            //SetSize(size);
            //SetPosition(position);
        }

        public void DestroyGObj()
        {
            GameObject.Destroy(panelGObj);
            GameObject.Destroy(textGObj);
            parentGObj = null;
        }

        public void SetPosition(Vector2 position)
        {
            panelRectTCp.offsetMin = new Vector2(position.x, -position.y - panelRectTCp.sizeDelta.y);
            panelRectTCp.offsetMax = new Vector2(position.x + panelRectTCp.sizeDelta.x, -position.y);
        }

        public void SetSize(Vector2 size)
        {
            panelRectTCp.sizeDelta = size;
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

        public void GetChildsInList(List<Node> list)
        {
            foreach (Node n in list)
            {
                if (data.nextNodesId.Contains(n.data.id)) // if this node is a child
                {
                    ChildsNodes.Add(n);
                }

                if (data.previousNodeId.Contains(n.data.id))
                {
                    FatherNode.Add(n);
                }
            }

            foreach (Node n in ChildsNodes)
            {
                n.GetChildsInList(list);
            }
        }

        public void EditText(string text)
        {
            data.text = text;
        }
    }
}
