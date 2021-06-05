using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Transform m_transform;
    private Transform cam_transform;
    private int n;
    private int selectedPosition;
    private Vector3 pos;
    public Vector3[] posList;
    private const int maxPos = 5;
    public Camera m_camera;
//    public Vector3 posCamera;
    private CMove moveCam;
    private CMove moveCube;

    ///////////////////
    // Start
    void Start()
    {
        m_transform = GetComponent<Transform>();
        cam_transform = m_camera.GetComponent<Transform>();
        cam_transform.LookAt(m_transform.position);
        n = 0;
        selectedPosition = 1;
//        posCamera = new Vector3(0, 5, -10);
        posList = new Vector3[maxPos];
        posList[0] = new Vector3(-3, 3.5f, 2);
        posList[1] = new Vector3(0, 1, 0);
        posList[2] = new Vector3(0, 3, 0);
        posList[3] = new Vector3(2, 3, -1);
        posList[4] = new Vector3(2, 2, 2);
        pos = posList[selectedPosition];
        moveCam = new CMove();
        moveCube = new CMove();
        moveCam.SetActionTime(2.0f);
        moveCube.SetActionSpeed(2.0f);
    }
    
    void FixedUpdate()
    {
        if (++n == 360)
            n = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !moveCube.IsActive())
        {
            if (++selectedPosition == maxPos) selectedPosition = 0;
            pos = posList[selectedPosition];
            if (m_transform.position != pos)
            {
                moveCube.SetPositions(m_transform.position, pos);
                moveCube.StartAction();
                moveCam.SetPositions(m_transform.position, pos);
//              moveCam.SetPositions(cam_transform.position, pos + posCamera);
                moveCam.StartAction();
                //m_transform.position = pos;
            }
        }
        if (moveCube.IsActive())
        {
            moveCube.UpdatePosition();
            m_transform.position = moveCube.GetCurrentPosition();
        }
        m_transform.rotation = Quaternion.Euler(n, n, 0);
    }

    void LateUpdate()
    {
        if (moveCam.IsActive())
        {
            moveCam.UpdatePosition();
//            cam_transform.position = moveCam.GetCurrentPosition();
            cam_transform.LookAt( moveCam.GetCurrentPosition() );
        }
    }

}
