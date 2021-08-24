using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class VotePeopleList : AddPeopleList
{
    protected bool _savingOrLoading = false;
    public override void disableAll()
    {
        _savingOrLoading = true;
        base.disableAll();
    }
    public override void enableAll()
    {
        _savingOrLoading = false;
        base.enableAll();
    }
    protected override void setNames()
    {
        savePath = PathManager.votedPath;
        saveExtention = ".voto";
        loadPath = PathManager.buildingPath;
        loadExtention = ".muebles";
    }
    protected override void write(StreamWriter writer, GameObject peopleObject)
    {
        VotePeople vote = peopleObject.GetComponent<VotePeople>();
        writer.WriteLine(vote?.getPersonName() + "+" + (int)vote?.GetVote());
    }
    public void LoadAuto()
    {
        if (!_savingOrLoading)
        {

            string fname = fileName;
            setFileName("auto");
            if (File.Exists(Application.persistentDataPath + savePath + fileName + saveExtention))
            {
                resetList();
                StreamReader reader = new StreamReader(Application.persistentDataPath + savePath + fileName + saveExtention);
                int num = int.Parse(reader.ReadLine());
                for (int i = 0; i < num; i++)
                {
                    string line = reader.ReadLine();
                    string[] personData = line.Split('+');
                    Add(personData[0]);
                    peopleList[i].GetComponent<VotePeople>().VotePersonProgramatically(int.Parse(personData[1]));
                }
                enableAll();
                reader.Close();
            }
            setFileName(fname);
        }
    }
    public override void Accept()
    {

        base.Accept();
        string name = fileName;
        if (!save && fileName != "auto")
        {
            setFileName("auto");
            Save();
            setFileName(name);
        }
    }


}
