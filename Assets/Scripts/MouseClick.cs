using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    public MainController cam;
    public int Id;

    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<MainController>();
    }

    private void OnMouseUpAsButton()
    {
        if (!cam.GetComponent<MainController>().IsLock)
            cam.ClickPanel(Id);
    }

}
