[System.Serializable]
public class Achievement
{
    public string id;          //nome unico de cada conquista
    public bool unlocked;      //se foi desbloqueada ou nao

    public Achievement(string id)
    {
        this.id = id;
        this.unlocked = false;
    }
}
