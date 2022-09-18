using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    int _order = 10;
    Stack<UI_PopUp> _PopUpStack = new Stack<UI_PopUp>();
    UI_Scene _SceneUI = null;


    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;
        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    //public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    //{
    //    if (string.IsNullOrEmpty(name))
    //        name = typeof(T).Name;

    //    GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");

    //    T SceneUI = Util.GetOrAddComponent<T>(go);
    //    _SceneUI = SceneUI;

    //    go.transform.SetParent(Root.transform);

    //    return SceneUI;
    //}

    public void ClosePopupUI(UI_PopUp popup)
    {
        if (_PopUpStack.Count == 0)
            return;

        if (_PopUpStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed");
            return;
        }

        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (_PopUpStack.Count == 0)
            return;

        UI_PopUp popup = _PopUpStack.Pop();
        //Managers.Resource.Destroy(popup.gameObject);
        popup = null;

        _order--;
    }

    public void CloseAllPopupUI()
    {
        while (_PopUpStack.Count > 0)
            ClosePopupUI();
    }
}