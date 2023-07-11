using System;
using System.Collections;
using System.Collections.Generic;
using CortexDeveloper.ECSMessages.Service;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private Button _button;

    // get panel
    public GameObject panel;

    /*Phương thức Awake() được gọi lần đầu tiên khi đối tượng chứa script này được khởi tạo và kích hoạt 
    trong một scene của Unity. Điều này xảy ra khi scene bắt đầu chạy hoặc khi đối tượng được tạo ra thông qua code. */
    private void Awake()
    {
        Debug.Log("aaaa") ;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(StartGame);
    }

    /*Phương thức OnDestroy() được gọi khi đối tượng chứa script này bị hủy, tức là khi đối tượng bị xóa khỏi scene hoặc khi trò chơi kết thúc. */
    private void OnDestroy()
    {
        Debug.Log("bbbb");
        _button.onClick.RemoveListener(StartGame);
    }

    private void StartGame()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        MessageBroadcaster
            .PrepareMessage()
            .AliveForUnlimitedTime()
            .PostImmediate(entityManager,
                new StartCommand
                {
                    IsStart = true
                });

        // turn UI off
        TurnUIOff();
    }

    private void TurnUIOff()
    {
        Debug.Log("TestTurnoff");
        this.gameObject.SetActive(false);
        panel.SetActive(false);
    }
}
