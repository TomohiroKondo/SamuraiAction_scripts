using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UniRx;

//レールを置くルートを決めるクラス
public class Designer : MonoBehaviour
{
	public bool jage = false;
	//基準座標を入れる変数
	public Vector3 railPos;
	//allmaxの取得
	public int maxRail;
	//cellPointの取得
	public Vector3[,] cellPoint;
	//MAPデータの入るList
	public List<Vector3> mapData = new List<Vector3> ();
	//cellPointのインデックス検索用配列
	public Vector3[] cellindex;
	//cellPointのIndex番号を取得するためのリスト
	public List<int> xIndex = new List<int> ();
	public List<int> zIndex = new List<int> ();
    //基準座標用の変数
	//mapDataの最新の一つ前の座標
    private Vector3 param = new Vector3();
    private Vector3 nowPos = new Vector3();
	private StageMake stageMake;
    

	//void Awake ()
	//{
	//	//スタート位置を生成
	//	railPos = cellPoint [UnityEngine.Random.Range (cellPoint.GetLength (0) - 1, 0), UnityEngine.Random.Range (0, cellPoint.GetLength (1) - 1)];
	//	mapData.Add (railPos);
	//	param = mapData [mapData.Count - 1];

	//	//cellPointのIndexを取得するためのリストに値を入れる
	//	for (int i = 0; i <= cellPoint.GetLength (0) - 1; i++) {
	//		for (int j = 0; j <= cellPoint.GetLength (1) - 1; j++) {
	//			xIndex.Add (i);
	//			zIndex.Add (j);
	//		}
	//	}

	//	//mapDataにデータを格納
	//	for (int i = 1; i <= maxRail - 1; i++) {
	//		railPos = randomMaker (zIndex, xIndex);
	//		mapData.Add (railPos);
	//		param = mapData [mapData.Count - 2];
	//	}
	//}

	private void Start()
	{
		stageMake = this.GetComponent<StageMake>();
		maxRail = stageMake.maxRail;
		cellPoint = stageMake.cellPoint;
		cellindex = new Vector3[cellPoint.Length];
	}

	//Awake()で走っていたスクリプト
	public void FirstMethod()
	{
		//スタート位置を生成
        railPos = cellPoint[UnityEngine.Random.Range(cellPoint.GetLength(0) - 1, 0), UnityEngine.Random.Range(0, cellPoint.GetLength(1) - 1)];
        mapData.Add(railPos);
        param = mapData[mapData.Count - 1];

        //cellPointのIndexを取得するためのリストに値を入れる
        for (int i = 0; i <= cellPoint.GetLength(0) - 1; i++)
        {
            for (int j = 0; j <= cellPoint.GetLength(1) - 1; j++)
            {
                xIndex.Add(i);
                zIndex.Add(j);
            }
        }

        //mapDataにデータを格納
        for (int i = 1; i <= maxRail - 1; i++)
        {
            railPos = RandomMaker(zIndex, xIndex);
            mapData.Add(railPos);
            param = mapData[mapData.Count - 2];
        }
	}

	//ランダムに座標を指定するメゾット
	public Vector3 RandomMaker (List<int>zIndex, List<int> xIndex)
	{
		int x = 0;
		int railPos_x = 0;
		int railPos_z = 0;
			
		//ランダムに隣接する座標を選択
		foreach (Vector3 vec in cellPoint) {
			x++;
			if (railPos == vec) {
				railPos_x = xIndex [x - 1];
				railPos_z = zIndex [x - 1];
			}
		}
		while (!jage) {
			jage = Random (railPos_x, railPos_z);
		}
		jage = false;
		return nowPos;
	}

	public bool Random (int railPos_x, int railPos_z)
	{
		switch (UnityEngine.Random.Range (0, 4)) {

		//cellPoint(i + 1, j)のセルを指定
		case 0:
			try {
				nowPos = cellPoint [railPos_x + 1, railPos_z];
				if (nowPos != param) {
					jage = true;
					break;
				}
			} catch {
				jage = false;
				break;
			} 
			jage = false;
			break;

		//cellPoint(i, j + 1)のセルを指定
		case 1:
			try {
				nowPos = cellPoint [railPos_x, railPos_z + 1];
				if (nowPos != param) {
					jage = true;
					break;
				}
			} catch {
				jage = false;
				break;
			} 
			jage = false;
			break;

		//cellPoint(i - 1, j)のセルを指定
		case 2:
			try {
				nowPos = cellPoint [railPos_x - 1, railPos_z];
				if (nowPos != param) {
					jage = true;
					break;
				}
			} catch {
				jage = false;
				break;
			} 
			jage = false;
			break;

		//cellPoint(i, j - 1)のセルを指定
		case 3:
			try {
				nowPos = cellPoint [railPos_x, railPos_z - 1];
				if (nowPos != param) {
					jage = true;
					break;
				}
			} catch {
				jage = false;
				break;
			} 
			jage = false;
			break;
		default:
			jage = false;
			break;
		}
		return jage;
	}
}