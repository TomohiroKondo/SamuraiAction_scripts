using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class StartComplete : MonoBehaviour {

	public ReactiveProperty<bool> completed = new ReactiveProperty<bool>(false);
	public Subject<int> startSubject = new Subject<int>();
	private RoadController roadController;
	private PlayerController playerController;
    private ButtonStatus button;
    private PlayerStatus playerStatus;


	void Start () {
		var playerControll = GameObject.Find("PlayerController");
		//var stageController = GameObject.Find("StageController");
		playerController = playerControll.GetComponent<PlayerController>();
		roadController = this.GetComponent<RoadController>();
        button = this.GetComponent<ButtonStatus>();

		startSubject.Where(x => x == 5)
					.Subscribe(_ => StartCoroutine(StartCompleted()));
	}
	
	IEnumerator StartCompleted()
	{
        var player = GameObject.Find("Player");
        playerStatus = player.GetComponent<PlayerStatus>();
        completed.Value = true;
        //playerStatus.playerStateSubject.OnNext(PlayerStatus.PlayerState.Runnimg);
        StartCoroutine("GameStart");
        startSubject.OnCompleted();

		yield return null;
	}

    IEnumerator GameStart()
    {
        var player = GameObject.Find("Player");
        playerStatus = player.GetComponent<PlayerStatus>();
        var anim = player.GetComponent<Animator>();

        playerStatus.playerStateSubject.OnNext(PlayerStatus.PlayerState.Idle);

        for(int i = 0; i <= 10; i ++)
        {
            yield return null;
        }
        anim.SetTrigger("Idle_end");
        playerStatus.playerStateSubject.OnNext(PlayerStatus.PlayerState.Runnimg);
        yield return null;
    }
}
