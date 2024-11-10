using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOTWeapon_Enemy : MonoBehaviour
{
    public bool bAttacking = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (bAttacking)
            {
                Vector3 hitPoint = gameObject.transform.position;
                GOTWeapon e = other.gameObject.GetComponentInChildren<GOTWeapon>();

                // 적 피격 함수 실행
                e.OnHit(hitPoint);

                // 적을 때렸을 경우 공격 종료 처리
                bAttacking = false;
            }
        }
    }
}
