using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class FormatGiver : MonoBehaviour
{
    [SerializeField] Sprite YES;
    [SerializeField] Sprite NO;
    [SerializeField] Sprite DOUBTS;
    [SerializeField] TMP_Text text;
    Sprite[] sprites;
    private void Awake()
    {
        sprites = new Sprite[] { YES, NO, DOUBTS };
    }
    public void Format(VotePeople.Vote vote,string nText)
    {
        text.fontSharedMaterial.mainTexture = sprites[(int)vote].texture;
        text.text = nText;
    }
}
