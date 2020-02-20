using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Pruebamodificarpersonas.Controllers;
using Pruebamodificarpersonas.Models;

namespace Pruebamodificarpersonas.Models
{
    public class Jugador : IComparable
    {
        public int ID { get; set; }
        //
        public static int Id;
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Club { get; set; }
        [Required]
        public string Posicion { get; set; }
        public float Salario { get; set; }
        public Jugador ()
        {
            Id++;
            ID = Id;
        }

        public static Comparison<Jugador> FindById = delegate (Jugador jugador1, Jugador jugador2)
        {
            return jugador1.CompareTo(jugador2);
        };
        public static Comparison<Jugador> FindBySalario = delegate (Jugador jugador1, Jugador jugador2)
        {
            return jugador1.CompareTo(jugador2);
        };

        public int CompareTo(object obj)
        {
            var comparer = ((Jugador)obj).ID;

            return comparer < 1 ? 1 : comparer == Salario ? 0 : -1;
        }
    }
}
