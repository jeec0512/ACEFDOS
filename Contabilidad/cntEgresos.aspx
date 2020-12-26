<%@ Page Title="" Language="C#" MasterPageFile="~/Contabilidad/mpContabilidad.master" AutoEventWireup="true" CodeFile="cntEgresos.aspx.cs" Inherits="Contabilidad_cntEgresos" EnableEventValidation="false" %>

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
                    <asp:DropDownList ID="ddlAno" runat="server" CssClass="mainSelect" Visible="true" Enabled="true" DataTextField="ANOS" DataValueField="ANOS" AutoPostBack="True" OnSelectedIndexChanged="ddlAno_SelectedIndexChanged"></asp:DropDownList>
                </asp:Panel>



                <asp:Panel ID="pnPeriodo" runat="server" Style="display: grid;" Visible="true">
                    <asp:Label runat="server" ID="Label3" Text="Período" class="titulo3"></asp:Label>
                    <asp:DropDownList ID="ddlPeriodo" runat="server" CssClass="mainSelect" Visible="true" Enabled="true" DataTextField="DESCRIP" DataValueField="PERIODO" AutoPostBack="True" OnSelectedIndexChanged="ddlPeriodo_SelectedIndexChanged"></asp:DropDownList>
                </asp:Panel>

                <div runat="server" id="divModificaRegistros" visible="false">
                    <h3 class="titulo3">Ingreso de fechas</h3>

                    <div class="containerSelect">
                        <asp:Panel ID="pnSucursal" runat="server" CssClass="mainSelect" Visible="false">
                            <asp:DropDownList ID="ddlSucursal" DataTextField="nom_suc" DataValueField="sucursal" runat="server"
                                AutoPostBack="True" CssClass="mainSelect__item">
                            </asp:DropDownList>
                        </asp:Panel>
                         <asp:Panel ID="pnId" runat="server" Style="display: grid;" Visible="true">
                            <asp:Label runat="server" ID="lblId" Text="ID" class="titulo3"></asp:Label>
                            <asp:TextBox runat="server" ID="txtId" placeholder="Id"></asp:TextBox>
                        </asp:Panel>
                        <asp:Panel ID="pnSemana" runat="server" Style="display: grid;" Visible="true">
                            <asp:Label runat="server" ID="lblSemana" Text="# Semana" class="titulo3"></asp:Label>
                            <asp:DropDownList ID="ddlSemana" runat="server" CssClass="mainSelect" Visible="true" Enabled="true" DataTextField="DECRIPCION" DataValueField="SECUENCIA"></asp:DropDownList>
                        </asp:Panel>

                        <asp:Panel ID="pnFechaInicio" runat="server" Style="display: grid; margin-bottom: 20px;" Visible="true">
                            <asp:Label runat="server" ID="lblFechaInicio" Text="Fecha Inicio" class="titulo3"></asp:Label>
                            <asp:TextBox runat="server" ID="txtFechaInicio" placeholder="Fecha Inicio"></asp:TextBox>
                            <act1:CalendarExtender ID="CalendarExtender1" PopupButtonID="" runat="server" TargetControlID="txtFechaInicio"
                                Format="dd/MM/yyyy"></act1:CalendarExtender>
                            <act1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtFechaInicio" Mask="99/99/9999"
                                MessageValidatorTip="true"
                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                                AcceptNegative="Left"
                                DisplayMoney="Left" ErrorTooltipEnabled="True" />
                        </asp:Panel>
                        <asp:Panel ID="pnFechaFin" runat="server" Style="display: grid; margin-bottom: 20px;" Visible="true">
                            <asp:Label runat="server" ID="lblFechaFin" Text="Fecha Fin" class="titulo3"></asp:Label>
                            <asp:TextBox runat="server" ID="txtFechaFin" placeholder="Fecha Fin"></asp:TextBox>
                            <act1:CalendarExtender ID="CalendarExtender2" PopupButtonID="" runat="server" TargetControlID="txtFechaFin"
                                Format="dd/MM/yyyy"></act1:CalendarExtender>
                            <act1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtFechaInicio" Mask="99/99/9999"
                                MessageValidatorTip="true"
                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                                AcceptNegative="Left"
                                DisplayMoney="Left" ErrorTooltipEnabled="True" />
                        </asp:Panel>
                        <asp:Panel ID="pnFechaExportacion" runat="server" Style="display: grid; margin-bottom: 20px;" Visible="true">
                            <asp:Label runat="server" ID="lblFechaExportacion" Text="Fecha Exportación" class="titulo3"></asp:Label>
                            <asp:TextBox runat="server" ID="txtFechaExportacion" placeholder="Fecha exportación"></asp:TextBox>
                            <act1:CalendarExtender ID="CalendarExtender3" PopupButtonID="" runat="server" TargetControlID="txtFechaExportacion"
                                Format="dd/MM/yyyy"></act1:CalendarExtender>
                            <act1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtFechaExportacion" Mask="99/99/9999"
                                MessageValidatorTip="true"
                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                                AcceptNegative="Left"
                                DisplayMoney="Left" ErrorTooltipEnabled="True" />
                        </asp:Panel>
                        <asp:Label ID="lblEstado" CssClass="lblForm" runat="server" Text="Estado"></asp:Label>
                        <asp:Panel ID="pnActivo" runat="server" CssClass="pnFormChk">
                            <asp:CheckBox ID="chkActivo" TextAlign="Left" runat="server" />
                        </asp:Panel>

                        <div class="areaBotones main-principalActivarCurso__botonera subitem">
                            <div class="contieneBotones">
                                <h3 class="titulo3">Acciones</h3>
                                <div class="cajaBotones">
                                    <div class="buttonHolder">
                                        <asp:Button ID="btnGuardar" runat="server" CssClass="button button-title" Text="Guarda" BorderStyle="None" Visible="true" OnClick="btnGuardar_Click" />
                                        <asp:Button ID="btnCancelar" runat="server" CssClass="button button-title" Text="Cancela" BorderStyle="None" Visible="true" OnClick="btnCancelar_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>



            <div class="areaHorarios main-principalActivarCurso__horariosDisponibles subitem">
                <h3 class="titulo3">Fechas a procesar</h3>
                <div class="contieneHorasDisponibles" style="display:flex;flex-direction:column;">
                    <asp:Panel ID="pnAutos" runat="server" Visible="true" class="horariosDisponibles__item areaAutos" Style="overflow-y: auto;">
                        <asp:GridView ID="grvFechas" runat="server"
                            DataKeyNames="id_etiquetaDiariosContables"
                            AutoGenerateColumns="False"
                           OnSelectedIndexChanged="grvFechas_SelectedIndexChanged"
                            CssClass="grilla" ForeColor="Blue">
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" />
                                <asp:BoundField DataField="id_etiquetaDiariosContables" HeaderText="id" />
                                <asp:BoundField DataField="semana" HeaderText="Semana" />
                                <asp:BoundField DataField="fechaInicio" HeaderText="FechaInicio" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="fechaFin" HeaderText="FechaFin" HeaderStyle-CssClass="ancho" ItemStyle-CssClass="ancho" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="fechaExportacion" HeaderText="FechaExportacion" DataFormatString="{0:dd/MM/yyyy}" />

                                <asp:CheckBoxField DataField="estado">
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" CssClass="checkBox" ForeColor="Blue" Height="20px" Width="20px" />
                                </asp:CheckBoxField>
                            </Columns>
                            

                        </asp:GridView>
                    </asp:Panel>
                    <div runat="server" id="divBotones" class="areaBotones main-principalActivarCurso__botonera subitem">
                        <div class="contieneBotones">
                            <h3 class="titulo3">Acciones</h3>
                            <div class="cajaBotones">
                                <div class="buttonHolder">
                                    <asp:Button ID="btnNuevo" runat="server" CssClass="button button-title" Text="Nuevo" BorderStyle="None" Visible="true" OnClick="btnNuevo_Click" />
                                    <asp:Button ID="btnModifica" runat="server" CssClass="button button-title" Text="Modifica" BorderStyle="None" Visible="false" OnClick="btnModifica_Click" />
                                    <asp:Button ID="btnRegresa" runat="server" CssClass="button button-title" Text="Regresa" BorderStyle="None" Visible="true" OnClick="btnRegresa_Click" />
                                    <asp:Button ID="btnRegresar" runat="server" CssClass="button button-title" Text="Regresar" BorderStyle="None" Visible="false" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <footer class="footer-activarCurso item">
            <p>CONTABILIDAD</p>
        </footer>
    </div>
</asp:Content>

