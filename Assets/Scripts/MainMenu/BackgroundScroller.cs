using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private RawImage _img;
    [SerializeField] private float _x, _y;
    
    void Update()
    {
        _img.uvRect = new Rect(_img.uvRect.position + new Vector2(_x*0.01f, _y*0.01f) * Time.deltaTime, _img.uvRect.size);
    }

    

}
