using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class FirstOrEnd : MonoBehaviour {

    public enum FirstAndEnd
    {
        First,
        End,
        Centrum
    }

    public enum LastSpart
    {
        firstHalf,
        lastHalf
    }

    public Subject<FirstAndEnd> CheckFE = new Subject<FirstAndEnd>();
    public Subject<LastSpart> checkSpart = new Subject<LastSpart>();

    private StageMake stageMake;
    private Designer designer;
    private List<Vector3> mapData;
    private GameObject[] stageRail;
    private int maxRail;
    private MapSize mapSize;

    // Use this for initialization
    void Start()
    {
        var stageController = GameObject.Find("StageController");
        stageMake = stageController.GetComponent<StageMake>();
        designer = stageController.GetComponent<Designer>();
        maxRail = stageMake.maxRail;
        mapData = designer.mapData;
        mapSize = this.GetComponent<MapSize>();

        CheckFirstOrEnd();

        mapSize.getInstance
               .Where(x => x == 1)
               .Subscribe(_ => CheckLastSpart());
    }

    private void CheckFirstOrEnd()
    {
        var thisMapPos = this.transform.position;
        var firstPos = mapData[0];
        var endPos = mapData[maxRail - 3];

        if (thisMapPos == firstPos)
        {
            CheckFE.OnNext(FirstAndEnd.First);
        }else if(thisMapPos == endPos)
        {
            CheckFE.OnNext(FirstAndEnd.End);
        }else{
            CheckFE.OnNext(FirstAndEnd.Centrum);
        }
    }

    void CheckLastSpart()
    {
        var thisMapPos = this.transform.position;

        var i = mapData.IndexOf(thisMapPos);

        if(i > maxRail - 4)
        {
            checkSpart.OnNext(LastSpart.lastHalf);
        }else{
            checkSpart.OnNext(LastSpart.firstHalf);
        }

    }
}
