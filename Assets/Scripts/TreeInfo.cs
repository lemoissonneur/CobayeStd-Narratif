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

    private void SaveTreeToFile()
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
            {
                primaleNode = n;
                break;
            }
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
