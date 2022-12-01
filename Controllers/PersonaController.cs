using CrudNetCore5.Data;
using CrudNetCore5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.IdentityModel.Logging;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CrudNetCore5.Controllers
{
    public class PersonaController : Controller
    {


        private readonly ApplicationDbContext _context;


        /// <summary>
        /// iniciar context de la aplicacion
        /// </summary>
        /// <param name="context"></param>
        public PersonaController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// listar todas las personas Index
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            IEnumerable<Persona> listPersona = _context.Persona;
            
            return View(listPersona);
        }
        /// <summary>
        ///  redireccionar a la vista de crear persona
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            
            return View();
          
        }

        /// <summary>
        /// valida a la hora de registrar una persona, que el numero de documento no este duplicado
        /// </summary>
        /// <param name="nroDoc"></param>
        /// <param name="accion"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public bool validarNroDocumento(string nroDoc,string accion, int? id)
        {
           
            bool validar = true;
            var person = _context.Persona.ToList();
            var perso = _context.Persona.Find(id);
            if (accion == "insertar")   {
                
                if (_context.Persona.Any(x => x.NroDocumento.Equals(nroDoc))) validar = false;
               
            }
           
            return validar;
        }
          
        

        /// <summary>
        /// metodo de persistencia de persona a la base de datos
        /// </summary>
        /// <param name="persona"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Persona persona)
        {
           
            if (validarNroDocumento(persona.NroDocumento,"insertar",persona.Id))
            {
                if (ModelState.IsValid)
                {
                    
                    _context.Persona.Add(persona);
                    _context.SaveChanges();

                    TempData["mensaje"] = "La persona ha sido creada correctamente";


                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["mensaje"] = "Error, este Número de Documento ya existe";
                return RedirectToAction("Index");
            }
            
            return View();

        }
        /// <summary>
        /// redireccionar vista de editar persona, validando previamente que el id no este nulo o vacio
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            
            //Obtener la Persona
            var persona = _context.Persona.Find(id);

            if (persona == null)
            {
                return NotFound();
            }


            return View(persona);
        }

        /// <summary>
        /// edicion o update de Persona en la base de datos de acuerdo al registro seleccionado en la ventana del listar Personas
        /// </summary>
        /// <param name="personaEditar"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(Persona personaEditar)
        {

                if (ModelState.IsValid)
                {

                        
                     _context.Persona.Update(personaEditar);
                     _context.SaveChanges();


                     TempData["mensaje"] = "El registro fue actualizado correctamente";


                    return RedirectToAction("Index");
                }
            return View();
        }

          
        
        /// <summary>
        /// metodo que retorna los mensajes de exito o error de acuerdo al resultado del metodo RemovePersona
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dif"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(int id,int? dif)
        {
            if (RemovePersona(id))
            {
                TempData["mensaje"] = "El registro fue eliminado correctamente";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["mensaje"] = "Error al eliminar registro";
            }
            
            return RedirectToAction("Index");
        }
        /// <summary>
        /// metodo que elimina la persona de la base de datos y retorna un boolean para confirmarlo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemovePersona(int? id)
        {
            var persona = _context.Persona.Find(id);
            _context.Persona.Remove(persona);
            _context.SaveChanges();
            return true;
        }

        /// <summary>
        /// metodo que redirecciona a la vista de delete o eliminar persona
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //Obtener la Persona
            var persona = _context.Persona.Find(id);

            if (persona == null)
            {
                return NotFound();
            }

            
            return View(persona);
        }
    }
}
