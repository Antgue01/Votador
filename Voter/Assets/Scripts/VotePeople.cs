using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VotePeople : HandlePeople
{
    public enum Vote { SI = 0, NO = 1, ABSTENCION = 2, TACHADO = 3 }
    Vote vote;
    public Vote GetVote() { return vote; }
    public void VotePerson(int voteSel)
    {
        vote = (Vote)voteSel;
        OwnerList.setFileName("auto");
        OwnerList.Save();
        
    }
}
