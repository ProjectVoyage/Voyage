using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �پ��� ������ �Ŵ��� Ŭ������ �ʿ�� �ϴµ� ��� Ŭ�������� �̷� ���� �Ҵ� �� üũ �ϴ� ������ ���ϱ� ����
/// ���ʸ�(Generic) Ŭ������ ������� �Ŵ��� Ŭ������ �̱��� ������ ������ �ս��� ��ӹ޾� ����ϴ� ���
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
                    "'�� ��ü�� �̹� �� ���α׷������� �ı��Ǿ����ϴ�." +
                    "���� �ٽ� �����ϸ� �ȵ˴ϴ�. - Null���� ��ȯ�մϴ�.");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogError("[Singleton] �����ΰ� �����Ǿ���." +
                            " _ �̱����� ���� 1�� �̻� ������ �ȵȴ�!" +
                            "������� �غ��� �ذ��� ������");
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
                        "�� ��ü�� Scene�� �ʿ��մϴ�, '" + singleton +
                        "�� DontDestroyOnLoad �� �����մϴ�.");
                }
                else
                {
                    Debug.Log("[Singleton] �̹� �����Ǿ� ������� ��ü: " +
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
