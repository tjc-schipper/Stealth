using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Coordinate system: X/Y, Z is up. 0Angle = +x, CCW.
/// </summary>
public class Movement : Photon.MonoBehaviour
{

    public SpyState state
    {
        get
        {
            if (_state == null)
                _state = GetComponent<SpyState>();
            return _state;
        }
        set
        {
            _state = value;
        }
    }
    private SpyState _state;
    //public SpyState state;

    public Vector3 Position
    {
        get
        {
            return rigidbody.position;
        }
        set
        {
            rigidbody.MovePosition(value);
            state.Position = value;
        }
    }

    public Quaternion Rotation
    {
        get
        {
            return rigidbody.rotation;
        }
        set
        {
            rigidbody.MoveRotation(value);
            state.Angle = Angle;
        }
    }

    public float Angle
    {
        get
        {
            return RotationToAngle(Rotation);
        }
        set
        {
            Rotation = AngleToRotation(value);
            state.Angle = value;
        }
    }

    public Vector3 Direction
    {
        get
        {
            return transform.forward;
        }
        set
        {
            Vector3 n = value.normalized;
            Rotation = Quaternion.LookRotation(n);
        }
    }

    public Vector3 Velocity
    {
        get
        {
            return rigidbody.velocity;
        }
        set
        {
            Vector3 deltaV = value - Velocity;
            rigidbody.AddForce(deltaV, ForceMode.VelocityChange);
        }
    }

    protected Quaternion AngleToRotation(float a)
    {
        // Convention: +x is 0, CCW. Unit circle
        Vector3 dir = AngleToDirection(a);
        return Quaternion.LookRotation(dir, transform.forward);
    }
    protected Vector3 AngleToDirection(float a)
    {
        float x, z;
        return new Vector3(
            Mathf.Sin(a),
            Mathf.Cos(a),
            0f
            );
    }

    protected float RotationToAngle(Quaternion q)
    {
        return q.eulerAngles.z;
    }
    protected Vector3 RotationToDirection(Quaternion q)
    {
        throw new NotImplementedException();
        return Vector3.zero;
    }
}
