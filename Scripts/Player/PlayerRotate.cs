using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerRotate : MonoBehaviour {

	public float rotateSpeed = 1.0f;
	public float clampAngle = 60.0f;
	public GameObject cameraFollow;

	void Start () {
		this.UpdateAsObservable()
			.Subscribe(_ => PlayerRotation());

	}

	public void PlayerRotation()
	{
		this.transform.LookAt(cameraFollow.transform.position);
	}
}
