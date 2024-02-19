using System;
using System.IO;

namespace MiniText
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MiniText Metin Düzenleyiciye Hoş Geldiniz!");

            while (true)
            {
                string userInput = GetFilePathFromUser();

                if (userInput.ToLower() == "exit")
                {
                    Console.WriteLine("Programdan çıkılıyor...");
                    break;
                }

                if (File.Exists(userInput))
                {
                    ProcessUserInput(userInput);
                }
                else
                {
                    Console.WriteLine("Dosya bulunamadı. Lütfen geçerli bir dosya yolu girin.");
                }
            }
        }

        static string GetFilePathFromUser()
        {
            Console.WriteLine("Lütfen bir dosya yolu girin (Çıkmak için 'exit' yazın):");
            return Console.ReadLine();
        }

        static void ProcessUserInput(string filePath)
        {
            string extension = Path.GetExtension(filePath);

            if (extension == ".txt" || extension == ".doc" || extension == ".docx")
            {
                string fileContent = File.ReadAllText(filePath);
                Console.WriteLine("Dosya İçeriği:");
                Console.WriteLine(fileContent);

                PerformEditingOperations(ref fileContent);
            }
            else
            {
                Console.WriteLine("Geçersiz dosya formatı. Lütfen bir metin dosyası seçin.");
            }
        }

        static void PerformEditingOperations(ref string content)
        {
            while (true)
            {
                Console.WriteLine("\nMetin Düzenleme İşlemleri:");
                Console.WriteLine("1. Metin İçinde Arama");
                Console.WriteLine("2. Metin İçinde Değiştirme");
                Console.WriteLine("3. Metin İçinde Silme");
                Console.WriteLine("4. Ana Menüye Geri Dön");

                Console.Write("\nLütfen yapmak istediğiniz işlemi seçin (1/2/3/4): ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        SearchText(content);
                        break;

                    case 2:
                        ReplaceText(ref content);
                        break;

                    case 3:
                        DeleteText(ref content);
                        break;

                    case 4:
                        return;

                    default:
                        Console.WriteLine("Geçersiz seçim.");
                        break;
                }
            }
        }

        static void SearchText(string content)
        {
            Console.WriteLine("\nMetin İçinde Arama:");
            Console.Write("Aranacak Kelimeyi Girin: ");
            string searchText = Console.ReadLine();

            int index = content.IndexOf(searchText);
            int count = 0;

            while (index != -1)
            {
                count++;
                Console.WriteLine($"'{searchText}' kelimesi metin içinde {index}. sırada bulundu.");

                index = content.IndexOf(searchText, index + 1);
            }

            if (count == 0)
            {
                Console.WriteLine($"'{searchText}' kelimesi metin içinde bulunamadı.");
            }
        }

        static void ReplaceText(ref string content)
        {
            Console.WriteLine("\nMetin İçinde Değiştirme:");
            Console.Write("Değiştirmek istediğiniz kelimeyi girin: ");
            string oldText = Console.ReadLine();

            int index = content.IndexOf(oldText);
            int count = 0;

            // Belirtilen kelimenin bulunduğu tüm konumları bulun
            while (index != -1)
            {
                count++;
                Console.WriteLine($"'{oldText}' kelimesi metin içinde {index}. sırada bulundu.");

                index = content.IndexOf(oldText, index + 1);
            }

            if (count == 0)
            {
                Console.WriteLine("Belirtilen kelime metin içinde bulunamadı.");
                return;
            }

            Console.Write($"Toplam {count} konumda bulundu. Hangi konumdakini değiştirmek istersiniz? (1-{count}): ");
            int position = Convert.ToInt32(Console.ReadLine());

            // Seçilen konumdaki kelimeyi bulun ve değiştir
            index = content.IndexOf(oldText);
            count = 0;
            while (index != -1)
            {
                count++;
                if (count == position)
                {
                    Console.Write("Yeni Kelimeyi Girin: ");
                    string newText = Console.ReadLine();
                    content = content.Remove(index, oldText.Length).Insert(index, newText);
                    Console.WriteLine($"'{oldText}' kelimesi başarılı bir şekilde {newText} ile değiştirildi.");
                    return;
                }
                index = content.IndexOf(oldText, index + 1);
            }

            Console.WriteLine($"Metin içinde {position}. konumda {oldText} kelimesi bulunamadı.");
        }

        static void DeleteText(ref string content)
        {
            Console.WriteLine("\nMetin İçinde Silme:");
            Console.Write("Silinecek Kelimeyi Girin: ");
            string deleteText = Console.ReadLine();
            content = content.Replace(deleteText, "");
            Console.WriteLine("Silinmiş Metin:");
            Console.WriteLine(content);
        }
    }
}
