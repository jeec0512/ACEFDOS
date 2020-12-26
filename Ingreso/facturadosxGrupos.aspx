<%@ Page Title="" Language="C#" MasterPageFile="~/Ingreso/mpIngreso.master" AutoEventWireup="true" CodeFile="facturadosxGrupos.aspx.cs"
    Inherits="Ingreso_facturadosxGrupos" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <!-- MENSAJE!-->
    <asp:Panel ID="pnMensaje2" CssClass="" runat="server" Visible="true">
        <asp:Label ID="lblMensaje" runat="server" Text="" Visible="true"></asp:Label>
        <asp:Button ID="btnIngresaProv" runat="server" Text="Ingrese el proveedor" Visible="false" />
    </asp:Panel>

    <!-- CABECERA INGRESO DE SUCURSAL Y FECHAs  !-->
    <asp:Panel ID="pnTitulos" CssClass="" runat="server" Visible="true">

        <fieldset id="fdTitulos" class="fieldset-principal">
            <legend>Facturas por grupos</legend>
            <asp:Panel ID="pnDatos" CssClass="pnPeq" runat="server" Visible="true">

                <asp:Label ID="lblSuc" runat="server" Text="Sucursal" CssClass="lblPeq"></asp:Label>

                <asp:DropDownList ID="ddlSucursal2" runat="server" CssClass="peqDdl" DataTextField="nom_suc" DataValueField="sucursal">
                </asp:DropDownList>

                <asp:TextBox runat="server" ID="txtFechaIni" CssClass="txtPeq"></asp:TextBox>
                <act1:CalendarExtender ID="Calfecha" PopupButtonID="" runat="server" TargetControlID="txtFechaIni" Format="dd/MM/yyyy">
                </act1:CalendarExtender>
                <act1:MaskedEditExtender ID="maskFecha" runat="server" TargetControlID="txtFechaIni" Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                    AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="True" />

                <asp:TextBox runat="server" ID="txtFechaFin" CssClass="txtPeq"></asp:TextBox>
                <act1:CalendarExtender ID="CalendarExtender1" PopupButtonID="" runat="server" TargetControlID="txtFechaFin"
                    Format="dd/MM/yyyy"></act1:CalendarExtender>
                <act1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtFechaFin" Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                    AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="True" />
            </asp:Panel>
            <asp:Panel ID="Panel3" CssClass="pnAccionGrid" runat="server">
                <asp:Button ID="btnTodos" runat="server" CssClass="btnProceso" Text="Facturas emitidas por sucursal"
                    OnClick="btnTodos_Click" />
                <asp:Button ID="btnConsolidado" runat="server" CssClass="btnProceso" Text="Totales por grupos"
                    OnClick="btnConsolidado_Click" Visible="true" />
                <asp:Button ID="btnFacturasAneta" runat="server" CssClass="btnProceso" Text="Cantidades por grupos"
                    Visible="true" OnClick="btnFacturasAneta_Click" />
            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnListado" runat="server" CssClass="" Visible="true">
        <fieldset id="fsListado" class="fieldset-principal">
            <legend>Facturas emitidas por grupo</legend>

            <asp:Button ID="btnExcelFe" runat="server" CssClass="btnLargoForm " Text="A Excel" Visible="true" OnClick="btnExcelFe_Click" />

            <asp:GridView ID="grvListadoFac" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="5"
                GridLines="Vertical" HorizontalAlign="Center" Width="90%" AllowPaging="True"
                AllowSorting="True" PageSize="100">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:CommandField ShowSelectButton="False" />

                    <asp:TemplateField HeaderText="Código" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("fac_sucursal") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sucursal" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("nom_suc") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Grupo" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("codigo") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Cantidad" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("cantidad") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Valor" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("valor") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unitario" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("Unitario") %>
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
        </fieldset>

    </asp:Panel>

    <asp:Panel ID="pnConsolidado" runat="server" CssClass="" Visible="true">
        <fieldset id="fsConsolidado" class="fieldset-principal">
            <legend>Total facturado por grupo y por sucursal</legend>

            <asp:Button ID="btnExcelC" runat="server" CssClass="btnLargoForm " Text="A Excel" Visible="true" OnClick="btnExcelC_Click" />

            <asp:GridView ID="grvConsolidado" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1"
                GridLines="Vertical" HorizontalAlign="Center" Width="90%"
                AllowPaging="True" AllowSorting="True" PageSize="50">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="fac_sucursal" HeaderText="Código" Visible="true" />
                   <asp:BoundField DataField="nom_suc" HeaderText="Sucursal" Visible="true" />
                    <asp:BoundField DataField="ACTIVOS_FIJOS" HeaderText="ACTIVOS_FIJOS" Visible="true" />
                    <asp:BoundField DataField="ARRIENDOS_LOCALES" HeaderText="ARRIENDOS_LOCALES" Visible="true" />
                    <asp:BoundField DataField="ARTICULOS_PUBLIC" HeaderText="ARTICULOS_PUBLIC" Visible="true" />
                    <asp:BoundField DataField="AUSPICIOS_COMISIONES" HeaderText="AUSPICIOS_COMISIONES" Visible="true" />
                    <asp:BoundField DataField="CERTIFICADO_DATOS" HeaderText="CERTIFICADO_DATOS" Visible="true" />
                    <asp:BoundField DataField="CURSOS_CAPACITACION" HeaderText="CURSOS_CAPACITACION" Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_CBP" HeaderText="CURSOS_CONDUCCION_CBP" Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_MOTO" HeaderText="CURSOS_CONDUCCION_MOTO" Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_PVF" HeaderText="CURSOS_CONDUCCION_PVF" Visible="true" />
                    <asp:BoundField DataField="CURSOS_KARTING" HeaderText="CURSOS_KARTING" Visible="true" />
                    <asp:BoundField DataField="CURSOS_TEORIA" HeaderText="CURSOS_TEORIA" Visible="true" />
                    <asp:BoundField DataField="EVALUACIONES_PRACT" HeaderText="EVALUACIONES_PRACT" Visible="true" />
                    <asp:BoundField DataField="EVALUACIONES_PSICOT" HeaderText="EVALUACIONES_PSICOT" Visible="true" />
                    <asp:BoundField DataField="EXAMEN_PRACTICO" HeaderText="EXAMEN_PRACTICO" Visible="true" />
                    <asp:BoundField DataField="EXAMEN_PSICOTECNICO" HeaderText="EXAMEN_PSICOTECNICO" Visible="true" />
                    <asp:BoundField DataField="EXAMEN_TEORICO" HeaderText="EXAMEN_TEORICO" Visible="true" />
                    <asp:BoundField DataField="HORAS_PRACTICA" HeaderText="HORAS_PRACTICA" Visible="true" />
                    <asp:BoundField DataField="HORAS_TEORIA" HeaderText="HORAS_TEORIA" Visible="true" />
                    <asp:BoundField DataField="LIBRETAS_PASO" HeaderText="LIBRETAS_PASO" Visible="true" />
                    <asp:BoundField DataField="LICENCIAS_DEPORTIVAS" HeaderText="LICENCIAS_DEPORTIVAS" Visible="true" />
                    <asp:BoundField DataField="LUBRICANTES" HeaderText="LUBRICANTES" Visible="true" />
                    <asp:BoundField DataField="MANUAL_CONDUCCION" HeaderText="MANUAL_CONDUCCION" Visible="true" />
                    <asp:BoundField DataField="MANUAL_CONDUCCION11" HeaderText="MANUAL_CONDUCCION11" Visible="true" />    
                    <asp:BoundField DataField="MEMBRESIA_EVALUAC" HeaderText="MEMBRESIA_EVALUAC" Visible="true" />
                    <asp:BoundField DataField="MEMBRESIA_MOTO" HeaderText="MEMBRESIA_MOTO    " Visible="true" />
                    <asp:BoundField DataField="MEMBRESIA_PREM" HeaderText="MEMBRESIA_PREM    " Visible="true" />
                    <asp:BoundField DataField="MEMBRESIA_PREM_DESC" HeaderText="MEMBRESIA_PREM_DESC  " Visible="true" />
                    <asp:BoundField DataField="MEMBRESIA_STAND" HeaderText="MEMBRESIA_STAND   " Visible="true" />
                    <asp:BoundField DataField="MEMBRESIA_TAXI" HeaderText="MEMBRESIA_TAXI    " Visible="true" />
                    <asp:BoundField DataField="PERMISOS_APRENDIZAJE_AUTOS" HeaderText="PERMISOS_APRENDIZAJE_AUTOS    " Visible="true" />
                    <asp:BoundField DataField="PERMISOS_APRENDIZAJE_MOTOS" HeaderText="PERMISOS_APRENDIZAJE_MOTOS    " Visible="true" />
                    <asp:BoundField DataField="RECUPERACION_PUNTOS" HeaderText="RECUPERACION_PUNTOS  " Visible="true" />
                    <asp:BoundField DataField="REPUESTOS" HeaderText="REPUESTOS     " Visible="true" />
                    <asp:BoundField DataField="SERVICIO_CSA" HeaderText="SERVICIO CSA    " Visible="true" />
                    <asp:BoundField DataField="SERVICIOS_ADMINIST" HeaderText="SERVICIOS_ADMINIST   " Visible="true" />
                    <asp:BoundField DataField="SERVICIOS_AUX" HeaderText="SERVICIOS_AUX " Visible="true" />
                    <asp:BoundField DataField="SERVICIOS_CHOF" HeaderText="SERVICIOS_CHOF    " Visible="true" />
                    <asp:BoundField DataField="SERVICIOS_CONV" HeaderText="SERVICIOS_CONV    " Visible="true" />
                    <asp:BoundField DataField="SERVICIOS_GRUA" HeaderText="SERVICIOS_GRUA    " Visible="true" />
                    <asp:BoundField DataField="TARJETAS_SOCIOS" HeaderText="TARJETAS_SOCIOS   " Visible="true" />
                    <asp:BoundField DataField="TEST_CONDUCCION" HeaderText="TEST_CONDUCCION   " Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_MOTOESM" HeaderText="CURSOS_CONDUCCION_MOTOESM     " Visible="true" />
                    <asp:BoundField DataField="EVALUACIONES_PSICOTDESC" HeaderText="EVALUACIONES_PSICOTDESC       " Visible="true" />
                    <asp:BoundField DataField="MEMBRESIA_PREM2" HeaderText="MEMBRESIA_PREM2   " Visible="true" />
                    <asp:BoundField DataField="MEMBRESIA_PREM3" HeaderText="MEMBRESIA_PREM3   " Visible="true" />
                    <asp:BoundField DataField="SIN_GRUPO" HeaderText="SIN_GRUPO     " Visible="true" />
                    <asp:BoundField DataField="PERMISOS_INTERNAC" HeaderText="PERMISOS_INTERNAC " Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_POL" HeaderText="CURSOS_CONDUCCION_POL" Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_TUM" HeaderText="CURSOS_CONDUCCION_TUM" Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_QTF" HeaderText="CURSOS_CONDUCCION_QTF" Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_CALEM" HeaderText="CURSOS_CONDUCCION_CALEM       " Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_SOL" HeaderText="CURSOS_CONDUCCION_SOL" Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_EVA" HeaderText="CURSOS_CONDUCCION_EVA" Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_EPS" HeaderText="CURSOS_CONDUCCION_EPS" Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_IN" HeaderText="CURSOS_CONDUCCION_IN " Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_NORM" HeaderText="CURSOS_CONDUCCION_NORM        " Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_SECOM" HeaderText="CURSOS_CONDUCCION_SECOM       " Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_CANTON" HeaderText="CURSOS_CONDUCCION_CANTON      " Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_CXC" HeaderText="CURSOS_CONDUCCION_CXC" Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_MTM" HeaderText="CURSOS_CONDUCCION_MTM" Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_PS" HeaderText="CURSOS_CONDUCCION_PS " Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_DESP" HeaderText="CURSOS_CONDUCCION_DESP        " Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_QCI" HeaderText="CURSOS_CONDUCCION_QCI" Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_POME" HeaderText="CURSOS_CONDUCCION_POME        " Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_MOTOESM3" HeaderText="CURSOS_CONDUCCION_MOTOESM3    " Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_MOTO1" HeaderText="CURSOS_CONDUCCION_MOTO1       " Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_DA" HeaderText="CURSOS_CONDUCCION_DA " Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_DAT" HeaderText="CURSOS_CONDUCCION_DAT" Visible="true" />
                    <asp:BoundField DataField="RECATEG_PRACTICA_EVA2" HeaderText="RECATEG_PRACTICA_EVA2" Visible="true" />
                    <asp:BoundField DataField="RECATEG_PRACTICA_EVA3" HeaderText="RECATEG_PRACTICA_EVA3" Visible="true" />
                    <asp:BoundField DataField="RECATEG_PSICOT_EVA2" HeaderText="RECATEG_PSICOT_EVA2  " Visible="true" />
                    <asp:BoundField DataField="RECATEG_PSICOT_EVA3" HeaderText="RECATEG_PSICOT_EVA3  " Visible="true" />
                    <asp:BoundField DataField="EXAMEN_PSICOTECNICO10" HeaderText="EXAMEN_PSICOTECNICO10" Visible="true" />
                    <asp:BoundField DataField="EXAMEN_PSICOTECNICO_OFTAMED" HeaderText="EXAMEN_PSICOTECNICO_OFTAMED   " Visible="true" />
                    <asp:BoundField DataField="EXAMEN_PSICOTECNICO_OPTA" HeaderText="EXAMEN_PSICOTECNICO_OPTA      " Visible="true" />
                    <asp:BoundField DataField="EXAMEN_PSICOTECNICO13" HeaderText="EXAMEN_PSICOTECNICO13" Visible="true" />
                    <asp:BoundField DataField="EXAMEN_PSICOTECNICO15" HeaderText="EXAMEN_PSICOTECNICO15" Visible="true" />
                    <asp:BoundField DataField="EXAMEN_PSICOTECNICO_BGR" HeaderText="EXAMEN_PSICOTECNICO_BGR       " Visible="true" />
                    <asp:BoundField DataField="EXAMEN_PSICOTECNICO_ANDEC" HeaderText="EXAMEN_PSICOTECNICO_ANDEC     " Visible="true" />
                    <asp:BoundField DataField="EXAMEN_PSICOTECNICO_FS" HeaderText="EXAMEN_PSICOTECNICO_FS        " Visible="true" />
                    <asp:BoundField DataField="EXAMEN_PSICOTECNICO20" HeaderText="EXAMEN_PSICOTECNICO20" Visible="true" />
                    <asp:BoundField DataField="EXAMEN_PSICOTECNICO30" HeaderText="EXAMEN_PSICOTECNICO30" Visible="true" />
                    <asp:BoundField DataField="EXAMEN_PSICOTECNICO45" HeaderText="EXAMEN_PSICOTECNICO45" Visible="true" />
                    <asp:BoundField DataField="EXAMEN_PSICOTECNICO_GAS" HeaderText="EXAMEN_PSICOTECNICO_GAS       " Visible="true" />
                    <asp:BoundField DataField="EXAMEN_PSICOTECNICO_FENA" HeaderText="EXAMEN_PSICOTECNICO_FENA      " Visible="true" />
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
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnCantidad" runat="server" CssClass="" Visible="true">
        <fieldset id="Fieldset1" class="fieldset-principal">
            <legend>Catidad de facturas por grupo y por sucursal</legend>

            <asp:Button ID="btnExcelA" runat="server" CssClass="btnLargoForm " Text="A Excel" Visible="true" OnClick="btnExcelA_Click" />

            <asp:GridView ID="grvCantidad" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1"
                GridLines="Vertical" HorizontalAlign="Center" Width="90%"
                AllowPaging="True" AllowSorting="True" PageSize="50">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="fac_sucursal" HeaderText="Código" Visible="true" />
                   <asp:BoundField DataField="nom_suc" HeaderText="Sucursal" Visible="true" />
                    <asp:BoundField DataField="ACTIVOS_FIJOS" HeaderText="ACTIVOS_FIJOS" Visible="true" />
                    <asp:BoundField DataField="ARRIENDOS_LOCALES" HeaderText="ARRIENDOS_LOCALES" Visible="true" />
                    <asp:BoundField DataField="ARTICULOS_PUBLIC" HeaderText="ARTICULOS_PUBLIC" Visible="true" />
                    <asp:BoundField DataField="AUSPICIOS_COMISIONES" HeaderText="AUSPICIOS_COMISIONES" Visible="true" />
                    <asp:BoundField DataField="CERTIFICADO_DATOS" HeaderText="CERTIFICADO_DATOS" Visible="true" />
                    <asp:BoundField DataField="CURSOS_CAPACITACION" HeaderText="CURSOS_CAPACITACION" Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_CBP" HeaderText="CURSOS_CONDUCCION_CBP" Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_MOTO" HeaderText="CURSOS_CONDUCCION_MOTO" Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_PVF" HeaderText="CURSOS_CONDUCCION_PVF" Visible="true" />
                    <asp:BoundField DataField="CURSOS_KARTING" HeaderText="CURSOS_KARTING" Visible="true" />
                    <asp:BoundField DataField="CURSOS_TEORIA" HeaderText="CURSOS_TEORIA" Visible="true" />
                    <asp:BoundField DataField="EVALUACIONES_PRACT" HeaderText="EVALUACIONES_PRACT" Visible="true" />
                    <asp:BoundField DataField="EVALUACIONES_PSICOT" HeaderText="EVALUACIONES_PSICOT" Visible="true" />
                    <asp:BoundField DataField="EXAMEN_PRACTICO" HeaderText="EXAMEN_PRACTICO" Visible="true" />
                    <asp:BoundField DataField="EXAMEN_PSICOTECNICO" HeaderText="EXAMEN_PSICOTECNICO" Visible="true" />
                    <asp:BoundField DataField="EXAMEN_TEORICO" HeaderText="EXAMEN_TEORICO" Visible="true" />
                    <asp:BoundField DataField="HORAS_PRACTICA" HeaderText="HORAS_PRACTICA" Visible="true" />
                    <asp:BoundField DataField="HORAS_TEORIA" HeaderText="HORAS_TEORIA" Visible="true" />
                    <asp:BoundField DataField="LIBRETAS_PASO" HeaderText="LIBRETAS_PASO" Visible="true" />
                    <asp:BoundField DataField="LICENCIAS_DEPORTIVAS" HeaderText="LICENCIAS_DEPORTIVAS" Visible="true" />
                    <asp:BoundField DataField="LUBRICANTES" HeaderText="LUBRICANTES" Visible="true" />
                    <asp:BoundField DataField="MANUAL_CONDUCCION" HeaderText="MANUAL_CONDUCCION" Visible="true" />
                    <asp:BoundField DataField="MANUAL_CONDUCCION11" HeaderText="MANUAL_CONDUCCION11" Visible="true" />    
                    <asp:BoundField DataField="MEMBRESIA_EVALUAC" HeaderText="MEMBRESIA_EVALUAC" Visible="true" />
                    <asp:BoundField DataField="MEMBRESIA_MOTO" HeaderText="MEMBRESIA_MOTO    " Visible="true" />
                    <asp:BoundField DataField="MEMBRESIA_PREM" HeaderText="MEMBRESIA_PREM    " Visible="true" />
                    <asp:BoundField DataField="MEMBRESIA_PREM_DESC" HeaderText="MEMBRESIA_PREM_DESC  " Visible="true" />
                    <asp:BoundField DataField="MEMBRESIA_STAND" HeaderText="MEMBRESIA_STAND   " Visible="true" />
                    <asp:BoundField DataField="MEMBRESIA_TAXI" HeaderText="MEMBRESIA_TAXI    " Visible="true" />
                    <asp:BoundField DataField="PERMISOS_APRENDIZAJE_AUTOS" HeaderText="PERMISOS_APRENDIZAJE_AUTOS    " Visible="true" />
                    <asp:BoundField DataField="PERMISOS_APRENDIZAJE_MOTOS" HeaderText="PERMISOS_APRENDIZAJE_MOTOS    " Visible="true" />
                    <asp:BoundField DataField="RECUPERACION_PUNTOS" HeaderText="RECUPERACION_PUNTOS  " Visible="true" />
                    <asp:BoundField DataField="REPUESTOS" HeaderText="REPUESTOS     " Visible="true" />
                    <asp:BoundField DataField="SERVICIO_CSA" HeaderText="SERVICIO CSA    " Visible="true" />
                    <asp:BoundField DataField="SERVICIOS_ADMINIST" HeaderText="SERVICIOS_ADMINIST   " Visible="true" />
                    <asp:BoundField DataField="SERVICIOS_AUX" HeaderText="SERVICIOS_AUX " Visible="true" />
                    <asp:BoundField DataField="SERVICIOS_CHOF" HeaderText="SERVICIOS_CHOF    " Visible="true" />
                    <asp:BoundField DataField="SERVICIOS_CONV" HeaderText="SERVICIOS_CONV    " Visible="true" />
                    <asp:BoundField DataField="SERVICIOS_GRUA" HeaderText="SERVICIOS_GRUA    " Visible="true" />
                    <asp:BoundField DataField="TARJETAS_SOCIOS" HeaderText="TARJETAS_SOCIOS   " Visible="true" />
                    <asp:BoundField DataField="TEST_CONDUCCION" HeaderText="TEST_CONDUCCION   " Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_MOTOESM" HeaderText="CURSOS_CONDUCCION_MOTOESM     " Visible="true" />
                    <asp:BoundField DataField="EVALUACIONES_PSICOTDESC" HeaderText="EVALUACIONES_PSICOTDESC       " Visible="true" />
                    <asp:BoundField DataField="MEMBRESIA_PREM2" HeaderText="MEMBRESIA_PREM2   " Visible="true" />
                    <asp:BoundField DataField="MEMBRESIA_PREM3" HeaderText="MEMBRESIA_PREM3   " Visible="true" />
                    <asp:BoundField DataField="SIN_GRUPO" HeaderText="SIN_GRUPO     " Visible="true" />
                    <asp:BoundField DataField="PERMISOS_INTERNAC" HeaderText="PERMISOS_INTERNAC " Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_POL" HeaderText="CURSOS_CONDUCCION_POL" Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_TUM" HeaderText="CURSOS_CONDUCCION_TUM" Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_QTF" HeaderText="CURSOS_CONDUCCION_QTF" Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_CALEM" HeaderText="CURSOS_CONDUCCION_CALEM       " Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_SOL" HeaderText="CURSOS_CONDUCCION_SOL" Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_EVA" HeaderText="CURSOS_CONDUCCION_EVA" Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_EPS" HeaderText="CURSOS_CONDUCCION_EPS" Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_IN" HeaderText="CURSOS_CONDUCCION_IN " Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_NORM" HeaderText="CURSOS_CONDUCCION_NORM        " Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_SECOM" HeaderText="CURSOS_CONDUCCION_SECOM       " Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_CANTON" HeaderText="CURSOS_CONDUCCION_CANTON      " Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_CXC" HeaderText="CURSOS_CONDUCCION_CXC" Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_MTM" HeaderText="CURSOS_CONDUCCION_MTM" Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_PS" HeaderText="CURSOS_CONDUCCION_PS " Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_DESP" HeaderText="CURSOS_CONDUCCION_DESP        " Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_QCI" HeaderText="CURSOS_CONDUCCION_QCI" Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_POME" HeaderText="CURSOS_CONDUCCION_POME        " Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_MOTOESM3" HeaderText="CURSOS_CONDUCCION_MOTOESM3    " Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_MOTO1" HeaderText="CURSOS_CONDUCCION_MOTO1       " Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_DA" HeaderText="CURSOS_CONDUCCION_DA " Visible="true" />
                    <asp:BoundField DataField="CURSOS_CONDUCCION_DAT" HeaderText="CURSOS_CONDUCCION_DAT" Visible="true" />
                    <asp:BoundField DataField="RECATEG_PRACTICA_EVA2" HeaderText="RECATEG_PRACTICA_EVA2" Visible="true" />
                    <asp:BoundField DataField="RECATEG_PRACTICA_EVA3" HeaderText="RECATEG_PRACTICA_EVA3" Visible="true" />
                    <asp:BoundField DataField="RECATEG_PSICOT_EVA2" HeaderText="RECATEG_PSICOT_EVA2  " Visible="true" />
                    <asp:BoundField DataField="RECATEG_PSICOT_EVA3" HeaderText="RECATEG_PSICOT_EVA3  " Visible="true" />
                    <asp:BoundField DataField="EXAMEN_PSICOTECNICO10" HeaderText="EXAMEN_PSICOTECNICO10" Visible="true" />
                    <asp:BoundField DataField="EXAMEN_PSICOTECNICO_OFTAMED" HeaderText="EXAMEN_PSICOTECNICO_OFTAMED   " Visible="true" />
                    <asp:BoundField DataField="EXAMEN_PSICOTECNICO_OPTA" HeaderText="EXAMEN_PSICOTECNICO_OPTA      " Visible="true" />
                    <asp:BoundField DataField="EXAMEN_PSICOTECNICO13" HeaderText="EXAMEN_PSICOTECNICO13" Visible="true" />
                    <asp:BoundField DataField="EXAMEN_PSICOTECNICO15" HeaderText="EXAMEN_PSICOTECNICO15" Visible="true" />
                    <asp:BoundField DataField="EXAMEN_PSICOTECNICO_BGR" HeaderText="EXAMEN_PSICOTECNICO_BGR       " Visible="true" />
                    <asp:BoundField DataField="EXAMEN_PSICOTECNICO_ANDEC" HeaderText="EXAMEN_PSICOTECNICO_ANDEC     " Visible="true" />
                    <asp:BoundField DataField="EXAMEN_PSICOTECNICO_FS" HeaderText="EXAMEN_PSICOTECNICO_FS        " Visible="true" />
                    <asp:BoundField DataField="EXAMEN_PSICOTECNICO20" HeaderText="EXAMEN_PSICOTECNICO20" Visible="true" />
                    <asp:BoundField DataField="EXAMEN_PSICOTECNICO30" HeaderText="EXAMEN_PSICOTECNICO30" Visible="true" />
                    <asp:BoundField DataField="EXAMEN_PSICOTECNICO45" HeaderText="EXAMEN_PSICOTECNICO45" Visible="true" />
                    <asp:BoundField DataField="EXAMEN_PSICOTECNICO_GAS" HeaderText="EXAMEN_PSICOTECNICO_GAS       " Visible="true" />
                    <asp:BoundField DataField="EXAMEN_PSICOTECNICO_FENA" HeaderText="EXAMEN_PSICOTECNICO_FENA      " Visible="true" />
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
        </fieldset>
    </asp:Panel>
</asp:Content>

