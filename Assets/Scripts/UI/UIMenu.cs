using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMenu : UIBase
{

    Button[] btns;


    private void Awake()
    {
        btns = GetComponentsInChildren<Button>();
    }

    private void Start()
    {
        btns[0].onClick.AddListener(() => SceneManager.LoadScene(CommonData.startScene));
        btns[1].onClick.AddListener(() => Application.Quit());
        btns[2].onClick.AddListener(() => Destroy(gameObject));
    }
}
