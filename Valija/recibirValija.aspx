<%@ Page Title="" Language="C#" MasterPageFile="~/Valija/mpValija.master" AutoEventWireup="true" CodeFile="recibirValija.aspx.cs" Inherits="Valija_recibirValija" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server"><asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>



    <!-- MENSAJE!-->
    <asp:Panel ID="pnMensaje2" CssClass="" runat="server" Visible="true">
        <asp:Label ID="lblMensaje" runat="server" Text="" Visible="true"></asp:Label>
        <asp:Button ID="btnIngresaProv" runat="server" Text="Ingrese el proveedor" Visible="false" />
    </asp:Panel>

    <!-- CABECERA INGRESO DE SUCURSAL Y FECHA  !-->
    <asp:Panel ID="pnTitulos" CssClass="" runat="server" Visible="true">

        <fieldset id="cabEgresos" class="fieldset-principal">
            <legend>Registro de valija recibida</legend>

            <asp:Panel ID="pnPrincipal" CssClass="pnBtnProcesos" runat="server" Visible="true">

                <asp:Label ID="lblSuc" runat="server" Text="Sucursal"></asp:Label>

                <asp:DropDownList ID="ddlSucursal2" runat="server" DataTextField="nom_suc" DataValueField="sucursal">
                </asp:DropDownList>
                <asp:Label ID="lblFecha" runat="server" Text="Fecha:"></asp:Label>
                <asp:TextBox runat="server" ID="txtFecha"></asp:TextBox>
                <act1:CalendarExtender ID="Calfecha" PopupButtonID="" runat="server" TargetControlID="txtFecha" Format="dd/MM/yyyy">
                </act1:CalendarExtender>
                <act1:MaskedEditExtender ID="maskFecha" runat="server" TargetControlID="txtFecha" Mask="99/99/9999" MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                    AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="True" />

                <asp:Button ID="btnConsultar" runat="server" CssClass="btnProceso" Text="Consultar"
                    OnClick="btnConsultar_Click" />

            </asp:Panel>

        </fieldset>


    </asp:Panel>

    <!-- LISTADO DE PAGOS!-->
    <asp:Panel ID="pnDetallePagos" runat="server" CssClass="" Visible="true">
        <fieldset id="listados" class="fieldset-principal">
            <legend>Listado de paquetes recibidos</legend>

            <asp:GridView ID="grvDetallePagos" runat="server"
                AutoGenerateColumns="False"
                CellPadding="5"
                GridLines="Vertical"
                HorizontalAlign="Center"
                Width="100%"
                AllowPaging="FALSE"
                AllowSorting="True"
                PageSize="50"
                DataKeyNames="NUMERO" >

                <AlternatingRowStyle
                    BackColor="#DCDCDC" />

                <Columns>
                    <asp:CommandField
                        ShowSelectButton="false" />

                    <asp:TemplateField HeaderText="ENTREGADO A">
                        <ItemTemplate>
                            <%# Eval("ENTREGADO") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SUCURSAL ORIGEN">
                        <ItemTemplate>
                            <%# Eval("NOM_SUC") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DESCRIPCION">
                        <ItemTemplate>
                            <%# Eval("descripcionRecibe") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="# GUIA">
                        <ItemTemplate>
                            <%# Eval("NUMEROGUIA") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FIRMA">
                        <ItemTemplate>
                            <%# Eval("FIRMA") %>
                        </ItemTemplate>
                    </asp:TemplateField>


                </Columns>
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#0C80BF" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="black" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView>
            <asp:Panel ID="pnMenu" CssClass="pnFormHijo" runat="server" Visible="true">
                <!-- BOTON PARA CREAR REGISTRO DE EGRESOS!-->
                <asp:Button ID="btnNuevoRegistro" runat="server" CssClass="btnProceso" Text="Nuevo Registro"
                    Visible="true" OnClick="btnNuevoRegistro_Click" />
                <asp:Button ID="btnRegresar" runat="server" CssClass="btnProceso" Text="Regresar" Visible="true" 
                    OnClick="btnRegresar_Click" />
            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <!-- INGRESO DE REGISTRO DE ENVIOS!-->
    <asp:Panel ID="pnEnvios" runat="server" CssClass="" Visible="true">
        <fieldset id="registrar" class="fieldset-principal">
            <legend>Registrar paquetes</legend>


            <!-- se utiliza para procesos internos no se visializa  !-->
            <asp:Panel ID="pnDatosGenerales" CssClass="pnPeq" runat="server" Visible="false" GroupingText="" ForeColor="#1d92e9">
                <asp:Label ID="lblSucursal" CssClass="lblPeq" runat="server" Text="Sucursal" Visible="true"></asp:Label>
                <asp:TextBox ID="txtSucursal" CssClass="txtPeq" runat="server" Visible="true"></asp:TextBox>
                <asp:Label ID="lblIFecha" CssClass="lblPeq" runat="server" Text="Fecha" Visible="true"></asp:Label>
                <asp:TextBox ID="txtIFecha" CssClass="txtPeq" runat="server" Visible="true"></asp:TextBox>

            </asp:Panel>

            <fieldset id="fsCaja" class="fieldset-principal">
                <legend></legend>
                <asp:Panel ID="pnBienes" CssClass="pnPeq" runat="server" Visible="true" GroupingText="" ForeColor="#1d92e9">
                    <!--<asp:Label ID="lblnumero" CssClass="lblPeq" runat="server" Text="Cód/caja" Visible="true"></asp:Label>!-->
                    <asp:TextBox ID="txtNumero" CssClass="txtPeq" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>

                    <!--<asp:Label ID="lblColaborador" CssClass="lblPeq" runat="server" Text="Colaborador" Visible="true"></asp:Label>!-->
                    <asp:Panel ID="pnEmisor" runat="server" CssClass="pnPeqDdl" Visible="true">
                        <asp:DropDownList ID="ddlEmisor" runat="server" CssClass="peqDdl" DataTextField="nombres" DataValueField="cedula"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </asp:Panel>


                </asp:Panel>
                <asp:Panel ID="pnProveedor" CssClass="pnPeq" runat="server" Visible="true" GroupingText="" ForeColor="#1d92e9">
                    <asp:Panel ID="pnSucDest" runat="server" CssClass="pnPeqDdl">

                        <asp:DropDownList ID="ddlSucDest" runat="server" CssClass="peqDdl" DataTextField="nom_suc" DataValueField="sucursal"
                            plaholder="Sucursal Destino">
                        </asp:DropDownList>
                    </asp:Panel>
                    <asp:Panel ID="pnDepartamento" runat="server" CssClass="pnPeqDdl">
                        <asp:DropDownList ID="ddlDepartamento" runat="server" CssClass="peqDdl" DataTextField="nom_dep" DataValueField="id_departamento"
                            plaholder="Departamento">
                        </asp:DropDownList>
                    </asp:Panel>
                    <asp:Panel ID="pnReceptor" runat="server" CssClass="pnPeqDdl" Visible="true">
                        <asp:DropDownList ID="ddlReceptor" runat="server" CssClass="peqDdl" DataTextField="nombres" DataValueField="cedula"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </asp:Panel>

                </asp:Panel>
                <asp:Panel ID="pnPaquete" CssClass="pnPeq" runat="server" Visible="true" GroupingText="" ForeColor="#1d92e9">
                    <!--<asp:Label ID="lblRuc" CssClass="lblPeq" runat="server" Text="R.U.C./C.C."></asp:Label>!-->
                    <asp:TextBox ID="txtGuia" CssClass="txtPeq" runat="server" AutoPostBack="True"
                        placeHolder="Número de Guía"></asp:TextBox>
                    <!--<asp:Label ID="lblNombres" CssClass="lblPeq" runat="server" Text="Nombres" Visible="true"></asp:Label>!-->
                    <asp:TextBox ID="txtDescripcion" CssClass="txtTitPeq" runat="server" Enabled="true" placeHolder="Descripción"></asp:TextBox>
                </asp:Panel>

            </fieldset>
        </fieldset>
        <asp:Panel ID="Panel4" runat="server" CssClass="" Visible="true">

            <fieldset id="fsBotonera" class="fieldset-principal">
                <legend></legend>
                <asp:Panel ID="pnMenuGrabar" CssClass="" runat="server">
                    <asp:Button ID="btnValidar" CssClass="btnProceso" runat="server" Text="Validar" Visible="false" />
                    <asp:Button ID="btnGrabarPago" CssClass="btnProceso" runat="server" Text="Grabar"
                        OnClick="btnGrabarPago_Click" />
                    <asp:Button ID="btnCancelarpago" CssClass="btnProceso" runat="server" Text="Regresar"
                        OnClick="btnCancelarpago_Click" />
                </asp:Panel>
            </fieldset>

        </asp:Panel>
    </asp:Panel>




    <!-- ELIMINAR!-->
    <asp:Panel ID="pnBorrar" runat="server" CssClass="" Visible="true">
        <fieldset id="eliminar" class="fieldset-principal">
            <legend>Eliminar paquete</legend>


            <asp:Label ID="lblBCodigo" CssClass="lblForm" runat="server" Text="Código" Visible="true"></asp:Label>
            <asp:TextBox ID="txtBCodigo" CssClass="txtForm" runat="server" Visible="true" Enabled="true"></asp:TextBox>
            <asp:Label ID="lblBsuc" CssClass="lblForm" runat="server" Text="Sucursal destino"></asp:Label>
            <asp:TextBox ID="txtBSuc" CssClass="txtForm" runat="server" Enabled="true"></asp:TextBox>
            <asp:Label ID="lblBDepartamento" CssClass="lblForm" runat="server" Text="Departamento"></asp:Label>
            <asp:TextBox ID="txtBDepartamento" CssClass="txtForm" runat="server" Enabled="true"></asp:TextBox>
            <asp:Label ID="lblBGuia" CssClass="lblForm" runat="server" Text="# Guía" Visible="true"></asp:Label>
            <asp:TextBox ID="txtBGuia" CssClass="txtForm" runat="server" Visible="true" Enabled="true"></asp:TextBox>

            <asp:Label ID="lblBDescripcion" CssClass="lblForm" runat="server" Text="Descripción"></asp:Label>
            <asp:TextBox ID="txtBDescripcion" CssClass="txtForm" runat="server" Enabled="true"></asp:TextBox>


            <asp:Panel ID="pnMenuBorrar" runat="server">
                <asp:Button ID="btnBorrarPago" CssClass="btnProceso" runat="server" Text="Borrar" 
                    OnClick="btnBorrarPago_Click" />
                <asp:Button ID="btnRegresar3" CssClass="btnProceso" runat="server" Text="Regresar" 
                    OnClick="btnRegresar3_Click" />
            </asp:Panel>



        </fieldset>
    </asp:Panel>

    <!-- EXPORTAR!-->
    <asp:Panel ID="pnExportar" CssClass="" runat="server" Wrap="true" Visible="true">
        <fieldset id="excel" class="fieldset-principal">
            <legend>EXPORTAR</legend>

            <asp:Button ID="btnExcelRe" runat="server" CssClass="btnProceso" Text="A Excel" Visible="true" 
                OnClick="btnExcelRe_Click" />

             <asp:Button ID="btnImprimir" runat="server" CssClass="btnProceso" Text="Enviar e imprimir" 
                Visible="true" OnClick="btnImprimir_Click" />
            <asp:Button ID="Button1" runat="server" CssClass="btnProceso" Text="Regresar" Visible="true" 
                    OnClick="btnRegresar_Click" />

        </fieldset>
    </asp:Panel>

    <!-- INGRESAR PROVEEDOR !-->
    <asp:Panel ID="pnIngresarProveedor" runat="server" Visible="false">
        <asp:Label ID="lblAviso" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>
        <asp:Panel ID="pnSucursal" runat="server">
            <fieldset id="fsSucursal">
                <legend>Datos de la Matriz</legend>
                <asp:Panel ID="Panel2" runat="server" CssClass="pnFormTitulo">
                    <asp:Label ID="lblProveedor" CssClass="lblForm" runat="server" Text="Documento de identificación "></asp:Label>
                    <asp:TextBox ID="txtProveedor" CssClass="txtForm" runat="server"></asp:TextBox>
                    <asp:ImageButton ID=ibConsultar runat="server" ImageUrl="~/images/iconos/219_2.png" />
                </asp:Panel>
                <asp:Label ID="lblrazonsocial" CssClass="lblForm" runat="server" Text="Razón Social"></asp:Label>
                <asp:TextBox ID="txtrazonsocial" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblnombreComercial" CssClass="lblForm" runat="server" Text="Nombre comercial"></asp:Label>
                <asp:TextBox ID="txtnombreComercial" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lbldirMatriz" CssClass="lblForm" runat="server" Text="Dirección"></asp:Label>
                <asp:TextBox ID="txtdirMatriz" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblcontribuyenteEspecial" CssClass="lblForm" runat="server" Text="# Contribuyente especial"></asp:Label>
                <asp:TextBox ID="txtcontribuyenteEspecial" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblobligadoContabilidad" CssClass="lblForm" runat="server" Text="Obligado a llevar Contabilidad"></asp:Label>
                <asp:Panel ID="pnObligado" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlObligado" runat="server">
                        <asp:ListItem Value="SI">SI</asp:ListItem>
                        <asp:ListItem Value="NO">NO</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
                <asp:Label ID="lblTel" CssClass="lblForm" runat="server" Text="Teléfono"></asp:Label>
                <asp:TextBox ID="txtTel" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblEmail" CssClass="lblForm" runat="server" Text="E-mail"></asp:Label>
                <asp:TextBox ID="txtEmail" CssClass="txtForm" runat="server"></asp:TextBox>


                <asp:Panel ID="pnBotonera" runat="server" CssClass="pnFormBotonera">
                    <asp:Button ID=btnGuardar runat="server" Text="Grabar" CssClass="btnForm" />
                    <asp:Button ID=btnRegresar2 runat="server" Text="regresar" CssClass="btnForm" />
                </asp:Panel>

            </fieldset>
        </asp:Panel>
    </asp:Panel>
</asp:Content>

