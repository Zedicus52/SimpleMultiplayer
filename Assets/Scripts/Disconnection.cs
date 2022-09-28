using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Disconnection : MonoBehaviour
{
    [SerializeField] private Button leave;

    private void OnEnable()
    {
        leave.onClick.AddListener(NetworkManager.Instance.LeaveFromRoom);
    }

    private void OnDisable()
    {
        leave.onClick.RemoveListener(NetworkManager.Instance.LeaveFromRoom);
    }
}
