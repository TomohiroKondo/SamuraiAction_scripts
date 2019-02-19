using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ColliderManeger : MonoBehaviour {

    private GameObject stageController;
    private RoadController roadController;
    private GameObject obj;
    private PassingJudge passingJudge;
    private PlayerRunning playerRunning;
    private StageMake stageMake;
    private PlayerStatus playerStatus;
    [SerializeField]List<PassingExit_V2> passingExit_V2List;


    // Use this for initialization
    void Start () {
        // TODO: あとで直す
        foreach (PassingExit_V2 passingExit_V2 in passingExit_V2List)
        {
            passingExit_V2.exit
                          .Distinct(x => x == InOrOut.IN)
                          .Distinct(x => x == InOrOut.OUT)
                          .Subscribe(_ => TriggerScript());
        }

        stageController = GameObject.Find("StageController");
        roadController = stageController.GetComponent<RoadController>();
        stageMake = stageController.GetComponent<StageMake>();
        obj = GameObject.Find("Player_Pack(Clone)");
        passingJudge = obj.GetComponent<PassingJudge>();
        playerRunning = obj.GetComponent<PlayerRunning>();
        playerStatus = obj.GetComponentInChildren<PlayerStatus>();
    }

    void TriggerScript()
    {
        if (passingJudge.playerJudge == false)
        {
            passingJudge.playerJudge = true;
        }
        else
        {
            roadController.InstantiateAndDestroy();
            passingJudge.ColliderOn();
            passingJudge.playerJudge = false;
            ChekcRun();
        }
    }

    private void ChekcRun()
    {
        if(passingJudge.ColliderCount == stageMake.maxRail - 1)
        {
            playerStatus.playerStateSubject.OnNext(PlayerStatus.PlayerState.Idle);
            playerStatus.lastFlag = true;
        }
    }
}
