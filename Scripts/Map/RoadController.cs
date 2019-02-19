using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class RoadController : MonoBehaviour
{
	//初期生成数
	public int FirstNumber = 3;
	//各変数の取得
    public GameObject[] stageRail;
	public List<Vector3> mapData;
    public int maxRail;
	public Subject<int> startSubject;
	private Designer designer;
	private StageMake stageMake;
	private RoadMaker roadMaker;
	private Vector3[,] cellPoint;
	private PlayerController playerControll;
	private StartComplete startComplete;

	private void Start()
	{
		var playerController = GameObject.Find("PlayerController");
		playerControll = playerController.GetComponent<PlayerController>();
		designer = this.GetComponent<Designer>();
		stageMake = this.GetComponent<StageMake>();
		roadMaker = this.GetComponent<RoadMaker>();
		startComplete = this.GetComponent<StartComplete>();
		cellPoint = stageMake.cellPoint;
		mapData = designer.mapData;
		maxRail = stageMake.maxRail;

		Observable.FromCoroutine(FirstCoroutine)
				  .SelectMany(SecondCoroutine)
				  .SelectMany(ThirdCoroutine)
		          .Subscribe(_ => StartCoroutine(ThirdCoroutine()));
	}

	public int InstantiateAndDestroy ()
	{
		int j = FirstNumber - 3;
		stageRail = roadMaker.stageRail;

        if (FirstNumber <= maxRail - 1) {
			stageMake.RoadDestroy (j);
			stageMake.RoadInstantite (FirstNumber, stageRail, mapData);
		}
        FirstNumber++;
		return FirstNumber;
	}

	IEnumerator FirstCoroutine()
	{
		designer.FirstMethod();

		yield return null;
	}

	IEnumerator SecondCoroutine()
	{
		roadMaker.RoadAdd();

		yield return null;
	}

	IEnumerator ThirdCoroutine()
	{
		stageMake.InsRail();

		startComplete.startSubject.OnNext(1);

		yield return null;
	}
}