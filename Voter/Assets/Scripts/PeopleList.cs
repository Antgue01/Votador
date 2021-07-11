using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class PeopleList : MonoBehaviour
{
    [SerializeField] protected GameObject PeoplePrefab;
    protected bool save;
    protected string fileName;
    protected List<GameObject> peopleList = new List<GameObject>();
    public void setFileName(string name)
    {
        fileName = name;
    }
    public void setSave() { save = true; }
    public void setLoad() { save = false; }
   public virtual void disableAll()
    {
        foreach (GameObject GO in peopleList)
        {
            GO.SetActive(false);
        }
    }
    public virtual void enableAll()
    {
        foreach (GameObject GO in peopleList)
        {
            GO.SetActive(true);
        }
    }
    public abstract void Save();
    public abstract void Load();

    public abstract void Add();
    public abstract void Remove(int pos);


}
