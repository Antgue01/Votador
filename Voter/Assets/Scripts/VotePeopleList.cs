using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class VotePeopleList : AddPeopleList
{
    protected override void setNames()
    {
        path = "Lists/Voted/";
        extention = ".voto";
    }
    protected override void write(StreamWriter writer, GameObject peopleObject)
    {
        VotePeople vote = peopleObject.GetComponent<VotePeople>();
        writer.WriteLine(vote?.getPersonName() + " " + vote?.GetVote());
    }
}
