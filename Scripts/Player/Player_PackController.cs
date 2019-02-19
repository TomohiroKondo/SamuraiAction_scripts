using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;

public class Player_PackController : MonoBehaviour
{
    public int playerLife = 3;
	private Vector3 Next;
	private GameObject obj, player, mainCamera, aim;
	private VectorChange vectorChange = new VectorChange();
	private Vector3 zero = new Vector3(0, 0, 0);
	private StageMake stageMake;
	private StartComplete startComplete;
    private ButtonStatus button;
	private PlayerRunning playerRunning;
    private PassingJudge passingJudge;
    private PlayerStatus playerStatus;
  

	public enum NextPlayerVector
	{
		x_Positive,
		x_Negative,
		z_Positive,
		z_Negative,
        Equal
	}

	// Use this for initialization
	void Start ()
	{
		obj = GameObject.Find("Player_Pack(Clone)");
		passingJudge = gameObject.GetComponent<PassingJudge>();
		GameObject playerController = GameObject.Find("PlayerController");
		var playerVector = playerController.GetComponent<PlayerVector>();
		playerRunning = obj.GetComponent<PlayerRunning>();

		player = GameObject.Find("Player_Pack(Clone)/Player");

		var stageController = GameObject.Find("StageController");
		stageMake = stageController.GetComponent<StageMake>();
		startComplete = stageController.GetComponent<StartComplete>();
        button = stageController.GetComponent<ButtonStatus>();
        playerStatus = this.GetComponent<PlayerStatus>();

		passingJudge.PlayerNextVector
		            .SkipLatestValueOnSubscribe()
		            .Delay(TimeSpan.FromSeconds(0.2f))
                    .Subscribe(x => StartCoroutine(CheckNextVector()));

        //this.ObserveEveryValueChanged(_ => passingJudge.ColliderCount)
            //.Where(x => x == stageMake.maxRail - 3)
            //.Subscribe(_ => 
                //{
                //playerStatus.playerStateSubject
                //                            .OnNext(PlayerStatus.PlayerState.Idle);
                //playerStatus.playerStateSubject.OnCompleted();
                //}
                                            //);
	}

	public NextPlayerVector npvValueSelect(Vector3 Next)
	{
		NextPlayerVector npvValue;
		if (Next[2] > zero[2])
		{
			npvValue = NextPlayerVector.x_Positive;
			return npvValue;
		}else if(Next[2] < zero[2]) {
			npvValue = NextPlayerVector.x_Negative;
			return npvValue;
		}else if(Next[0] > zero[0]){
			npvValue = NextPlayerVector.z_Positive;
			return npvValue;
		}else if(Next[0] < zero[0]){
			npvValue = NextPlayerVector.z_Negative;
			return npvValue;
		}else{
			npvValue = NextPlayerVector.Equal;
			return npvValue;
		}
	}

    IEnumerator CheckNextVector()
    {
        Next = passingJudge.PlayerNextVector.Value;
        int CollierCount = passingJudge.ColliderCount;
        var npvValue = npvValueSelect(Next);
        vectorChange.ChangeVector(npvValue, obj);

        yield return null;
    }

    IEnumerator CheckIdle()
    {
        int i = passingJudge.ColliderCount;
        if (i == stageMake.maxRail)
        {
            playerStatus.playerStateSubject.OnNext(PlayerStatus.PlayerState.Idle);
        }

        yield return null;
    }
}
