using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class SelectInsObj : MonoBehaviour {

    public enum InsEnemyObj
    {
        inObj,
        centerObj,
        outObj
    };

    public Subject<InsEnemyObj> insObj = new Subject<InsEnemyObj>();
    //private MapSize mapSize;
    private FirstOrEnd firstOrEnd;
    [SerializeField] private List<FirstOrEnd> lastSpartList = new List<FirstOrEnd>();

    void Start () {
        firstOrEnd = this.GetComponent<FirstOrEnd>();
        lastSpartList.Add(firstOrEnd);

        foreach(FirstOrEnd Half in lastSpartList)
        {
            firstOrEnd.checkSpart
                      .Where(x => x == FirstOrEnd.LastSpart.firstHalf)
                      .Subscribe(_ => FirstForEnemy());

            firstOrEnd.checkSpart
                      .Where(x => x == FirstOrEnd.LastSpart.lastHalf)
                      .Subscribe(_ => LastForEnemy());
        }
	}

    private int FirstHalfEnemy()
    {
        var i = Random.Range(1, 4);
        return i;
    }

    private int LastHalfEnemy()
    {
        var i = Random.Range(3, 5);
        return i;
    }

    private void FirstForEnemy()
    {
        var i = FirstHalfEnemy();
        for (int n = 0; n < i; n++)
        {
            RamdomSelectObj();
        }
    }

    private void LastForEnemy()
    {
        var i = LastHalfEnemy();
        for (int n = 0; n < i; n++)
        {
            RamdomSelectObj();
        }
    }

    private void RamdomSelectObj()
    {
        var i =  Random.Range(0, 3);

        switch(i)
        {
            case 0:
                insObj.OnNext(InsEnemyObj.inObj);
                break;

            case 1:
                insObj.OnNext(InsEnemyObj.centerObj);
                break;

            case 2:
                insObj.OnNext(InsEnemyObj.outObj);
                break;
        }
    }
}
