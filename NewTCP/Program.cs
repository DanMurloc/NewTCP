using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;      // потребуется
using System.Net.Sockets;    // потребуется

namespace NewTCP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // устанавливаем IP-адрес сервера и номер порта 7777
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 7777);
            server.Start();  // запускаем сервер
            while (true)   // бесконечный цикл обслуживания клиентов
            {
                TcpClient client = server.AcceptTcpClient();  // ожидаем подключение клиента
                NetworkStream ns = client.GetStream(); // для получения и отправки сообщений
                byte[] hello = new byte[100];   // любое сообщение должно быть сериализовано
                hello = Encoding.Default.GetBytes("Ok,server on");  // конвертируем строку в массив байт
                ns.Write(hello, 0, hello.Length);     // отправляем сообщение
                while (client.Connected)  // пока клиент подключен, ждем приходящие сообщения
                {
                    byte[] msg = new byte[1024];     // готовим место для принятия сообщения
                    int count = ns.Read(msg, 0, msg.Length);   // читаем сообщение от клиента
                    Console.Write(Encoding.Default.GetString(msg, 0, count)); // выводим на экран полученное сообщение в виде строки
                }
            }
        }
    }
}
