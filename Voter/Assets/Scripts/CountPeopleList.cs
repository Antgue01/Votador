using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountPeopleList : AddPeopleList
{
    [SerializeField] protected Counter _counter;
    [SerializeField] RectTransform _No;
    [SerializeField] RectTransform _doubts;
    [SerializeField] float _offset;

    private void Start()
    {
        initializePositionVars();
        nextPos.y -= _offset / _scaler.referencePixelsPerUnit;
        _counter.Count();
        foreach (string person in _counter.Persons(VotePeople.Vote.SI))
        {
            Add();
            peopleList[peopleList.Count - 1].GetComponent<FormatGiver>().Format(VotePeople.Vote.SI, person, _counter.Results(person));
        }
        _No.position = nextPos;
        nextPos.y -= peopleSize + _offset / _scaler.referencePixelsPerUnit + (_No.rect.height / _scaler.referencePixelsPerUnit);
        foreach (string person in _counter.Persons(VotePeople.Vote.NO))
        {
            Add();
            peopleList[peopleList.Count - 1].GetComponent<FormatGiver>().Format(VotePeople.Vote.NO, person, _counter.Results(person));

        }
        foreach (string person in _counter.Persons(VotePeople.Vote.NR))
        {
            Add();
            peopleList[peopleList.Count - 1].GetComponent<FormatGiver>().Format(VotePeople.Vote.NR, person, _counter.Results(person));

        }
        _doubts.position = nextPos;
        nextPos.y -= peopleSize + _offset / _scaler.referencePixelsPerUnit + (_doubts.rect.height / _scaler.referencePixelsPerUnit);
        foreach (string person in _counter.Persons(VotePeople.Vote.ABSTENCION))
        {
            Add();
            peopleList[peopleList.Count - 1].GetComponent<FormatGiver>().Format(VotePeople.Vote.ABSTENCION, person, _counter.Results(person));

        }
    }
    protected override void initializePositionVars()
    {
        peopleSize = PeoplePrefab.GetComponent<RectTransform>().rect.height / _scaler.referencePixelsPerUnit;
        nextPos = (initialPos.position) + Vector3.down * (peopleSize + (initialPos.rect.height / _scaler.referencePixelsPerUnit));
        nextPos.x = 0;
        initPos = nextPos;
        scaleVector = new Vector3(_xPercentage / _scaler.referencePixelsPerUnit, PeoplePrefab.transform.localScale.y, PeoplePrefab.transform.localScale.z);

    }
    public override void Add()
    {
        peopleList.Add(GameObject.Instantiate(PeoplePrefab, this.transform, false));
        peopleList[peopleList.Count - 1].transform.localScale = scaleVector;
        RectTransform current = peopleList[peopleList.Count - 1].GetComponent<RectTransform>();
        current.position = nextPos;
        nextPos.y -= peopleSize;
    }
}
