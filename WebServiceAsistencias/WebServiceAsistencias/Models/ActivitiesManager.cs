﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using WebServiceAsistencias.Models;
namespace WebServiceAsistencias.Models
{
    public class ActivitiesManager
    {
        public string cadenaConexion = RouteConfig.cadenaConexion;
        public List<Actividad> ObtenerActividadesDeAdministrador(string ida)
        {
            List<Actividad> lista = new List<Actividad>();

            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "select a.idActividad,a.nombre,a.lugar,a.horaInicio,a.horaFinal,a.descripcion,a.fecha,a.cupo,a.duracion from Actividad as a";

            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataReader reader =
                cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                Actividad actividad = new Actividad();
                actividad.idActividad = reader.GetString(0);
                actividad.nombre = reader.GetString(1);
                actividad.lugar = reader.GetString(2);
                actividad.horaInicio = reader.GetTimeSpan(3).ToString();
                actividad.horaFinal = reader.GetTimeSpan(4).ToString();
                actividad.descripcion = reader.GetString(5).ToString();
                actividad.fecha = reader.GetDateTime(6).Date.ToString();
                actividad.cupo = reader.GetInt32(7);
                actividad.duracion = reader.GetTimeSpan(8).ToString();

                lista.Add(actividad);
            }
            reader.Close();
            return lista;
        }
            public bool InsertarActivity(Actividad act)
            {
                SqlConnection con = new SqlConnection(cadenaConexion);
                con.Open();
                string sql = "INSERT INTO Actividad (idActividad,nombre,descripcion,fecha,cupo,lugar,horaInicio,horaFinal,duracion) VALUES (@id,@name,@desc,@date,@cup,@place,@horI,@horF,@dur)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@id", System.Data.SqlDbType.NVarChar).Value = act.idActividad;
                cmd.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value = act.nombre;
                cmd.Parameters.Add("@desc", System.Data.SqlDbType.NVarChar).Value = act.descripcion;
                cmd.Parameters.Add("@date", System.Data.SqlDbType.NVarChar).Value = act.fecha;
                cmd.Parameters.Add("@cup", System.Data.SqlDbType.NVarChar).Value = act.cupo;
                cmd.Parameters.Add("@place", System.Data.SqlDbType.NVarChar).Value = act.lugar;
                cmd.Parameters.Add("@horI", System.Data.SqlDbType.NVarChar).Value = act.horaInicio;
                cmd.Parameters.Add("@horF", System.Data.SqlDbType.NVarChar).Value = act.horaFinal;
                cmd.Parameters.Add("@dur", System.Data.SqlDbType.NVarChar).Value = act.duracion;
            int res = cmd.ExecuteNonQuery();

                con.Close();

                return (res == 1);
            }


    }
}