using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicemanController : MonoBehaviour
{

    public Transform Head = null;

    public float FieldOfView = 30.0f;

    public float LookAtDistance = 2.0f;
    public float LookAtStrength = 10.0f;

    public bool LookAt = false;

    public float aaaa = 0.0f;

    // Use this for initialization
    void Start()
    {
        if (Random.value > 0.85f)
        {
            LookAt = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (EntController.Player == null) return;
        if (EntController.Player.Head == null) return;

        if (LookAt)
        {
            var r = LookAtStrength * Time.deltaTime;

            bool rotateToTarget = false;

            rotateToTarget = (EntController.Player.Head.position - Head.position).magnitude < LookAtDistance;

            aaaa = Vector3.Angle(Head.forward, transform.forward);

            if (aaaa > 110.0f)
            {
                rotateToTarget = false;
            }

            if (rotateToTarget)
            {
                RotateTo(Head, EntController.Player.Head.position, r);
            }
            else
            {
                RotateTo(Head, Head.position + transform.forward, r);
            }
        }

        var dir = EntController.Player.Head.position - Head.position;
        var angle = GetAngle(EntController.Player.Head.position, Head.position);

        if (angle < FieldOfView)
        {
            EntController.Player.InSight();
        }
    }

    public float GetAngle(Vector3 pos0, Vector3 pos1)
    {
        var dir = pos0 - pos1;
        return Vector3.Angle(Head.forward, dir);
    }

    public void RotateTo(Transform t, Vector3 target, float r)
    {
        var rotot = t.rotation;

        var angle0 = GetAngle(target, t.position);
        t.Rotate(Vector3.up, r);

        var angle1 = GetAngle(target, t.position);

        t.rotation = rotot;
        t.Rotate(Vector3.up, -r);

        var angle2 = GetAngle(target, t.position);

        t.rotation = rotot;

        if (angle1 > angle0 && angle2 > angle1)
        {
            t.rotation = rotot;
        }
        else if (angle0 > angle1 && angle2 > angle1)
        {
            t.Rotate(Vector3.up, r);
        }
        else if (angle1 > angle2 && angle0 > angle2)
        {
            t.Rotate(Vector3.up, -r);
        }
    }
}
