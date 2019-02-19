using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class CameraTargetFollow : MonoBehaviour {
    
	public float moveSpeed = 1f;
	public float rotateSpeed = 1f;
	private GameObject cameraRotate,cameraPosition;
	private GameObject rotateOrigin, cameraFollow;
	private StartComplete startComplete;
    
	// Use this for initialization
	void Start () {      
		var stageController = GameObject.Find("StageController");
		startComplete = stageController.GetComponent<StartComplete>();

		startComplete.startSubject
					  .Where(x => x == 3)
					  .Subscribe(_ => StartCoroutine(GetInstance()));

		this.FixedUpdateAsObservable()
		    .Where(_ => startComplete.completed.Value == true)
			.Subscribe(_ => CameraMove());

	}

    void CameraMove()
	{
		var nowPos = this.transform.position;
		var targetPos = cameraPosition.transform.position;
		this.transform.position = Vector3.Lerp(nowPos, targetPos, moveSpeed * Time.deltaTime);

		var nowAngle = this.transform.rotation;
		var targetObjPos = cameraFollow.transform.position;
		var targetVector = targetObjPos - nowPos;
		var newAngle = Quaternion.LookRotation(targetVector);
		this.transform.rotation = Quaternion.Slerp(nowAngle, newAngle, rotateSpeed * Time.deltaTime);
	}

	IEnumerator GetInstance()
	{
		cameraRotate = GameObject.Find("CameraRotate");
        cameraPosition = cameraRotate.transform.Find("CameraPosition").gameObject;
        rotateOrigin = GameObject.Find("RotateOrigin");
        cameraFollow = rotateOrigin.transform.Find("CameraFollow").gameObject;
		      
		startComplete.startSubject.OnNext(4);

		yield return null;
	}
}