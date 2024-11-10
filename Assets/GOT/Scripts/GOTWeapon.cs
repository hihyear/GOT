using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GOTWeapon : MonoBehaviour
{
    public ParticleSystem hitVfx;                   
    public bool bAttacking = false;                     // true�� ���ȿ��� ���� �̺�Ʈ ���� (�ɾ�ٴҶ��� ���ݵǸ� �ȵǴϱ�,,)

    [Header("UI")]
    public Slider Slider_Hp;
    public Slider Slider_Gauge;
    public TextMeshProUGUI Txt_Kill;

    int _maxGauge = 5;
    int _currentGauge = 0;

    int _killCount = 0;

    // Weapon�� hp�� �ִ°� �´����� �𸣰�����...
    int _maxHp = 10;
    int _currentHp = 10;

    bool bArmor = false;

    Collider _col;
    ThirdPersonController _player;
    
    void Start()
    {
        _col = GetComponent<SphereCollider>();
        _player = GetComponentInParent<ThirdPersonController>();
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
                IncreaseGauge();

                // ����Ʈ Spawn
                float s = Random.Range(0.1f, 0.3f);

                GameObject vfx = Instantiate(hitVfx, hitPoint, Quaternion.identity, other.gameObject.transform).gameObject;
                vfx.transform.localScale = new Vector3(s, s, s);
                
                // ���� ������ ��� ���� ���� ó��
                bAttacking = false;
            }
        }
    }

    private void IncreaseGauge()
    {
        _currentGauge = Mathf.Clamp(_currentGauge + 1, 0, _maxGauge);
        Slider_Gauge.value = (float)_currentGauge/(float)_maxGauge;

        _killCount++;
        Txt_Kill.text = $"Kill : {_killCount}";
    }

    public void OnHit(Vector3 hitPoint)
    {
        if (bArmor) return;

        _player.OnHitAnimation();
        _currentHp = Mathf.Clamp(_currentHp - 1, 0, _maxHp);
        Slider_Hp.value = (float)_currentHp / (float)_maxHp;
    }
}
