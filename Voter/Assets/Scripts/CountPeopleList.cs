using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountPeopleList : AddPeopleList
{
    [SerializeField] protected Counter _counter;
    private void Awake()
    {
        _counter.Count();
        foreach (string item in _counter.Persons(VotePeople.Vote.SI))
        {

        }
    }
}
