using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour
{
    public EnemyPool PoolManager { get; set; }

   public void DestroyOrRelease()
    {
        if (PoolManager != null)
        {
            Debug.Log("Release");
            PoolManager.ReleaseGameObject(gameObject);
        }
        else
        {
            Debug.Log("Destroy");
            Destroy(gameObject);
        }

    }
}
