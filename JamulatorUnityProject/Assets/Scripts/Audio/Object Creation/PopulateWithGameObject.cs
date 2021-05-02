using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateWithGameObject : MonoBehaviour
{

    [SerializeField] GameObject objectToCreate;
    [SerializeField] GameObject dropArea;
    [SerializeField] int numberToDistribute;
    Vector3 dropAreaSize;

    public List<GameObject> createdObjects = new List<GameObject>();


    private void Start()
    {
        dropAreaSize = dropArea.transform.localScale;

        for (int i = 0; i < numberToDistribute; ++i)
        {
            Vector3 randPos = dropArea.transform.position +
                new Vector3(Random.Range(-dropAreaSize.x / 2, dropAreaSize.x / 2),
                            Random.Range(-dropAreaSize.y / 2, dropAreaSize.y / 2),
                            Random.Range(-dropAreaSize.z / 2, dropAreaSize.z / 2));

            var go = Instantiate<GameObject>(objectToCreate, this.transform, true);
            go.transform.position = randPos;
            go.name = this.gameObject.name + "-" + i;

            createdObjects.Add(go);
        }

        this.gameObject.name = this.gameObject.name + "(x" + numberToDistribute + ")";
    }
}
