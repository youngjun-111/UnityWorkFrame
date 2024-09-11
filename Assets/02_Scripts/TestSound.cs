using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    private void Start()
    {
        Managers.Sound.Play("Sounds/univ0003", Define.Sound.Bgm);
    }
    private void OnTriggerEnter(Collider other)
    {

    }
}
