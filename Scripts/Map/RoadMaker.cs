using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//決められたセルに対応するレールするクラス
public class RoadMaker : MonoBehaviour
{
	//レールのゲームオブジェクト
	public GameObject lengthStright;
	public GameObject upLeft;
	public GameObject upRight;
	public GameObject downLeft;
	public GameObject downRight;
	public GameObject widthStright;

	//mapDataの取得
	public List<Vector3> mapData;   
	//最大正整数を取得
	public int maxRail;
	public GameObject[] stageRail;
	//cellPointの取得
	public Vector3[,] cellPoint;   
	//cellPointインデックス番号検索用リストの取得
	public List<int> xIndex;
	public List<int> zIndex;
	//分岐条件を列挙
	public enum beforeState
	{
		xPositive,
		xNegative,
		zPositive,
		zNegative
	}

	public enum nextState
	{
		xPositive,
		xNegative,
		zPositive,
		zNegative
	}

	public beforeState BeforeState;
	public nextState NextState;

	private StageMake stageMake;
	private Designer designer;

	void Start ()
	{
        designer = this.GetComponent<Designer>();
		stageMake = this.GetComponent<StageMake>();
		mapData = designer.mapData;
		maxRail = stageMake.maxRail;
		cellPoint = stageMake.cellPoint;
		xIndex = designer.xIndex;
		zIndex = designer.zIndex;


		//RoadAdd();
	}

	//最大生成数がMAX以下のときオブジェクトを生成するメゾット
	public void RoadAdd()
	{
		//mapData内の基準点とその前後
		Vector3 before, next, now;
		GameObject rail;
		//各インデックス番号の入る配列
		int[] nextIndex = new int[2];
		int[] beforeIndex = new int[2];
		int[] nowIndex = new int[2];
		//c[0]にnext,beforeのX軸の和を、c[1]にZ軸の和を入れる配列
		int[] beforeconparison = new int[2];
		int[] nextconparison = new int[2];
		stageRail = new GameObject[maxRail];

		//始点の生成
		now = mapData [0];
		next = mapData [1];
		nowIndex = NowIndex (now);
		nextIndex = NextIndex (next);
		nextconparison [0] = nextIndex [0] - nowIndex [0];
		nextconparison [1] = nextIndex [1] - nowIndex [1];
		BeforeState = CheckBeforeState (beforeconparison);
		NextState = CheckNextState (nextconparison);
		rail = RailMake (BeforeState, NextState);
		stageRail [0] = rail;

		//mapData[1]から終点の一つ前までを生成
		for (int i = 1; i < mapData.Count - 1; i++) {
			before = mapData [i - 1];
			next = mapData [i + 1];
			now = mapData [i];
			beforeIndex = BeforeIndex (before);
			nextIndex = NextIndex (next);
			nowIndex = NowIndex (now);
			//beforeconparisionにx軸z軸の差を代入
			beforeconparison [0] = nowIndex [0] - beforeIndex [0];
			beforeconparison [1] = nowIndex [1] - beforeIndex [1];
			//nextconparisionにx軸z軸の差を代入
			nextconparison [0] = nextIndex [0] - nowIndex [0];
			nextconparison [1] = nextIndex [1] - nowIndex [1];
			BeforeState = CheckBeforeState (beforeconparison);
			NextState = CheckNextState (nextconparison);
			rail = RailMake (BeforeState, NextState);
			stageRail [i] = rail;
		}

		//終点の生成
		now = mapData [mapData.Count - 1];
		before = mapData [mapData.Count - 2];
		nowIndex = NowIndex (now);
		beforeIndex = BeforeIndex (before);
		beforeconparison [0] = nowIndex [0] - beforeIndex [0];
		beforeconparison [1] = nowIndex [1] - beforeIndex [1];
		BeforeState = CheckBeforeState (beforeconparison);
		NextState = CheckNextState (nextconparison);
		rail = RailMake (BeforeState, NextState);
		stageRail [maxRail - 1] = rail;
	}

	//mapDataの現在のインデックスを取得
	public int[] NowIndex (Vector3 now)
	{
		int x = 0;
		int now_x = 0, now_z = 0;
		int[] nowIndex = new int[2];
		foreach (Vector3 vec in cellPoint) {
			x++;
			if (now == vec) {
				now_x = xIndex [x - 1];
				now_z = zIndex [x - 1];
				nowIndex [0] = now_x;
				nowIndex [1] = now_z;
				return nowIndex;
			}
		}
		return nowIndex;
	}

	//mapData[i] + 1のインデックスを取得
	public int[] NextIndex (Vector3 next)
	{
		int x = 0;
		int next_x = 0, next_z = 0;
		int[] nextIndex = new int[2];
		foreach (Vector3 vec in cellPoint) {
			x++;
			if (next == vec) {
				next_x = xIndex [x - 1];
				next_z = zIndex [x - 1];
				nextIndex [0] = next_x;
				nextIndex [1] = next_z;
				return nextIndex;
			}
		}
		return nextIndex;
	}

	//mapData[i] - 1のインデックスを取得
	public int[] BeforeIndex (Vector3 before)
	{
		int x = 0;
		int before_x = 0, before_z = 0;
		int[] beforeIndex = new int[2];
	
		foreach (Vector3 vec in cellPoint) {
			x++;
			if (before == vec) {
				before_x = xIndex [x - 1];
				before_z = zIndex [x - 1];
				beforeIndex [0] = before_x;
				beforeIndex [1] = before_z;
				return beforeIndex;
			}
		}
		return beforeIndex;
	}

	//X軸の分岐条件を判定
	public beforeState CheckBeforeState (int[] beforeconparison)
	{
		int cx = beforeconparison [0];
		int cz = beforeconparison [1];
		beforeState b;

		if (cx == 0 && cz == 1) {
			b = beforeState.zPositive;
			return b;
		} else if (cx == 0 && cz == -1) {
			b = beforeState.zNegative;
			return b;
		} else if (cx == 1 && cz == 0) {
			b = beforeState.xPositive;
			return b;
		} else if (cx == -1 && cz == 0) {
			b = beforeState.xNegative;
			return b;
		} else {
			b = GenerateStart ();
			return b;
		}
	}

	public beforeState GenerateStart ()
	{
		beforeState b = beforeState.xNegative;
		int x = Random.Range (0, 4);
		switch (x) {
		case 0:
			b = beforeState.xPositive;
			break;
		case 1:
			b = beforeState.xNegative;
			break;
		case 2:
			b = beforeState.zPositive;
			break;
		case 3:
			b = beforeState.zNegative;
			break;
		}
		return b;
	}

	//Z軸の分岐条件を判定
	public nextState CheckNextState (int[] nextconparison)
	{
		int cx = nextconparison [0];
		int cz = nextconparison [1];
		nextState n;

		if (cx == 0 && cz == 1) {
			n = nextState.zPositive;
			return n;
		} else if (cx == 0 && cz == -1) {
			n = nextState.zNegative;
			return n;
		} else if (cx == 1 && cz == 0) {
			n = nextState.xPositive;
			return n;
		} else if (cx == -1 && cz == 0) {
			n = nextState.xNegative;
			return n;
		} else {
			n = GenerateFinal ();
			return n;
		}
	}

	public nextState GenerateFinal ()
	{
		nextState n = nextState.xNegative;
		int x = Random.Range (0, 4);
		switch (x) {
		case 0:
			n = nextState.xPositive;
			break;
		case 1:
			n = nextState.xNegative;
			break;
		case 2:
			n = nextState.zPositive;
			break;
		case 3:
			n = nextState.zNegative;
			break;
		}
		return n;
	}

	public GameObject RailMake (beforeState BeforeState, nextState NextState)
	{
		GameObject obj = widthStright;

		switch (BeforeState) {
		case beforeState.xPositive:
			switch (NextState) {
			case nextState.xPositive:
				obj = widthStright;
				break;
			case nextState.xNegative:
				obj = widthStright;
				break;
			case nextState.zPositive:
				obj = upRight;
				break;
			case nextState.zNegative:
				obj = downRight;
				break;
			}
			break;

		case beforeState.xNegative:
			switch (NextState) {
			case nextState.xPositive:
				obj = widthStright;
				break;
			case nextState.xNegative:
				obj = widthStright;
				break;
			case nextState.zPositive:
				obj = upLeft;
				break;
			case nextState.zNegative:
				obj = downLeft;
				break;
			}
			break;

		case beforeState.zPositive:
			switch (NextState) {
			case nextState.zPositive:
				obj = lengthStright;
				break;
			case nextState.zNegative:
				obj = lengthStright;
				break;
			case nextState.xPositive:
				obj = downLeft;
				break;
			case nextState.xNegative:
				obj = downRight;
				break;
			}
			break;

		case beforeState.zNegative:
			switch (NextState) {
			case nextState.zPositive:
				obj = lengthStright;
				break;
			case nextState.zNegative:
				obj = lengthStright;
				break;
			case nextState.xPositive:
				obj = upLeft;
				break;
			case nextState.xNegative:
				obj = upRight;
				break;
			}
			break;
		}
		return obj;
	}
}
	

