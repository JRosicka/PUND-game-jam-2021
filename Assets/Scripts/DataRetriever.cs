using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRetriever : MonoBehaviour
{
    public Transform node;
    // Start is called before the first frame update
    void Awake()
    {
        node.position = new Vector3(PlayerPrefs.GetInt("cameraPosX"), PlayerPrefs.GetInt("cameraPosY"), PlayerPrefs.GetInt("cameraPosZ"));
        node.rotation = new Quaternion(PlayerPrefs.GetInt("cameraRotX"), PlayerPrefs.GetInt("cameraRotY"),PlayerPrefs.GetInt("RotZ"),PlayerPrefs.GetInt("RotW"));
    }
}
