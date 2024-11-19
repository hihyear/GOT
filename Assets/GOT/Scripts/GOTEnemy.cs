using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class GOTEnemy : MonoBehaviour
{
    public float forcePower = 500.0f;
    public GOTWeapon_Enemy Weapon;

    bool isAttacking = false;
    bool isDead = false;

    Coroutine co;

    Animator _anim;
    Material _mat;
    NavMeshAgent _nav;
    Collider _col;
    Rigidbody _rb;

    ThirdPersonController _player;

    private void Start()
    {
       _anim = GetComponentInChildren<Animator>();
       _mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
       _nav = GetComponentInChildren<NavMeshAgent>();
       _col = GetComponentInChildren<CapsuleCollider>();
       _rb = GetComponentInChildren<Rigidbody>();

       _player = FindObjectOfType<ThirdPersonController>();


        float randomAttackSpeed = Random.Range(1.0f, 1.5f);
        float randomMoveSpeed = Random.Range(0.7f, 1.4f);
        _anim.SetFloat("AttackSpeed", randomAttackSpeed);
        _anim.SetFloat("MoveSpeed", randomMoveSpeed);
    }


    private void Update()
    {
        if (isDead) return;
        
        if (_player != null)
        {
            if (_player.Weapon.bDead == true)
            {
                _anim.SetBool("Move", false);
                return;
            }
            
            // �÷��̾ �ִ°����� �̵�
            _nav.SetDestination(_player.transform.position);

            // �ִϸ��̼� ����
            if (_nav.velocity.sqrMagnitude == 0.0f)
            {
                if (Vector3.Distance(transform.position, _player.transform.position) <= 2.0f && isAttacking == false)
                {
                    OnAttackEnter();
                }
                else
                {
                    _anim.SetBool("Move", false);
                }
            }
            else
            {
                _anim.SetBool("Move", true);
            }
        }
    }


    public void OnHit(Vector3 attackPos, GOTWeapon attacker)
    {
        // �̹� ������쿡�� �Ѿ��
        if (isDead)
        {
            if (co != null)
                StopCoroutine(co);
            return;
        }

        isDead = true;
        attacker.IncreaseKillCount();

        if(co != null)
            StopCoroutine(co);
        ChangeHitColor();

        // �з�����
        //if (_rb != null)
        //{
        //    Vector3 direction = (transform.position - attackPos).normalized;
        //   _rb.AddForce(direction * forcePower);
        //}

        // �׾ �ɸ����Ÿ��°� ���ֱ�
        _col.enabled = false;
        _rb.isKinematic = true;

        _anim.SetTrigger("Dead");
        Destroy(gameObject, 3.0f);
    }

 
    public void ChangeHitColor()
    {
        _mat.color = Color.red;
        Invoke("ReturnColor", 0.1f);
    }

    private void ReturnColor()
    {
        _mat.color = Color.white;
    }

    public void OnAttackEnter()
    {
        if (isAttacking == false)
        {
            isAttacking = true;
            Weapon.bAttacking = true;
            _anim.SetTrigger("Attack");

            co = StartCoroutine(SetIsAttackingFalse());
        }
    }

    public void OnAttackExit()
    {
        Weapon.bAttacking = false;
    }

    // ���� ���Ӱ���(?) ����
    IEnumerator SetIsAttackingFalse()
    {
        yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
        isAttacking = false;
    }
}
