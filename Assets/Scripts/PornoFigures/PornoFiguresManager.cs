using UnityEngine;

public class PornoFiguresManager : MonoBehaviour
{
    [SerializeField] private Animator[] _animators = new Animator[0];
    //private static readonly int Number1 = Animator.StringToHash("Number");

    private void Start()
    {
        for (int i = 0; i < _animators.Length; i++)
        {
            _animators[i].SetInteger("Number", i);
        }
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.H))
    //     {
    //         StartAnimations();
    //     }
    //     if (Input.GetKeyDown(KeyCode.J))
    //     {
    //         StopAnimations();
    //     }
    // }


    public void StartAnimations()
    {
        foreach (var anim in _animators)
        {
            anim.SetBool("isWorking", true);
        }
    }
    public void StopAnimations()
    {
        foreach (var anim in _animators)
        {
            anim.SetBool("isWorking", false);
        }
    }
}
