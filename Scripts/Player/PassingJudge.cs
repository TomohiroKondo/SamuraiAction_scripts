using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PassingJudge : MonoBehaviour
{

	public int ColliderCount;
	public ReactiveProperty<Vector3> PlayerNextVector = new ReactiveProperty<Vector3>(new Vector3 (0, 0, 0));
	public bool playerJudge = false;
    public int maxRail;
	private PlayerVector playerVector = new PlayerVector();
	private GameObject enemyController;
	private EnemyController enemyClass;
	private Designer designer;
    private StageMake stageMake;


    private void Start()
	{
		var stageController = GameObject.Find("StageController");
        designer = stageController.GetComponent<Designer>();
        stageMake = stageController.GetComponent<StageMake>();
        maxRail = stageMake.maxRail;
        enemyController = GameObject.Find("EnemyController");
		enemyClass = enemyController.GetComponent<EnemyController>();

		ColliderCount = 1;
	}

	public int ColliderOn()
	{
        Vector3 NextVector = 
            playerVector.GetNextVector(maxRail, ColliderCount, designer.mapData);
		PlayerNextVector.Value = NextVector;
		ColliderCount++;
		return ColliderCount;
	}
}