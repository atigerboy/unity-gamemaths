using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalJump : MonoBehaviour
{
    public AnimationCurve curve;
    public float V0(float Vx, float halfLen, float TopH)
    {
        return 2 * TopH * Vx / halfLen;
    }

    public float Accel(float Vx, float halfLen, float TopH)
    {
        return -2 * TopH * Vx * Vx / (halfLen * halfLen);
    }

    private float _vel;//v
    private float _pos;
    private float _deltaT;
    private float _time;

    private float _posX;
    private void MoveUpdate()
    {
        _pos = curve.Evaluate(_time);
    }


    private Vector3 currentPosition;
    private void OnEnable()
    {
        _pos = 0;
        _posX = 0;
        currentPosition = transform.position;
        _time = 0;
    }
    private void FixedUpdate()
    {
        _time += Time.fixedDeltaTime;
        _time -= Mathf.Floor(_time);
        MoveUpdate();
        transform.position = currentPosition + new Vector3(_posX,  _pos, 0);
        if (transform.position.y < -5)
        {
            if (transform.position.x > 10)
                transform.position = new Vector3(-4, 0, 0);
            OnEnable();
        }
    }
}
