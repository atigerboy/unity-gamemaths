using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleJump : MonoBehaviour
{
    public float topHeight;//h
    public float halfLenghth;//Xh
    public float velX;//Vx
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
    private float _acc;
    private float _deltaT;

    private float _posX;
    private void MoveUpdate()
    {
        //_posX += velX * _deltaT;
        _pos += _vel * _deltaT + 0.5f * _acc * _deltaT * _deltaT;
        float new_acc = NewAcc(_posX);// constant _acc
        _vel += 0.5f * (_acc + new_acc) * _deltaT;
        _acc = new_acc;
    }

    private float NewAcc(float pos)
    {
        return _acc;
    }

    private Vector3 currentPosition;
    private void OnEnable()
    {
        _vel = V0(velX, halfLenghth, topHeight);
        _pos = 0;
        _acc = Accel(velX, halfLenghth, topHeight);
        _posX = 0;
        currentPosition = transform.position;
    }
    private void FixedUpdate()
    {
        _deltaT = Time.fixedDeltaTime;
        MoveUpdate();
        transform.position = currentPosition + new Vector3(_posX, _pos, 0);
        if (transform.position.y < -5)
        {
            enabled = false;
            OnEnable();
            enabled = true;
        }
    }

}
