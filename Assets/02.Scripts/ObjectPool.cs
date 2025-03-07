using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
   
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize;
    [SerializeField] private Transform parent;

    private Queue<GameObject> pool;
    
    private static ObjectPool instance;
    public static ObjectPool Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
        pool = new Queue<GameObject>();
    }

    /// <summary>
    /// ObjectPool에 새로운 오브젝트 생성 메서드
    /// </summary>
    private void CreateNewObject()
    {
        GameObject newObject = Instantiate(prefab, parent);
        newObject.SetActive(false);
        pool.Enqueue(newObject);
    }

    /// <summary>
    /// ObjectPool에 있는 오브젝트 반환 메서드
    /// </summary>
    /// <returns></returns>
    public GameObject GetObject()
    {
        if(pool.Count == 0) CreateNewObject();
        
        GameObject dequeueObject = pool.Dequeue();
        dequeueObject.SetActive(true);
        return dequeueObject;
    }

    /// <summary>
    /// 사용한 오브젝트를 ObjectPool로 되돌려주는 메서드
    /// </summary>
    /// <param name="returnObject">반환할 오브젝트</param>
    public void ReturnObject(GameObject returnObject)
    {
        returnObject.SetActive(false);
        pool.Enqueue(returnObject);
    }

}