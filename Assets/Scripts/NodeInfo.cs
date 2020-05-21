using System;
using UnityEngine;
using UnityEngine.UI;

public class NodeInfo : MonoBehaviour
{
    private Node CurrentNode { get; set; }

    [SerializeField]
    private readonly GameObject ColorPickerPrefab = null;
    [SerializeField]
    private readonly GameObject TargetCamera = null;

    private GameObject ColorPickerGobj;
    private ColorPickerTriangle ColorPickerCp;
    private bool IsPainting = false;
    private bool PaintTargetIsText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsPainting)
            UpdateColor();
    }

    public void LoadNode(Node newNode)
    {
        CurrentNode = newNode;
    }

    private void DisplayNodeInfo()
    {
        GameObject.Find("NodeIdData").GetComponent<Text>().text = CurrentNode.data.id.ToString();
        GameObject.Find("NodeParentData").GetComponent<Text>().text = CurrentNode.data.previousNodeId.ToString();
        GameObject.Find("NodeChildData").GetComponent<Text>().text = CurrentNode.data.nextNodesId.ToString();
        GameObject.Find("NodeTypeData").GetComponent<Dropdown>().value = (int)CurrentNode.data.type;

        GameObject.Find("NodeTextFontData").GetComponent<Text>().text = CurrentNode.data.format.fontName;
        GameObject.Find("NodeTextSizeData").GetComponent<Text>().text = CurrentNode.data.format.size.ToString();
        GameObject.Find("NodeTextStyleData").GetComponent<Dropdown>().value = (int)CurrentNode.data.format.style;
        GameObject.Find("NodeTextColorData").GetComponent<Image>().color = CurrentNode.data.format.textColor;
        GameObject.Find("NodeTextBackColorData").GetComponent<Image>().color = CurrentNode.data.format.backColor;
    }

    /*
     *      Node Data Settings callbacks
     * */
    public void OnNodeTypeChanged(Int32 type)
    {
        CurrentNode.data.type = (Node.NodeType)type;
    }

    /*
     *      Node Text Settings callbacks
     * */
    public void OnNodeFontChanged(string font)
    {
        CurrentNode.data.format.fontName = font;
        CurrentNode.UpdateDisplay();
    }

    public void OnNodeSizeChanged(string size)
    {
        CurrentNode.data.format.size = int.Parse(size);
        CurrentNode.UpdateDisplay();
    }

    public void OnNodeStyleChanged(Int32 style)
    {
        CurrentNode.data.format.style = (FontStyle)style;
        CurrentNode.UpdateDisplay();
    }

    public void OnNodeTextColorClick()
    {
        if (IsPainting)
        {
            if (PaintTargetIsText)
                StopPaint();
            else
            {
                PaintTargetIsText = true;
                ColorPickerCp.SetNewColor(CurrentNode.data.format.textColor);
            }
        }
        else
        {
            StartPaint(CurrentNode.data.format.textColor);
            PaintTargetIsText = true;
        }
    }

    public void OnNodeBackColorClick()
    {
        if (IsPainting)
        {
            if (PaintTargetIsText)
            {
                PaintTargetIsText = false;
                ColorPickerCp.SetNewColor(CurrentNode.data.format.backColor);
            }
            else
                StopPaint();
        }
        else
        {
            StartPaint(CurrentNode.data.format.backColor);
            PaintTargetIsText = false;
        }
    }

    /*
     *      Color Edition
     * */
    private void StartPaint(Color targetColor)
    {
        ColorPickerGobj = (GameObject)Instantiate(ColorPickerPrefab, Vector3.zero, Quaternion.identity);
        ColorPickerGobj.transform.LookAt(TargetCamera.transform);
        ColorPickerCp = ColorPickerGobj.GetComponent<ColorPickerTriangle>();
        ColorPickerCp.SetScale(3);
        ColorPickerCp.SetNewColor(targetColor);
        IsPainting = true;
    }

    private void StopPaint()
    {
        Destroy(ColorPickerGobj);
        IsPainting = false;
    }

    private void UpdateColor()
    {
        if (PaintTargetIsText)
            GameObject.Find("NodeTextColorData").GetComponent<Image>().color = CurrentNode.data.format.textColor = ColorPickerCp.TheColor;
        else
            GameObject.Find("NodeTextBackColorData").GetComponent<Image>().color = CurrentNode.data.format.backColor = ColorPickerCp.TheColor;

        CurrentNode.UpdateDisplay();
    }
}
