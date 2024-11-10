using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class GOTEnemy : MonoBehaviour
{
    public float forcePower = 500.0f;

    bool isDead = false;

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
    }


    private void Update()
    {
        if(_player != null)
        {
            // �÷��̾ �ִ°����� �̵�
            _nav.SetDestination(_player.transform.position);

            // �ִϸ��̼� ����
            if (_nav.velocity.sqrMagnitude == 0.0f)
            {
                _anim.SetBool("Move", false);
            }
            else
            {
                _anim.SetBool("Move", true);
            }
        }
    }


    public void OnHit(Vector3 attackPos)
    {
        // �̹� ������쿡�� �Ѿ��
        if (isDead) return;
        
        ChangeHitColor();

        // �з�����
        if (_rb != null)
        {
            Vector3 direction = (transform.position - attackPos).normalized;
           _rb.AddForce(direction * forcePower);
        }

        // TODO : ������ �ϴµ� ������ �ɸ����Ÿ�;
        _col.enabled = false;

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
}
