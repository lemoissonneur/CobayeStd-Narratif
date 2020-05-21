using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Node primaleNode;
    public Node currentNode;
    public List<Node> nodesList = new List<Node>();

    public string JsonFilePath = "Temp/test.json";

    private StreamReader fileStreamReader;
    private StreamWriter fileStreamWriter;

    GameObject ScrollContainerGObj;

    // Start is called before the first frame update
    void Awake()
    {
        ScrollContainerGObj = GameObject.Find("ScrollAreaContainer");
        ScrollContainerGObj.GetComponent<RectTransform>().offsetMax = new Vector2(ScrollContainerGObj.GetComponent<RectTransform>().offsetMax.x, 0);
        //primaleNode = new Node(ScrollContainerGObj);

        ExtractFromFile(JsonFilePath);
        BuildNodeTree();

        //TestGenerate();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ExtractFromFile(string FilePath)
    {
        fileStreamReader = new StreamReader(FilePath, false);
        string newline;

        while((newline = fileStreamReader.ReadLine()) != null)
        {
            Node.NodeDataStorage data = JsonUtility.FromJson<Node.NodeDataStorage>(newline);
            nodesList.Add(new Node(ScrollContainerGObj, data));
        }
    }

    void BuildNodeTree()
    {
        // get node 0
        foreach(Node node in nodesList)
        {
            if (node.data.id == 0)
            {
                primaleNode = node;
                break;
            }
        }

        Populate(primaleNode);
    }

    void Populate(Node node)
    {
        foreach(uint childId in node.data.nextNodesId)  // for each child node id
        {
            foreach(Node n in nodesList)                // find this node in the list
            {
                if (n.data.id == childId)
                {
                    node.ChildsNodes.Add(n);
                    Populate(n);
                }
            }
        }
    }

    void TestGenerate()
    {
        Node.NodeDataStorage data0;
        data0.id = 0;
        data0.text = "node 0";
        data0.previousNodeId = 0;
        data0.nextNodesId = new List<uint>();
        data0.nextNodesId.Add(1);
        data0.type = Node.NodeType.TEXT;
        data0.format = Node.defaultTextFormat;

        Node.NodeDataStorage data1;
        data1.id = 1;
        data1.text = "node 1";
        data1.previousNodeId = 0;
        data1.nextNodesId = new List<uint>();
        data1.nextNodesId.Add(2);
        data1.type = Node.NodeType.TEXT;
        data1.format = Node.defaultTextFormat;

        Node.NodeDataStorage data2;
        data2.id = 2;
        data2.text = "node 2";
        data2.previousNodeId = 1;
        data2.nextNodesId = new List<uint>();
        data2.type = Node.NodeType.TEXT;
        data2.format = Node.defaultTextFormat;

        fileStreamWriter = new StreamWriter(JsonFilePath, false);
        fileStreamWriter.AutoFlush = true;

        fileStreamWriter.WriteLine(JsonUtility.ToJson(data0));
        fileStreamWriter.WriteLine(JsonUtility.ToJson(data1));
        fileStreamWriter.WriteLine(JsonUtility.ToJson(data2));
    }

    public void NewTextNodeBtn()
    {

    }

    public void NewChoiceNodeBtn()
    {

    }
}
