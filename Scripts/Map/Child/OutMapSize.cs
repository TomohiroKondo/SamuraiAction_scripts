using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 多分使わない
/// </summary>
public class OutMapSize : MonoBehaviour {

    public float outWidth;
    public float outLength;

    // Use this for initialization
    void Start()
    {
        outWidth = this.gameObject.GetComponent<Renderer>().bounds.size.x;
        outLength = this.gameObject.GetComponent<Renderer>().bounds.size.z;
    }
}
