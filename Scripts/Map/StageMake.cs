using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.AI;

//道をInstanceteするクラス
public class StageMake : MonoBehaviour
{
	//全体の最大生成数
	public int maxRail = 8;
	//同時の最大生成数
	public int stMax = 3;
	//疑似セルの横幅と縦幅のコマ数
	public int lengthSize = 2;
	public int widthSize = 2;
	//疑似セルの二次元配列
	public Vector3[,] cellPoint = new Vector3[3, 3];
	//各クラスのレール情報の定義
	public List<Vector3> mapData;
	public GameObject[] stageRail;
	public GameObject[] objs;

    private bool flag = false;
	private Designer designer;
	private RoadMaker roadMaker;

	//	public int i = 1;

	void Awake ()
	{
		MakeCell ();
	}

	void Start ()
	{
		designer = this.GetComponent<Designer>();
		roadMaker = this.GetComponent<RoadMaker>();
		objs = new GameObject[maxRail];

		//this.UpdateAsObservable()
            //.Where(_ => flag == false)
            //.Subscribe(_ =>
            //{
            //    InsRail();
            //    flag = true;
            //});
	}
    
	//疑似セルを作るメゾット
	public void MakeCell ()
	{
		int x = 30;
		int z = 30;
		for (int i = 0; i <= lengthSize; i++) {
			for (int j = 0; j <= widthSize; j++) {
				cellPoint [i, j] = new Vector3 (i * x, 0, j * z);
			}
		}
	}

	//自身のいる道の一つ前を消すメソッド
	public void RoadDestroy (int j)
	{
		Destroy (objs [j]);
	}

	//2つ先の道を作る
	public void RoadInstantite (int i, GameObject[] stageRail, List<Vector3> mapData)
	{
		objs [i] = (GameObject)Instantiate (stageRail [i], mapData [i], Quaternion.identity);
	}

	//スタートの生成
	public void InsRail ()
	{
        if (flag == false)
        {
            //変数の初期化
            mapData = designer.mapData;
            stageRail = roadMaker.stageRail;

            for (int i = 0; i <= 2; i++)
            {
                objs[i] = (GameObject)Instantiate(stageRail[i], mapData[i], Quaternion.identity);
            }
            flag = true;
        }
	}
}