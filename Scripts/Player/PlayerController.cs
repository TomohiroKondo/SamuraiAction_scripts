using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerController : MonoBehaviour {
 
	public GameObject player;
    public List<Vector3> mapData;
    private GameObject obj;
    private PlayerVector playerVector = new PlayerVector();
    private Vector3 Zero = new Vector3(0, 0, 0);
	private Designer designer;
	//private RoadController roadController;
	private StartComplete startComplete;
    
	private void Start()
	{
		var stageController = GameObject.Find("StageController");
        designer = stageController.GetComponent<Designer>();
		startComplete = stageController.GetComponent<StartComplete>();
        
		startComplete.startSubject
					  .Where(x => x == 1)
		              .Subscribe(_ => StartCoroutine(InsPlayerCoroutine()));

		//var Player = InsPlayer();

	}

	public GameObject InsPlayer(List<Vector3> mapData)
    {
        mapData = designer.mapData;
		Vector3 FirstVec = playerVector.FirstVector(mapData);

        if (FirstVec[0] > Zero[0])
            obj = Instantiate(player, mapData[0], Quaternion.Euler(0, 90, 0));
        else if (FirstVec[0] < Zero[0])
            obj = Instantiate(player, mapData[0], Quaternion.Euler(0, -90, 0));
        else if (FirstVec[2] > Zero[2])
            obj = Instantiate(player, mapData[0], Quaternion.identity);
        else if (FirstVec[2] < Zero[2])
            obj = Instantiate(player, mapData[0], Quaternion.Euler(0, 180, 0));
        return obj;
    }

	IEnumerator InsPlayerCoroutine()
	{
		InsPlayer(mapData);

		startComplete.startSubject.OnNext(2);

		yield return null;
	}
}
