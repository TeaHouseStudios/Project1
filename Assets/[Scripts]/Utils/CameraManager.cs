using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.TextCore.Text;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera vcam1;
    private GameObject character1;
    private GameObject character2;


    // Start is called before the first frame update
    void Start()
    {
        character1 = GameObject.FindGameObjectWithTag("Character1");
        character2 = GameObject.FindGameObjectWithTag("Character2");
        vcam1 = GameObject.FindGameObjectWithTag("Cam1").GetComponent<CinemachineVirtualCamera>();
        LookAtFirst();
    }


    private void OnEnable()
    {
        Events.onSwitch.AddListener(CharacterSwitchCam);
    }
    private void OnDisable()
    {
        Events.onSwitch.RemoveListener(CharacterSwitchCam);
    }

    void LookAtFirst()
    {
        vcam1.Follow = character1.transform;
    }

    void CharacterSwitchCam(int currentChar)
    {
        if (currentChar == 1)
        {
            vcam1.Follow = character1.transform;
        }
        else
        {
            vcam1.Follow = character2.transform;

        }
    }
}
