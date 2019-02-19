using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartAnime : MonoBehaviour {

    private Animator anim;
	// Use this for initialization
	void Start () {
        anim = this.GetComponent<Animator>();
        anim.SetTrigger("Running");
	}
}
