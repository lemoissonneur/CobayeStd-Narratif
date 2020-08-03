using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cobaye.ScenarioSystem;

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
        /*
        ScrollContainerGObj = GameObject.Find("ScrollAreaContainer");
        ScrollContainerGObj.GetComponent<RectTransform>().offsetMax = new Vector2(ScrollContainerGObj.GetComponent<RectTransform>().offsetMax.x, 0);
        //primaleNode = new Node(ScrollContainerGObj);

        ExtractFromFile(JsonFilePath);
        BuildNodeTree();
        */
        TestGenerate();
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
            nodesList.Add(new Node(data));
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
        data0.text = "node 0D: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam at erat in mi aliquam pulvinar. Integer vel libero imperdiet, viverra nisi a, condimentum enim. Sed nec ultrices velit. Maecenas nec vehicula massa. Vivamus efficitur, quam sit amet feugiat semper, dolor eros accumsan dolor, ac pharetra ligula sem vel ante. Donec convallis rutrum nibh a lacinia. Fusce ut sem non eros convallis fringilla non ac lectus. Nulla vitae arcu ac ligula lobortis bibendum id sit amet nibh. Aenean sollicitudin nisl id diam bibendum, ac varius nulla mattis. Nam mattis nulla ac nisi accumsan rhoncus. Etiam auctor risus justo, a vestibulum massa tincidunt id. Aliquam erat volutpat. Morbi vel mattis urna, vel semper enim. Fusce ac tellus vel nisl sollicitudin consequat et sed tortor. Quisque mattis orci sed euismod posuere. Fusce luctus urna ut leo facilisis pretium.";
        data0.previousNodeId = new List<uint>();
        data0.nextNodesId = new List<uint>();
        data0.nextNodesId.Add(1);
        data0.type = Node.NodeType.TEXT;
        data0.format = Node.defaultTextFormat;

        Node.NodeDataStorage data1;
        data1.id = 1;
        data1.text = "node 1D: Mauris vel pretium nulla. Praesent nisl tortor, consequat vel ullamcorper sed, ullamcorper vel nulla. Duis volutpat mollis neque. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Nulla semper arcu varius sapien sodales, sed finibus odio tincidunt. Donec imperdiet mauris ut nunc scelerisque facilisis. Praesent ac nisl sem.";
        data1.previousNodeId = new List<uint>();
        data1.previousNodeId.Add(0);
        data1.nextNodesId = new List<uint>();
        data1.nextNodesId.Add(2);
        data1.type = Node.NodeType.TEXT;
        data1.format = Node.defaultTextFormat;

        Node.NodeDataStorage data2;
        data2.id = 2;
        data2.text = "node 2D: Ut sollicitudin, ligula nec mollis gravida, velit eros laoreet ipsum, quis tincidunt quam arcu quis tortor. Donec pellentesque, ante eget euismod lacinia, augue arcu luctus est, quis consequat lacus velit sit amet velit. Curabitur consectetur, ante nec pharetra tempus, turpis magna ultricies nunc, ac vestibulum eros erat lacinia elit. Curabitur sit amet molestie nunc. Vestibulum fringilla dui ligula. Vivamus egestas id odio ut rhoncus. Suspendisse potenti.";
        data2.previousNodeId = new List<uint>();
        data2.previousNodeId.Add(1);
        data2.nextNodesId = new List<uint>();
        data2.type = Node.NodeType.TEXT;
        data2.format = Node.defaultTextFormat;

        fileStreamWriter = new StreamWriter(JsonFilePath, false);
        fileStreamWriter.AutoFlush = true;

        fileStreamWriter.WriteLine(JsonUtility.ToJson(data0));
        fileStreamWriter.WriteLine(JsonUtility.ToJson(data1));
        fileStreamWriter.WriteLine(JsonUtility.ToJson(data2));

        fileStreamWriter.Close();
    }

    public void NewTextNodeBtn()
    {

    }

    public void NewChoiceNodeBtn()
    {

    }
}
