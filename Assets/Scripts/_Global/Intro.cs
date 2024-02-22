using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    private void Awake()
    {
        UIManager.Instance.ShowUI<UIStart>();
    }
}
