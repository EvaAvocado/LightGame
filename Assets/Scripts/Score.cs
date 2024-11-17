using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text _text;
    
    private int _points;
    
    public static event Action OnAddScore;
    public static event Action OnScoreReady;
    
    public void OnEnable()
    {
        ObjectLamps.OnReady += AddScore;
        ObjectFlower.OnReady += AddScore;
        ObjectGlass.OnReady += AddScore;
        ObjectBug.OnReady += AddScore;
        ObjectCandle.OnReady += AddScore;
    }

    public void OnDisable()
    {
        ObjectLamps.OnReady -= AddScore;
        ObjectFlower.OnReady -= AddScore;
        ObjectGlass.OnReady -= AddScore;
        ObjectBug.OnReady -= AddScore;
        ObjectCandle.OnReady -= AddScore;
    }

    private void AddScore()
    {
        _points++;
        _text.text = _points + "/10";
        OnAddScore?.Invoke();
        
        if (_points == 10) OnScoreReady?.Invoke();
    }
}
