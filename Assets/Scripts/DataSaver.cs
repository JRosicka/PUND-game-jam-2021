using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaver : MonoBehaviour
{
    public Transform cameraPos;

    void Update()
    {

        PlayerPrefs.SetFloat("cameraPosX", cameraPos.position.x);
        PlayerPrefs.SetFloat("camerPosY", cameraPos.position.y);
        PlayerPrefs.SetFloat("camerPosZ", cameraPos.position.z);

        PlayerPrefs.SetFloat("camerRotX", cameraPos.rotation.x);
        PlayerPrefs.SetFloat("camerRotY", cameraPos.rotation.y);
        PlayerPrefs.SetFloat("camerRotZ", cameraPos.rotation.z);
        PlayerPrefs.SetFloat("camerRotW", cameraPos.rotation.w);
    }
}
