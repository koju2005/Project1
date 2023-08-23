using System;
using System.Threading;

namespace ConsoleApp11
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Warrior warrior = new Warrior();
            Shop shop = new Shop();
            shop.ShopItem();
            while (true)
            {
                warrior.ShowMainMenu();
                string input = Console.ReadLine();
                if (input == "1") // 인포보기
                {
                    Console.Clear();
                    warrior.ShowPlayerInfo();
                    string input1 = Console.ReadLine();

                    if (input1 == "0") //돌아가기
                    {
                        Console.Clear();
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.WriteLine("메인 메뉴로 돌아갑니다.");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("*아무 키나 눌러주세요*");
                        Console.ResetColor();
                        Console.ReadKey();
                    }

                }

                else if (input == "2") // 장비보기
                {
                    while (true)
                    {
                        warrior.ShowInven();

                        int selectedItemIndex = int.Parse(Console.ReadLine()) - 1; // 필요에 따라 조정
                        if (selectedItemIndex > -1)
                        {
                            Item selectedItem = warrior.ITEM[selectedItemIndex];

                            warrior.Equip(selectedItem); // 선택한 아이템 
                        }
                        else
                        {
                            Console.Clear();
                            break;
                        }
                    }
                }

                else if (input == "3") // 아이템 구매 , 판매
                {
                    Console.Clear ();
                    shop.ShowShop();
                    Console.WriteLine("");
                    Console.WriteLine("보유 골드: ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("{0}", warrior.Money);
                    Console.ResetColor();
                    Console.WriteLine("");
                    Console.WriteLine("");

                    while (true)
                    {
                        int selectedItemIndex = int.Parse(Console.ReadLine()) - 1;
                        if (selectedItemIndex > -1)
                        {
                            Item selectedItem = shop.SHOPITEM[selectedItemIndex];

                            if (warrior.ITEM.Contains(selectedItem))
                            {
                                Console.WriteLine("이미 구매하였습니다.");
                                Console.ReadKey();
                                continue;
                            }
                            else
                            {
                                if (warrior.Money < selectedItem.Gold)
                                {
                                    Console.WriteLine("골드가 부족합니다.");
                                    Console.ReadKey();
                                    continue;
                                }
                                else
                                {
                                    warrior.ITEM.Add(selectedItem);
                                    selectedItem.IsBuy = true;
                                    warrior.Money -= selectedItem.Gold;
                                    break;
                                }
                            }
                        }
                        else { Console.Clear(); break; }
                    }
                }
                else 
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("*아무 키나 눌러주세요*");
                    Console.ResetColor();
                    Console.ReadKey();
                }
                Console.Clear();
            }
            
        }

    }

    public class Warrior
    {
        public string Chad;
        public int Level;
        public int AttackPoint;
        public int Shelid;
        public int Hp;
        public int Money;
        public List<Item> ITEM = new List<Item>(); //내 템창에 있는 아이템

       public Warrior()
        {
            Chad = "김훈";
            Level = 1;
            AttackPoint = 10;
            Shelid = 5;
            Hp = 100;
            Money = 1500;
            InvenItem();
            
        }

       public void ShowPlayerInfo()
        {

                Console.WriteLine("");
                Console.WriteLine("Lv     : {0} ", Level);
                Console.WriteLine("Chad   : {0} ", Chad);
                Console.WriteLine("공격력 : {0} ", AttackPoint);
                Console.WriteLine("방어력 : {0}", Shelid);
                Console.WriteLine("생명력 : {0} ", Hp);
                Console.WriteLine("Gold   : {0} ", Money);
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");
                Console.WriteLine("원하시는 행동을 입력해주세요");
                Console.WriteLine("===================================");
                Console.Write(">>");

        }
        public void ShowInven()
        {
            Console.Clear();
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("인벤토리 ");
            Console.ResetColor();
            Console.WriteLine("- 장착관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[아이템 목록]");//나중에  for문이나 foreach문으로 아이템많아지면 목록 불러오기할수있음
            Console.ResetColor();
            int index = 1;
            foreach (Item item in ITEM)
            {
                Console.Write("{0}.",index);
                if (item.IsEquip)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("[E]");
                    Console.ResetColor();
                }
                Console.WriteLine(" {0}\t|공격력: {1}\t|방어력: {2}\t|생명력: {3}\t|설명: {4}", item.Name,item.Atk,item.Def,item.Hp,item.Comment);
                index++;
            }
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.WriteLine("===================================");
            Console.Write(">>");

        }
        public void ShowMainMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다");
            Console.WriteLine("");
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("");
            Console.WriteLine("3. 상점방문");
            Console.WriteLine("");
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.WriteLine("====================================================");
            Console.Write(">>");
        }
        public void InvenItem() 
        {

            Item 무쇠갑옷 = new Item("무쇠갑옷",0,5,0,500,"무쇠로 만들어져 튼튼한 갑옷입니다.",false,"", false);
            ITEM.Add(무쇠갑옷);
            Item 낡은검 = new Item("낡은검", 2, 0, 0, 500, "쉽게 볼 수 있는 낡은 검입니다.",false , "", false);
            ITEM.Add(낡은검);
        }

        public void Equip(Item item)
        {
            if (item.IsEquip == false)
            {
                AttackPoint += item.Atk;
                Shelid += item.Def;
                Hp += item.Hp;
                item.EquipSym = "[E]";
                item.IsEquip = true; // 아이템을 장착 상태로 변경
                Console.WriteLine("{0} 아이템을 장착했습니다.", item.Name);
            }
            else
            {
                AttackPoint -= item.Atk;
                Shelid -= item.Def;
                Hp -= item.Hp;
                item.EquipSym = "";
                item.IsEquip = false; // 아이템을 장착 해제 상태로 변경
                Console.WriteLine("{0} 아이템을 해제했습니다.", item.Name);
            }
  
        }
    }
    public class Shop
    {
        public List<Item> SHOPITEM = new List<Item>(); //상점에 있는 아이템
        Warrior warrior = new Warrior();
        public void ShowShop()
        {
            Console.WriteLine("==========================");
            Console.WriteLine("");
            Console.WriteLine("");
            int index = 1;
            foreach (Item item in SHOPITEM)
            {
                Console.Write("{0}.", index);
                if (item.IsBuy)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("[$]");
                    Console.ResetColor();
                }
                Console.WriteLine(" {0}\t|공격력: {1}\t|방어력: {2}\t|생명력: {3}\t|가격: {4}\t|설명: {5}", item.Name, item.Atk, item.Def, item.Hp,item.Gold,item.Comment);
                index++;
            }
            Console.WriteLine("");
            Console.WriteLine("무엇을 구매할까요?");
            Console.WriteLine("");
            Console.WriteLine("0.메인메뉴");
            Console.WriteLine("==========================");
        }

        public void ShopItem() //list에 추가할 아이템 추가
        {
            Item 강철검 = new Item("강철검", 10, 0, 0, 500, "단단한 강철로 만들어진 검입니다.", false, "", false);
            SHOPITEM.Add(강철검);
            Item 강철갑옷 = new Item("강철갑옷", 0, 0, 10, 500, "단단한 강철로 만들어진 갑옷입니다.", false, "", false);
            SHOPITEM.Add(강철갑옷);
            Item 전설의갑옷 = new Item("전설의갑옷", 0, 5000, 500, 5000, "전설의 갑옷이라고한다.", false, "", false);
            SHOPITEM.Add(전설의갑옷);
            Item 정화된데몬슬레이어 = new Item("정화된 데몬 슬레이어", 1000, 0, 0, 50, "누군가의 코와 닮은 검이다...", false, "", false);
            SHOPITEM.Add(정화된데몬슬레이어);
        }
    }
    public class Item
    {
        public string Name { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Gold { get; }
        public string Comment { get; }
        public string EquipSym { get; set; }
        public bool IsEquip{ get; set; }
        public bool IsBuy { get; set; }
        public Item(string name, int atk, int def, int hp, int gold,string comment,bool isequip,string equipsym,bool isbuy) //equipssym 필요없을듯
        {
            Name = name;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
            Comment = comment;
            IsEquip = isequip;  
            EquipSym = equipsym;
            IsBuy = isbuy;
        }
    }

}