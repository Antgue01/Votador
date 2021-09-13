using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using TMPro;

public class Counter : MonoBehaviour
{
    // Start is called before the first frame update
    const string path = "/Lists/Ready/";
    Dictionary<string, int[]> results;
    List<List<string>> persons;
    const float _quorum = 2 / 3;
    [SerializeField] TMP_Text number;


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
            persons.Add(new List<string>());
        }
        print(persons.Count);
        int councilMembers = 0;
        //para cada archivo relleno el diccionario de personas-votos, conteándolos todos
        foreach (FileInfo file in info.GetFiles())
        {
            councilMembers++;
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
        //Para evitar problemas con la precisión del punto flotante, comprobamos a mano que el último dígito es >=5
        int integerHalf = (councilMembers / 2);
        int halfPlusOne = (councilMembers * 5) % 10 >= 5 ? integerHalf + 2 : integerHalf + 1;
        number.text = halfPlusOne.ToString();
        //Para cada persona calculamos el resultado y
        //Rellenamos el diccionario de resultados-personas
        foreach (string person in results.Keys)
        {
            int voteResult = computeVoteResult(results[person], halfPlusOne);
            persons[voteResult].Add(person);
        }

    }

    int computeVoteResult(int[] votes, int halfPlusOne)
    {
        //Si hay más de mitad de abst, entonces es directamente no
        if (votes[3] >= halfPlusOne)
            return 1;
        int validVotes = 0;
        //sumamos todos los votos menos las abstenciones
        for (int i = 0; i < 2; i++)
        {
            validVotes += votes[i];
        }
        int YesQuorumValidVotes = (int)Math.Round(_quorum * validVotes, MidpointRounding.AwayFromZero);
        int halfValidVotes = validVotes / 2;
        int DoubtQuorumValidVotes = (validVotes * 5) % 10 >= 5 ? halfValidVotes + 1 : halfValidVotes;
        //asumimos que no
        int voteResult = 1;
        if (votes[0] >= YesQuorumValidVotes)
            voteResult = 0;
        else if (votes[0] >= DoubtQuorumValidVotes)
            voteResult = 2;
        return voteResult;
    }

}
