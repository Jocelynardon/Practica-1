using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pruebamodificarpersonas.Models;
using System.Web;
using System.IO;

namespace Pruebamodificarpersonas.Controllers
{
    public class JugadoresController : Controller
    {
        public static List<Jugador> listaJugadores = new List<Jugador>();

        //INDEX 
        public ActionResult Index()
        {
            ViewBag.unicalista = listaJugadores;
            

                return View();
        }

      
        //CREATE
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(IFormCollection jugador)
        {          
            Jugador nuevojugador = new Jugador();
            nuevojugador.Nombre = jugador["Nombre"];
            nuevojugador.Apellido = jugador["Apellido"];
            nuevojugador.Club = jugador["Club"];
            nuevojugador.Posicion = jugador["Posicion"];
            nuevojugador.Salario = float.Parse(jugador["Salario"]);

            listaJugadores.Add(nuevojugador);
            ViewBag.unicalista = listaJugadores;
            return View("Index");
        }   

        //EDIT
        public ActionResult Edit(int id)
        {
            var jugador = listaJugadores.FirstOrDefault(x => x.ID == id);
            return View(jugador);
        }
        [HttpPost]
        public ActionResult EditarJugador(int id, IFormCollection collection)
        {
            try
            {
                int posicion = 0;
                for (int i=0;i<listaJugadores.Count();i++)
                {
                    if (listaJugadores[i].ID==id)
                    {
                        posicion = i;
                    }
                }
                listaJugadores[id - 1].Club = collection["Club"];
                listaJugadores[id - 1].Salario = float.Parse(collection["Salario"]);
                ViewBag.unicalista = listaJugadores;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View("Index");
            }
        }

        //DELETE
        public ActionResult Delete(int id)
        {
            var jugador = listaJugadores.FirstOrDefault(x => x.ID == id);
            return View(jugador);
        }
        [HttpPost]
        public ActionResult EliminarJugador(int id)
        {
            try
            {
                listaJugadores.RemoveAll(x => x.ID == id);     
                ViewBag.unicalista = listaJugadores;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
            
        }
        //public ActionResult Buscar()
        //{
        //    return View();
        //}

       

        public ActionResult CrearArchivo(FormCollection collection)
        {
            StreamReader streamReader = new StreamReader(collection["path"]);
            var listaJugadores = (streamReader.ReadToEnd()).Split('\r');

            for (int i = 0; i < listaJugadores.Length; i++)
            {
                if (listaJugadores[i][0]=='\r')
                {
                    listaJugadores[i] = listaJugadores[i].Substring(i);
                }
                
            }

            foreach (var caracteristicas in listaJugadores)
            {
                var caracteristicasArreglo = caracteristicas.Split(',');
                Jugador jugador1 = new Jugador
                {
                    Nombre = caracteristicasArreglo[0],
                    Apellido = caracteristicasArreglo[1],
                    Club = caracteristicasArreglo[2],
                    Posicion = caracteristicasArreglo[3],
                    Salario = Convert.ToInt32(caracteristicasArreglo[4]),
                    ID=Convert.ToInt32(caracteristicasArreglo[5])
                };
                //Storage.Instance.playerList.AddFirst(jugador1);
            }
            return RedirectToAction("Index");

        }

        public ActionResult BuscarJugador(string palabraBuscar)
        {
            return View();   
        }

        [HttpPost]
        public ActionResult Buscador(string palabra)
        {
            var busqueda = from abuscar in listaJugadores select abuscar;

            if (!String.IsNullOrEmpty(palabra))
            {
                busqueda = busqueda.Where(j => j.Nombre.Contains(palabra) || j.Apellido.Contains(palabra)||j.Club.Contains(palabra)||j.Posicion.Contains(palabra));
            }
           
            return View(busqueda.ToList());
        }
    }
    
}