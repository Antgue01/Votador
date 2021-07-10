using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleList : MonoBehaviour
{
    [SerializeField] protected GameObject PeoplePrefab;
    protected List<GameObject> peopleList=new List<GameObject>();
}
