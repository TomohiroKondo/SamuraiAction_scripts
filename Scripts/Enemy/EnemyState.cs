using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class EnemyState : MonoBehaviour
{

    public enum SetState
    {
        idle,
        chase,
        attack,
        attackAftar,
        deth
    }

    public Subject<SetState> checkState = new Subject<SetState>();
    private NavMeshAgent agent;
    private float chaceDistance = 20.0f;
    private float attackDistance = 3f;
    private float targetDistance;

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        checkState.OnNext(SetState.idle);
    }

    public void MoveCheck(float targetDistance)
    {
        if (targetDistance < chaceDistance)
    	{
            checkState.OnNext(SetState.chase);
    	}else{
            checkState.OnNext(SetState.idle);
        }
    }

    public void AttackCheck(float targetDistance)
    {
        if(targetDistance < attackDistance)
    	{
            checkState.OnNext(SetState.attack);
    	}
    }

    public float DistanceCheck(Vector3 targetPos)
    {
        targetDistance = Vector3.Distance(this.agent.transform.position, targetPos);
        return targetDistance;
    }
}
