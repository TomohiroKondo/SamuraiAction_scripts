using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 多分使わない
/// </summary>
public class CenterMapSize : MonoBehaviour {

    public float centerWidth;
    public float centerLength;

    // Use this for initialization
    void Start()
    {
        centerWidth = this.gameObject.GetComponent<Renderer>().bounds.size.x;
        centerLength = this.gameObject.GetComponent<Renderer>().bounds.size.z;
    }
}
