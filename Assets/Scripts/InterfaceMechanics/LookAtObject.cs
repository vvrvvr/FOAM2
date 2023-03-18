using System.Collections;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    [SerializeField] private Transform lookingObj;
    [SerializeField] private float lookTime = 0.2f;
    [SerializeField] private float lookInterval = 1f;
    [SerializeField] private bool isActive = true;
    private float[] intervals = new[] { 1f, 0.3f, 2f, 0.4f, 2.5f, 0.6f };
    private float timer = 0f;

    void Update()
    {
        if (isActive)
        {
            timer += Time.deltaTime;

            if (timer >= lookInterval)
            {
                timer = 0f;
                StartCoroutine(LookCoroutine());
                lookInterval = RandomExtensions.GetRandomElement(intervals);
            }
        }
    }

    IEnumerator LookCoroutine()
    {
        Quaternion startRotation = transform.rotation;
        Vector3 targetDirection = (lookingObj.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        float t = 0f;
        while (t < lookTime)
        {
            t += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t / lookTime);
            yield return null;
        }

        transform.rotation = targetRotation;
    }
}