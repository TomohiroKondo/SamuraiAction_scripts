using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class MapSize : MonoBehaviour {

    public Subject<int> getInstance = new Subject<int>();
    public float inX;
    public float inZ;
    public float centerX;
    public float centerZ;
    public float outX;
    public float outZ;

    private FirstOrEnd firstOrEnd;
    [SerializeField] private GameObject inObject;
    [SerializeField] private GameObject centerObject;
    [SerializeField] private GameObject outObject;
    [SerializeField] private List<FirstOrEnd> CheckFEList = new List<FirstOrEnd>();

	// Use this for initialization
	void Start () {

        firstOrEnd = this.GetComponent<FirstOrEnd>();
        CheckFEList.Add(firstOrEnd);

        foreach(FirstOrEnd CheckFE in CheckFEList)
        {
            firstOrEnd.CheckFE
                     .First()
                      .Where(x => x == FirstOrEnd.FirstAndEnd.First)
                     .Subscribe(_ => FirstMap());

            firstOrEnd.CheckFE
                     .First()
                      .Where(x => x == FirstOrEnd.FirstAndEnd.Centrum)
                     .Subscribe(_ => StartCoroutine(DelayGetInstiance(5)));

            firstOrEnd.CheckFE
                     .First()
                      .Where(x => x == FirstOrEnd.FirstAndEnd.End)
                     .Subscribe(_ => EndMap());
        }
	}

    private IEnumerator GetMapSize()
    {
        GetInMapSize();
        GetCenterMapSize();
        GetOutMapSize();
        getInstance.OnNext(1);
        getInstance.OnCompleted();

        yield return null;

        Destroy(this);
    }
	
    private IEnumerator DelayGetInstiance(int waitTime)
    {
        for (int i = 0; i < waitTime; i++)
        {
            yield return null;
        }
        StartCoroutine(GetMapSize());
    }

    private void GetInMapSize()
    {
        inX = inObject.GetComponent<Renderer>().bounds.size.x;
        inZ = inObject.GetComponent<Renderer>().bounds.size.z;
    }

    private void GetCenterMapSize()
    {
        centerX = centerObject.GetComponent<Renderer>().bounds.size.x;
        centerZ = centerObject.GetComponent<Renderer>().bounds.size.z;
    }

    private void GetOutMapSize()
    {
        outX = outObject.GetComponent<Renderer>().bounds.size.x;
        outZ = outObject.GetComponent<Renderer>().bounds.size.z;
    }

    private void FirstMap()
    {
        getInstance.OnCompleted();
        Destroy(this);
    }

    private void EndMap()
    {
        getInstance.OnCompleted();
        Destroy(this);
    }
}
