using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class GeometryObjectModel : MonoBehaviour
{
    public string ObjectType;
    public int MinClicksCount;
    public int MaxClicksCount;
    public Color Color;
    public int ClickCount;

    public List<ClickColorData> Click;
    public int ObservableTime;
    private IDisposable _update;

    void Start()
    {
        LoadData();

        _update = Observable.Interval(TimeSpan.FromMilliseconds(ObservableTime)).Subscribe(x =>
        {
            ChangeColor();
        });
    }

    void Update()
    {
        ClickManage();
    }

    void ClickManage()
    {
        if(ClickCount >= MinClicksCount && ClickCount <= MaxClicksCount)
        {
            this.GetComponent<MeshRenderer>().material.color = Color;
        }
    }

    void ChangeColor()
    {
        this.GetComponent<MeshRenderer>().material.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    void LoadData()
    {
        for (int i = 0; i < Click.Count; i++)
        {
            if (ObjectType == Click[i].ObjectType)
            {
                MinClicksCount = Click[i].MinClicksCount;
                MaxClicksCount = Click[i].MaxClicksCount;
                Color = Click[i].Color;
            }
        }
    }
}
