<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo/mpCatalogo.master" AutoEventWireup="true"
    CodeFile="curso.aspx.cs" Inherits="Catalogo_curso" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <asp:Panel ID="pnActualizacion" runat="server">
        <asp:Label ID="lblMensaje" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>
        <asp:Panel ID="pnCurso" runat="server">
            <fieldset id="fsCurso">
                <legend>Cursos</legend>
                <asp:TextBox ID="txtCur_id" CssClass="txtForm" runat="server" Visible="false"></asp:TextBox>
                <asp:Label ID="lblSucursal" CssClass="lblForm" runat="server" Text="Sucursal" Visible="false"></asp:Label>
                <asp:Panel ID="pnSucursal" runat="server" CssClass="pnFormDdl" Visible="false">
                    <asp:DropDownList ID="ddlSucursal" DataTextField=nom_suc DataValueField=sucursal runat="server">
                    </asp:DropDownList>
                </asp:Panel>

                <asp:Label ID="lblModalidad" CssClass="lblForm" runat="server" Text="Modalidad"></asp:Label>
                <asp:Panel ID="pnModalidad" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlModalidad" DataTextField="mod_descripcion" DataValueField="mod_id" runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlModalidad_SelectedIndexChanged">
                    </asp:DropDownList>
                </asp:Panel>
                <asp:Label ID="lblNomenclatura" CssClass="lblForm" runat="server" Text="Nomenclatura"></asp:Label>
                <asp:TextBox ID="txtNomenclatura" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblDescripcion" CssClass="lblForm" runat="server" Text="Descripción"></asp:Label>
                <asp:TextBox ID="txtDescripcion" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblFechaInicio" CssClass="lblForm" runat="server" Text="Fecha de inicio"></asp:Label>
                <asp:TextBox ID="txtFechaInicio" CssClass="txtForm" runat="server"></asp:TextBox>
                <act1:CalendarExtender ID="CalendarExtender1" PopupButtonID="" runat="server" TargetControlID="txtFechaInicio"
                    Format="dd/MM/yyyy"></act1:CalendarExtender>
                <act1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtFechaInicio" Mask="99/99/9999"
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

                <asp:Label ID="lblEstado" CssClass="lblForm" runat="server" Text="Estado"></asp:Label>
                <asp:Panel ID="pnEstado" runat="server" CssClass="pnFormChk">
                    <asp:CheckBox ID=chkEstado TextAlign=Left runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnBotonera" runat="server" CssClass="pnFormBotonera">
                    <asp:Button ID=btnGuardar runat="server" Text="Grabar" CssClass="btnForm" OnClick="btnGuardar_Click" />
                    <asp:HyperLink ID=blRegresar runat="server" Text="Regresar" NavigateUrl="~/catalogo/inicioCatalogo.aspx"></asp:HyperLink>
                </asp:Panel>
            </fieldset>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="pnDetalleCurso" CssClass="" runat="server" Visible="true">

        <fieldset id="fdDetalleCurso" class="fieldset-principal">
            <legend></legend>
            <asp:GridView ID="grvCursoDetalle" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="90%"
                AllowSorting="True" PageSize="50" OnRowCommand="grvCursoDetalle_RowCommand"
                OnRowDataBound="grvCursoDetalle_RowDataBound" ShowFooter="false">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:ButtonField HeaderText="Modificar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/mod.ico"
                        CommandName="modReg" ItemStyle-Width="60" />
                    <asp:BoundField DataField="cur_id" HeaderText="Código" Visible="true" ItemStyle-CssClass="DisplayNone"
                        HeaderStyle-CssClass="DisplayNone" />
                    <asp:BoundField DataField="cur_nomenclatura" HeaderText="Nomenclatura" Visible="true" />
                    <asp:BoundField DataField="cur_descripcion" HeaderText="Descripción" Visible="true" />
                    <asp:BoundField DataField="cur_fecha_inicio" HeaderText="Fecha de inicio" Visible="true" />
                    <asp:BoundField DataField="cur_fecha_fin" HeaderText="Fecha de finalización" Visible="true" />
                    <asp:BoundField DataField="cur_activo" HeaderText="Estado" Visible="true" />


                </Columns>
                <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="True" Font-Size="Medium"
                    Font-Strikeout="False" />
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

