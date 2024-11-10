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

                // �� �ǰ� �Լ� ����
                e.OnHit(hitPoint);

                // ���� ������ ��� ���� ���� ó��
                bAttacking = false;
            }
        }
    }
}
