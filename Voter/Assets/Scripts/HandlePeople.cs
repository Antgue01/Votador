using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandlePeople : MonoBehaviour
{
    [SerializeField]  InputField personName;
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
}
