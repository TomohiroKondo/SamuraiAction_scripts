using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PassingExit_V2 : MonoBehaviour {

    //public Subject<int> exit = new Subject<int>();
    public Subject<InOrOut> exit = new Subject<InOrOut>();
    private InOrOut inOrOut = InOrOut.OUT;

    void OnTriggerExit(Collider other)
    {
        var layerName = LayerMask.LayerToName(other.gameObject.layer);
        //Debug.Log(other.name);
        if (layerName == "Player_Pack")
        {
            if(inOrOut == InOrOut.OUT)
            {
                inOrOut = InOrOut.IN;
                exit.OnNext(inOrOut);
            }else{
                inOrOut = InOrOut.OUT;
                exit.OnNext(inOrOut);
            }
        }
    }
}

public enum InOrOut
{
    IN,
    OUT
}
