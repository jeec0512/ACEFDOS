using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace AuticationBDD
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://devaneta.org.ec/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public string encriptaDatos(string palabra)
        {
            return Helper.EncodePassword(palabra);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public Boolean validaUsuario(string usuario, string password)
        {
            return Servicio.validarUsuarioAutenticado(usuario, password);
        }
    }
}
