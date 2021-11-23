using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateAround : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public float sensitivity = 3;
    public float limit = 80;
    public float dist = 3f;
    [SerializeField] private float X, Y;

    private int idleSecond = 0;
    public float idleSpeed = 5f;
    public bool IsActive;


    void Start()
    {
        limit = Mathf.Abs(limit);
        if (limit > 90) limit = 90;
        offset.z = -dist;
        StartCoroutine("Timer");
        idleSecond = 60;
    }

    void Update()
    {

        if (this.GetComponent<MainController>().IsLock)
            return;

        MouseControl();

        if (idleSecond >= 10)
        {
            AutoMove();
        }

    }


    IEnumerator Timer()
    {
        while (true)
        {
            idleSecond++;
            yield return new WaitForSeconds(1);
        }
    }


    private void MouseControl()
    {

        if (!Input.GetMouseButton(0))
        {
            IsActive = false;
            return;
        }
        IsActive = true;

        idleSecond = 0;
        offset.z = -dist;


        X = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
        Y += Input.GetAxis("Mouse Y") * sensitivity;
        Y = Mathf.Clamp(Y, -limit, limit);
        transform.localEulerAngles = new Vector3(-Y, X, 0);
        transform.position = transform.localRotation * offset + target.position;
    }


    private void AutoMove()
    {
        X = transform.localEulerAngles.y + Time.deltaTime * idleSpeed;
        if (Y > 0)
        {
            Y -= Time.deltaTime * idleSpeed;
        }
        if (Y < 0)
        {
            Y += Time.deltaTime * idleSpeed;
        }
        transform.localEulerAngles = new Vector3(-Y, X, 0);
        transform.position = transform.localRotation * offset + target.position;
    }



}