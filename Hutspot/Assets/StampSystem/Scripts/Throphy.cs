using UnityEngine;
using UnityEngine.UI;

public class Throphy : MonoBehaviour
{
    [SerializeField] private int _trophyId;
    public Image trophyImage;

    public void ShowTrophy()
    {
        trophyImage.color = Color.green;
    }

    public int GetTrophyID()
    {
        return _trophyId;
    }
}
