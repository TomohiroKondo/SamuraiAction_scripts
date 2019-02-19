using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamege : MonoBehaviour {

    private PlayerStatus playerStatus;
    private Player_PackController player_PackController;


    void Start()
    {
        playerStatus = this.GetComponent<PlayerStatus>();
        player_PackController = this.GetComponentInParent<Player_PackController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var weaponLayer = LayerMask.LayerToName(other.gameObject.layer);
        if (weaponLayer == "Weapon")
        {
             if (player_PackController.playerLife > 0)
            {
                playerStatus.playerStateSubject
                    .OnNext(PlayerStatus.PlayerState.Down);
                player_PackController.playerLife 
                    = player_PackController.playerLife - 1;
            }
            else
            {
                playerStatus.playerStateSubject
                            .OnNext(PlayerStatus.PlayerState.Deth);
            }
        }
    }
}
