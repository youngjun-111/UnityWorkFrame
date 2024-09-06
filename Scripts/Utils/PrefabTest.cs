using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTest : MonoBehaviour
{
    GameObject prefab;
    GameObject cube;

    void Start()
    {
        //prefab = Resources.Load<GameObject>("Prefabs/Cube");
        //cube = Instantiate(prefab);

        cube = Managers.Resource.Instantiate("Cube");

        Destroy(cube, 3f);
    }
}
