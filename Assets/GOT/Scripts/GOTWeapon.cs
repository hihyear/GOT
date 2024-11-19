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
    public bool bAttacking = false;                     // true인 동안에만 공격 이벤트 가능 (걸어다닐때도 공격되면 안되니까,,)

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

    // Weapon에 hp를 넣는게 맞는지는 모르겠지만...
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
            // 일반 공격
            if (bAttacking)
            {
                Vector3 hitPoint = gameObject.transform.position;
                GOTEnemy e = other.gameObject.GetComponent<GOTEnemy>();

                CinemachineShake.Instance.ShakeCamera();

                // 적 피격 함수 실행
                e.OnHit(hitPoint, this);
                IncreaseGauge();

                // 이펙트 Spawn
                float s = Random.Range(0.1f, 0.3f);
                GameObject vfx = Instantiate(hitVfx, hitPoint, Quaternion.identity, other.gameObject.transform).gameObject;
                vfx.transform.localScale = new Vector3(s, s, s);
                
                // 적을 때렸을 경우 공격 종료 처리
                bAttacking = false;
            }

            // 스킬 공격
            if(bSkilling)
            {
                Vector3 hitPoint = other.gameObject.transform.position;
                GOTEnemy e = other.gameObject.GetComponent<GOTEnemy>();

                CinemachineShake.Instance.ShakeCamera();

                // 적 피격 함수 실행
                e.OnHit(hitPoint, this);

                // 이펙트 Spawn
                float s = Random.Range(0.3f, 0.6f);
                GameObject vfx = Instantiate(skillHitVfx, hitPoint, Quaternion.identity, other.gameObject.transform).gameObject;
                vfx.transform.localScale = new Vector3(s, s, s);

                // 스킬 공격은 여러명 때릴것이기 때문에 공격 종료 처리를 하지 않는다
            }
        }
    }

    private void IncreaseGauge()
    {
        // 게이지 업데이트
        _currentGauge = Mathf.Clamp(_currentGauge + 1, 0, _maxGauge);
        Slider_Gauge.value = (float)_currentGauge/(float)_maxGauge;
    }

    public void IncreaseKillCount()
    {
        // 킬 카운트 업데이트
        // 적이 죽으면 Weapon.IncreaseKillCount() 하도록 함 (OnHit때 this 전달)
        _killCount++;
        Txt_Kill.text = $"Kill : {_killCount}";

        if(_killCount == 20)
        {
            GOTGameManager.Instance.OpenClearUI(true);
        }
    }

    public void ResetGauge()
    {
        // 스킬 사용했을 경우 게이지 초기화
        _currentGauge = 0;
        Slider_Gauge.value = 0;
    }

    public void OnHit(Vector3 hitPoint)
    {
        // 무적일땐 맞지 않는다
        if (bArmor || bDead) return;

        
        if (_currentHp <= 0)
        {
            // 죽었을때
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
        // 게이지가 찼는지 리턴
        return _currentGauge >= _maxGauge;
    }

    public void SkillVfx(bool isSpawn)
    {
        // 스킬 공격시 플레이어한테 생기는 이펙트 생성 및 삭제
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
