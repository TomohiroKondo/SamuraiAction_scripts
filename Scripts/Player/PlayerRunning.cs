using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UniRx;
using System;
using DG.Tweening;

public class PlayerRunning : MonoBehaviour {

    //public ReactiveProperty<bool> checkRun = new ReactiveProperty<bool>();
    //public Subject<bool> checkRun = new Subject<bool>();
    //private bool checkRun = false;
	private float speed = 0.15f;
	private GameObject obj, stageController;
	private StageMake stageMake;
	private int maxRail;
    //private PassingJudge passingJudge;

	// Use this for initialization
	void Start () {
		obj = GameObject.Find("Player_Pack(Clone)");
		stageController = GameObject.Find("StageController");
		stageMake = stageController.GetComponent<StageMake>();
		maxRail = stageMake.maxRail;
        //passingJudge = obj.GetComponent<PassingJudge>();
	}

    /// <summary>
    /// 旧スクリプト
    /// </summary>
    public void PlayerMove(GameObject obj, GameObject player)
    {
    	var playerForward = player.transform.forward;
    	obj.transform.Translate(playerForward.x * speed, 0,
    	                        playerForward.z * speed,
    	                        Space.World);
    }

    //public bool CheckRunning(GameObject obj)
    //{
    	//var passingJudge = obj.GetComponent<PassingJudge>();
    	//int i = passingJudge.ColliderCount;

    	//bool checkPoint;

    	//if(i < maxRail)
    	//{
    	//	checkPoint = false;
    	//}else{
    	//	checkPoint = true;
    	//}

    	//return checkPoint;
    //}

    //public void IdlePlayer(GameObject player)
    //{      
    //	var animator = player.GetComponent<Animator>();
    //	animator.SetBool("Idle_start", true);
    //}

    //public void RunningPlayer()
    //{
    //	GameObject player = GameObject.Find("Player_Pack(Clone)/Player");

    //       var animator = player.GetComponent<Animator>();
    //	animator.SetBool("Idle_start", false);
    //}
}
