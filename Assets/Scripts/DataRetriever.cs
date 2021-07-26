using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRetriever : MonoBehaviour
{
    public Transform node;
    // Start is called before the first frame update
    void Start()
    {
        node.transform.position = new Vector3(PlayerPrefs.GetFloat("cameraPosX"), PlayerPrefs.GetFloat("cameraPosY"), PlayerPrefs.GetFloat("cameraPosZ"));
        node.transform.rotation = new Quaternion (180, PlayerPrefs.GetFloat("cameraRotY"), 0, 0);
    }
}
