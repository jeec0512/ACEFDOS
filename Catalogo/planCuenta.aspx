<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo/mpCatalogo.master" AutoEventWireup="true" CodeFile="planCuenta.aspx.cs" Inherits="Catalogo_planCuenta" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">

    <asp:Label ID=lblMensaje runat=server Text=""></asp:Label>
    <asp:Panel ID="pnVer" runat="server">

        <asp:Panel ID="pnVCuentaCble" runat="server" CssClass="pnForm">
            <asp:Panel ID="pnTitulo" runat="server" CssClass="pnFormTitulo">
                <asp:Label ID="lblCta" CssClass="lblForm" runat="server" Text="Cuenta Contable"></asp:Label>
                <asp:TextBox ID="txtCta" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:ImageButton ID=ibConsultar runat="server" ImageUrl="~/images/iconos/219_2.png"
                    OnClick="ibConsultar_Click" />
            </asp:Panel>
            <asp:GridView ID="grvVerCta"
                runat="server" AutoGenerateColumns="False"
                CellPadding="5"
                GridLines="Vertical"
                HorizontalAlign="Center"
                Width="100%"
                AllowPaging="True"
                AllowSorting="True"
                DataKeyNames="MAE_CUE"
                PageSize="10" OnSelectedIndexChanged="grvVerCta_SelectedIndexChanged"
                OnPageIndexChanging="grvVerCta_PageIndexChanging"
                OnSelectedIndexChanging="grvVerCta_SelectedIndexChanging">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:TemplateField HeaderText="Número Id" Visible=false>
                        <ItemTemplate>
                            <%# Eval("id_mae_cue") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Cuenta">
                        <ItemTemplate>
                            <%# Eval("MAE_CUE") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Descripción">
                        <ItemTemplate>
                            <%# Eval("NOM_CTA") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Tipo">
                        <ItemTemplate>
                            <%# Eval("TIP_CTA") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Naturaleza">
                        <ItemTemplate>
                            <%# Eval("NAT_CTA") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <%# Eval("estado") %>
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
            <asp:Panel ID="pnBotonera" runat="server" CssClass="pnFormHijo">
                <asp:Button ID=btnNuevo runat="server" Text="Nueva cuenta" CssClass="btnForm"
                    OnClick="btnNuevo_Click" />
                <asp:Button ID=btnModificar runat="server" Text="Modifica cuenta" CssClass="btnForm"
                    OnClick="btnModificar_Click" />
                <asp:HyperLink ID=blRegresar runat="server" Text="Regresar" NavigateUrl="~/catalogo/inicioCatalogo.aspx"></asp:HyperLink>
            </asp:Panel>
        </asp:Panel>

        <asp:Panel ID="pnMCuentaCble" runat=server CssClass="pnForm" Visible=false>

            <asp:Label ID="lblMensaje2" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>

            <fieldset id="cuentaCble">
                <legend>Datos de la Cuenta Contable</legend>
                <asp:Label ID="lblMAE_CUE" CssClass="lblForm" runat="server" Text="Cuenta "></asp:Label>
                <asp:TextBox ID="txtMAE_CUE" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblNOM_CTA" CssClass="lblForm" runat="server" Text="Descripción"></asp:Label>
                <asp:TextBox ID="txtNOM_CTA" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblTIP_CTA" CssClass="lblForm" runat="server" Text="Tipo"></asp:Label>
                <asp:Panel ID="pnTIP_CTA" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlTIP_CTA" runat="server">
                        <asp:ListItem Value=1>Mayor</asp:ListItem>
                        <asp:ListItem Value=2>Auxiliar</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
                <asp:Label ID="lblNAT_CTA" CssClass="lblForm" runat="server" Text="Naturaleza"></asp:Label>
                <asp:Panel ID="pnNAT_CTA" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlNAT_CTA" runat="server">
                        <asp:ListItem Value=1>Débito</asp:ListItem>
                        <asp:ListItem Value=2>Crédito</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
                <asp:Label ID="lblMEstado" CssClass="lblForm" runat="server" Text="Estado"></asp:Label>
                <asp:Panel ID="pnMEstado" runat="server" CssClass="pnFormChk">
                    <asp:CheckBox ID=chkMEstado TextAlign=Left runat="server" />
                </asp:Panel>
                <asp:Button ID=btnGuardar runat="server" Text="Grabar" CssClass="btnForm"
                    OnClick="btnGuardar_Click" />
                <asp:Button ID=btnRegresar runat="server" Text="Regresar" textcolor="blue" CssClass="btnForm"
                    OnClick="btnRegresar_Click" />
            </fieldset>

        </asp:Panel>
    </asp:Panel>

</asp:Content>

