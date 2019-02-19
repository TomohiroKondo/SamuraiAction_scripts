using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bake : MonoBehaviour {

    private void Awake()
    {
        var navMeshSurface = this.GetComponent<NavMeshSurface>();
        navMeshSurface.BuildNavMesh();
    }
}