using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


public class PlayerStatus : MonoBehaviour {

    public enum PlayerState
    {
        Default,
        Idle,
        Runnimg,
        Attack,
        Roll,
        Down,
        Deth
    }

    public Subject<PlayerState> playerStateSubject = new Subject<PlayerState>();
    public bool cancelFlag = true;
    public bool runningFlag = true;
    public bool lastFlag = false;
    private PlayerAnime anim;
    private ReturnUI returnUI;
    [SerializeField] private List<PlayerStatus> PlayerStatesList = new List<PlayerStatus>();
    
    void Start () {
        PlayerStatesList.Add(this);
        anim = this.GetComponent<PlayerAnime>();
        returnUI = GameObject.Find("UIController").GetComponent<ReturnUI>();

        this.ObserveEveryValueChanged(x => runningFlag)
            .Subscribe(x => Debug.Log(x));

        this.ObserveEveryValueChanged(x => lastFlag)
            .Where(x => x == true)
            .First()
            .Subscribe(x => {
                playerStateSubject.OnCompleted();
                returnUI.ReturnFlashing();
             });

        foreach (PlayerStatus playerStatue in PlayerStatesList)
        {
            playerStateSubject.Where(x => x == PlayerState.Runnimg)
                              .Subscribe(_ => runningFlag = true);

            playerStateSubject.Where(x => x == PlayerState.Attack)
                              .Subscribe(_ => {
                                  runningFlag = false;
                                  anim.Attack();
                              });

            playerStateSubject.Where(x => x == PlayerState.Roll)
                              .Subscribe(_ => {
                                  cancelFlag = false;
                                  runningFlag = false;
                                  anim.Roll();
                              });

            playerStateSubject.Where(x => x == PlayerState.Down)
                              .ThrottleFirstFrame(10)
                              .Subscribe(_ => {
                                  cancelFlag = false;
                                  runningFlag = false;
                                  anim.Damage();
                              });

            playerStateSubject.Where(x => x == PlayerState.Deth)
                              .Subscribe(_ => {
                                  runningFlag = false;
                                  anim.Deth();
                              });

            playerStateSubject.Where(x => x == PlayerState.Idle)
                              .Subscribe(_ => {
                                      runningFlag = false;
                                      anim.IdleStart();
                              });

        }
	}
}
