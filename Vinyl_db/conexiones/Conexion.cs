using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Vinyl_db.model;

namespace Vinyl_db.Conexiones
{
    public class Conexion
    {
        private string cadenaConexion = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = D:\\MY DEVELOP\\Vinyl_db\\vinyl_db_v1.accdb";

        private OleDbConnection conexion;
        private OleDbCommand command;
        private OleDbDataReader reader;

        public ObservableCollection<album> GetVinilos()
        {
            conexion = new OleDbConnection(cadenaConexion);

            command = new OleDbCommand("SELECT * FROM albums", conexion);
            command.CommandType = CommandType.Text;

            ObservableCollection<album> albums = new ObservableCollection<album>();

            try
            {
                conexion.Open(); 
                reader = command.ExecuteReader(); 

                while (reader.Read())
                {
                    album album = new album();
                    album.IdAlbum = (String)reader["idAlbum"];
                    album.titulo = (String)reader["Titulo"];
                    album.nombreArtista = (String)reader["NombreArtista"];
                    album.numero_canciones = (String)reader["Numero_canciones"];
                    album.calificacion = (String)reader["Calificacion"];
                    album.genero = (String)reader["Genero"];
                    album.coloresVinilo = (String)reader["ColoresVinilo"];
                    album.cantidadVinilos = (String)reader["CantidadVinilos"];

                    albums.Add(album);
                }
            }
            catch (Exception ex)
            {
                throw ex; 
            }
            finally
            {
                conexion.Close();
            }

            return albums; 
        }


        public void addAlbum(album nuevoAlbum)
        {
            conexion = new OleDbConnection(cadenaConexion);
            command = new OleDbCommand();
            command.Connection = conexion;
            command.CommandType = CommandType.Text;

            command.CommandText = "INSERT INTO albums (idAlbum, titulo, nombreArtista, numero_canciones, calificacion, genero, coloresVinilo, cantidadVinilos) VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8);";

            // establecemos los valores que tomarán los parámetros de la instrucción OleDb. 
            command.Parameters.AddWithValue("@p1", nuevoAlbum.IdAlbum);
            command.Parameters.AddWithValue("@p2", nuevoAlbum.titulo);
            command.Parameters.AddWithValue("@p3", nuevoAlbum.nombreArtista);
            command.Parameters.AddWithValue("@p4", nuevoAlbum.numero_canciones);
            command.Parameters.AddWithValue("@p5", nuevoAlbum.calificacion);
            command.Parameters.AddWithValue("@p6", nuevoAlbum.genero);
            command.Parameters.AddWithValue("@p7", nuevoAlbum.coloresVinilo);
            command.Parameters.AddWithValue("@p8", nuevoAlbum.cantidadVinilos);

            conexion.Open();
            command.ExecuteNonQuery();


            conexion.Close();
        }

        public int deleteAlbum(string idAlbum)
        {
            conexion = new OleDbConnection(cadenaConexion);
            command = new OleDbCommand("DELETE FROM albums WHERE idAlbum = @p0", conexion);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@p0", idAlbum);

            try
            {
                conexion.Open();
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }

        public int editAlbum(string idAlbum, string titulo, string nombreArtista, string numero_canciones,string calificacion, string genero, string coloresVinilo, string cantidadVinilos)
        {
            conexion = new OleDbConnection(cadenaConexion);
            command = new OleDbCommand("UPDATE albums" 
                + "SET idAlbum = @p1, titulo = @p2, nombreArtista = @p3, numero_canciones = @p4, calificacion = @p5, genero = @p6, coloresVinilo = @p7, cantidadVinilos = @p8"
                + "WHERE idAlbum = @p1", conexion);

            command.CommandType = CommandType.Text;

            command.Parameters.AddWithValue("@p1", nuevoAlbum.IdAlbum);
            command.Parameters.AddWithValue("@p2", nuevoAlbum.titulo);
            command.Parameters.AddWithValue("@p3", nuevoAlbum.nombreArtista);
            command.Parameters.AddWithValue("@p4", nuevoAlbum.numero_canciones);
            command.Parameters.AddWithValue("@p5", nuevoAlbum.calificacion);
            command.Parameters.AddWithValue("@p6", nuevoAlbum.genero);
            command.Parameters.AddWithValue("@p7", nuevoAlbum.coloresVinilo);
            command.Parameters.AddWithValue("@p8", nuevoAlbum.cantidadVinilos);


            try
            {
                conexion.Open();
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }
    }
}
