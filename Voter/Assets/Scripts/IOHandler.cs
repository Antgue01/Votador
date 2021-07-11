using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IOHandler : MonoBehaviour
{
    [SerializeField] GameObject IOPanel;
    [SerializeField] PeopleList list;
    public void ActivateIOPanel(bool save)
    {
        IOPanel.SetActive(true);
        if (save)
            list.setSave();
        else
            list.setLoad();
    }
    public void deactivatePanel()
    {
        IOPanel.SetActive(false);
    }
}
