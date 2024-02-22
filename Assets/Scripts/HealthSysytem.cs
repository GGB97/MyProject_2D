using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSysytem : MonoBehaviour
{
    public int ID { get; private set; }
    public float maxHealth = 100f;
    public float health;

    public bool isDie = false;
    [SerializeField] Image healthImg;

    private void Awake()
    {
        ID = GameManager.Instance.healthID++;
        health = maxHealth;
    }

    private void Start()
    {
        UpdateHealth();
    }

    public void ChangeHealth(float value)
    {
        // 추가 할만한게 체력이 회복될때는 초록색 이펙트 감소할때는 붉은색 이펙트정도?
        health += value;

        if(health > maxHealth)
            health = maxHealth;
        else if (health < 0)
        {
            health = 0;
            isDie = true;
        }

        UpdateHealth();
    }

    void UpdateHealth()
    {
        healthImg.fillAmount = health / 100;
    }

    // 죽었을 때 이벤트 하나 걸어두고 재생성 해도 될듯
}
