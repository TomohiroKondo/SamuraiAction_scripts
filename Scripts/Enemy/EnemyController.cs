using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EnemyController : MonoBehaviour {

    private StartComplete startComplete;
    private GameObject stageController;
	private Designer designer;
	private StageMake stageMake;
	private RoadController roadController;
    private GameObject[] stageRail;
	private List<Vector3> mapData;
	[SerializeField] private GameObject enemy;

	void Start () 
	{
        //TODO あとでやる
        stageController = GameObject.Find("StageController");
        startComplete = stageController.GetComponent<StartComplete>();
        startComplete.startSubject
                     .Where(x => x == 4)
                     .Subscribe(_ => StartCoroutine(GetInstance()));
    }

    IEnumerator GetInstance()
    {
        designer = stageController.GetComponent<Designer>();
        stageMake = stageController.GetComponent<StageMake>();
        mapData = designer.mapData;
        stageRail = stageMake.stageRail;
        roadController = stageController.GetComponent<RoadController>();

        startComplete.startSubject.OnNext(5);

        yield return null;
    }
}
