using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIStart : UIBase
{
    Button[] btns;

    private void Awake()
    {
        btns = GetComponentsInChildren<Button>();
    }

    private void Start()
    {
        btns[0].onClick.AddListener(() => SceneManager.LoadScene(CommonData.mainScene));
        btns[1].onClick.AddListener(() => Application.Quit());
    }
}
