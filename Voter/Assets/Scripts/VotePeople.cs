using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VotePeople : HandlePeople
{
    protected bool _autosave = true;
    public enum Vote { SI = 0, NO = 1, NR = 2 ,ABSTENCION = 3, }
    Vote vote;
    public Vote GetVote() { return vote; }
    [SerializeField] protected Dropdown _dropdown;
    public void VotePerson(int voteSel)
    {

        vote = (Vote)voteSel;
        if (_autosave)
        {
            string name = OwnerList.getFileName();
            OwnerList.setFileName("auto");
            OwnerList.Save();
            OwnerList.setFileName(name);
        }
    }
    public void VotePersonProgramatically(int voteSel)
    {
        vote = (Vote)voteSel;
        _autosave = false;
        _dropdown.value = voteSel;
        _autosave = true;
    }
}
