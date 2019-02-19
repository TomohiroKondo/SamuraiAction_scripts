using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityForge.AnimCallbacks;
using UnityEngine.AI;
using System;

public class PlayerAnime : MonoBehaviour {

    private GameObject stageController;
    private Animator anim;
    private Animation Animation;
    private GameObject parent;
    private BoxCollider weaponCollider;
    private GameObject player;
    private GameObject obj;
    private PlayerRunning playerRunning;
    private PlayerStatus playerStatus;
    private CapsuleCollider playerCollider;
    private Sequence Seq;
    private NavMeshAgent agent;
    [SerializeField] private GameObject target;

    //Debug
    private List<Vector3> mapData;

    // Use this for initialization
    void Start () {
        stageController = GameObject.Find("StageController");
        anim = GetComponent<Animator>();
        weaponCollider = this.GetComponentInChildren<BoxCollider>();
        player = this.gameObject;
        obj = transform.root.gameObject;
        playerStatus = this.GetComponent<PlayerStatus>();
        weaponCollider.enabled = false;
        playerCollider = player.GetComponent<CapsuleCollider>();
        agent = obj.GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        agent.updateRotation = false;

        //debug
        mapData = stageController.GetComponent<Designer>().mapData;

        //現行のスクリプト
        this.UpdateAsObservable()
        .Select(_ => playerStatus.runningFlag)
        .Where(x => x == true)
        .Subscribe(_ => AgentUpdata());

        this.UpdateAsObservable()
        .Select(_ => playerStatus.runningFlag)
        .Where(x => x == true)
        .Subscribe(_ => Running());
    }

    public void AgentUpdata()
    {
        var targetPos = target.transform.position;
        agent.SetDestination(targetPos);
    }

    public void Running()
    {
        obj.transform.position = agent.nextPosition;
    }

    public void Attack()
    {
        var playerLocalPos = player.transform.localPosition;
        var stepPos = playerLocalPos + new Vector3(0.1f, 0, 0f);
        var playerWorldPos = transform.TransformPoint(stepPos);
        parent = obj;
        
        weaponCollider.enabled = true;
        anim.SetTrigger("Attack");
        var num = playerWorldPos[2];
        Seq = DOTween
            .Sequence()
            .Append(obj.transform.DOMoveZ(num, 1.5f)
           .OnUpdate(() =>
           {
               playerStatus.playerStateSubject
               .Where(x => x == PlayerStatus.PlayerState.Roll)
               .Subscribe(_ =>
               {
                   Seq.Complete();
                   anim.SetTrigger("Cancel");
                   weaponCollider.enabled = false;
               });

               playerStatus.playerStateSubject
               .Where(x => x == PlayerStatus.PlayerState.Down)
               .Subscribe(_ =>
               {
                   Seq.Complete();
                   anim.SetTrigger("DownCancel");
                   weaponCollider.enabled = false;
               });

               playerStatus.playerStateSubject
               .Where(x => x == PlayerStatus.PlayerState.Deth)
               .Subscribe(_ =>
               {
                   Seq.Complete();
                   anim.SetTrigger("Deth");
                   weaponCollider.enabled = false;
               });
           }
                    )
           .OnComplete(() =>
        {
            if(playerStatus.cancelFlag == true)
            {
                playerStatus.playerStateSubject
                            .OnNext(PlayerStatus.PlayerState.Runnimg);
            }
            weaponCollider.enabled = false;
        }
                      )
                                       );

        Seq.Play();


    }

    public void Roll()
    {
        ///<summary>
        /// 旧スクリプト
        /// </summary>
        //var playerLocalPos = player.transform.localPosition;
        //var stepPos = playerLocalPos + new Vector3(0, 0, 5f);
        //var stepWorldPos = transform.TransformPoint(stepPos);

        //weaponCollider.enabled = false;
        //playerCollider.enabled = false;
        //anim.SetTrigger("Forwardroll");
        //obj.transform.DOMove(stepWorldPos, 1f)
        //.OnComplete(() => Restert());

        var targetPos = target.transform.position;
        var rollEndPos = new Vector3(0, 0, targetPos.z * 5);
        var agentSpeedOrigin = agent.speed;
        agent.speed = 10.0f;

        weaponCollider.enabled = false;
        playerCollider.enabled = false;
        anim.SetTrigger("Forwardroll");
        agent.updatePosition = true;
        agent.SetDestination(rollEndPos);

        StartCoroutine(MoveEnd(targetPos, agentSpeedOrigin));

    }

    public void Damage()
    {
        playerCollider.enabled = false;
        weaponCollider.enabled = false;
        anim.SetTrigger("Damage");
    }

    public void Deth()
    {
        playerCollider.enabled = false;
        weaponCollider.enabled = false;
        anim.SetTrigger("Deth");
        playerStatus.playerStateSubject.OnCompleted();
    }

    public void Restert()
    {
        playerStatus.cancelFlag = true;
        playerStatus.playerStateSubject
                    .OnNext(PlayerStatus.PlayerState.Runnimg);
        Observable.Timer(TimeSpan.FromSeconds(1))
                  .Subscribe(_ => playerCollider.enabled = true);
    }

    public void IdleStart()
    {
        anim.SetTrigger("Idle_start");
    }

    public void IdleEnd()
    {
        anim.SetTrigger("Idle_end");
        playerStatus.playerStateSubject.OnNext(PlayerStatus.PlayerState.Runnimg);
    }

    IEnumerator MoveEnd(Vector3 target,float speed)
    {
        var playerPos = this.transform.position;
        if(playerPos != target)
        {
            yield return new WaitForEndOfFrame();
        }
        agent.updatePosition = false;
        agent.speed = speed;
        Restert();
        yield return null;
    }
}
