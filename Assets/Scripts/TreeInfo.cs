using System;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TreeInfo : MonoBehaviour
{
    public Node primaleNode;
    public Node currentNode;
    public List<Node> nodesList = new List<Node>();

    private string scenariosPath;
    private TextAsset[] jsonScenariosFileList;
    private TextAsset selectedScenarios;
    private string status;


    private StreamReader fileStreamReader;
    private StreamWriter fileStreamWriter;

    // Start is called before the first frame update
    void Start()
    {
        scenariosPath = Application.dataPath + "/Scenarios";
        BuildScenariosFileList();
        //LoadTreeFromFile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelectedScenarioFileChanged(Int32 value)
    {
        Debug.Log(value);
        selectedScenarios = jsonScenariosFileList[value];
        LoadTreeFromFile();
    }

    private void LoadTreeFromFile()
    {
        Debug.Log(selectedScenarios.text);
        string[] lines = selectedScenarios.text.Split('\n');
        Debug.Log(lines);
        foreach (string line in lines)
        {
            Debug.Log(line);
            Node.NodeDataStorage data = JsonUtility.FromJson<Node.NodeDataStorage>(line);
            nodesList.Add(new Node(null, data));
        }
        Debug.Log(nodesList.Count);
    }

    private void SaveTreeToFile()
    {

    }

    private void BuildScenariosFileList()
    {
        jsonScenariosFileList = Resources.LoadAll<TextAsset>("Scenarios/");

        Dropdown.OptionDataList jsonList = new Dropdown.OptionDataList();

        foreach(TextAsset t in jsonScenariosFileList)
        {
            jsonList.options.Add(new Dropdown.OptionData(t.name));
            Debug.Log("found : " + t.name);
        }
        GameObject.Find("TreeLoaderDropdown").GetComponent<Dropdown>().ClearOptions();
        GameObject.Find("TreeLoaderDropdown").GetComponent<Dropdown>().AddOptions(jsonList.options);
    }

    private void UpdateTreeInfoDisplay()
    {
        GameObject.Find("TreeStatusData").GetComponent<Text>().text = status;
        GameObject.Find("TreeSizeData").GetComponent<Text>().text = nodesList.Count.ToString();
        GameObject.Find("TreeFileNameData").GetComponent<Text>().text = selectedScenarios.name;
    }
}
