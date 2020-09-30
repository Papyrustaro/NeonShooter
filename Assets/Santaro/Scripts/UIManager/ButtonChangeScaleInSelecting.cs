using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonChangeScaleInSelecting : MonoBehaviour
{
    [SerializeField] private float expandRate = 1.1f;
    [SerializeField] private float oneRoutineTime = 2f;
    private Tween _tween;

    private void Awake()
    {
        this._tween =
            this.transform.DOScale(0.1f, 1f)
            .SetRelative(true)
            .SetEase(Ease.OutQuart)
            .SetLink(this.gameObject)
            .SetLoops(-1, LoopType.Restart)
            .OnPause(() => this._tween.Goto(0f));
        this._tween.Pause();

        InitSet();
    }

    public void OnSelect()
    {
        this._tween.Play();
    }

    public void OnDeselect()
    {
        this._tween.Pause();
    }

    private void OnDisable()
    {
        this._tween.Pause();
    }

    public void InitSet()
    {
        EventTrigger eventTrigger = GetComponent<EventTrigger>();
        if (eventTrigger == null) eventTrigger = this.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Select;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener((eventData) => this.OnSelect());
        eventTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Deselect;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener((eventData) => this.OnDeselect());
        eventTrigger.triggers.Add(entry);
    }
}
