using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    public float topHeight;//h
    public float halfLenghth;//Xh
    public float velX;//Vx
    public float topHeight2;
    public float V0(float Vx, float halfLen, float TopH)
    {
        return 2 * TopH * Vx / halfLen;
    }

    public float Accel(float Vx, float halfLen, float TopH)
    {
        return -2 * TopH * Vx * Vx / (halfLen * halfLen);
    }
    private float[] accArray = new float[] { -1, -1f };
    private int accPoint = 0;
    private float _vel;//v
    private float _pos;
    private float _acc;
    private float _deltaT;

    private float _posX;
    private void MoveUpdate()
    {
        _posX += velX * _deltaT;
        _pos += _vel * _deltaT + 0.5f * _acc * _deltaT * _deltaT;
        float new_acc = NewAcc(_posX);// constant _acc
        _vel += 0.5f * (_acc + new_acc) * _deltaT;
        _acc = new_acc;
    }

    private float NewAcc(float pos)
    {
        if (_posX > halfLenghth * 1.3f && accPoint == 0)//change new acc
        {
            accPoint += 1;
            accArray[accPoint] = Accel(velX, halfLenghth * 0.5f, topHeight2 );
            _acc = accArray[accPoint];
            _vel = V0(velX, halfLenghth, topHeight2);
        }
        return accArray[accPoint];
    }

    private Vector3 currentPosition;
    private void OnEnable()
    {
        _vel = V0(velX, halfLenghth, topHeight);
        _pos = 0;
        _acc = Accel(velX, halfLenghth, topHeight);
        _posX = 0;
        currentPosition = transform.position;
        accArray[0] = _acc;
        accPoint = 0;
        accArray[1] = -1f;
    }
    private void FixedUpdate()
    {
        _deltaT = Time.fixedDeltaTime;
        MoveUpdate();
        transform.position = currentPosition + new Vector3(_posX, _pos, 0);
        if (transform.position.y < -5)
        {
            if (transform.position.x > 10)
                transform.position = new Vector3(-4, 0, 0);
            OnEnable();
        }
    }
}
