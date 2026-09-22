using System.Collections.Generic;
using UnityEngine;

public class RaceAndClassCollection : Singleton<RaceAndClassCollection>
{
     public static new RaceAndClassCollection instance => Singleton<RaceAndClassCollection>.instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<Race_SO> heroRaceList = new List<Race_SO>();
    public List <HeroClass_SO> heroClassList = new List<HeroClass_SO>();

    public Race_SO ReturnHeroRaceSO_FromName(string name)
    {
        Race_SO returnHeroRace = null;
        foreach(Race_SO heroRace in heroRaceList)
        {
            if(heroRace.name == name)
            {
                returnHeroRace = heroRace;
                break;
            }
        }
        return returnHeroRace;
    }
    public HeroClass_SO ReturnHeroClassSO_FromName(string name)
    {
        HeroClass_SO returnClass = null;
        foreach(HeroClass_SO heroClass in heroClassList)
        {
            if(heroClass.name == name)
            {
                returnClass = heroClass;
                break;
            }
        }
        return returnClass;
    }
}
