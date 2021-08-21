using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class FormatGiver : MonoBehaviour
{
    [SerializeField] Material YES;
    [SerializeField] Material NO;
    [SerializeField] Material DOUBTS;
    [SerializeField]  TMP_Text text;
    Material[] sprites;
    [SerializeField] List<TMP_Text> votes;

    private void Awake()
    {
        sprites = new Material[] { YES, NO, DOUBTS };
    }
    public void Format(VotePeople.Vote vote,string nText,int[]results)
    {
        text.fontSharedMaterial.CopyPropertiesFromMaterial(sprites[(int)vote]);
        text.text = nText;
        for (int i = 0; i < votes.Count; i++)
        {
            votes[i].text = results[i].ToString();
        }
    }
}
