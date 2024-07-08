using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCircle : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = -140f; // 시계방향(음수), 반시계방향(양수)

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isGameOver) {
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        }
    }
}
