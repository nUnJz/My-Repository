using System;
using System.IO;
using System.Text;

namespace Lab_7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Default;
            int i, k, count;
            int N;
            int index;
            Console.Write("Введіть кількість натуральних чисел у послідовності N = ");
            N = Convert.ToInt32(Console.ReadLine());
            int[] A = new int[N]; // послідовність натуральних чисел представлено у вигляді одновимірного масиву А
            int[] B = new int[A.Length + 1];
            Random rnd = new Random();
            using (FileStream fs = new FileStream("file7.txt", FileMode.Create, FileAccess.Write))
            {
                using (BinaryWriter bw = new BinaryWriter(fs, Encoding.Default))
                {
                    for (i = 0; i < N; i++)
                    {
                        A[i] = rnd.Next(1, 9);
                        Console.Write("{0,2:D}", A[i]);
                        // елементи масиву записуються в файл
                        bw.Write(A[i]);
                    }
                }
            }
            Console.WriteLine();
            // елементи масиву зчитуються з файлу для контролю
            using (FileStream fs = new FileStream("file7.txt", FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                {
                    for (i = 0; i < N; i++)
                    {
                        A[i] = br.ReadInt32();
                        Console.Write("{0,2:D}", A[i]);
                    }
                }
            }
            index = A.Length;
            Console.WriteLine();
            Console.WriteLine("Пошук першого числа 1 в послідовності");
            count = 0;
            using (FileStream fs = new FileStream("file7.txt", FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                {
                    for (i = 0; i < N; i++)
                    {
                        A[i] = br.ReadInt32();
                        if (A[i] == 1)
                        {
                            count++;
                            k = i;
                            Console.Write($"Число 1 є в послідовності чисел, його індекс: {k}");
                            break;
                        }
                        Console.WriteLine();
                    }
                }
            }
            // перезапис файла
            using (FileStream fs = new FileStream("file7.txt", FileMode.Create, FileAccess.Write))
            {
                using (BinaryWriter bw = new BinaryWriter(fs, Encoding.Default))
                {
                    if (count == 0)
                    {
                        // запис масиву та числа 1 в файл
                        for (i = 0; i < index; i++)
                        {
                            B[i] = A[i];
                            bw.Write(B[i]);
                        }
                        B[index] = 1;
                        bw.Write(B[index]);
                    }
                    Console.WriteLine();
                }
            }
            using (FileStream fs = new FileStream("file7.txt", FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                {
                    if (count == 0)
                    {
                        Console.WriteLine("Зчитаний з файла масив, що містить додане число 1: ");
                        for (i = 0; i < N + 1; i++)
                        {
                            B[i] = br.ReadInt32();
                            Console.Write("{0,2:D}", B[i]);
                        }
                    }
                }
            }
            Console.ReadLine();
        }
    }
}