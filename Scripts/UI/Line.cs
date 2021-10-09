using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private List<GameObject> _lineObjects = new List<GameObject>();

    private void OnTriggerStay2D(Collider2D collision)
    {
        print("Trigger Stay working " + collision.gameObject.name);

        if (collision.GetComponent<Unit>())
        {
            if (!_lineObjects.Contains(collision.gameObject))
            {
                _lineObjects.Add(collision.gameObject);
            }
        }
    }

    public void RemoveFromObjects(GameObject gameObject)
    {
        _lineObjects.Remove(gameObject);
    }
}
