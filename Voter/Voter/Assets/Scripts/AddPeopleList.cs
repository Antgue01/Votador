using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddPeopleList : PeopleList
{
    [SerializeField] Button addButton;
    RectTransform addButtonTr;
    float peopleSize;
    Vector3 nextPos;
    private void Awake()
    {
        addButton.onClick.AddListener(add);
        peopleSize = Mathf.Abs(PeoplePrefab.GetComponent<RectTransform>().rect.height);
        addButtonTr = addButton.GetComponent<RectTransform>();
        nextPos = new Vector3(addButtonTr.position.x, addButtonTr.position.y-peopleSize, 0);
    }
    public void add()
    {
        peopleList.Add(GameObject.Instantiate(PeoplePrefab, this.transform, false));
        peopleList[peopleList.Count - 1].GetComponent<RectTransform>().position = nextPos;
        nextPos.y -= peopleSize;
        addButtonTr.position = nextPos;
    }
}
