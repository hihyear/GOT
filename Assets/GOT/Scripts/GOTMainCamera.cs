using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOTMainCamera : MonoBehaviour
{
    public GameObject Target;

    public float offsetX = 0.0f;            // ī�޶��� x��ǥ
    public float offsetY = 2.6f;            // ī�޶��� y��ǥ
    public float offsetZ = -3.0f;           // ī�޶��� z��ǥ

    public float CameraSpeed = 10.0f;       // ī�޶��� �ӵ�
    Vector3 TargetPos;

    void FixedUpdate()
    {
        TargetPos = new Vector3(
            Target.transform.position.x + offsetX,
            Target.transform.position.y + offsetY,
            Target.transform.position.z + offsetZ
            );

        // ī�޶��� �������� �ε巴�� �ϴ� �Լ�(Lerp)
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * CameraSpeed);
    }
}
