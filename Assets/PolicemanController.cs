using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PolicemanController : MonoBehaviour
{

    public Transform Head = null;

    public float FieldOfView = 30.0f;

    public float LookAtDistance = 2.0f;
    public float LookAtStrength = 10.0f;

    public bool LookAt = false;
    public bool Rest = false;

    public float RestDistance = 5.0f;
    public float RestTimeMin = 5.0f;
    public float RestTimeMax = 10.0f;

    public float RestTime = 0.0f;
    public bool Resting = false;

    public float aaaa = 0.0f;

    public NavMeshAgent NavMeshAgent = null;
    public ActorController ActorController = null;
    public PolicemanNEtworking PolicemanNEtworking = null;

    public bool Civil = false;

    public GameObject Hat = null;

    public Animator Animator = null;

    public Vector3 prevposition = Vector3.zero;

    public Transform HEadBone = null;

    public Renderer Renderer = null;

    // Use this for initialization
    void Start()
    {
        if (Random.value > 0.85f)
        {
            LookAt = true;
        }
        else if (Random.value > 0.65f)
        {
            Rest = true;
        }

        if (Civil)
        {
            MakeCivilian();
        }

        prevposition = transform.position;
    }

    public void MakeCivilian()
    {
        Hat.SetActive(false);

        var materials = Renderer.materials;

        materials[1].SetColor("_Color", new Color(Random.value, Random.value, Random.value));
        materials[2].SetColor("_Color", new Color(Random.value, Random.value, Random.value));

        Renderer.materials = materials;
    }

    // Update is called once per frame
    void Update()
    {
        if (EntController.Player == null) return;
        if (EntController.Player.Head == null) return;

        if(OfflineGame.Instance == null)
        {
            if (!PolicemanNEtworking.isServer) return;
        }        

        if (Resting)
        {
            RestTime -= Time.deltaTime;
            if(RestTime <= 0.0f)
            {
                Resting = false;
                NavMeshAgent.destination = ActorController.Destination;
            }
            
        }
        else
        {
            if (Rest)
            {
                if ((EntController.Player.Head.position - Head.position).magnitude < RestDistance)
                {
                    if (Random.value > 0.5f)
                    {
                        Resting = true;
                        Rest = false;
                        RestTime = Random.Range(RestTimeMin, RestTimeMax);
                        NavMeshAgent.destination = transform.position;
                    }
                }
            }

            if (!Civil)
            {
                var dir = EntController.Player.Head.position - Head.position;
                var angle = GetAngle(EntController.Player.Head.position, Head.position);

                if (angle < FieldOfView)
                {
                    var hits = Physics.RaycastAll(Head.position, dir, dir.magnitude);
                    if (hits.Length == 0)
                    {
                        EntController.Player.InSight(transform.position);
                    }
                }
            }
        }

        var speed = (transform.position - prevposition).magnitude / Time.deltaTime;
        prevposition = transform.position;

        Animator.SetFloat("Speed", speed);
    }

    private void LateUpdate()
    {
        if (LookAt)
        {
            var r = LookAtStrength * Time.deltaTime;

            bool rotateToTarget = false;

            rotateToTarget = (EntController.Player.Head.position - Head.position).magnitude < LookAtDistance;

            aaaa = Vector3.Angle(Head.forward, transform.forward);

            if (aaaa > 90.0f)
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

            HEadBone.rotation = Head.rotation;
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
