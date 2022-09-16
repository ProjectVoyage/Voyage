using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 다양한 종류의 매니저 클래스를 필요로 하는데 모든 클래스마다 이런 동적 할당 및 체크 하는 로직을 피하기 위해
/// 제너릭(Generic) 클래스를 기반으로 매니저 클래스에 싱글톤 패턴의 로직을 손쉽게 상속받아 사용하는 방법
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static bool applicationIsQutting = false;
    private static object _lock = new object();

    public static T Instance
    {
        get
        {
            if (applicationIsQutting)
            {
                Debug.LogWarning("[Singleton'" + typeof(T) +
                    "'의 객체는 이미 이 프로그램에서는 파괴되었습니다." +
                    "절대 다시 생성하면 안됩니다. - Null값만 반환합니다.");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogError("[Singleton] 무엇인가 절못되었다." +
                            " _ 싱글톤은 절대 1개 이상 있으면 안된다!" +
                            "재시작을 해보면 해결할 수있음");
                        return _instance;
                    }
                }

                if (_instance == null)
                {
                    GameObject singleton = new GameObject();
                    _instance = singleton.AddComponent<T>();
                    singleton.name = "(singleton)" + typeof(T).ToString();

                    DontDestroyOnLoad(singleton);

                    Debug.Log("[Singleton]" + typeof(T) +
                        "의 객체가 Scene에 필요합니다, '" + singleton +
                        "을 DontDestroyOnLoad 로 생성합니다.");
                }
                else
                {
                    Debug.Log("[Singleton] 이미 생성되어 사용중인 객체: " +
                        _instance.gameObject.name);
                }
            }
            return _instance;
        }
    }

    public void OnDestroy()
    {
        applicationIsQutting = true;
    }
}
