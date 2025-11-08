using AutoMapper;
using Examen.GestionLibros.DTO.Request;
using Examen.GestionLibros.DTO.Response;
using Examen.GestionLibros.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.GestionLibros.Negocio.Mappers
{
   public class LibroProfile:Profile
    {
        public LibroProfile()
        {

            CreateMap<Libro, LibroResponse>().ReverseMap();
            CreateMap<Libro, LibroRequest>().ReverseMap();

        }
    }
}
