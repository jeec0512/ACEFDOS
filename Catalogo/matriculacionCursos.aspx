<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo/mpCatalogo.master" AutoEventWireup="true" CodeFile="matriculacionCursos.aspx.cs"
     Inherits="Catalogo_matriculacionCursos" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">
    <link href="../App_Themes/Estilos/anexo4.css" rel="stylesheet" />
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <!-- MENSAJE!-->



    <asp:Panel ID="pnMensaje" CssClass="" runat="server" Visible="true">
        <asp:Label ID="lblMensaje" runat="server" Text="" Visible="true" ForeColor="Red" Font-Size="Medium"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="pnMatricula" CssClass="pnFormSocio" Style="width: 90vw; display: flex; flex-direction: column; flex-wrap: wrap;" runat="server" Visible="true">
        <asp:Label runat="server" ID="lblDispAutos" Text="Fechas para matriculación de cursos" Style="display: block; margin: 0.5rem; text-align: center; color: orange; font-size: 1.5rem; font-weight: 700"></asp:Label>
        <asp:Panel runat="server" ID="Fieldset1" Style="display: flex; justify-content: space-between; margin-bottom: 1rem;">

            <asp:Panel ID="pnEscuela" runat="server" Style="display: flex; flex-direction: column; flex-wrap: wrap; justify-content: space-evenly; width: 40vw;" ForeColor="#0033cc">


                <asp:Panel ID="pnSucursal" runat="server" CssClass="pnPeqDdl" Visible="false">
                    <asp:UpdatePanel ID="upSucursal" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblSucursal" runat="server" Text="Sucursal" Visible="true" CssClass="lblPeq"></asp:Label>
                            <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="pnSocioDdl" DataTextField="nom_suc"
                                DataValueField="sucursal" AutoPostBack="True">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>

                

            </asp:Panel>


            <asp:Panel ID="pnCalendario" runat="server" Style="display: flex; flex-direction: column; justify-content: space-between; width: 30vw;" ForeColor="#0033cc">
                <asp:Panel ID="Panel1" runat="server">

                    <asp:Button ID="btnNuevoVehiculo" runat="server" Text="Nuevo " Style="height: 2rem; width: 15rem; background: blue; color: white; font-size: 1.5rem" OnClick="btnNuevoVehiculo_Click" />
                    <asp:Label ID="lblNuevo" runat="server" Text="" Visible="false" ForeColor="Red" Font-Size="Medium"></asp:Label>


                </asp:Panel>

            </asp:Panel>

        </asp:Panel>
    </asp:Panel>



    <asp:Panel ID="pnListaAutos" runat="server" GroupingText="" ForeColor="#0033cc" Style="margin-top: 2rem;">
        <fieldset id="fsPractica">
            <legend>Autos</legend>

            <asp:Panel ID="pnAutos" runat="server" Style="max-height: 60vh; overflow-y: scroll; margin-bottom: 1.5rem;">
                <asp:GridView ID="grvAutos" runat="server" BackColor="White" BorderColor="#3366CC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="4" HorizontalAlign="Center"
                    Width="98%"
                    OnRowCommand="grvAutos_RowCommand"
                    OnRowDataBound="grvAutos_RowDataBound"
                    AllowSorting="True" PageSize="5" AutoGenerateColumns="False">
                    <Columns>
                        <asp:ButtonField HeaderText="Modificar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/mod.ico"
                            CommandName="modReg" ItemStyle-Width="60" />
                        <asp:BoundField DataField="id" HeaderText="Código" Visible="true" ItemStyle-CssClass="DisplayNone"
                            HeaderStyle-CssClass="DisplayNone" />
                        <asp:BoundField DataField="cur_nomenclatura" HeaderText="Curso" Visible="true" />
                        <asp:BoundField DataField="fecha_inicio_matricula" HeaderText="FechaIni" Visible="true" />
                        <asp:BoundField DataField="fecha_fin_matricula" HeaderText="FechaFin" Visible="true" />
                    </Columns>
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" ForeColor="#003399" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                    <SortedDescendingHeaderStyle BackColor="#002876" />

                </asp:GridView>

            </asp:Panel>

        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnActualizacion" runat="server" Visible="false" Style="margin-top: 1rem;">
        <asp:Label ID="Label1" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>
        <asp:Panel ID="pnAuto" runat="server">
            <fieldset id="fsAuto">
                <legend>Acualización</legend>
                <asp:TextBox ID="txtId" CssClass="txtForm" runat="server" Visible="false"></asp:TextBox>
                <asp:Panel ID="pnCurso" runat="server" CssClass="pnFormDdl" Style="margin-bottom: 1rem;" Visible="true">
                    <asp:Label ID="lblCurso" CssClass="lblPeq" runat="server" Text="Curso" Visible="true"></asp:Label>
                    <asp:DropDownList ID="ddlCurso" DataTextField="cur_nomenclatura" DataValueField="cur_id" runat="server"
                        AutoPostBack="True" 
                        Style="font-size: 1rem; width: 80%; display: block; margin: 0.5rem; text-align: left; color: blue; font-weight: 600">
                    </asp:DropDownList>
                </asp:Panel>

                <asp:Label ID="lblFeachaIni" CssClass="lblForm" runat="server" Text="Feacha de inicio"></asp:Label>
                <asp:TextBox ID="txtFechaIni" CssClass="txtForm" runat="server"></asp:TextBox>
                <act1:CalendarExtender ID="CalendarExtender1" PopupButtonID="" runat="server" TargetControlID="txtFechaIni"
                    Format="dd/MM/yyyy"></act1:CalendarExtender>
                <act1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtFechaIni" Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                    AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="True" />


                <asp:Label ID="lblFechaFin" CssClass="lblForm" runat="server" Text="Fecha de finalización"></asp:Label>
                <asp:TextBox ID="txtFechaFin" CssClass="txtForm" runat="server"></asp:TextBox>
                <act1:CalendarExtender ID="CalendarExtender2" PopupButtonID="" runat="server" TargetControlID="txtFechaFin"
                    Format="dd/MM/yyyy"></act1:CalendarExtender>
                <act1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtFechaFin" Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                    AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="True" />

                 <asp:Panel ID="pnBotonera" runat="server" CssClass="pnFormBotonera">
                    <asp:Button ID="btnGuardar" runat="server" Text="Grabar" CssClass="btnForm" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btnForm" OnClick="btnCancelar_Click" />
                    <asp:HyperLink ID="blRegresar" runat="server" Text="Regresar" NavigateUrl="~/catalogo/inicioCatalogo.aspx"></asp:HyperLink>
                </asp:Panel>
                
            </fieldset>
        </asp:Panel>
    </asp:Panel>
</asp:Content>

