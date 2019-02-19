using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UniRx;

public class EnemyAnimeController : MonoBehaviour {

    public bool dethFlag = false;
    private EnemyState enemyState;
    private NavMeshAgent agent;
    private EnemyAnime enemyAnime;

    [SerializeField] private List<EnemyState> enemyStatesList;

    void Start () {
        enemyState = this.GetComponent<EnemyState>();
        enemyStatesList.Add(this.GetComponent<EnemyState>());
        agent = this.GetComponent<NavMeshAgent>();
        enemyAnime = this.GetComponent<EnemyAnime>();
        var player = GameObject.Find("Player_Pack(Clone)");


        foreach (EnemyState state in enemyStatesList)
        {
            enemyState.checkState
                      .Where(x => x == EnemyState.SetState.chase)
                      .Subscribe(_ => agent.SetDestination(player.transform.position));

            enemyState.checkState
                      .Where(x => x == EnemyState.SetState.attack)
                      .First()
                      .Subscribe(_ =>
                      {
                          agent.enabled = false;
                          enemyAnime.Attack();
                          enemyState.checkState.OnNext(EnemyState.SetState.attackAftar);
                      }
                                );

            enemyState.checkState
                      .Where(x => x == EnemyState.SetState.deth)
                      .First()
                      .Subscribe(_ =>
                      {
                          EnemyDead();
                          enemyState.checkState.OnCompleted();
                      }
                                );
        }
    }

    private void EnemyDead()
    {
        enemyAnime.Deth();
    }
}
