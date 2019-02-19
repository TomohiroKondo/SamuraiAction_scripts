using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EnemyInsController : MonoBehaviour {

    private EnemyInstantiate enemyInstantiate;
    [SerializeField] private List<SelectInsObj> selectInsObjsList = new List<SelectInsObj>();
    //[SerializeField] private int MaxEnemy;

	void Start () {
        var selectInsObj = this.GetComponent<SelectInsObj>();
        selectInsObjsList.Add(selectInsObj);
        enemyInstantiate = this.GetComponent<EnemyInstantiate>();

        foreach(SelectInsObj selectIns in selectInsObjsList)
        {
            selectInsObj.insObj
                        .Where(x => x == SelectInsObj.InsEnemyObj.inObj)
                        .Subscribe(_ => 
            {
                enemyInstantiate.InInsPos();
            }
                                  );
            selectInsObj.insObj
                        .Where(x => x == SelectInsObj.InsEnemyObj.centerObj)
                        .Subscribe(_ => enemyInstantiate.CenterInsPos());

            selectInsObj.insObj
                        .Where(x => x == SelectInsObj.InsEnemyObj.outObj)
                        .Subscribe(_ => enemyInstantiate.OutInsPos());
        }
	}
}
