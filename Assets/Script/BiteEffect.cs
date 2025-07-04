using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteEffect : MonoBehaviour
{
   
    void Start()
    {
        StartCoroutine(ScaleUpCoroutine());

    }


    IEnumerator ScaleUpCoroutine()
    {
        gameObject.layer = LayerMask.NameToLayer("Escape");
        yield return null;
        Destroy(gameObject);
    }
}
