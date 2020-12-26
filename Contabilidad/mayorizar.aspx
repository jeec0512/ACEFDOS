<%@ Page Title="" Language="C#" MasterPageFile="~/Contabilidad/mpContabilidad.master" AutoEventWireup="true" CodeFile="mayorizar.aspx.cs" 
    Inherits="Contabilidad_mayorizar"  EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">
     <link href="https://file.myfontastic.com/82NMqBMRcbALbXYak8fmp/icons.css" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,700,900" rel="stylesheet">
    <link rel="stylesheet" href="../App_Themes/estilos/fonts.css">

    <link rel="stylesheet" href="../App_Themes/estilos/icons.css">
    <link href="../App_Themes/estilos/grilla.css" rel="stylesheet" />

    <link rel="stylesheet" href="../App_Themes/estilos/activarCurso.css">

     
    <asp:ScriptManager runat="server" ID="sm1">
       
    </asp:ScriptManager>

    <div class="main-activarCurso">

        <div class="header-activarCurso item">
            <asp:Label runat="server" ID="lblMensaje" class="error-msg" Visible="true"><span class="icon-cancelcirculo"></span></asp:Label>
            <asp:Label ID="Label1" runat="server"></asp:Label>

            
        </div>
        <div class="areaActivarCurso main-principalActivarCurso item">
            <div class="areaSelects main-principalActivarCurso__select subitem">
                <asp:Panel ID="pnAno" runat="server" Style="display: grid;" Visible="true">
                    <asp:Label runat="server" ID="Label2" Text="Año" class="titulo3"></asp:Label>
                    <asp:DropDownList ID="ddlAno" runat="server" CssClass="mainSelect" Visible="true" Enabled="true" DataTextField="ANOS" DataValueField="ANOS" AutoPostBack="True"></asp:DropDownList>
                </asp:Panel>



                <asp:Panel ID="pnPeriodo" runat="server" Style="display: grid;" Visible="true">
                    <asp:Label runat="server" ID="Label3" Text="Período" class="titulo3"></asp:Label>
                    <asp:DropDownList ID="ddlPeriodo" runat="server" CssClass="mainSelect" Visible="true" Enabled="true" DataTextField="DESCRIP" DataValueField="PERIODO" AutoPostBack="True"></asp:DropDownList>
                </asp:Panel>

                <div runat="server" id="divModificaRegistros" visible="false">
                    <h3 class="titulo3">Ingreso de fechas</h3>

                    <div class="containerSelect">
                        <asp:Panel ID="pnSucursal" runat="server" CssClass="mainSelect" Visible="false">
                            <asp:DropDownList ID="ddlSucursal" DataTextField="nom_suc" DataValueField="sucursal" runat="server"
                                AutoPostBack="True" CssClass="mainSelect__item">
                            </asp:DropDownList>
                        </asp:Panel>

                    </div>
                </div>
            </div>

            <div class="areaHorarios main-principalActivarCurso__horariosDisponibles subitem">
                <h3 class="titulo3">Procesar</h3>
                 <div runat="server" id="divBotones" class="areaBotones main-principalActivarCurso__botonera subitem">
                        <div class="contieneBotones">
                            <div class="cajaBotones">
                                <div runat="server" class="buttonHolder" id="buttonHolder">
                                    <asp:Button ID="btnNuevo" runat="server" CssClass="button button-title" Text="Mayorizar" BorderStyle="None" Visible="true" OnClick="btnNuevo_Click" />
                                    <asp:Button ID="btnModifica" runat="server" CssClass="button button-title" Text="Modifica" BorderStyle="None" Visible="false" OnClick="btnModifica_Click" />
                                    <asp:Button ID="btnRegresa" runat="server" CssClass="button button-title" Text="Regresa" BorderStyle="None" Visible="false" OnClick="btnRegresa_Click" />
                                    <asp:Button ID="btnRegresar" runat="server" CssClass="button button-title" Text="Regresar" BorderStyle="None" Visible="false" />
                                     <asp:Button ID="btnExcelFe" runat="server" CssClass="button button-title" Text="Excel" BorderStyle="None" Visible="true" OnClick="btnExcelFe_Click"/>
                                </div>
                                <div id="procesando" style="display:none;width:30vw;height:30vh;background-color:rgba(128, 128, 128,0.5);z-index:999">
                                    <p style="color:rgb(255, 0, 0);font-size:large">
                                        Procesando...
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
					
                <div class="contieneHorasDisponibles" style="display:flex;flex-direction:column;">
                    <asp:Panel ID="pnAutos" runat="server" Visible="true" class="horariosDisponibles__item areaAutos" Style="overflow-y: auto;">
                        <asp:GridView ID="grvMayores" runat="server"
                            DataKeyNames="CODIGO_CUENTA_CONTABLE"
                            AutoGenerateColumns="False"
                            CssClass="" ForeColor="Blue" 
                            OnRowDataBound="grvMayores_RowDataBound"
                            ShowFooter="True" HtmlEncode=False;>
                            <Columns>
                                <asp:BoundField DataField="CODIGO_CUENTA_CONTABLE" HeaderText="Cuenta" />
                                <asp:BoundField DataField="NOMBRE_CUENTA_CONTABLE" HeaderText="Cuenta" />
                                <asp:BoundField DataField="DEBE" HeaderText="DEBE" DataFormatString="{0:N}" />
                                <asp:BoundField DataField="HABER" HeaderText="HABER" DataFormatString="{0:N}" />
                                <asp:BoundField DataField="SALDO" HeaderText="SALDO" DataFormatString="{0:N}" />
                            </Columns>
                            

                        </asp:GridView>
                    </asp:Panel>
                   
                </div>
            </div>
        </div>
        <footer class="footer-activarCurso item">
            <p>CONTABILIDAD</p>
        </footer>
    </div>
    <script type="text/javascript">
        function desactivarBoton() {
            document.getElementById("contenidoPrincipal_contenidoPrincipal_btnNuevo").style.display = "none";
            document.getElementById("contenidoPrincipal_contenidoPrincipal_btnExcelFe").style.display = "none";
            document.getElementById("procesando").style.display = "block";
        }
        function activarBoton() {
            document.getElementById("contenidoPrincipal_contenidoPrincipal_btnNuevo").disabled = false;
            document.getElementById("contenidoPrincipal_contenidoPrincipal_btnExcelFe").disabled = false;
            document.getElementById("procesando").style.display = "none";
        }
        window.onbeforeunload = desactivarBoton;

    </script>
</asp:Content>

