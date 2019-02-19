using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;

public class ButtonState : MonoBehaviour {

    private PlayerAnime anim;
    private ButtonStatus button;
    private GameObject obj;
    private GameObject player;
    private PlayerRunning playerRunning;
    private StartComplete startComplete;
    private PlayerStatus playerStatus;
    [SerializeField] private List<ButtonStatus> buttonStatusesList = new List<ButtonStatus>();

    void Start()
	{
        button = this.GetComponent<ButtonStatus>();
        startComplete = this.GetComponent<StartComplete>();
        buttonStatusesList.Add(button);

        startComplete.startSubject
                      .Where(x => x == 2)
                      .Subscribe(_ => StartCoroutine(GetInstance()));

        foreach(ButtonStatus status in buttonStatusesList)
        {
            button.pushButton
                  .Where(x => x == ButtonStatus.ButtonState.Fire1)
                  .ThrottleFirst(TimeSpan.FromSeconds(2))
                  .Select(_ => playerStatus.cancelFlag)
                  .Where(x => x == true)
                  .Subscribe(_ =>
                  {
                      playerStatus.playerStateSubject.OnNext(PlayerStatus.PlayerState.Attack);
                  });

            button.pushButton
                  .Where(x => x == ButtonStatus.ButtonState.Jump)
                  .ThrottleFirst(TimeSpan.FromSeconds(2.5))
                  .Select(_ => playerStatus.cancelFlag)
                  .Where(x => x == true)
                  .Subscribe(_ =>
                  {
                      playerStatus.playerStateSubject.OnNext(PlayerStatus.PlayerState.Roll);
                  });

            button.pushButton
                  .Where(x => x == ButtonStatus.ButtonState.Jump)
                  .Select(x => playerStatus.lastFlag)
                  .Where(x => x == true)
                  .First()
                  .Subscribe(_ => SceneManager.LoadScene("Title"));
        }
    }

    IEnumerator GetInstance()
    {
        obj = GameObject.Find("Player_Pack(Clone)");
        player = GameObject.Find("Player");
        anim = player.GetComponent<PlayerAnime>();
        playerRunning = obj.GetComponent<PlayerRunning>();
        playerStatus = player.GetComponent<PlayerStatus>();
        startComplete.startSubject.OnNext(3);

        yield return null;
    }
}