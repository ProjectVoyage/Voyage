using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class UI_StartButton : UI_Base
{
    enum GameObjects
    {
       StarButton,
       StartText,
    }
    string _name;

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));


    }
   
    public void OnButtonCliked(PointerEventData data)
    {
        //Todo

    }
}
