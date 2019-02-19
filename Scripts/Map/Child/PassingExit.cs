using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UniRx;
//using UniRx.Triggers;

public class PassingExit : MonoBehaviour {

    /// <summary>
    /// 前Ver.
    /// </summary>
	private GameObject stageController;
	private RoadController roadController;
    private GameObject obj;
    private PassingJudge passingJudge;
    private PlayerRunning playerRunning;


    private void Start()
    {
        // TODO: あとで直す
		stageController = GameObject.Find("StageController");
		roadController = stageController.GetComponent<RoadController>();
        obj = GameObject.Find("Player_Pack(Clone)");
        passingJudge = obj.GetComponent<PassingJudge>();
        playerRunning = obj.GetComponent<PlayerRunning>();
    }

	void OnTriggerExit(Collider other)
    {
        //this.GetComponent<BoxCollider>().enabled = false;
        Debug.Log(other.name);
        if (other.gameObject.tag == "Player_Pack")
        {
            Debug.Log("Hoge");
            TrigerScript();
        }
    }

    void TrigerScript()
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
            ///<summary>
            /// TODO
            /// Subject管理するのでSubjectで実装
            /// 前Verなので修正の必要なし
            /// </summary>

            //playerRunning.checkRun.Value = playerRunning.CheckRunning(obj);
        }
    }
}
