[System.Serializable]
public class TrophyData
{
    public int[] _collectedTrophies;

    public int [] GetCollectedTrophies()
    {
        return _collectedTrophies;
    }

    public void SetCollectedTrophies(int[] array)
    {
        _collectedTrophies = array;
    }
}
