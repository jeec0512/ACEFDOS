<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ingresar.aspx.cs" Inherits="ingresar" Culture="es-ES" UICulture="es-ES"%>

<html>
<head id=Head1 runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>ANETA-AcefDos</title>
    <link href="App_Themes/Estilos/Login.css" rel="stylesheet" />
    <link rel="shortcut icon" href="images~/iconos/aneta.ico"/>
</head>
<body>
    <form id="form1" runat="server">
        <span class="TextoTitulo"
            style="margin: 20px 0px 0px 0px; color:  #094697; font-size: 50px; text-shadow: inherit; font-weight:800">AcefDos</span>
        <div class="FormaLogin">
            <div class="LeftFormaLogin"  style="opacity:0.6;">
            </div>
            <div class="CenterFormaLogin" style="opacity:0.4;">
                <div class="cPosRel" style="width: 370px; height: 90px; margin: 34px auto 0px auto; text-align: justify;
                    top: -19px; left: 0px;  opacity:1;">
                    <span class="TextoBienvenido">Al ingresar al Sistema ústed está de acuerdo en aceptar
                    nuestros Términos y condiciones legales, cualquier cambio que realize en esta página
                    será monitoreado.</span>
                   
                    <span>
                        <%= Request.ServerVariables["REMOTE_ADDR"]%></span><br />
                    <span>
                        <asp:Literal ID="ltMac" runat="server"></asp:Literal></span>
                </div>
                <div class="cFL cPosRel" style="width: 450px; height: 150px; top: 4px; left: 2px;">
                    <div class="cFL cPosRel" style="width: 100px; height: 25px;">
                        <span class="cFR cPosRel TextoLogin" style="margin-top: 7px; color: #ff5000;">Usuario:</span>
                    </div>
                    <div class="cFL cPosRel" style="width: 290px; height: 25px;">
                        <asp:TextBox ID="txtUsuario" CssClass="txtControl" runat="server"></asp:TextBox>
                    </div>
                    <div class="cFL cPosRel" style="width: 100px; height: 25px; margin-top: 15px; ">
                        <span class="cFR cPosRel TextoLogin" style="margin-top: 7px; color: #ff5000; ">Contraseña:</span>
                    </div>
                    <div class="cFL cPosRel" style="width: 290px; height: 25px; margin-top: 15px;">
                        <asp:TextBox ID="txtContraseña" TextMode="Password" CssClass="txtControl" runat="server"></asp:TextBox>
                    </div>
                    <div class="cFL cPosRel" style="width: 460px;">
                        <div style="width: 120px; height: 30px; margin-left: auto; margin-right: auto; margin-top: 15px; opacity:1; color:black;">
                            <asp:ImageButton ID="btnIniciar" ImageUrl="~/images/login/ingresarsistema.jpg" runat="server" 
                                OnClick="btnIniciar_Click"  />
                        </div>
                    </div>
                </div>
                <asp:Label ID="lblMensaje" CssClass="cFL" runat="server" ForeColor="red" ></asp:Label>
            </div>
            <div class="RightFormaLogin"  style="opacity:0.6;">
            </div>
             <asp:Label ID="lblNota" CssClass="cFL" runat="server" ForeColor="red" Text=""></asp:Label>
        </div>
    </form>
    <script src="js/jquery-1.11.1.min.js"></script>
    <script src="bootstrap/js/bootstrap.min.js"></script>
    <script src="js/jquery.backstretch.min.js"></script>
    <script src="js/scripts.js"></script>
    <script src="~/js/cuerpo.js"></script>
</body>
</html>
