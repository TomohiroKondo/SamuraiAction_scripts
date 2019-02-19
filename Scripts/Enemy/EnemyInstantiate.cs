using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EnemyInstantiate : MonoBehaviour {

    public enum EnemyDis
    {
        close,
        distant
    };
    public Subject<EnemyDis> checkDistance = new Subject<EnemyDis>();

    [SerializeField] private MapSize mapSize;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject inObject;
    [SerializeField] private GameObject centerObject;
    [SerializeField] private GameObject outObject;

	void Start () {
		
	}

    public void InInsPos()
    {
        var inX = mapSize.inX;
        var inZ = mapSize.inZ;
        var enemyPos = inObject.transform.position + 
                               new Vector3(Random.Range(-inX / 2, inX / 2),
                                           0,
                                           Random.Range(-inZ / 2, inZ / 2));

        Instantiate(enemy, enemyPos, Quaternion.identity);
    }

    public void CenterInsPos()
    {
        var centerX = mapSize.centerX;
        var centerZ = mapSize.centerZ;
        var enemyPos = inObject.transform.position +
                               new Vector3(Random.Range(-centerX / 2, centerX / 2),
                                           0,
                                           Random.Range(-centerZ / 2, centerZ / 2));

        Instantiate(enemy, enemyPos, Quaternion.identity);
    }

    public void OutInsPos()
    {
        var outX = mapSize.outX;
        var outZ = mapSize.outZ;
        var enemyPos = inObject.transform.position +
                               new Vector3(Random.Range(-outX / 2, outX / 2),
                                           0,
                                           Random.Range(-outZ / 2, outZ / 2));

        Instantiate(enemy, enemyPos, Quaternion.identity);
    }
}
