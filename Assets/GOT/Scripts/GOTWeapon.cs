using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOTWeapon : MonoBehaviour
{
    public ParticleSystem hitVfx;                   
    public bool bAttacking = false;                     // true인 동안에만 공격 이벤트 가능 (걸어다닐때도 공격되면 안되니까,,)

    Collider _col;
    
    void Start()
    {
        _col = GetComponent<SphereCollider>();
    }


   /**  OnCollisionEnter가 HitPoint를 가져올수 있지만 . . 
        왜인지 충돌 이벤트가 잘 발생되지 않기 때문에
        OnTriggerEnter로 공격판정 진행 (_ _)
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

                // 적 피격 함수 실행
                e.OnHit(hitPoint);

                // 이펙트 Spawn
                float s = Random.Range(0.1f, 0.3f);

                GameObject vfx = Instantiate(hitVfx, hitPoint, Quaternion.identity, other.gameObject.transform).gameObject;
                vfx.transform.localScale = new Vector3(s, s, s);
                
                // 적을 때렸을 경우 공격 종료 처리
                bAttacking = false;
            }
        }
    }
}
