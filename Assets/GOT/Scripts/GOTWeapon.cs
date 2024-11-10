using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOTWeapon : MonoBehaviour
{
    public ParticleSystem hitVfx;                   
    public bool bAttacking = false;                     // true�� ���ȿ��� ���� �̺�Ʈ ���� (�ɾ�ٴҶ��� ���ݵǸ� �ȵǴϱ�,,)

    Collider _col;
    
    void Start()
    {
        _col = GetComponent<SphereCollider>();
    }


   /**  OnCollisionEnter�� HitPoint�� �����ü� ������ . . 
        ������ �浹 �̺�Ʈ�� �� �߻����� �ʱ� ������
        OnTriggerEnter�� �������� ���� (_ _)
   **/
   //private void OnCollisionEnter(Collision collision)
   //{
   //    if(collision.gameObject.tag == "Enemy")
   //    {
   //        if (bAttacking)
   //        {
   //            GOTEnemy e = collision.gameObject.GetComponent<GOTEnemy>();
   //            e.ChangeHitColor();
   //
   //            Vector3 hitPoint = collision.contacts[0].point;
   //            Instantiate(hitVfx, hitPoint, Quaternion.identity);
   //        }
   //    }
   //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (bAttacking)
            {
                Vector3 hitPoint = gameObject.transform.position;
                GOTEnemy e = other.gameObject.GetComponent<GOTEnemy>();

                // �� �ǰ� �Լ� ����
                e.OnHit(hitPoint);

                // ����Ʈ Spawn
                float s = Random.Range(0.1f, 0.3f);

                GameObject vfx = Instantiate(hitVfx, hitPoint, Quaternion.identity, other.gameObject.transform).gameObject;
                vfx.transform.localScale = new Vector3(s, s, s);
                
                // ���� ������ ��� ���� ���� ó��
                bAttacking = false;
            }
        }
    }
}
