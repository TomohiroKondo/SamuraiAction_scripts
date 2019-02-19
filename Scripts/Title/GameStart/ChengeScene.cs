using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

public class ChengeScene : MonoBehaviour {

    public Subject<Unit> startFlag = new Subject<Unit>();

	void Start () {
        startFlag.Where(x => x == Unit.Default)
            .First()
            .Subscribe(_ => ChengePlay());

	}

    private void ChengePlay()
    {
        SceneManager.LoadScene("SamuraiAction_Play");
    }
}
