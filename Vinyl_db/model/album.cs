using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vinyl_db.model
{
    public class album
    {
        public string IdAlbum { get; set; }
        public string titulo { get; set; }
        public string nombreArtista { get; set; }
        public string numero_canciones { get; set; }
        public string calificacion { get; set; }
        public string genero { get; set; }
        public string coloresVinilo { get; set; }
        public string cantidadVinilos { get; set; }

        public album()

        public album(
            string IdAlbum, 
            string titulo, 
            string nombreArtista, 
            string numero_canciones, 
            string calificacion, 
            string genero,
            string coloresVinilo,
            string cantidadVinilos)
        {
            this.IdAlbum = IdAlbum;
            this.titulo = titulo;
            this.nombreArtista = nombreArtista;
            this.coloresVinilo = coloresVinilo;
            this.cantidadVinilos = cantidadVinilos;
            this.numero_canciones = numero_canciones;
            this.calificacion = calificacion;
            this.genero = genero;
        }

    }
}
