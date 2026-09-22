using UnityEngine;

public class HeroManager : Singleton<HeroManager>
{
    public static new HeroManager instance => Singleton<HeroManager>.instance;
    public Hero hero;
    
}
