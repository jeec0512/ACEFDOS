using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Catalogo_activarUsuario : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    #endregion

    #region INICIAR
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblMensaje.Text = string.Empty;
        }
    }

    #endregion
    
    protected void txtUserName_TextChanged(object sender, EventArgs e)
    {
        /*BUSCAR EN USUARIO*/
        string userName = txtUserName.Text.Trim();


        var cUsuario = from tUsuario in dc.Usuario
                       where tUsuario.Username == userName
                       select new
                       {
                           UsuarioID = tUsuario.UsuarioID,
                           nivel = tUsuario.nivel,
                           sucursal = tUsuario.sucursal,
                           grupo = tUsuario.grupo,
                           ruc = tUsuario.ruc,
                           apellidos = tUsuario.apellidos,
                           nombres = tUsuario.nombres,
                           Username = tUsuario.Username,
                           Contraseña = tUsuario.Contraseña,
                           Email = tUsuario.Email,
                           FechaRegistro = tUsuario.FechaRegistro,
                           Tipo = tUsuario.Tipo,
                           firstTime = tUsuario.firstTime,
                           maquina = tUsuario.maquina,
                           estado = tUsuario.estado
                       };

        if (cUsuario.Count() == 0)
        {
            lblMensaje.Text = "NO EXISTE EL USUARIO";
        }
        else
        {
            foreach (var registro in cUsuario)
            {
                lblMensaje.Text = string.Empty;
                txtApellidos.Text = registro.apellidos;
                txtNombres.Text = registro.nombres;
                txtUserName.Text = registro.Username;
                chkEstado.Checked = Convert.ToBoolean(registro.estado);
            }
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        /* tbl_secuenciales tbl_secuenciales = dc.tbl_secuenciales.SingleOrDefault(p => p.sucursal == suc);
            tbl_secuenciales.retencion = sec;
            dc.SubmitChanges();*/

        string userName = txtUserName.Text.Trim();

        Usuario usuario = dc.Usuario.SingleOrDefault(p => p.Username == userName);
        usuario.estado = chkEstado.Checked;
        usuario.nivel = 1;
        dc.SubmitChanges();

    }
}