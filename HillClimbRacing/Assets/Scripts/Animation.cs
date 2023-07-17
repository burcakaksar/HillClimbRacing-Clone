using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Animation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DORotate(new Vector3(1,360,1),3, RotateMode.FastBeyond360).SetLoops(-1,LoopType.Restart);
        transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1.5f).SetLoops(-1, LoopType.Yoyo);
        //Sequence mySequence = DOTween.Sequence();

        //mySequence.Append (transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 3).SetLoops(-1, LoopType.Yoyo));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}