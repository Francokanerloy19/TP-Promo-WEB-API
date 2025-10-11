using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ClienteNegocio
    {
        public Cliente buscarDNI(string dni)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            Cliente cliente = null;
            try
            {
                accesoDatos.setearConsulta("SELECT id,Documento,Nombre,Apellido,Email,Direccion,Ciudad,CP FROM Clientes where Documento = @Documento");
                accesoDatos.setearParametros("@Documento", dni);
                accesoDatos.ejecutarConsulta();

                if (accesoDatos.Lector.Read()) 
                {
                    cliente = new Cliente();
                    cliente.id = (int)accesoDatos.Lector["id"];
                    cliente.DNI = (string)accesoDatos.Lector["Documento"];
                    cliente.nombre = (string)accesoDatos.Lector["Nombre"];
                    cliente.apellido = (string)accesoDatos.Lector["Apellido"];
                    cliente.email = (string)accesoDatos.Lector["Email"];
                    cliente.direccion = (string)accesoDatos.Lector["Direccion"];
                    cliente.ciudad = (string)accesoDatos.Lector["Ciudad"];
                    cliente.codPostal = (int)accesoDatos.Lector["CP"];

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally 
            {
                accesoDatos.cerrarConexion();
            }
            return cliente; // retorna null si no encuentra nada
        }

        public void agregar(Cliente cliente) 
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                accesoDatos.setearConsulta("INSERT INTO Clientes (Documento, Nombre, Apellido, Email, Direccion, Ciudad, CP) VALUES (@Documento, @Nombre, @Apellido, @Email, @Direccion, @Ciudad, @CP);");
                accesoDatos.setearParametros("@Documento", cliente.DNI);
                accesoDatos.setearParametros("@Nombre", cliente.nombre);
                accesoDatos.setearParametros("@Apellido", cliente.apellido);
                accesoDatos.setearParametros("@Email", cliente.email);
                accesoDatos.setearParametros("@Direccion", cliente.direccion);
                accesoDatos.setearParametros("@Ciudad", cliente.ciudad);
                accesoDatos.setearParametros("@CP", cliente.codPostal);
                accesoDatos.ejecutarConsulta();
            }
            catch (Exception ex)
            {

                throw ex;
            }finally
            {
                accesoDatos.cerrarConexion();
            }
            
        }
    }
}
