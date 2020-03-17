using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public GameObject thisObject;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.thisObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
