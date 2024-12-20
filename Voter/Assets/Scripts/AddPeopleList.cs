using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class AddPeopleList : PeopleList
{
    [SerializeField] Button addButton;
    [SerializeField]protected RectTransform initialPos;
    [SerializeField] protected CanvasScaler _scaler;
    [SerializeField] protected float _xPercentage=85;
    RectTransform addButtonTr;
    protected float peopleSize;

    protected Vector3 nextPos;
    protected Vector3 initPos;
    protected string savePath;
    protected string loadPath;
    protected string saveExtention;
    protected string loadExtention;
    protected Vector3 scaleVector;

    const float offset = 10;
    public virtual void Accept()
    {
        if (save)
            Save();
        else Load();
    }
    protected void resetList()
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

    private void Awake()
    {
        initializePositionVars();
        setNames();

    }
    protected virtual void initializePositionVars()
    {
        if (addButton)
        {
            addButton.onClick.AddListener(Add);
            addButtonTr = addButton.GetComponent<RectTransform>();
        }
        peopleSize = 1;
        nextPos = (initialPos.position) + Vector3.down * .4f;
        initPos = nextPos;
        scaleVector = new Vector3(_xPercentage / _scaler.referencePixelsPerUnit, PeoplePrefab.transform.localScale.y, PeoplePrefab.transform.localScale.z);
    }
    protected virtual void setNames()
    {
        savePath = loadPath = PathManager.buildingPath;
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
        peopleList[peopleList.Count - 1].transform.localScale = scaleVector;
        RectTransform current = peopleList[peopleList.Count - 1].GetComponent<RectTransform>();
        if (peopleList.Count < 2)
        {
            current.position = nextPos;
            nextPos.y -= peopleSize;

        }
        else
        {
            RectTransform previous = peopleList[peopleList.Count - 2].GetComponent<RectTransform>();
            current.position = previous.position + Vector3.down * peopleSize;
            nextPos = current.position + Vector3.down * peopleSize;
        }
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
        if (File.Exists(Application.persistentDataPath + loadPath + fileName + loadExtention))
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
}
