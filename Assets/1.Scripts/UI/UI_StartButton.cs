using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class UI_StartButton : UI_Base
{
    //enum GameObjects
    //{
    //   StartText,
    //}

    enum Buttons 
    {
        StarButton,
    }

    enum Texts 
    {
        StarText,
    }
   

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));


    }
    
    
    string _name;
    public void OnButtonCliked(PointerEventData data)
    {
        //Todo

    }
}
