using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOTMainCamera : MonoBehaviour
{
    public GameObject Target;

    public float offsetX = 0.0f;            // 카메라의 x좌표
    public float offsetY = 2.6f;            // 카메라의 y좌표
    public float offsetZ = -3.0f;           // 카메라의 z좌표

    public float CameraSpeed = 10.0f;       // 카메라의 속도
    Vector3 TargetPos;

    void FixedUpdate()
    {
        TargetPos = new Vector3(
            Target.transform.position.x + offsetX,
            Target.transform.position.y + offsetY,
            Target.transform.position.z + offsetZ
            );

        // 카메라의 움직임을 부드럽게 하는 함수(Lerp)
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * CameraSpeed);
    }
}
