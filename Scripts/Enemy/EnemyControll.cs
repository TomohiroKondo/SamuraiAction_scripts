using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UniRx;
using UniRx.Triggers;

public class EnemyControll : MonoBehaviour {

	private Animator anim;
	private GameObject player;
	private EnemyState enemyState;
    private NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animator>();
        player = GameObject.Find("Player_Pack(Clone)");
        enemyState = this.GetComponent<EnemyState>();
        agent = this.GetComponent<NavMeshAgent>();

        this.UpdateAsObservable()
            .Where(_ => agent.enabled == true)
            .Subscribe(_ =>
            {
                var targetPos = player.transform.position;
                EnemyLook();
                var targetDistance = enemyState.DistanceCheck(targetPos);
                enemyState.MoveCheck(targetDistance);
                enemyState.AttackCheck(targetDistance);
            }
                      );
    }

	private void OnTriggerEnter(Collider other)
	{
		var lifeCollider = this.GetComponent<CapsuleCollider>();
		if(other.name == "Cylinder001")
		{
			lifeCollider.enabled = false;
            enemyState.checkState.OnNext(EnemyState.SetState.deth);
		}
	}

	private void EnemyLook()
	{
		this.transform.LookAt(player.transform.position);
	}
}
