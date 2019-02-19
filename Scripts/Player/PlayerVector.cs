using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVector : MonoBehaviour {


	//private List<Vector3> mapData;
	private Vector3 BeforeVector, NextVector;
	private Designer designer;

	private void Start()
	{
		var stageController = GameObject.Find("StageController");
        designer = stageController.GetComponent<Designer>();
	}

	//コライダー通過時の次のプレイヤーの向き
	public Vector3 GetNextVector(int maxRail, int i, List<Vector3> mapData)
	{
		Vector3 NowPos, NextPos, BeforePos;

        if (i < maxRail - 1)
		{
			NowPos = mapData[i];
			NextPos = mapData[i + 1];
			BeforePos = mapData[i - 1];
			NextVector = NextPos - NowPos;
			BeforeVector = NowPos - BeforePos;

			if (NextPos == NowPos)
			{
				NextVector = new Vector3(0, 0, 0);
			}
		}

        return NextVector;
	}

    //最初のプレイヤーの向き
	public Vector3 FirstVector(List<Vector3> mapData)
	{
		Vector3 FirstPos, NextPos, FirstVec;
		NextPos = mapData[1];
		FirstPos = mapData[0];

		FirstVec = NextPos - FirstPos;

		return FirstVec;
	}

    //プレイ中のプレイヤーの向き
	public Vector3 PlayerVectorNow()
	{
		var player = GameObject.Find("Player_Pack(Clone)");
		var mainAim = GameObject.Find("Player_Pack(Clone)/AimRotateOrigin/MainAim");
		var playerPos = player.transform.position;
		var mainAimPos = mainAim.transform.position;
		Vector3 targetVector = playerPos - mainAimPos;
		return targetVector;
	}
}
