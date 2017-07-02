using Eventos.IO.Domain.Models;
using System;

namespace ConsoleTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            var evento = new Evento(
                "Nome do Evento",
                DateTime.Now,
                DateTime.Now,
                false,
                50,
                false,
                "Vinicius Lopes de Almeida");

            var evento2 = evento;


            Console.WriteLine(evento.ToString());       
            Console.ReadKey();
        }
    }
}