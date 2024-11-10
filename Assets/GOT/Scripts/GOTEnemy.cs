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
            // 플레이어가 있는곳으로 이동
            _nav.SetDestination(_player.transform.position);

            // 애니메이션 설정
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
        // 이미 죽은경우에는 넘어간다
        if (isDead) return;
        
        ChangeHitColor();

        // 밀려나기
        if (_rb != null)
        {
            Vector3 direction = (transform.position - attackPos).normalized;
           _rb.AddForce(direction * forcePower);
        }

        // TODO : 꺼지긴 하는데 여전히 걸리적거림;
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
