using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StetusCallback : StateMachineBehaviour {

    private GameObject obj;
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if(stateInfo.IsName("Getup"))
        { 
        obj = GameObject.Find("Player");
        var playerAnim = obj.GetComponent<PlayerAnime>();
        playerAnim.Restert();
        }
    }
}