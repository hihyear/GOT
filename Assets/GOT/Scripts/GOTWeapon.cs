using StarterAssets;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GOTWeapon : MonoBehaviour
{
    [Header("Attack")]
    public ParticleSystem hitVfx;                   
    public bool bAttacking = false;                     // true�� ���ȿ��� ���� �̺�Ʈ ���� (�ɾ�ٴҶ��� ���ݵǸ� �ȵǴϱ�,,)

    [Header("Skill")]
    public Collider _rangeCol;
    public bool bSkilling = false;

    public ParticleSystem skillVfx;
    public ParticleSystem skillHitVfx;

    private GameObject _skillVfxObject;

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

    public bool bArmor = false;
    public bool bDead = false;

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
            // �Ϲ� ����
            if (bAttacking)
            {
                Vector3 hitPoint = gameObject.transform.position;
                GOTEnemy e = other.gameObject.GetComponent<GOTEnemy>();

                CinemachineShake.Instance.ShakeCamera();

                // �� �ǰ� �Լ� ����
                e.OnHit(hitPoint, this);
                IncreaseGauge();

                // ����Ʈ Spawn
                float s = Random.Range(0.1f, 0.3f);
                GameObject vfx = Instantiate(hitVfx, hitPoint, Quaternion.identity, other.gameObject.transform).gameObject;
                vfx.transform.localScale = new Vector3(s, s, s);
                
                // ���� ������ ��� ���� ���� ó��
                bAttacking = false;
            }

            // ��ų ����
            if(bSkilling)
            {
                Vector3 hitPoint = other.gameObject.transform.position;
                GOTEnemy e = other.gameObject.GetComponent<GOTEnemy>();

                CinemachineShake.Instance.ShakeCamera();

                // �� �ǰ� �Լ� ����
                e.OnHit(hitPoint, this);

                // ����Ʈ Spawn
                float s = Random.Range(0.3f, 0.6f);
                GameObject vfx = Instantiate(skillHitVfx, hitPoint, Quaternion.identity, other.gameObject.transform).gameObject;
                vfx.transform.localScale = new Vector3(s, s, s);

                // ��ų ������ ������ �������̱� ������ ���� ���� ó���� ���� �ʴ´�
            }
        }
    }

    private void IncreaseGauge()
    {
        // ������ ������Ʈ
        _currentGauge = Mathf.Clamp(_currentGauge + 1, 0, _maxGauge);
        Slider_Gauge.value = (float)_currentGauge/(float)_maxGauge;
    }

    public void IncreaseKillCount()
    {
        // ų ī��Ʈ ������Ʈ
        // ���� ������ Weapon.IncreaseKillCount() �ϵ��� �� (OnHit�� this ����)
        _killCount++;
        Txt_Kill.text = $"Kill : {_killCount}";

        if(_killCount == 20)
        {
            GOTGameManager.Instance.OpenClearUI(true);
        }
    }

    public void ResetGauge()
    {
        // ��ų ������� ��� ������ �ʱ�ȭ
        _currentGauge = 0;
        Slider_Gauge.value = 0;
    }

    public void OnHit(Vector3 hitPoint)
    {
        // �����϶� ���� �ʴ´�
        if (bArmor || bDead) return;

        
        if (_currentHp <= 0)
        {
            // �׾�����
            _player.OnDeadAnimation();
            Slider_Hp.value = 0;

            bDead = true;
            GOTGameManager.Instance.OpenClearUI(false);
        }
        else
        {
            _player.OnHitAnimation();
            _currentHp = Mathf.Clamp(_currentHp - 1, 0, _maxHp);
            Slider_Hp.value = (float)_currentHp / (float)_maxHp;
        }
    }

    public bool CanSkill()
    {
        // �������� á���� ����
        return _currentGauge >= _maxGauge;
    }

    public void SkillVfx(bool isSpawn)
    {
        // ��ų ���ݽ� �÷��̾����� ����� ����Ʈ ���� �� ����
        if (isSpawn)
        {
            _skillVfxObject = Instantiate(skillVfx, _player.transform.position , Quaternion.identity, _player.transform).gameObject;
        }
        else
        {   
            if(_skillVfxObject != null)
                Destroy(_skillVfxObject);
        }
    }
}
