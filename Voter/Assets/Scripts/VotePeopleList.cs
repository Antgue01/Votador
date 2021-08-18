using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class VotePeopleList : AddPeopleList
{

    protected override void setNames()
    {
        savePath = "/Lists/Voted/";
        saveExtention = ".voto";
        loadPath = "/Lists/Building/";
        loadExtention = ".muebles";
    }
    protected override void write(StreamWriter writer, GameObject peopleObject)
    {
        VotePeople vote = peopleObject.GetComponent<VotePeople>();
        writer.WriteLine(vote?.getPersonName() + "-" + (int)vote?.GetVote());
    }
    public void LoadAuto()
    {
        setFileName("auto");
        if (File.Exists(Application.persistentDataPath + savePath + fileName + saveExtention))
        {
            //string load = loadPath;
            //loadPath = savePath;
            //string ext = loadExtention;
            //loadExtention = saveExtention;
            setLoad();
            //loadPath = load;
            //loadExtention=ext;
            Accept();
            enableAll();
            print("hola");
        }
    }
    public override void Accept()
    {

        base.Accept();
        if (!save && fileName != "auto")
        {
            setFileName("auto");
            Save();
        }
    }


}
