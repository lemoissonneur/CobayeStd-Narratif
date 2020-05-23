using System;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TreeInfo : MonoBehaviour
{
    public Node primaleNode;
    public List<Node> nodesList = new List<Node>();

    public NodeInfo NodeInfoGObj;
    private int NavigationIndex = 0;

    private string scenariosDirectoryPath;
    private List<string> scenariosFilesList = new List<string>();
    private int currentScenarioIndex;

    private string status;

    private StreamReader fileStreamReader;
    private StreamWriter fileStreamWriter;

    // Start is called before the first frame update
    void Start()
    {
        NodeInfoGObj = GameObject.Find("NodeInfoPanel").GetComponent<NodeInfo>();
        scenariosDirectoryPath = Application.dataPath + "/Scenarios";
        BuildScenariosFileList();
    }

    // Update is called once per frame
    void Update()
    {
        ArrowKeyWatcher();
    }

    public void OnSelectedScenarioFileChanged(Int32 value)
    {
        currentScenarioIndex = value;

        LoadTreeFromFile(scenariosFilesList[currentScenarioIndex]);

        UpdateTreeInfoDisplay();

        GameObject.Find("NodeInfoPanel").GetComponent<NodeInfo>().LoadNode(primaleNode);

        NavigationIndex = 0;
    }

    private void LoadTreeFromFile(string filePath)
    {
        fileStreamReader = new StreamReader(filePath, false);
        nodesList = new List<Node>();
        string newline;

        while((newline = fileStreamReader.ReadLine()) != null)
        {
            Node.NodeDataStorage data = JsonUtility.FromJson<Node.NodeDataStorage>(newline);
            nodesList.Add(new Node(data));
        }
        Debug.Log(nodesList.Count);

        fileStreamReader.Close();

        BuildTree();

        status = "Loaded";

        NodeInfoGObj.LoadNode(primaleNode);
    }

    public void SaveTreeToFile()
    {
        fileStreamWriter = new StreamWriter(scenariosFilesList[currentScenarioIndex], false);

        foreach(Node n in nodesList)
        {
            fileStreamWriter.WriteLine(JsonUtility.ToJson(n.data));
        }

        fileStreamWriter.Close();

        status = "Saved";
    }

    private void BuildTree()
    {
        // get first node
        foreach(Node n in nodesList)
        {
            if (n.data.id == 0)
                primaleNode = n;

            // break the tree before rebuilding it
            n.FatherNode = new List<Node>();
            n.ChildsNodes = new List<Node>();
        }

        primaleNode.GetChildsInList(nodesList);

        Debug.Log("finished building tree");
    }

    private void BuildScenariosFileList()
    {
        // get list of files in scenarios directory
        List<string> filesList = new List<string>(Directory.GetFiles(scenariosDirectoryPath));

        // create new dropdown options list
        Dropdown.OptionDataList jsonList = new Dropdown.OptionDataList();
        scenariosFilesList = new List<string>();

        foreach (string filePath in filesList)
        {
            if(!filePath.Contains(".meta"))
            {
                Debug.Log("found : " + filePath);           // print

                scenariosFilesList.Add(filePath);           // add this to the list

                int start = filePath.LastIndexOf("\\")+1;   // extract filename
                int stop = filePath.LastIndexOf(".json");
                string fileName = filePath.Substring(start, stop - start);

                jsonList.options.Add(new Dropdown.OptionData(fileName));
            }
        }
        GameObject.Find("TreeLoaderDropdown").GetComponent<Dropdown>().ClearOptions();
        GameObject.Find("TreeLoaderDropdown").GetComponent<Dropdown>().AddOptions(jsonList.options);
    }

    private void UpdateTreeInfoDisplay()
    {
        GameObject.Find("TreeStatusData").GetComponent<Text>().text = status;
        GameObject.Find("TreeSizeData").GetComponent<Text>().text = nodesList.Count.ToString();
        GameObject.Find("TreeFileNameData").GetComponent<Text>().text = GameObject.Find("TreeLoaderDropdown").GetComponent<Dropdown>().options[currentScenarioIndex].text;
    }

    public void OnNewNode()
    {
        Node node = new Node((uint)nodesList.Count);
        if(NodeInfoGObj.CurrentNode != null)
        {
            node.data.format = NodeInfoGObj.CurrentNode.data.format;
            node.data.previousNodeId.Add(NodeInfoGObj.CurrentNode.data.id);
            node.data.format = NodeInfoGObj.CurrentNode.data.format;
            NodeInfoGObj.CurrentNode.data.nextNodesId.Add(node.data.id);
        }

        nodesList.Add(node);

        BuildTree();

        NodeInfoGObj.LoadNode(node);
    }

    public void OnDeleteNode()
    {
        if (NodeInfoGObj.CurrentNode != null)
        {
            // get first father Id (we will jump to it after delete
            Node nodeToJmp = NodeInfoGObj.CurrentNode.FatherNode[0];

            // remove the current node ref in fathers & childs nodes
            foreach (Node n in nodesList)
            {
                if (NodeInfoGObj.CurrentNode.data.previousNodeId.Contains(n.data.id))   // this node is a father
                    n.data.nextNodesId.Remove(NodeInfoGObj.CurrentNode.data.id);

                if (NodeInfoGObj.CurrentNode.data.nextNodesId.Contains(n.data.id))    // this node is a child
                {
                    n.data.previousNodeId.Remove(NodeInfoGObj.CurrentNode.data.id);
                    if (n.data.previousNodeId.Count < 1)
                        Debug.LogError("WARNING ! node " + n.data.id + " will become fatherless !!! ");
                }
            }

            nodesList.Remove(NodeInfoGObj.CurrentNode);

            BuildTree();

            NodeInfoGObj.LoadNode(nodeToJmp);
        }
    }

    // Tree Navigation
    private void ArrowKeyWatcher()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            OnUpArrow();
        if (Input.GetKeyDown(KeyCode.DownArrow))
            OnDownArrow();
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            OnLeftArrow();
        if (Input.GetKeyDown(KeyCode.RightArrow))
            OnRightArrow();
    }

    public void OnUpArrow() // go up the tree
    {
        if (NodeInfoGObj.CurrentNode.FatherNode.Count > 0)
        {
            NodeInfoGObj.LoadNode(NodeInfoGObj.CurrentNode.FatherNode[NavigationIndex]);
            NavigationIndex = 0;
        }
    }

    public void OnDownArrow()   // go down tree
    {
        if (NodeInfoGObj.CurrentNode.ChildsNodes.Count > 0)
        {
            NodeInfoGObj.LoadNode(NodeInfoGObj.CurrentNode.ChildsNodes[NavigationIndex]);
            NavigationIndex = 0;
        }
    }

    public void OnLeftArrow()   // select left
    {
        if (NavigationIndex > 0)
            NavigationIndex--;
    }

    public void OnRightArrow()  // select right
    {
        if (NavigationIndex < NodeInfoGObj.CurrentNode.ChildsNodes.Count - 1)
            NavigationIndex++;
    }

}
