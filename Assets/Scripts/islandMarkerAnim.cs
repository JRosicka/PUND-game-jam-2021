using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class islandMarkerAnim : MonoBehaviour
{
    
    public float omega = 1f;
    public float amplitude = 0.5f;
    public float translate = 1.2f;
    public float period = 1;

    private float index;
    void Update()
    {
        index += Time.deltaTime;

        float sineWave = amplitude * Mathf.Sin((omega * index)/period) + translate;
        

        transform.Rotate(Vector3.up * Time.deltaTime * 100f);
        transform.localScale = new Vector3 (sineWave, 1, sineWave);
    }
}
