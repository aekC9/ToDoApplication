using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<TeamMember> teamMembers = new List<TeamMember>
            {
                new TeamMember(1,"Bahadir"),
                new TeamMember(2,"Ayşegül"),
                new TeamMember(3,"Mehmet"),
                new TeamMember(4,"Selin")
            };
            Board board = new Board();
            board.Todo.Add(new Card("Alışveriş Yap", "Market alışverişi yap", teamMembers[0], CardSize.M));
            board.Todo.Add(new Card("Rapor Yaz", "Proje raporu hazırla", teamMembers[1], CardSize.L));
            board.Todo.Add(new Card("E-mail Gönder", "Müşterilere bilgilendirme email'i gönder", teamMembers[2], CardSize.S));

            while (true)
            {
                Console.WriteLine("Lütfen yapmak istediğiniz işlemi seçiniz: ");
                Console.WriteLine("******************************************");
                Console.WriteLine("(1) Board Listelemek");
                Console.WriteLine("(2) Board'a Kart Eklemek");
                Console.WriteLine("(3) Board'dan Kart Silmek");
                Console.WriteLine("(4) Kart Taşımak");
                Console.WriteLine("(5) Çıkış");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        ListBoard(board);
                        break;
                    case 2:
                        AddCard(board, teamMembers);
                        break;
                    case 3:
                        DeleteCard(board);
                        break;
                    case 4:
                        MoveCard(board);
                        break;
                    case 5:
                        Console.WriteLine("Programdan çıkılıyor...");
                        return;
                    default:
                        Console.WriteLine("Hatalı seçim yaptınız. Lütfen tekrar deneyiniz.");
                        break;
                }
            }
            void ListBoard(Board listBoard)
            {
                Console.WriteLine("TODO Line");
                Console.WriteLine("******************************************");
                foreach (var card in listBoard.Todo)
                {
                    PrintCard(card);
                }

                Console.WriteLine("IN PROGRESS Line");
                Console.WriteLine("******************************************");
                foreach (var card in listBoard.InProgress)
                {
                    PrintCard(card);
                }

                Console.WriteLine("DONE Line");
                Console.WriteLine("******************************************");
                foreach (var card in listBoard.Done)
                {
                    PrintCard(card);
                }
            }
            void PrintCard(Card card)
            {
                Console.WriteLine($"Başlık: {card.Title}");
                Console.WriteLine($"İçerik: {card.Content}");
                Console.WriteLine($"Atanan Kişi: {card.AssignedMember.Name}");
                Console.WriteLine($"Boyut: {card.Size}");
                Console.WriteLine("-");
            }
            void AddCard(Board addCardBoard, List<TeamMember> teamMembersAC)
            {
                Console.Write("Başlık giriniz: ");
                string title = Console.ReadLine();

                Console.Write("İçerik giriniz: ");
                string content = Console.ReadLine();

                Console.WriteLine("Büyüklük seçiniz -> XS(1), S(2), M(3), L(4), XL(5)");
                int sizeInput = int.Parse(Console.ReadLine());

                if (!Enum.IsDefined(typeof(CardSize), sizeInput))
                {
                    Console.WriteLine("Hatalı bir büyüklük girdiniz!");
                    return;
                }
                CardSize size = (CardSize)sizeInput;

                Console.Write("Kişi seçiniz (ID): ");
                int memberId = int.Parse(Console.ReadLine());

                TeamMember assignedMember = teamMembersAC.FirstOrDefault(x => x.Id == memberId);
                if (assignedMember == null)
                {
                    Console.WriteLine("Hatalı bir (ID) girdiniz!");
                    return;
                }
                Card newCard = new Card(title, content, assignedMember, size);
                addCardBoard.Todo.Add(newCard);

                Console.WriteLine("Kart başarıyla eklendi!");
            }
            void DeleteCard(Board deleteCardBoard)
            {
                Console.Write("Lütfen silmek istediğiniz kartın başlığını giriniz: ");
                string title = Console.ReadLine();

                var allCards = deleteCardBoard.Todo.Concat(deleteCardBoard.InProgress).Concat(deleteCardBoard.Done).ToList();
                Card cardToDelete = allCards.FirstOrDefault(c => c.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

                if(cardToDelete == null)
                {
                    Console.WriteLine("Aradığınız kriterlere uygun kart board'da bulunamadı!");
                    Console.WriteLine("Silmeyi sonlandırmak için: (1)");
                    Console.WriteLine("Yeniden denemek için: (2)");

                    int choice = int.Parse(Console.ReadLine());
                    if(choice == 2)
                    {
                        DeleteCard(deleteCardBoard);
                    }
                    return;
                }
                deleteCardBoard.Todo.Remove(cardToDelete);
                deleteCardBoard.InProgress.Remove(cardToDelete);
                deleteCardBoard.Done.Remove(cardToDelete);

                Console.WriteLine("Kart başarıyla silindi!");
            }
            void MoveCard(Board moveCardBoard)
            {
                Console.Write("Lütfen taşımak istediğiniz kartın başlığını yazınız: ");
                string title = Console.ReadLine();

                var allCards = moveCardBoard.Todo.Concat(moveCardBoard.InProgress).Concat(moveCardBoard.Done).ToList();
                Card cardToMove = allCards.FirstOrDefault(c => c.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

                if(cardToMove == null)
                {
                    Console.WriteLine("Aradığınız kriterlere uygun kart board'da bulunamadı!");
                    Console.WriteLine("İşlemi sonlandırmak için: (1)");
                    Console.WriteLine("Yeniden denemek için: (2)");

                    int choice = int.Parse(Console.ReadLine());
                    if(choice == 2)
                    {
                        MoveCard(moveCardBoard);
                    }
                    return;
                }
                Console.WriteLine("Bulunan Kart Bilgileri: ");
                PrintCard(cardToMove);

                Console.WriteLine("Lütfen taşımak istediğiniz Line'ı seçiniz: ");
                Console.WriteLine("(1) TODO");
                Console.WriteLine("(2) IN PROGRESS");
                Console.WriteLine("(3) DONE");

                int lineChoice = int.Parse(Console.ReadLine());

                moveCardBoard.Todo.Remove(cardToMove);
                moveCardBoard.InProgress.Remove(cardToMove);
                moveCardBoard.Done.Remove(cardToMove);

                switch (lineChoice)
                {
                    case 1:
                        moveCardBoard.Todo.Add(cardToMove);
                        break;
                    case 2:
                        moveCardBoard.InProgress.Add(cardToMove);
                        break;
                    case 3:
                        moveCardBoard.Done.Add(cardToMove);
                        break;
                    default:
                        Console.WriteLine("Hatalı bir seçim yaptınız!");
                        return;
                }
                Console.WriteLine("Kart başarıyla taşındı!");
            }
        }
    }
}
