using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendShapesHandler : MonoBehaviour
{
    // private SkinnedMeshRenderer skinnedMeshRenderer;
    //
    // void Start()
    // {
    //     skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    //     int blendShapeCount = skinnedMeshRenderer.sharedMesh.blendShapeCount;
    //     Debug.Log("Blend Shape Count: " + blendShapeCount);
    //     for (int i = 0; i < blendShapeCount; i++)
    //     {
    //         float val = Random.Range(0, 100f);
    //         skinnedMeshRenderer.SetBlendShapeWeight(i, val);
    //     }
    // }
    //  public SkinnedMeshRenderer _skinnedMeshRenderer;
    // public float _speed = 0.1f;
    // public float _waitTime = 1f;
    //
    // private float[] _blendShapeWeights;
    // private int _numBlendShapes;
    // private bool _isMoving;
    // private Coroutine _moveCoroutine;
    //
    // void Start()
    // {
    //     _numBlendShapes = _skinnedMeshRenderer.sharedMesh.blendShapeCount;
    //     _blendShapeWeights = new float[_numBlendShapes];
    //     for (int i = 0; i < _numBlendShapes; i++)
    //     {
    //         _blendShapeWeights[i] = Random.Range(0f, 100f);
    //     }
    //     _skinnedMeshRenderer.SetBlendShapeWeight(0, _blendShapeWeights[0]);
    //     _isMoving = true;
    //     _moveCoroutine = StartCoroutine(MoveBlendShapes());
    // }
    //
    // private IEnumerator MoveBlendShapes()
    // {
    //     while (_isMoving)
    //     {
    //         int randomIndex = Random.Range(0, _numBlendShapes);
    //         float randomValue = Random.Range(0f, 100f);
    //         while (Mathf.Abs(_blendShapeWeights[randomIndex] - randomValue) > 0.01f)
    //         {
    //             for (int i = 0; i < _numBlendShapes; i++)
    //             {
    //                 if (i == randomIndex)
    //                 {
    //                     if (_blendShapeWeights[i] < randomValue)
    //                     {
    //                         _blendShapeWeights[i] += _speed;
    //                     }
    //                     else
    //                     {
    //                         _blendShapeWeights[i] -= _speed;
    //                     }
    //                 }
    //                 else
    //                 {
    //                     if (_blendShapeWeights[i] > 0f)
    //                     {
    //                         _blendShapeWeights[i] -= _speed;
    //                     }
    //                 }
    //                 _skinnedMeshRenderer.SetBlendShapeWeight(i, _blendShapeWeights[i]);
    //             }
    //             yield return null;
    //         }
    //         yield return new WaitForSeconds(_waitTime);
    //     }
    // }
    //
    // void OnDestroy()
    // {
    //     if (_moveCoroutine != null)
    //     {
    //         StopCoroutine(_moveCoroutine);
    //     }
    // }
    //
}
