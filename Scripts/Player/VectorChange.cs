using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;
using UnityEngine.AI;

public class VectorChange : MonoBehaviour {

    private GameObject player_Pack;
    private Player_PackController player_PackController;

    private void Start()
	{
        player_Pack = GameObject.Find("Player_Pack(Clone)");
        player_PackController = player_Pack.GetComponent<Player_PackController>();

    }

	public void ChangeVector(Player_PackController.NextPlayerVector state, GameObject obj)
	{
		switch (state)
		{
			case Player_PackController.NextPlayerVector.x_Positive:
				{
					obj.transform.DORotate(new Vector3(0, 0, 0), 0.5f);
					break;
				};

			case Player_PackController.NextPlayerVector.x_Negative:
				{
					obj.transform.DORotate(new Vector3(0, 180f, 0), 0.5f);
					break;
				};
                
			case Player_PackController.NextPlayerVector.z_Positive:
				{
					obj.transform.DORotate(new Vector3(0, 90f, 0), 0.5f);
					break;
				};

			case Player_PackController.NextPlayerVector.z_Negative:
				{
					obj.transform.DORotate(new Vector3(0, -90f, 0), 0.5f);
					break;
				}

			case Player_PackController.NextPlayerVector.Equal:
				{
					//Debug.LogFormat("Equal : {0}", obj.transform.name);
					break;
				}
		}
	}
}
