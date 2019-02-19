using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class ButtonStatus : MonoBehaviour {

	//private ButtonState bs = new ButtonState();
	//private PlayerAnime anime = new PlayerAnime()
 //   public BoolReactiveProperty attack = new BoolReactiveProperty(false);
	//public BoolReactiveProperty rightStep = new BoolReactiveProperty(false);
	//public BoolReactiveProperty leftStep = new BoolReactiveProperty(false);
	//public BoolReactiveProperty roll = new BoolReactiveProperty(false);
	//public BoolReactiveProperty up = new BoolReactiveProperty(false);
	//public BoolReactiveProperty right = new BoolReactiveProperty(false);
	//public BoolReactiveProperty left = new BoolReactiveProperty(false);
	//public BoolReactiveProperty down = new BoolReactiveProperty(false);

    public enum ButtonState
    {
        Fire1,
        Jump,
        PositiveHorizontal,
        NegativeHorizontal,
        PositiveVertical,
        NegativeVertical,
        PositiveJump,
        NegativeJump
    }

    //public enum ButtonState2
    //{
    //    PositiveJump,
    //    NegativeJump
    //}

    public Subject<ButtonState> pushButton = new Subject<ButtonState>();
    //public Subject<ButtonState2> pushButton2 = new Subject<ButtonState2>();
    
    private StartComplete startComplete;
    //private PlayerStatus playerStatus;
    [SerializeField]private List<ButtonStatus> buttonStatusesList = new List<ButtonStatus>();


	private void Start()
	{
        startComplete = this.GetComponent<StartComplete>();
        buttonStatusesList.Add(this);

		this.UpdateAsObservable()
            .Where(_ => startComplete.completed.Value == true)
            .Select(_ => Input.GetAxisRaw("Fire1"))
            .Where(x => x > 0)
            .Subscribe(_ => pushButton.OnNext(ButtonState.Fire1));

        this.UpdateAsObservable()
            .Where(_ => startComplete.completed.Value == true)
            .Select(_ => Input.GetAxisRaw("Jump"))
            .Where(x => x > 0)
            .Subscribe(_ => pushButton.OnNext(ButtonState.Jump));

        this.UpdateAsObservable()
            .Where(_ => startComplete.completed.Value == true)
            .Select(_ => Input.GetAxisRaw("Horizontal"))
            .Where(x => x > 0)
            .Subscribe(_ => pushButton.OnNext(ButtonState.PositiveHorizontal));

        this.UpdateAsObservable()
            .Where(_ => startComplete.completed.Value == true)
            .Select(_ => Input.GetAxisRaw("Horizontal"))
            .Where(x => x < 0)
            .Subscribe(_ => pushButton.OnNext(ButtonState.NegativeHorizontal));

        this.UpdateAsObservable()
            .Where(_ => startComplete.completed.Value == true)
            .Select(_ => Input.GetAxisRaw("Vertical"))
            .Where(x => x > 0)
            .Subscribe(_ => pushButton.OnNext(ButtonState.PositiveVertical));

        this.UpdateAsObservable()
        .Where(_ => startComplete.completed.Value == true)
        .Select(_ => Input.GetAxisRaw("Vertical"))
        .Where(x => x < 0)
        .Subscribe(_ => pushButton.OnNext(ButtonState.NegativeVertical));


        ///TODO
        /// 実装予定

        //foreach (ButtonStatus buttonStatus in buttonStatusesList)
        //{
        //    pushButton.Where(x => x == ButtonState.PositiveHorizontal)
        //              .Select(_ => Input.GetAxisRaw("Jump"))
        //              //.TimeoutFrame(3)
        //              //.DoOnError(x => Debug.Log(x))
        //              //.OnErrorRetry((TimeoutException ex)
        //                            //=> pushButton.OnNext(ButtonState.Non))
        //              .Where(x => x > 0)
        //              .Subscribe(_ => pushButton.OnNext(ButtonState.PositiveJump));

        //    pushButton.Where(x => x == ButtonState.NegativeHorizontal)
        //              .Select(_ => Input.GetAxisRaw("Jump"))
        //              .Where(x => x > 0)
        //              .Subscribe(_ => pushButton.OnNext(ButtonState.NegativeJump));
        //}


        ///<summary>
        /// TODO
        /// 過去スクリプト
        /// </summary>
        //this.UpdateAsObservable()
        //.Where(_ => Input.GetKey(KeyCode.RightArrow) && Input.GetKeyDown(KeyCode.S))
        //.Subscribe(_ => rightStep.Value = true);

        //this.UpdateAsObservable()
        //	 .Where(_ => Input.GetKey(KeyCode.LeftArrow) && Input.GetKeyDown(KeyCode.S))
        //                    .Subscribe(_ => leftStep.Value = true);

        //this.UpdateAsObservable()
        //	.Where(_ => Input.GetKeyDown(KeyCode.Space))
        //				.Subscribe(_ => roll.Value = true);

        //this.UpdateAsObservable()
        //	.Where(_ => Input.GetKeyDown(KeyCode.UpArrow))
        //    .Subscribe(_ => up.Value = true);

        //this.UpdateAsObservable()
        //          .Where(_ => Input.GetKeyUp(KeyCode.UpArrow))
        //               .Subscribe(_ => up.Value = false);

        //this.UpdateAsObservable()
        //	.Where(_ => Input.GetKeyDown(KeyCode.RightArrow))
        //    .Subscribe(_ => right.Value = true);

        //this.UpdateAsObservable()
        //          .Where(_ => Input.GetKeyUp(KeyCode.RightArrow))
        //                  .Subscribe(_ => right.Value = false);

        //this.UpdateAsObservable()
        //    .Where(_ => Input.GetKeyDown(KeyCode.LeftArrow))
        //    .Subscribe(_ => left.Value = true);

        //this.UpdateAsObservable()
        //                .Where(_ => Input.GetKeyUp(KeyCode.LeftArrow))
        //                .Subscribe(_ => left.Value = false);

        //this.UpdateAsObservable()
        //          .Where(_ => Input.GetKeyDown(KeyCode.DownArrow))
        //    .Subscribe(_ => down.Value = true);

        //this.UpdateAsObservable()
        //.Where(_ => Input.GetKeyUp(KeyCode.DownArrow))
        //.Subscribe(_ => down.Value = false);


    }
}