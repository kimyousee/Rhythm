using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//generic object pool
public class ObjectPooler : MonoBehaviour {

	public static ObjectPooler current;
	public GameObject pooledObject;
	public int pooledAmount;
	public bool willGrow = true;

	List<GameObject> pooledObjects;

	void Awake(){
		current = this;
	}

	void Start () {
		pooledObjects = new List<GameObject>();
		for (int i = 0; i < pooledAmount; i++){
			GameObject obj = (GameObject)Instantiate(pooledObject);
			obj.SetActive(false);
			pooledObjects.Add(obj);
		}
	}

	public GameObject getPooledObject(){
		for (int i = 0; i < pooledObjects.Count;i++){
			if (pooledObjects[i].activeInHierarchy){
				return pooledObjects[i];
			}
		}
		//out of objects and we're allowed to make more
		if (willGrow){
			GameObject obj = (GameObject)Instantiate(pooledObject);
			pooledObjects.Add(obj);
			return obj;
		}
		//out of objects and can't make any more
		return null;
	}
	

}
