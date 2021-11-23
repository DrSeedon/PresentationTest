using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{

    public bool IsLock = false;
    public int Id = 0;
    public GameObject[] Panels;
    public GameObject[] PanelsCanvas;
    public GameObject[] PanelsScroll;
    public Transform target;
    public Vector3 newPosition;
    public Vector3 oldPosition;

    public float range = 1f;
    public float speed = 5f;
    public float rotationSpeed = 5f;

    private void Start()
    {
        newPosition = transform.position;
        target = GetComponent<CameraRotateAround>().target;
    }

    private void Update()
    {

        if (!IsLock)
            return;

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * speed);

        Quaternion lookOnLook = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime * rotationSpeed);
    }

    private void MoveCamera()
    {
        target = Panels[Id].transform;

        oldPosition = transform.position;

        newPosition = target.position;
        newPosition += target.up * range;

    }

    public void ClickPanel(int id)
    {
        Id = id;
        IsLock = true;
        OpenPanel();
        MoveCamera();
    }

    public void OpenPanel()
    {
        PanelsCanvas[Id].SetActive(true);
        ScrollRect mScrollRect;
        mScrollRect = PanelsScroll[Id].GetComponent<ScrollRect>();
        mScrollRect.normalizedPosition = new Vector2(0, 1);
    }

    public void ClosePanel()
    {
        PanelsCanvas[Id].SetActive(false);
        newPosition = oldPosition;
        Invoke("UnlockMoving", 0.7f);
        target = GetComponent<CameraRotateAround>().target;
    }

    private void UnlockMoving()
    {
        IsLock = false;
    }

}
