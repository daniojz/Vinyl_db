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
        int contadorFilas;

        OleDbConnection Conexion;
        OleDbDataAdapter DataAdapter;
        DataSet DataSet;

        OleDbCommandBuilder Builder;


        public ICommand ComandoAccionInsert { get; set; }
        public ICommand ComandoAccionDelete { get; set; }
        public ICommand ComandoAccionEditar { get; set; }



        public MainWindowModel()
        {
            contadorFilas = 0;

            Conexion = new OleDbConnection(); //objeto/clase conexión
            Conexion.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = D:\\MY DEVELOP\\Vinyl_db\\vinyl_db_v1.accdb"; //establezco la conexión con la base de datos

            DataAdapter = new OleDbDataAdapter("SELECT * FROM albums", Conexion);
            Builder = new OleDbCommandBuilder(DataAdapter);
            DataSet = new DataSet();

            Conexion.Open();
            DataAdapter.Fill(DataSet, "albums"); //pasamos el resutado de la query con un nombre al data set (se pasa como una tabla)
            Conexion.Close();

            ActualizarDatos();

            //Comands->>
            ComandoAccionInsert = new Command(AccionInsert);
            ComandoAccionDelete = new Command(AccionDelete);
            ComandoAccionEditar = new Command(AccionEditar);


            actualizarContador();
        }

        public void ActualizarDatos()
        {
            DataRow fila;
            ObservableCollection<album> albums = new ObservableCollection<album>();
            int numeroDeFilas = (int)DataSet.Tables["albums"].Rows.Count;

            for (int i = 0; i < numeroDeFilas; i++) {
                fila = DataSet.Tables["albums"].Rows[i];

                album album = new album(
                    fila["IdAlbum"].ToString(),
                    fila["titulo"].ToString(),
                    fila["nombreArtista"].ToString(),
                    fila["numero_canciones"].ToString(),
                    fila["calificacion"].ToString(),
                    fila["genero"].ToString(),
                    fila["coloresVinilo"].ToString(),
                    fila["cantidadVinilos"].ToString()
                    );

                albums.Add(album);
            }

            ListaAlbums = albums;
        }
        


        //comands------------------------------------------------------------------------------>
        private void AccionInsert(object parámetro)
        {
            //nos ponemos en la ultima posicion que es la del album insertado
            int numeroDeFilas = (int)DataSet.Tables["albums"].Rows.Count;
            contadorFilas = numeroDeFilas - 1;

            DataRow F;

            F = DataSet.Tables["albums"].NewRow(); //creamos una fila con el diseño de la tabla album

            //añadimos a cada una de las columnas de la fila el nuevo album---------->
            F["IdAlbum"] = contadorFilas+1;
            F["titulo"] = Titulo;
            F["nombreArtista"] = nombreArtista;
            F["numero_canciones"] = Numero_canciones;
            F["calificacion"] = Calificacion;
            F["genero"] = Genero;
            F["coloresVinilo"] = coloresVinilo;
            F["cantidadVinilos"] = cantidadVinilos;


            DataSet.Tables["albums"].Rows.Add(F);

            MessageBox.Show("Vinilo añadido");


            ActualizarDatos(); //actualizamos el listbox

            actualizarContador();
            actualizarDB();
        }

        private void AccionDelete(object parámetro)
        {
            actualizarDB();

            DataRow F = DataSet.Tables["albums"].Rows[Int32.Parse(ViniloSeleccioando.IdAlbum)-1];

            MessageBoxResult mb = MessageBox.Show("Seguro que desea eliminar \"" + F["titulo"] + "\"", "Confirmar eliminación de registro", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (mb == MessageBoxResult.Yes)
            {
                F.Delete();

                //mediante el método getchanges(), obtenemos una tabla con las filas borradas

                DataTable TablaBorrados;
                TablaBorrados = DataSet.Tables["albums"].GetChanges(DataRowState.Deleted);

                DataAdapter.Update(TablaBorrados);
                DataSet.Tables["albums"].AcceptChanges();
                MessageBox.Show("Vinilo Borrado");

                contadorFilas = DataSet.Tables["albums"].Rows.Count - 1;
                ActualizarDatos();
                actualizarContador();
            }
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
            DataRow F = DataSet.Tables["albums"].Rows[Int32.Parse(ViniloSeleccioando.IdAlbum) - 1];
            F["IdAlbum"] = ViniloSeleccioando.IdAlbum;
            F["titulo"] = ViniloSeleccioando.titulo;
            F["nombreArtista"] = viniloSeleccioando.nombreArtista;
            F["numero_canciones"] = viniloSeleccioando.numero_canciones;
            F["calificacion"] = viniloSeleccioando.calificacion;
            F["genero"] = viniloSeleccioando.genero;
            F["coloresVinilo"] = viniloSeleccioando.coloresVinilo;
            F["cantidadVinilos"] = viniloSeleccioando.cantidadVinilos;


            actualizarDB();

        ActualizarDatos();

            actualizarDB();

            MessageBox.Show("La base de datos se ha actualizado");
            actualizarContador();
            actualizarDB();
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
        public void actualizarDB()
        {
            Conexion.Open();
            DataAdapter.Update(DataSet.Tables["albums"]);
            Conexion.Close();
        }
        public void actualizarContador()
        {
            Contador = contadorFilas + "/" + ((DataSet.Tables["albums"].Rows.Count) - 1);
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
