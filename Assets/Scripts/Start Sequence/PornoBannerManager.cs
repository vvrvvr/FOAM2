using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Random = UnityEngine.Random;

public class PornoBannerManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _bannerCollection;
    private PlayableDirector _playableDirector;

    private void Awake()
    {
        _playableDirector = GetComponent<PlayableDirector>();
    }

    public void BannersAppear()
    {
        _playableDirector.Play();
    }

    public void RemoveRandomBanner()
    {
        if (_bannerCollection.Count > 0)
        {
            int randomIndex = Random.Range(0, _bannerCollection.Count);
            GameObject randomObj = _bannerCollection[randomIndex];
            randomObj.SetActive(false);
            _bannerCollection.RemoveAt(randomIndex);
        }
    }

    public void TurnOffBanners()
    {
        gameObject.SetActive(false);
    }
}