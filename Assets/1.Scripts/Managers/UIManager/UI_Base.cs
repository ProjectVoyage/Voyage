using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public abstract class UI_Base : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();
    public abstract void Init();

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects); // Dictionary 에 추가

        // T에 속하는 오브젝트들을 Dictionary의 Value인 objects 배열의 원소들에 하나하나 추가
        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
            {
                objects[i] = Util.FindChild(gameObject, names[i], true);
            }
            else
            {
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);
            }

            if (objects[i] == null)
                Debug.Log($"Failed to bind({names[i]})");
        }

    }

    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;

        return objects[idx] as T;
    }

    protected Text GetText(int idx) { return Get<Text>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }

    //  BindEvent 하는일
    /// go 오브젝트에 UI_EventHandler를 붙여 go 오브젝트가 이벤트 콜백을 받을 수 있도록 한다.
    //  UI_EventHandler 에 정의되어 있는 이벤트들이 발생하면 action 액션에 등록된 것들이 실행되도록 한다.
    //  UI_EventHandler의 액션 멤버 OnClickHandler, OnDragHandler
    //  람다 함수나 함수 이름, 즉 함수 포인터가 파라미터로 넘겨지고 이를 매개변수 action에서 받는다.
    //  Define에 정의된 이벤트 종류별 enum 도 같이 넘겨서 어떤 액션에 등록할 것인지를 받는다.
    //  Define.UIEvent.Click 클릭 이벤트라면 OnClickHandler에 action이 등록된다.이제 go에 마우스 클릭 이벤트가 발생하면 OnClickHandler에 등록된 액션이 실행된다.
    public static void BindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

        switch (type)
        {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action; // 혹시나 이미 있을까봐 빼줌
                evt.OnClickHandler += action;
                break;
            case Define.UIEvent.Drag:
                evt.OnDragHandler -= action; // 혹시나 이미 있을까봐 빼줌
                evt.OnDragHandler += action;
                break;
        }
    }
}