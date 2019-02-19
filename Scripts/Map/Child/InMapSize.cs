using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 多分使わない
/// </summary>
public class InMapSize : MonoBehaviour {

    public float inWidth;
    public float inLength;

    // Use this for initialization
    void Start () {
        inWidth = this.gameObject.GetComponent<Renderer>().bounds.size.x;
        inLength = this.gameObject.GetComponent<Renderer>().bounds.size.z;
	}
}
