using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandlePeople : MonoBehaviour
{
    [SerializeField] InputField personName;
    public void blockPerson()
    {
        if (personName)
            personName.interactable = false;
    }
    public void removePerson()
    {
        if (personName)
        {
            personName.interactable = true;
            personName.text = "";
        }

    }
    public void setPersonName(string name)
    {
        if (personName)
        {
            personName.text = name;
        }
    }
    public string getPersonName()
    {
        if (personName)
            return personName.text;
        return "Error";
    }
    private void OnDisable()
    {
        if (personName && personName.interactable)
            personName.text = "";
    }
}
