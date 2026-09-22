using UnityEngine;

public class TestHero : MonoBehaviour
{
    [SerializeField] private string _name = "0000";

    public string ReturnName()
    {
        return _name;
    }
    public void SetName(string name)
    {
        _name =name;
    }
}
