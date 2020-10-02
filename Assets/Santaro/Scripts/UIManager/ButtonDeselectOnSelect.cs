using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonDeselectOnSelect : MonoBehaviour
{
    private Button _button;
    private void Awake()
    {
        _button = GetComponent<Button>();

        EventTrigger eventTrigger = GetComponent<EventTrigger>();
        if (eventTrigger == null) eventTrigger = this.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        //entry.eventID = EventTriggerType.;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener((eventData) => { _button.enabled = false; StartCoroutine(SantaroCoroutineManager.DelayMethod(1, () => _button.enabled = true)); });
        eventTrigger.triggers.Add(entry);
    }
}
