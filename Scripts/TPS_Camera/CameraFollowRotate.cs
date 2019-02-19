using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class CameraFollowRotate : MonoBehaviour {

	public float moveSpeed = 10f;
	public float clampAngle = 40f;
	//public GameObject player;

	void Start () {

		this.UpdateAsObservable()
		    .Subscribe(_ => CameraFollowMoveAndRotate());
	}

	public void CameraFollowMoveAndRotate()
	{
		var horizontalState = Input.GetAxisRaw("Horizontal") * moveSpeed;
		this.transform.localEulerAngles += new Vector3(0, horizontalState, 0);
		var nowY = this.transform.localEulerAngles.y;
		nowY = Mathf.Abs(nowY) > 180f ? nowY - 360f : nowY;
		this.transform.localEulerAngles = new Vector3(0,
												 Mathf.Clamp(nowY, -clampAngle, clampAngle),
												 0);
	}
}