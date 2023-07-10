using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    void Update()
    {
        //位置をターゲットの位置に設定し、Y軸にオフセットを設定する。
        Vector3 ActorPos = new Vector3(target.transform.position.x, target.transform.position.y - 0.5f, target.transform.position.z);
        gameObject.transform.position = ActorPos;
    }
}
