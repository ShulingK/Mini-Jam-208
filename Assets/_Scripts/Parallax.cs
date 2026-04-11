using System;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Serializable]
    struct LayerParallax
    {
        public LayerMask _layerMask;
        public float _parallaxMultiplier;
    }


    [SerializeField] private Transform cameraTransform; 
    [SerializeField] private LayerParallax[] _layers;

    private Vector3 lastCameraPosition;

    private void Start()
    {
        lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        foreach (var layer in _layers)
        {
            foreach (GameObject obj in FindObjectsByType<GameObject>(FindObjectsSortMode.None))
            {
                if (((1 << obj.layer) & layer._layerMask) != 0)
                {
                    obj.transform.position += new Vector3(
                        deltaMovement.x * layer._parallaxMultiplier,
                        0f,
                        0f
                    );
                }
            }
        }

        lastCameraPosition = cameraTransform.position;
    }
}