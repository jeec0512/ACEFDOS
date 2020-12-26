using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;


/// <summary>
/// Descripción breve de Helper
/// </summary>
/// 

public class Helper
{
	public Helper()
	{
		
	}
    public static string EncodePassword(string originalPassword)
    {
        //Clave que se utilizará para encriptar el usuario y la contraseña
        string clave = "7f9facc418f74439c5e9709832;0ab8a5:OCOdN5Wl,q8SLIQz8i|8agmu¬s13Q7ZXyno/";
        //Se instancia el objeto sha512 para posteriormente usarlo para calcular la matriz de bytes especificada
        SHA512 sha512 = new SHA512CryptoServiceProvider();

        //Se crea un arreglo llamada inputbytes donde se convierte el usuario, la contraseña y la clave a una secuencia de bytes.
        byte[] inputBytes = (new UnicodeEncoding()).GetBytes(originalPassword + clave);
        //Se calcula la matriz de bytes del arreglo anterior y se encripta.
        byte[] hash = sha512.ComputeHash(inputBytes);
        //Convertimos el arreglo de bytes a cadena.
        return Convert.ToBase64String(hash);
    }
}