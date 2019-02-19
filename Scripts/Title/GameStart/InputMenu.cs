using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class InputMenu : MonoBehaviour {

    private float start;
    private ChengeScene chengeScene;
	// Use this for initialization
	void Start () {
        start = 0.0f;
        chengeScene = this.GetComponent<ChengeScene>();

        StartCoroutine(GameStart());
	}

    private IEnumerator GameStart()
    {
        while(start == 0)
        {
            start = Input.GetAxis("Jump");
            yield return new WaitForEndOfFrame();
        }

        chengeScene.startFlag.OnNext(Unit.Default);

        yield return null;
    }
}
