namespace ConsoleApp21
{
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
    public class Item
    {
        public string Name { get; }
        public string Description { get; }
        public int Type { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public bool IsEquipped { get; set; }

        public static int ItemCount = 0;

        public Item(string name, string description, int type, int atk, int def, int hp, bool isEquipped = false)
        {
            Name = name;
            Description = description;
            Type = type;
            Atk = atk;
            Def = def;
            Hp = hp;
            IsEquipped = isEquipped;

        }
        
        public void PrintItemStatDescription(bool withNumber = false, int idx = 0)
        {
            Console.Write("- ");
            if(withNumber)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("{0}", idx);
                Console.ResetColor();
            }
            if (IsEquipped)
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("E");
                Console.ResetColor();
                Console.Write("]");
                Console.Write(PadRightForMixedText(Name, 9));
            }
            else Console.Write(PadRightForMixedText(Name, 12));
            Console.Write(" | ");

            //(Atk >= 0 ? "+" : "") [조건 ? 조건이 참이라면 : 조건이 거짓이라면]  삼항연산자
            if(Atk != 0) Console.Write($"Atk{(Atk>=0? "+" : "")}{Atk}");
            if(Def != 0) Console.Write($"Def{(Def>=0? "+" : "")}{Def}");
            if(Hp != 0) Console.Write($"Hp{(Hp>=0? "+" : "")}{Hp}");

            Console.Write(" | ");
            Console.WriteLine(Description);
        }
        public static int GetPrintableLength(string str)
        {
            int length = 0;
            foreach (char c in str)
            {
                if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                {
                    length += 2; // 한글과 같은 넓은 문자에 대해 길이를 2로 취급
                }
                else
                {
                    length += 1; // 나머지 문자에 대해 길이를 1로 취급
                }
            }

            return length;
        }

        public static string PadRightForMixedText(string str, int totalLength)
        {
            int currentLength = GetPrintableLength(str);
            int padding = totalLength - currentLength;
            return str.PadRight(str.Length + padding);
        }
    }
    internal class Program
    {
        static Character player;
        static Item[] items;
        static void Main(string[] args)
        {
            //구성
            //0.데이터초기화
            //1.스타팅로고를 보여줌(게임 처음 킬 때만)
            //2.선택화면을 보여줌 상태/인벤토리
            //3.상태화면을 구현함 캐릭터/아이템
            //4. 인벤토리 화면 구현함
            //4-1.장비장착 화면 구현
            //5.선택화면 확장
            GameDataSetting();
            PrintStartLogo();
            StartMenu();
        }

        static void StartMenu()
        {
            //구성
            //0.화면정리
            //1.선택 멘트를 줌
            //2.선택 결과값을 검증
            //3.선택 결과에 따라 메뉴로 보내줌
            Console.Clear();
            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
            Console.WriteLine("");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("");

            //유저들이 착할때만? 
            switch(CheckValidInput(1, 2))
            {
                case 1:
                    StatusMenu();
                    break;
                case 2:
                    InventoryMenu();
                    break;
            }
        }

        private static void InventoryMenu()
        {
            Console.Clear();
            ShowHighlightedText("■인벤토리■");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다. ");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");

            for(int i = 0; i < Item.ItemCount; i++) 
            {
                items[i].PrintItemStatDescription();
            }
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 장착관리");
            Console.WriteLine(""); ;
            switch (CheckValidInput(0, 1))
            {
                case 0:
                    StatusMenu();
                    break;
                case 1:
                    EquipMenu();
                    break;
            }

        }

        private static void EquipMenu()
        {
            Console.Clear();

            ShowHighlightedText("■인벤토리 - 장착관리■");
            Console.WriteLine("보유중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < Item.ItemCount; i++)
            {
                items[i].PrintItemStatDescription(true, i+1);
            }
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");

            int keyInput = CheckValidInput(0, Item.ItemCount);

            switch (keyInput)
            {
                case 0:
                    InventoryMenu();
                    break;
                default://default 모든 케이스가 아니여서 마지막 case 만 볼 때 사용
                    ToggleEquipStatus(keyInput - 1); // 유저가 입력하는건 1, 2, 3 : 실제 배열에는 0, 1, 2...
                    EquipMenu();
                    break;
            }
        }

        private static void ToggleEquipStatus(int idx)
        {
            items[idx].IsEquipped = !items[idx].IsEquipped;
        }

        private static void StatusMenu()
        {
            Console.Clear();
            ShowHighlightedText("■상태보기■");
            Console.WriteLine("캐릭터의 정보가 표기됩니다.");

            PrintTextWithHighlights("Lv. ", player.Level.ToString("00"));
            Console.WriteLine("");
            Console.WriteLine("{0} ({1})", player.Name, player.Job);

            int bonusAtk = getSumBonusAtk();
            PrintTextWithHighlights("공격력 : ", (player.Atk+ bonusAtk).ToString(), bonusAtk > 0 ? string.Format("(+{0})", bonusAtk) : "");
            int bonusDef = getSumBonusDef();
            PrintTextWithHighlights("방어력 : ", (player.Def + bonusDef).ToString(), bonusDef > 0 ? string.Format("(+{0})", bonusDef) : "");
            int bonusHp = getSumBonusHp();
            PrintTextWithHighlights("체력 : ", (player.Hp + bonusHp).ToString(), bonusHp > 0 ? string.Format("(+{0})", bonusHp) : "");
            PrintTextWithHighlights("골드 : ", player.Gold.ToString());
            Console.WriteLine("");
            Console.WriteLine("0. 뒤로가기");
            Console.WriteLine("");
            switch (CheckValidInput(0, 0))
            {
                case 0:
                    StartMenu();
                    break;
            }
        }

        private static int getSumBonusAtk()
        {
            int sum = 0;
            for(int i = 0; i < Item.ItemCount; i++)
            {
                if (items[i].IsEquipped) sum += items[i].Atk;
            }
            return sum;
        }

        private static int getSumBonusDef()
        {
            int sum = 0;
            for (int i = 0; i < Item.ItemCount; i++)
            {
                if (items[i].IsEquipped) sum += items[i].Def;
            }
            return sum;
        }

        private static int getSumBonusHp()
        {
            int sum = 0;
            for (int i = 0; i < Item.ItemCount; i++)
            {
                if (items[i].IsEquipped) sum += items[i].Hp;
            }
            return sum;
        }

        private static void ShowHighlightedText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        private static void PrintTextWithHighlights(string s1, string s2, string s3 = "")
        {
            Console.Write(s1);
            Console.ForegroundColor= ConsoleColor.Yellow;
            Console.Write(s2);
            Console.ResetColor();
            Console.WriteLine(s3);
        }




        private static int CheckValidInput(int min, int max)
        {
            //아래 두가지 상황은 비정상->재입력 수행
            //1. 숫자가 아닌 입력을 받은 경우
            //2. 숫자가 최소값 - 최대값의 범위를 넘는 경우
            int keyInput; // tryParse에 필요
            bool result; //while에 필요\
            do//일단 한 번 실행을 보장

            {
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                result = int.TryParse(Console.ReadLine(), out keyInput);
            } while (result==false || CheckIfValid(keyInput, min, max)==false);

            //여기에 왔다는 것은 제대로 입력을 받았다는 것
            return keyInput;

        }

        private static bool CheckIfValid(int keyInput, int min, int max)
        {
            if (min <= keyInput && keyInput <= max) return true;
            return false;
        }

        private static void PrintStartLogo()
        {
            Console.WriteLine(" _|_|_|_|_|                      _|            _|_|_|                                      ");

            Console.WriteLine("     _|      _|_|    _|    _|  _|_|_|_|      _|          _|_|_|  _|_|_|  _|_|      _|_|    ");

            Console.WriteLine("     _|    _|_|_|_|    _|_|      _|          _|  _|_|  _|    _|  _|    _|    _|  _|_|_|_|  ");

            Console.WriteLine("     _|    _|        _|    _|    _|          _|    _|  _|    _|  _|    _|    _|  _|        ");

            Console.WriteLine("     _|      _|_|_|  _|    _|      _|_|        _|_|_|    _|_|_|  _|    _|    _|    _|_|_|   ");

            Console.WriteLine("=============================================================================================");
            Console.WriteLine("                                      PRESS ANYKEY TO START                                  ");
            Console.WriteLine("=============================================================================================");
            Console.ReadKey();
        }

        static void GameDataSetting()
        {
            // 캐릭터 정보 세팅
            player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);

            // 아이템 정보 세팅
            items = new Item[10];
            AddItem(new Item("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 0, 5, 0));
            AddItem(new Item("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", 1, 2, 0, 0));

        }

        static void AddItem(Item item)
        {
            if (Item.ItemCount == 10) return;//10이면 아무일도 안일어남
            items[Item.ItemCount] = item; // 0개 -> 0번 인덱스 / 1개 -> 1번 인덱스
            Item.ItemCount++;
        }
    }
}