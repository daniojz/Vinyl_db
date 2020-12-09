using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Vinyl_db.model;

namespace Vinyl_db.ViewModel
{
    public class MainWindowModel : INotifyPropertyChanged
    {

        public Conexiones.Conexion conexion;

        public ICommand ComandoAccionInsert { get; set; }
        public ICommand ComandoAccionDelete { get; set; }
        public ICommand ComandoAccionEditar { get; set; }

         
        
        public MainWindowModel()
        {
           
            conexion = new Conexiones.Conexion();
            listaAlbums = conexion.getAlbums();
            Contador = listaAlbums.Count().ToString();


            //Comands->>
            ComandoAccionInsert = new Command(AccionInsert);
            ComandoAccionDelete = new Command(AccionDelete);
            ComandoAccionEditar = new Command(AccionEditar);


            actualizarContador();
        }

        public void ActualizarDatos()
        {
            conexion = new Conexiones.Conexion();
            ListaAlbums = conexion.getAlbums();
            actualizarContador();
        }



        //comands------------------------------------------------------------------------------>
        private void AccionInsert(object parámetro)
        {
            album newAlbum = new album(
                Contador+1,
                Titulo,
                NombreArtista,
                Numero_canciones,
                Calificacion,
                Genero,
                ColoresVinilo,
                CantidadVinilos
                );

            conexion.addAlbum(newAlbum);
            MessageBox.Show("Registro aregado exitosamente");
            ActualizarDatos();
        }

        private void AccionDelete(object parámetro)
        {
            album deletealbum = new album();
            deletealbum = ViniloSeleccioando;

            conexion.deleteAlbum(deletealbum.IdAlbum);
            ActualizarDatos();
        }
        

        private void AccionEditar(object parámetro)
        {
            View.EditAlbumWindow editWindow = new View.EditAlbumWindow(this);
            editWindow.Show();

            EditAlbumWindowModel editAlumnoModel = editWindow.dataContext;

            editAlumnoModel.IdAlbum = ViniloSeleccioando.IdAlbum;
            editAlumnoModel.Titulo = ViniloSeleccioando.titulo;
            editAlumnoModel.NombreArtista = ViniloSeleccioando.nombreArtista;
            editAlumnoModel.Numero_canciones = ViniloSeleccioando.numero_canciones;
            editAlumnoModel.Calificacion = ViniloSeleccioando.calificacion;
            editAlumnoModel.Genero = ViniloSeleccioando.genero;
            editAlumnoModel.ColoresVinilo = ViniloSeleccioando.coloresVinilo;
            editAlumnoModel.CantidadVinilos = ViniloSeleccioando.cantidadVinilos;

        }

        public void guardarEditar()
        {
            conexion.editAlbum(viniloSeleccioando);

            ActualizarDatos();
            MessageBox.Show("La base de datos se ha actualizado");


        }




        //para enlazar las propiedades de la view/ventana con binding---------------------------->
        private ObservableCollection<album> listaAlbums;
        public ObservableCollection<album> ListaAlbums
        {
            get
            {
                return listaAlbums;
            }
            set
            {
                listaAlbums = value;
                OnPropertyChanged("ListaAlbums");
            }
        }

        private album viniloSeleccioando;
        public album ViniloSeleccioando
        {
            get
            {
                return viniloSeleccioando;
            }
            set
            {
                viniloSeleccioando = value;
                OnPropertyChanged("ViniloSeleccioando");
            }
        }

        private String contador;
        public String Contador
        {
            get
            {
                return contador;
            }
            set
            {
                contador = value;
                OnPropertyChanged("Contador");
            }
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


        //---------------------------------------------------------------------------------------------<
        public void actualizarContador()
        {
            Contador = ListaAlbums.Count().ToString();
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
