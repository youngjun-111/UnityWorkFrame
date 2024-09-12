using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    //랩핑
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }


    public GameObject Instantiate(string path, Transform parent = null)
    {
        //경로만 지정해주면 범용적으로 사용가능함
        //Resources.Load<T>(path);를 사용할경우 최상위 폴더 명이 무조건 Resources여야함
        //즉, 경로가 Resources 폴더 -> Prefabs 폴더 안에 있는 ObjectPrefab이란 뜻.
        //세부화 할시 Prefabs는 생략 가능함. 이미 ResourceManager에서 정의해 줘서
        //1. 오리지널이 있으면 바로 사용 없으면 아래처럼 써야함
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");

        if (prefab == null)
        {
            //경로를 찾지 못했거나, Prefab을 못찾아서 생성시킨게 없으면 나오는 경고 문
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }
        //2. 혹시 풀링된 오브젝트가 있으면 그것을 반환
        GameObject go = Object.Instantiate(prefab, parent);
        //"(Clone)"문자열을 찾아서 인덱스를 반환
        int index = go.name.IndexOf("(Clone)");
        if(index > 0)
        {
            go.name = go.name.Substring(0, index);
        }
        return go;
    }

    //랩핑해본것일 뿐 실제로는 필요없다.
    //3. 만약에 풀링이 필요한 오브젝트라면 바로 삭제하는 것이 아니라 풀링 매니저한테 위탁
    public void Destroy(GameObject go)
    {
        if (go == null)
            return;
        Object.Destroy(go);
    }
}
