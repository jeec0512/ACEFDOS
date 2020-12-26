<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ingresar.aspx.cs" Inherits="ingresar" Culture="es-ES" UICulture="es-ES" %>

<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>ANETA</title>
    <link rel="shortcut icon" href="~/images/iconos/icoaneta2.png" />
    <link href="App_Themes/Estilos/Login.css" rel="stylesheet" />
    <style>
        body {
            /* width:100vw;*/
        }

        .container {
            max-width: 100vw;
            margin: 0 auto;
        }

        .bg-image {
            width: 80%;
            padding: 1%;
            background-size: contain;
            background: url(images/fondos/fondo3.jpg);
            background-repeat: no-repeat;
        }
        .mensajes {
            display: flex;
            width:800px;
            height:40px;
            color:#fff;
            font-size:40px;
            line-height:40px;

            position:absolute;
            top:0;
            right:0;
            bottom:0;
            left:0;
            margin:auto;
            overflow:hidden;
        }

        ul {
            list-style: none;
            padding-left:10px;
            animation: cambiar 7s infinite;
        }

        ul, p {
            margin:0;
        }

        @keyframes cambiar {
            0% {margin-top: 0;}
            20% {margin-top: 0;}

            25% {margin-top: -40px;}
            50% {margin-top: -40px;}
            
            55% {margin-top: -80px;}
            80% {margin-top: -80px;}

            85% {margin-top: -40px;}
            95% {margin-top: -40px;}

            100% {margin-top: 0px;}

        }

        #particles-js {
            width: 100%;
            height: 100%;
            position: fixed;
            /*/   background:rgba(0,0,0,.8);*/
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="container">
        <div>
            <div style="display: flex; justify-content: space-between; height: 2.5rem; background-color: #FEF5EB; margin-top: -10px;">
                <div id="tituloAneta" style="margin: 0.3rem; padding: 0.5rem;">
                    <asp:Label runat="server" ID="txtTituloAneta" Text="ANETA" Style="font-size: 1.5rem; color: blue;"></asp:Label>
                </div>
                <div style="display: flex; justify-content: right; padding: 0.5rem;">
                    <div style="margin: 0.3rem;">
                        <span style="font-size: 1rem; color: #2d2d2d;">Usuario:</span>
                    </div>
                    <div>
                        <asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>
                    </div>
                    <div style="margin: 0.3rem;">
                        <span style="font-size: 1rem; color: #2d2d2d;">Contraseña:</span>
                    </div>
                    <div>
                        <asp:TextBox ID="txtContraseña" TextMode="Password" runat="server"></asp:TextBox>
                    </div>
                    <div>

                        <asp:Button ID="btnIngresar" Text="Ingresar" runat="server" OnClick="btnIngresar_Click" Style="font-size: 1rem; color: #2d2d2d; background-color: #094697; color: #FEF5EB" />
                    </div>

                </div>

            </div>
            <asp:Label ID="lblMensaje" CssClass="cFR" runat="server" ForeColor="white" Style="font-size: 1rem"></asp:Label>
        </div>
        <div id="particles-js" class="bg-image">
            <div class="mensajes">
                <p>
                    
                </p>
                <ul>
                    <!--<li>Que la prosperidad y paz</li>
                    <li>inunde el nuevo año</li>
                    <li>y que sea 20/20 </li>-->

                </ul>
            </div>
        </div>
    </form>
    <!--<script src="js/jquery-1.11.1.min.js"></script>
    <script src="bootstrap/js/bootstrap.min.js"></script>
    <script src="js/jquery.backstretch.min.js"></script>
    <script src="js/scripts.js"></script>
    <script src="~/js/cuerpo.js"></script>-->
    <script src="js/particles.js"></script>
    <script src="js/particulas.js"></script>

</body>
</html>
