using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class AddPeopleList : PeopleList
{
    [SerializeField] Button addButton;
    RectTransform addButtonTr;
    float peopleSize;
    Vector3 nextPos;
    Vector3 initPos;
    const string path = "Lists/Building/";
    const string extention = ".muebles";
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
        addButtonTr.position = nextPos;
    }
    private void Awake()
    {
        addButton.onClick.AddListener(Add);
        peopleSize = Mathf.Abs(PeoplePrefab.GetComponent<RectTransform>().rect.height);
        addButtonTr = addButton.GetComponent<RectTransform>();
        nextPos = new Vector3(addButtonTr.position.x, addButtonTr.position.y - peopleSize, 0);
        initPos = nextPos;
    }
    public override void Remove(int pos)
    {
        peopleList.RemoveAt(pos);
        Vector3 offset = new Vector3(0, peopleSize, 0);
        nextPos += offset;
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
        addButton.gameObject.SetActive(false);
    }
    public override void enableAll()
    {
        base.enableAll();
        addButton.gameObject.SetActive(true);
    }
    public override void Save()
    {

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        StreamWriter writer = new StreamWriter(path + fileName + extention, false, System.Text.Encoding.UTF8);
        writer.WriteLine(peopleList.Count);
        foreach (GameObject item in peopleList)
        {
            writer.WriteLine(item.GetComponent<HandlePeople>()?.getPersonName());
        }
        writer.Close();
        enableAll();
    }
    public override void Load()
    {
        resetList();
        enableAll();
        StreamReader reader = new StreamReader(path + fileName + extention, System.Text.Encoding.UTF8);
        int n = int.Parse(reader.ReadLine());
        for (int i = 0; i < n; i++)
        {
            string personName = reader.ReadLine();
            Add(personName);

        }
        reader.Close();

    }
}
