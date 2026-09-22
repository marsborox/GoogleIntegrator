using System.IO;
using MessagePack;
using UnityEngine;
[MessagePackObject]

public class TestData
{//this is our saveFile
    [Key(0)] public string heroName;
    [Key(1)] public int level;
}
public class Test : MonoBehaviour
{
    public string heroName;
    public int level;
}

public class TestSaveSystem : MonoBehaviour
{
    Test test;

    private void Start()
    {
        test = FindFirstObjectByType<Test>();
    }
    public void Save()
    { 
        TestData data = new TestData();
        data.heroName = test.heroName;
        data.level = test.level;

        byte[] bytes = MessagePackSerializer.Serialize(data);
        string savePath = Application.persistentDataPath + "/saveHeroObject.fun";
        File.WriteAllBytes(savePath, bytes);
    }

    public void Load()
    {
        byte[] bytes = File.ReadAllBytes(Application.persistentDataPath + "/saveHeroObject.fun");
        TestData data = MessagePackSerializer.Deserialize<TestData>(bytes);
    }
}