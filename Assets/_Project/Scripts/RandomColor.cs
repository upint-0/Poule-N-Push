using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    SkinnedMeshRenderer rend;

    private void Start()
    {
        rend = GetComponent<SkinnedMeshRenderer>();

        rend.material.SetColor( "_Color", Color.HSVToRGB( Random.Range( 0, 1 ), 0.3f, 0.7f ) );
        rend.material.SetFloat( "_Deform", Random.Range( 0.0f, 0.25f ) );
    }
}
