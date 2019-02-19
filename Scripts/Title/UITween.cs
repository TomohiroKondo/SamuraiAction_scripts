using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UITween : MonoBehaviour {

    [SerializeField] private UnityEngine.UI.Text text;
	// Use this for initialization
	void Start () {
        text.DOFade(0.0f, 1.0f).SetEase(Ease.InSine).SetLoops(-1, LoopType.Yoyo);
	}
}
