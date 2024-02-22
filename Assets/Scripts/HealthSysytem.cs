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
        // �߰� �Ҹ��Ѱ� ü���� ȸ���ɶ��� �ʷϻ� ����Ʈ �����Ҷ��� ������ ����Ʈ����?
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

    // �׾��� �� �̺�Ʈ �ϳ� �ɾ�ΰ� ����� �ص� �ɵ�
}
