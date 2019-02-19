using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ReturnUI : MonoBehaviour {

    [SerializeField] private Text text;

    // Use this for initialization
	void Start () {
        text.enabled = false;
	}

    public void ReturnFlashing()
    {
        text.enabled = true;
        text.DOFade(0.0f, 1.0f).SetEase(Ease.InSine).SetLoops(-1, LoopType.Yoyo);
    }

}
