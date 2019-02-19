using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class MainCameraRotate : MonoBehaviour {

	private GameObject rotateOrigin;
	//private GameObject cameraFollow;

	void Start () {
		rotateOrigin = GameObject.Find("RotateOrigin");

		this.UpdateAsObservable()
			.Subscribe(_ => CameraRotate());
		
	}

	public void CameraRotate()
	{
		var nowRot = rotateOrigin.transform.localEulerAngles;
		this.transform.localEulerAngles = nowRot;
	}
}
