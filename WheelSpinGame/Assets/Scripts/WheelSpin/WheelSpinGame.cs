using System;
using Tween;
using UnityEngine;

public class WheelSpinGame : MonoBehaviour
{
    [SerializeField] private PageTweenController playgroundPageTweenController;
    [SerializeField] private PageTweenController losePageTweenController;

    private void Awake()
    {
        Exit();
    }

    public void Spin()
    {
        
    }

    public void Exit()
    {
        playgroundPageTweenController.ShowOut();
    }

    public void Bomb()
    {
        playgroundPageTweenController.ShowOut();
    }
}
