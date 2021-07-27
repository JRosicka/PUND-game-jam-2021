using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroMover : MonoBehaviour
{
    private const string SHOOT_NAME = "Shoot";

    [Range(0f, 10f)] public float cameraMoveSpeed = 2.5f;
    public Rail rail;
    
    private int currentSeg;
    private float transition;
    private bool doneWithOutro;

    private void Update()
    {
        if(!rail)
        {
            return;
        }

        if (doneWithOutro) return;

        Play();
;
    }

    private void Play()
    {
        transition += Time.deltaTime * 1 / cameraMoveSpeed;
        if(transition >1)
        {
            transition = 0;
            currentSeg++;
        }
        else if(transition < 0)
        {
            transition = 1;
            currentSeg--;
        }

        if (currentSeg >= rail.nodes.Length - 1) {
            doneWithOutro = true;
            return;
        }

        transform.position = rail.LinearPosition(currentSeg, transition);
        transform.rotation = rail.Orientation(currentSeg, transition);

    }
}
