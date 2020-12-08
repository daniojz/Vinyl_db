using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Vinyl_db.ViewModel
{
    public class EditAlbumWindowModel : INotifyPropertyChanged
    {
        MainWindowModel MainModel;
        public ICommand ComandAccionGuardar { get; set; }

        public EditAlbumWindowModel(MainWindowModel mainWindowModel) 
        {
            MainModel = mainWindowModel;
            ComandAccionGuardar = new Command(AccionGuardar);
            
        }

        private void AccionGuardar(object parámetro)
        {
            MainModel.ViniloSeleccioando.IdAlbum = idAlbum;
            MainModel.ViniloSeleccioando.titulo = Titulo;
            MainModel.ViniloSeleccioando.nombreArtista = NombreArtista;
            MainModel.ViniloSeleccioando.numero_canciones = Numero_canciones;
            MainModel.ViniloSeleccioando.calificacion = Calificacion;
            MainModel.ViniloSeleccioando.genero = Genero;
            MainModel.ViniloSeleccioando.coloresVinilo = ColoresVinilo;
            MainModel.ViniloSeleccioando.cantidadVinilos = CantidadVinilos;

            MainModel.guardarEditar();
        }

        private String idAlbum;
        public String IdAlbum
        {
            get
            {
                return idAlbum;
            }
            set
            {
                idAlbum = value;
                OnPropertyChanged("IdAlbum");
            }
        }

        private String titulo;
        public String Titulo
        {
            get
            {
                return titulo;
            }
            set
            {
                titulo = value;
                OnPropertyChanged("Titulo");
            }
        }

        private String nombreArtista;
        public String NombreArtista
        {
            get
            {
                return nombreArtista;
            }
            set
            {
                nombreArtista = value;
                OnPropertyChanged("NombreArtista");
            }
        }

        private String numero_canciones;
        public String Numero_canciones
        {
            get
            {
                return numero_canciones;
            }
            set
            {
                numero_canciones = value;
                OnPropertyChanged("Numero_canciones");
            }
        }

        private String calificacion;
        public String Calificacion
        {
            get
            {
                return calificacion;
            }
            set
            {
                calificacion = value;
                OnPropertyChanged("Calificacion");
            }
        }

        private String genero;
        public String Genero
        {
            get
            {
                return genero;
            }
            set
            {
                genero = value;
                OnPropertyChanged("Genero");
            }
        }

        private String coloresVinilo;
        public String ColoresVinilo
        {
            get
            {
                return coloresVinilo;
            }
            set
            {
                coloresVinilo = value;
                OnPropertyChanged("ColoresVinilo");
            }
        }

        private String cantidadVinilos;
        public String CantidadVinilos
        {
            get
            {
                return cantidadVinilos;
            }
            set
            {
                cantidadVinilos = value;
                OnPropertyChanged("CantidadVinilos");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new
                PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
