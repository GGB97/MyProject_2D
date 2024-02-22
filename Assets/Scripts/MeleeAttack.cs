using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] LayerMask targetLayer;
    Vector2 attackRange;
    int damage;

    Dictionary<int, HealthSysytem> enemies;

    public void Init(AttackInfoData data)
    {
        if (enemies == null)
            enemies = new Dictionary<int, HealthSysytem>();

        GetComponent<BoxCollider2D>().size = data.AttackRange;
        damage = data.Damage;

        Invoke(nameof(SetActiveTrue), data.Delay);
    }

    void SetActiveTrue()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void SetActiveCollider(bool isActive = true)
    {
        if (enemies != null && isActive == false)
            enemies.Clear();

        GetComponent<BoxCollider2D>().enabled = isActive;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 공격 처리
        // Layer 검사 -> 중복 검사 -> 데미지 처리 -> 딕셔너리 추가 / 만약 있다면 그냥 리턴
        if(targetLayer == (1 << collision.gameObject.layer))
        {
            Debug.Log(collision.gameObject.name);
            HealthSysytem target = collision.GetComponent<HealthSysytem>();
            if (enemies.ContainsKey(target.ID))
            {
                // 이미 데미지 처리가 들어간녀석
                return;
            }
            else
            {
                // 아직 안맞은놈
                target.ChangeHealth(-damage);

                enemies.Add(target.ID, target);
            }
        }
    }
}
