using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Counter : MonoBehaviour
{
    // Start is called before the first frame update
    const string path = "/Lists/Ready/";
    Dictionary<string, int[]> results;
    List<List<string>> persons;
    public List<string> Persons(VotePeople.Vote vote)
    {
        return persons[(int)vote];
    }
    public int[] Results(string personName)
    {
        return results[personName];
    }
    public void Count()
    {
        DirectoryInfo info = new DirectoryInfo(Application.persistentDataPath + path);
        results = new Dictionary<string, int[]>();
        persons = new List<List<string>>();
        for (int i = 0; i < 4; i++)
        {
            persons[i] = new List<string>();
        }
        //para cada archivo relleno el diccionario de personas-votos, conteándolos todos
        foreach (FileInfo file in info.GetFiles())
        {
            //FileStream myfile = file.Open(FileMode.Open);
            StreamReader reader = new StreamReader(Application.persistentDataPath + path + file.Name);
            int num = int.Parse(reader.ReadLine());
            for (int i = 0; i < num; i++)
            {
                int[] personResults;
                string line = reader.ReadLine();
                string[] personData = line.Split('+');
                if (!results.TryGetValue(personData[0], out personResults))
                {
                    int[] aux = { 0, 0, 0, 0 };
                    aux[int.Parse(personData[1])]++;
                    results.Add(personData[0], aux);
                }
                else
                {
                    personResults[int.Parse(personData[1])]++;
                    results[personData[0]] = personResults;
                }
            }
            reader.Close();
        }
        //Para cada persona calculamos el resultado y
        //Rellenamos el diccionario de resultados-personas
        foreach (string person in results.Keys)
        {
            int voteResult = computeVoteResult(results[person]);
            persons[voteResult].Add(person);
        }
        print("SI: " + Persons(VotePeople.Vote.SI).Count);
        foreach (string item in Persons(VotePeople.Vote.SI))
        {
            print(item);
        }
        print("NO: " + Persons(VotePeople.Vote.NO).Count);
        foreach (string item in Persons(VotePeople.Vote.NO))
        {
            print(item);
        }
        print("ABS: " + Persons(VotePeople.Vote.ABSTENCION).Count);
        foreach (string item in Persons(VotePeople.Vote.ABSTENCION))
        {
            print(item);
        }
        print("TACH: " + Persons(VotePeople.Vote.TACHADO).Count);
        foreach (string item in Persons(VotePeople.Vote.TACHADO))
        {
            print(item);
        }
    }
    int computeVoteResult(int[] votes)
    {
        int index = 0;
        int max = 0;
        for (int i = 0; i < votes.Length; i++)
        {
            if (votes[i] > max)
            {
                max = votes[i];
                index = i;
            }
        }
        return index;

    }
    // Update is called once per frame
    void Update()
    {

    }
}
