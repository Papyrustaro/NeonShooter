using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクトプーリング用クラス
/// </summary>
public class ObjectPoolManager : MonoBehaviour
{
    private static ObjectPoolManager _instance;

    public static ObjectPoolManager Instance
    {
        get
        {
            if (_instance == null)
            {

                // シーン上から取得する
                _instance = FindObjectOfType<ObjectPoolManager>();

                if (_instance == null)
                {

                    // ゲームオブジェクトを作成しObjectPoolManagerコンポーネントを追加する
                    _instance = new GameObject("ObjectPoolManager").AddComponent<ObjectPoolManager>();
                }
            }
            return _instance;
        }
    }

    /// <summary>
    /// GameObject管理用。KeyがPrefabのInstanceID。ValueがInstanceIDのPrefabのリスト
    /// </summary>
    private Dictionary<int, List<GameObject>> pooledGameObjects = new Dictionary<int, List<GameObject>>();

    private void Awake()
    {
        if (_instance == null) _instance = this;
    }

    /// <summary>
    /// GameObjectをpooledGameObjectsから取得。必要であれば、新たにGameObjectを生成。
    /// </summary>
    /// <param name="prefab">取得したいGameObject</param>
    /// <param name="position">生成するPosition</param>
    /// <param name="rotation">生成時のRotation</param>
    /// <returns></returns>
    public GameObject InstantiateGameObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        // プレハブのインスタンスIDをkeyとする
        int key = prefab.GetInstanceID();

        // Dictionaryにkeyが存在しなければ作成する
        if (pooledGameObjects.ContainsKey(key) == false)
        {
            pooledGameObjects.Add(key, new List<GameObject>());
        }

        List<GameObject> gameObjects = pooledGameObjects[key];
        GameObject go = null;

        for (int i = 0; i < gameObjects.Count; i++)
        {

            go = gameObjects[i];
            if (go.activeInHierarchy == false)
            {
                go.transform.position = position;
                go.transform.rotation = rotation;
                go.SetActive(true);
                return go;
            }
        }

        // 使用できるものがないので新たに生成する
        go = (GameObject)Instantiate(prefab, position, rotation);

        // ObjectPoolゲームオブジェクトの子要素にする
        go.transform.parent = transform;

        gameObjects.Add(go);
        return go;
    }

    /// <summary>
    /// GameObjectを非アクティブに
    /// </summary>
    /// <param name="releaseObj">非アクティブにするGameObject</param>
    public void ReleaseGameObject(GameObject releaseObj)
    {
        releaseObj.SetActive(false);
    }

    /// <summary>
    /// 予めprefabを生成し、pooledGameObjectsにセットする
    /// </summary>
    /// <param name="prefab">予め生成するprefab</param>
    /// <param name="poolNum">生成する数</param>
    public void PoolGameObject(GameObject prefab, int poolNum)
    {
        // プレハブのインスタンスIDをkeyとする
        int key = prefab.GetInstanceID();

        // Dictionaryにkeyが存在しなければ作成する
        if (pooledGameObjects.ContainsKey(key) == false)
        {
            pooledGameObjects.Add(key, new List<GameObject>());
        }

        List<GameObject> gameObjects = pooledGameObjects[key];

        for(int i = 0; i < poolNum; i++)
        {
            GameObject go = (GameObject)Instantiate(prefab, Vector3.zero, Quaternion.identity);
            go.SetActive(false);

            // ObjectPoolゲームオブジェクトの子要素にする
            go.transform.parent = transform;

            gameObjects.Add(go);
        }
    }
}
