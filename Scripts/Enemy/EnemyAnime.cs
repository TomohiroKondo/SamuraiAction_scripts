using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using UniRx;

public class EnemyAnime : MonoBehaviour {
    
	private Animator anim;
	private GameObject weapon;
	private SphereCollider weaponCollider;
	private Renderer rend;
    private Color color;
    private float fedaTime;
	private Sequence seq;
	private EnemyState enemyState;
    private NavMeshAgent agent;
    private GameObject player;

	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator>();
        //weapon = GameObject.Find("U_Char_1");
        //weaponCollider = weapon.GetComponent<SphereCollider>();
        weaponCollider = this.gameObject.GetComponentInChildren<SphereCollider>();
		weaponCollider.enabled = false;
		seq = DOTween.Sequence();
		enemyState = this.GetComponent<EnemyState>();
        agent = this.GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player_Pack(Clone)");

        rend = this.GetComponent<Renderer>();
	}

	public void RandomAttack()
	{
		var i = Random.Range(0, 2);

        if(i == 0)
		{
			Attack();
		}else{
			ComboAttack();
		}

		//enemyState.state.Value = EnemyState.SetState.attackAftar;
	}

    public void Attack()
	{
        var thisWorldPos = this.transform.position;
        var thisForward = this.transform.forward;
        var moveDistance = new Vector3(3.0f, 0f, 3.0f);
        var forwardDistance = new Vector3(thisForward[0] * moveDistance[0], 0
                                          , thisForward[2] * moveDistance[2]);
        var nextWorldPos = thisWorldPos + forwardDistance;

        if(weaponCollider != null)
        {
            weaponCollider.enabled = true;
        }
        //weaponCollider.enabled = true;
		anim.Play("Attack01");
        DOTween
            .Sequence()
            .Append(this.transform.DOLocalMove(nextWorldPos, 0.4f)
            .OnUpdate(() =>
            {
                enemyState.checkState
                .Where(x => x == EnemyState.SetState.deth)
                .Subscribe(_ =>
                {
                    seq.Complete();
                    anim.SetTrigger("Deth");
                });
            }
            )
            .OnComplete(() =>
            {
                weaponCollider.enabled = false;
            }
		               ));

        seq.Play();
	}

    public void ComboAttack()
	{
        var thisWorldPos = this.transform.position;
        var thisForward = this.transform.forward;
        var moveDistance = new Vector3(4.0f, 0f, 4.0f);
        var forwardDistance = new Vector3(thisForward[0] * moveDistance[0], 0
                                          , thisForward[2] * moveDistance[2]);
        var nextWorldPos = thisWorldPos + forwardDistance;

        weaponCollider.enabled = true;
	    anim.Play("Attack02");

		seq.Append(
            this.transform.DOLocalMove(nextWorldPos, 0.8f)
		);

		seq.Append(
            this.transform.DOLocalMove(thisWorldPos, 0.6f)
            .OnComplete(() =>
                        weaponCollider.enabled = false
                       )
		);
	}

	public void Deth()
	{
        var thisWorldPos = this.transform.position;
        var thisForward = this.transform.forward;
        var moveDistance = new Vector3(4.0f, 0f, 4.0f);
        var forwardDistance = new Vector3(thisForward[0] * moveDistance[0], 0
                                          , thisForward[2] * moveDistance[2]);
        var nextWorldPos = thisWorldPos + forwardDistance;

        //seq.Complete();
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false;
        }
        anim.Play("Deth");
        this.transform.DOLocalMove(nextWorldPos, 0.3f)
		    .OnComplete(() =>
			{
                Destroy(this.gameObject, 3);
                Destroy(this);
			}
                       );
	}
}
