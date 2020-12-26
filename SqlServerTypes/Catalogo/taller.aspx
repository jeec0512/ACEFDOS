<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo/mpCatalogo.master" AutoEventWireup="true" CodeFile="taller.aspx.cs" Inherits="Catalogo_taller" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <asp:Panel ID="pnActualizacion" runat="server">

        <asp:Label ID="lblMensaje" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>
        <asp:Panel ID="pnFechaTalleres" runat="server">
            <fieldset id="fsFechaTalleres">
                <legend>Fechas para talleres</legend>
                <asp:TextBox ID="txtTal_id" CssClass="txtForm" runat="server" Visible="false"></asp:TextBox>
                <asp:Label ID="lblSucursal" CssClass="lblForm" runat="server" Text="Sucursal" Visible="true"></asp:Label>
                <asp:Panel ID="pnSucursal" runat="server" CssClass="pnFormDdl" Visible="true">
                    <asp:DropDownList ID="ddlSucursal" DataTextField=nom_suc DataValueField=sucursal runat="server"
                        AutoPostBack="True" >
                    </asp:DropDownList>
                </asp:Panel>
                 <asp:Label ID="lblModalidad" CssClass="lblForm" runat="server" Text="Modalidad"></asp:Label>
                <asp:Panel ID="pnModalidad" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlModalidad" DataTextField=nom_suc DataValueField=mod_id runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlModalidad_SelectedIndexChanged"  >
                        <asp:ListItem Value=-1>Seleccione modalidad</asp:ListItem>
                        <asp:ListItem Value=1>15 días</asp:ListItem>
                        <asp:ListItem Value=2>7 días</asp:ListItem>
                        <asp:ListItem Value=3>Fines de semana</asp:ListItem>
                        <asp:ListItem Value=4>Curso corporativo</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
                <asp:Label ID="lblCurso" CssClass="lblForm" runat="server" Text="Curso" Visible="true"></asp:Label>
                <asp:Panel ID="pnCurso" runat="server" CssClass="pnFormDdl" Visible="true">
                    <asp:DropDownList ID="ddlCurso" DataTextField=cur_nomeNclatura DataValueField=cur_id runat="server"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </asp:Panel>

                <asp:Label ID="lblMateria" CssClass="lblForm" runat="server" Text="Materia" Visible="true"></asp:Label>
                <asp:Panel ID="pnMateria" runat="server" CssClass="pnFormDdl" Visible="true">
                    <asp:DropDownList ID="ddlMateria" DataTextField=mat_descripcion DataValueField=mat_id runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlMateria_SelectedIndexChanged">
                    </asp:DropDownList>
                </asp:Panel>

                 <asp:Label ID="lblFecha" CssClass="lblForm" runat="server" Text="Fecha"></asp:Label>
                <asp:TextBox ID="txtFecha" CssClass="txtForm" runat="server"></asp:TextBox>
                <act1:CalendarExtender ID="CalendarExtender1" PopupButtonID="" runat="server" TargetControlID="txtFecha"
                    Format="dd/MM/yyyy"></act1:CalendarExtender>
                <act1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtFecha" Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                    AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="True" />

                  <asp:Label ID="lblEstado" CssClass="lblForm" runat="server" Text="Estado"></asp:Label>
                <asp:Panel ID="pnEstado" runat="server" CssClass="pnFormChk">
                    <asp:CheckBox ID=chkEstado TextAlign=Left runat="server" />
                </asp:Panel>

                <asp:Panel ID="pnBotonera" runat="server" CssClass="pnFormBotonera">
                    <asp:Button ID=btnGuardar runat="server" Text="Asignar" CssClass="btnForm" OnClick="btnGuardar_Click"/>
                    <asp:HyperLink ID=blRegresar runat="server" Text="Regresar" NavigateUrl="~/catalogo/inicioCatalogo.aspx"></asp:HyperLink>
                </asp:Panel>

            </fieldset>
        </asp:Panel>
    </asp:Panel>

     <asp:Panel ID="pnDetalleTaller" CssClass="" runat="server" Visible="true">

        <fieldset id="fdDetalleTaller" class="fieldset-principal">
            <legend></legend>
            <asp:GridView ID="grvTallerDetalle" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="90%"
                AllowSorting="True" PageSize="50" ShowFooter="false" 
                OnRowCommand="grvTallerDetalle_RowCommand" OnRowDataBound="grvTallerDetalle_RowDataBound">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:ButtonField HeaderText="Modificar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/mod.ico"
                        CommandName="modReg" ItemStyle-Width="60" />
                    <asp:ButtonField HeaderText="Eliminar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/garbage.png"
                        CommandName="eliReg" ItemStyle-Width="60" />
                    <asp:BoundField DataField="tal_id" HeaderText="Código" Visible="true" ItemStyle-CssClass="DisplayNone"
                        HeaderStyle-CssClass="DisplayNone" />
                    <asp:BoundField DataField="TAL_FECHA" HeaderText="Fecha" Visible="true" />
                     <asp:BoundField DataField="TAL_ESTADO" HeaderText="Estado" Visible="true" />
                   
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

