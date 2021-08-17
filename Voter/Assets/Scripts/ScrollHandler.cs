using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScrollHandler : MonoBehaviour
{
    [SerializeField] ScrollRect _rect;
    private void Update()
    {
        //_rect.verticalNormalizedPosition += 1f;
        Debug.Log(_rect.verticalNormalizedPosition);
    }
    public void HandleScroll(Vector2 v)
    {
        
        //if (_rect.verticalNormalizedPosition < .1f)
        //{
        //    _rect.verticalNormalizedPosition = 0;
        //}
        //else if (_rect.verticalNormalizedPosition > .9f)
        //{
        //    _rect.verticalNormalizedPosition = 1;

        //}
    }
}
