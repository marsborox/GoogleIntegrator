using System.Collections.Generic;
using UnityEngine;

public class DiceCollection : Singleton<DiceCollection>
//public class DiceCollection
{
    public static new DiceCollection instance => Singleton<DiceCollection>.instance;
    public string testString = "dice";
    public Dice d2 = new Dice(DiceType.D2,"d2",1,2);
    public Dice d3 = new Dice(DiceType.D3,"d3",1,3);
    public Dice d4 = new Dice(DiceType.D4,"d4",1,4);
    public Dice d6 = new Dice(DiceType.D6,"d6",1,6);
    public Dice d8 = new Dice(DiceType.D8,"d8",1,8);
    public Dice d10 = new Dice(DiceType.D10,"d10",0,9);
    public Dice d12 = new Dice(DiceType.D12,"d12",1,12);
    public Dice d20 = new Dice(DiceType.D20,"d20",1,20);
    public Dice d100 = new Dice(DiceType.D100,"d100",0,99);

    public List<Dice> diceList = new List<Dice>();
    [System.Serializable]
    public class Dice
    {
        public DiceType diceType;
        public string name;
        public int rollMin;
        public int rollMax;
       
        public Dice(DiceType inputType,string inputName,int inputRollMin, int inputRollMax)
        {
            diceType = inputType;
            name = inputName;
            rollMin = inputRollMin;
            rollMax = inputRollMax;
        }
        public int RollDice()
        {
            return Random.Range(rollMin,rollMax+1);
        }
    }
    void Awake()
    {
        base.Awake();
        AddDiceToList();
    }
    public Dice ReturnDicePerType(DiceType diceType)
    {
        Dice returnDice = null;
        foreach(Dice dice in diceList)
        {
            if (diceType == dice.diceType)
            returnDice = dice;
        }
        //Debug.Log("Dice is: "+((returnDice==null)? "null" : "notNull"));
        return returnDice;
    }
    public string ReturnDiceNamePerType(DiceType diceType)
    {
        Dice returnDice = null;
        foreach(Dice dice in diceList)
        {
            if (diceType == dice.diceType)
            returnDice = dice;
        }
        return returnDice.name;
    }
    private void AddDiceToList()
    {
        diceList.Add(d2);
        diceList.Add(d3);
        diceList.Add(d4);
        diceList.Add(d6);
        diceList.Add(d8);
        diceList.Add(d10);
        diceList.Add(d12);
        diceList.Add(d20);
        diceList.Add(d100);
    }
    private int RollDice(Dice dice)
    {
        return Random.Range(dice.rollMin,dice.rollMax+1);
    }
    #region  RollDice
    public int RollD2()
    {
        return d2.RollDice();
    }
    public int RollD3()
    {
        return d3.RollDice();
    }
    public int RollD4()
    {
        return d4.RollDice();
    }
    public int RollD6()
    {
        return d6.RollDice();
    }
    public int RollD8()
    {
        return d8.RollDice();
    }
    public int RollD10()
    {
        return d10.RollDice();
    }
    public int RollD12()
    {
        return d12.RollDice();
    }
    public int RollD20()
    {
        return d20.RollDice();
    }    
    public int RollD100()
    {
        return d100.RollDice();
    }                
    #endregion
}
