using System.ComponentModel;
using System.Collections.Generic;

internal class Program
{
    private static Character player;
    private static Equipment equipment;

    static void Main(string[] args)
    {
        GameDataSetting();
        DisplayGameIntro();
    }
    

    static void GameDataSetting()
    {
        // 캐릭터 정보 세팅
        player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);

        // 아이템 정보 세팅
        equipment = new Equipment("낡은 검", "낡은 창", "낡은 방패", "낡은 갑옷", 2, 3, 2, 3);
    }

    static void DisplayGameIntro()
    {
        Console.Clear();

        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("1. 상태보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                DisplayMyInfo();
                break;

            case 2:
                DisplayInventory();
                break;
        }
    }

    static void DisplayMyInfo()
    {
        Console.Clear();

        Console.WriteLine("상태보기");
        Console.WriteLine("캐릭터의 정보를 표시합니다.");
        Console.WriteLine();
        Console.WriteLine($"Lv.{player.Level}");
        Console.WriteLine($"{player.Name}({player.Job})");
        Console.WriteLine($"공격력 :{player.Atk}");
        Console.WriteLine($"방어력 : {player.Def}");
        Console.WriteLine($"체력 : {player.Hp}");
        Console.WriteLine($"Gold : {player.Gold} G");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");

        int input = CheckValidInput(0, 0);
        switch (input)
        {
            case 0:
                DisplayGameIntro();
                break;
        }
    }

    static void DisplayInventory()
    {
        Console.Clear();
        Console.WriteLine("[아이템 목록]");
        Console.WriteLine($"-1. {equipment.Sword}         |공격력 +{equipment.Swordatk}|");
        Console.WriteLine($"-2. {equipment.Spear}         |공격력 +{equipment.Spearatk}|");
        Console.WriteLine($"-3. {equipment.Shield}       |방어력 +{equipment.Shielddef}|");
        Console.WriteLine($"-4. {equipment.Armor}       |방어력 +{equipment.Armordef}|");

        
        

        int input = CheckValidInput(0, 4);
        switch (input)
        {
            case 0:
                DisplayGameIntro();
                break;
            case 1:
                Console.Clear();

                break;
           


        }
        
        



    }

    static int CheckValidInput(int min, int max)
    {
        while (true)
        {
            string input = Console.ReadLine();

            bool parseSuccess = int.TryParse(input, out var ret);
            if (parseSuccess)
            {
                if (ret >= min && ret <= max)
                    return ret;
            }

            Console.WriteLine("잘못된 입력입니다.");
        }
    }
}


public class Character
{
    public string Name { get; }
    public string Job { get; }
    public int Level { get; }
    public int Atk { get; }
    public int Def { get; }
    public int Hp { get; }
    public int Gold { get; }
    

    public Character(string name, string job, int level, int atk, int def, int hp, int gold)
    {
        Name = name;
        Job = job;
        Level = level;
        Atk = atk;
        Def = def;
        Hp = hp;
        Gold = gold;
        
    }
}

public class Equipment 
{
    public string Sword { get; }
    public string Spear { get; }
    public string Shield { get; }
    public string Armor { get; }
    public int Swordatk { get; }
    public int Spearatk { get; }
    public int Shielddef { get; }
    public int Armordef { get; }
    

    public Equipment(string sword, string spear, string shield, string armor, int swordAtk, int spearAtk, int shieldDef, int armorDef)
    {
        Sword = sword;
        Spear = spear;
        Shield = shield;
        Armor = armor;
        Swordatk = swordAtk;
        Spearatk = spearAtk;
        Shielddef = shieldDef;
        Armordef = armorDef;
        
    }

}