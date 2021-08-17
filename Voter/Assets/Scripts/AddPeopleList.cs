using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class AddPeopleList : PeopleList
{
    [SerializeField] Button addButton;
    [SerializeField] RectTransform initialPos;
    RectTransform addButtonTr;
    float peopleSize;

    Vector3 nextPos;
    Vector3 initPos;
    protected string savePath;
    protected string loadPath;
    protected string saveExtention;
    protected string loadExtention;
    const float offset = 10;
    public void Accept()
    {
        if (save)
            Save();
        else Load();
    }
    void resetList()
    {
        foreach (GameObject GO in peopleList)
        {
            GameObject.Destroy(GO);
        }
        peopleList.Clear();
        nextPos = initPos;
        if (addButtonTr)
            addButtonTr.position = nextPos;
    }
    public void kk(Vector2 v)
    {
        print(v);
    }
    private void Awake()
    {
        if (addButton)
        {
            addButton.onClick.AddListener(Add);
            addButtonTr = addButton.GetComponent<RectTransform>();
        }
        peopleSize = 1;
        nextPos = (initialPos.position) +Vector3.down*.5f;
        initPos = nextPos;
        setNames();
        //print(c.referencePixelsPerUnit / PeoplePrefab.GetComponent<RectTransform>().rect.height);

    }
    protected virtual void setNames()
    {
        savePath = loadPath = "/Lists/Building/";
        saveExtention = loadExtention = ".muebles";

    }
    protected virtual void write(StreamWriter writer, GameObject peopleObject)
    {
        writer.WriteLine(peopleObject.GetComponent<HandlePeople>()?.getPersonName());

    }
    public override void Remove(int pos)
    {
        peopleList.RemoveAt(pos);
        Vector3 offset = new Vector3(0, peopleSize, 0);
        nextPos += offset;
        if (addButtonTr)
            addButtonTr.position += offset;
        for (int i = pos; i < peopleList.Count; i++)
        {
            peopleList[i].GetComponent<RectTransform>().position += offset;
            peopleList[i].GetComponent<HandlePeople>().decreasePosInList();

        }
    }
    public override void Add()
    {
        peopleList.Add(GameObject.Instantiate(PeoplePrefab, this.transform, false));
        peopleList[peopleList.Count - 1].GetComponent<RectTransform>().position = nextPos;
        nextPos.y -= peopleSize;
        if (addButtonTr)
            addButtonTr.position = nextPos;
        peopleList[peopleList.Count - 1].GetComponent<HandlePeople>()?.setOwnerList(this, peopleList.Count - 1);
    }
    public void Add(string name)
    {
        Add();
        HandlePeople hP = peopleList[peopleList.Count - 1].GetComponent<HandlePeople>();
        hP?.setPersonName(name);
        hP?.blockPerson();


    }
    public override void disableAll()
    {
        base.disableAll();
        if (addButton)
            addButton.gameObject.SetActive(false);
    }
    public override void enableAll()
    {
        base.enableAll();
        if (addButton)
            addButton.gameObject.SetActive(true);
    }
    public override void Save()
    {

        if (!Directory.Exists(Application.persistentDataPath + savePath))
            Directory.CreateDirectory(Application.persistentDataPath + savePath);
        StreamWriter writer = new StreamWriter(Application.persistentDataPath + savePath + fileName + saveExtention, false, System.Text.Encoding.UTF8);
        writer.WriteLine(peopleList.Count);
        foreach (GameObject item in peopleList)
        {
            write(writer, item);
        }
        writer.Close();
        enableAll();
    }
    public override void Load()
    {
        resetList();
        enableAll();
        StreamReader reader = new StreamReader(Application.persistentDataPath + loadPath + fileName + loadExtention, System.Text.Encoding.UTF8);
        int n = int.Parse(reader.ReadLine());
        for (int i = 0; i < n; i++)
        {
            string personName = reader.ReadLine();
            Add(personName);

        }
        reader.Close();

    }
}
