<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo/mpCatalogo.master" AutoEventWireup="true" CodeFile="producto.aspx.cs" Inherits="Catalogo_producto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">
    <asp:Label ID=lblMensaje runat=server Text=""></asp:Label>
    <asp:Panel ID="pnVer" runat="server">

        <asp:Panel ID="pnVproducto" runat="server" CssClass="pnForm">
            <asp:Panel ID="pnConsultar" runat="server" CssClass="pnFormTitulo">
                <asp:Label ID="lblPro" CssClass="lblForm" runat="server" Text="Producto "></asp:Label>
                <asp:TextBox ID="txtPro" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:ImageButton ID=ibConsultar runat="server" ImageUrl="~/images/iconos/219_2.png"
                    OnClick="ibConsultar_Click" />
            </asp:Panel>

            <asp:GridView ID="grvVerPro"
                runat="server" AutoGenerateColumns="False"
                CellPadding="5"
                GridLines="Vertical"
                HorizontalAlign="Center"
                Width="100%"
                AllowPaging="True"
                AllowSorting="True"
                DataKeyNames="PRO_ID"
                PageSize="20" OnPageIndexChanged="grvVerPro_PageIndexChanged"
                OnPageIndexChanging="grvVerPro_PageIndexChanging"
                OnSelectedIndexChanging="grvVerPro_SelectedIndexChanging"
                OnSelectedIndexChanged="grvVerPro_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:TemplateField HeaderText="Número Id" Visible=false>
                        <ItemTemplate>
                            <%# Eval("PRO_ID") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Código">
                        <ItemTemplate>
                            <%# Eval("PRO_CODIGO") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Descripción">
                        <ItemTemplate>
                            <%# Eval("PRO_DESCRIPCION") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Precio">
                        <ItemTemplate>
                            <%# Eval("PRO_PRECIO") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cta. Contable">
                        <ItemTemplate>
                            <%# Eval("PRO_CUENTACONTABLE") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Cta.Descuento">
                        <ItemTemplate>
                            <%# Eval("PRO_CCDESCUENTO") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Grupo">
                        <ItemTemplate>
                            <%# Eval("PRO_GRUPO") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <%# Eval("PRO_ESTADO") %>
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
                <asp:Button ID=btnNProducto runat="server" Text="Nuevo producto" CssClass="btnForm"
                    OnClick="btnNProducto_Click" />
                <asp:Button ID=btnMProducto runat="server" Text="Modifica producto" CssClass="btnForm"
                    OnClick="btnMProducto_Click" />
            </asp:Panel>

        </asp:Panel>

        <asp:Panel ID="pnMproducto" runat=server CssClass="pnForm" Visible=false>

            <asp:Label ID="lblMensaje2" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>

            <fieldset id="sucursal">
                <legend>Datos del Productol</legend>
                <asp:Label ID="lblPro_codigo" CssClass="lblForm" runat="server" Text="Código "></asp:Label>
                <asp:TextBox ID="txtPro_codigo" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblPro_descripcion" CssClass="lblForm" runat="server" Text="Nombre"></asp:Label>
                <asp:TextBox ID="txtPro_descripcion" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblPro_iva" CssClass="lblForm" runat="server" Text="I.V.A."></asp:Label>
                <asp:Panel ID="pnIva" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlIva" runat="server">
                        <asp:ListItem Value="S">Si</asp:ListItem>
                        <asp:ListItem Value="N">No</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
                <asp:Label ID="lblPro_precio1" CssClass="lblForm" runat="server" Text="Precio de venta 1"></asp:Label>
                <asp:TextBox ID="txtPro_precio1" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblPro_precio2" CssClass="lblForm" runat="server" Text="Precio de venta 2"></asp:Label>
                <asp:TextBox ID="txtPro_precio2" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblPro_precio3" CssClass="lblForm" runat="server" Text="Precio de venta 3"></asp:Label>
                <asp:TextBox ID="txtPro_precio3" CssClass="txtForm" runat="server"></asp:TextBox>

                <asp:Label ID="lblPro_combo" CssClass="lblForm" runat="server" Text="# Comb"></asp:Label>
                <asp:TextBox ID="txtPro_combo" CssClass="txtForm" runat="server"></asp:TextBox>

                <asp:Label ID="lblPve_id" CssClass="lblForm" runat="server" Text="# PVE"></asp:Label>
                <asp:TextBox ID="txtPve_id" CssClass="txtForm" runat="server"></asp:TextBox>

                <asp:Label ID="lblPro_modificacion" CssClass="lblForm" runat="server" Text="Modificación"></asp:Label>
                <asp:Panel ID="pnPro_modificacion" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlPro_modificacion" runat="server">
                        <asp:ListItem Value="S">Si</asp:ListItem>
                        <asp:ListItem Value="N">No</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>


                <asp:Label ID="lblPro_cuentacontable" CssClass="lblForm" runat="server" Text="Cuenta contable"></asp:Label>
                <asp:Panel ID="pnCtaCble" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlCtaCble" DataTextField="nom_cta" DataValueField="mae_cue" runat="server">
                    </asp:DropDownList>
                </asp:Panel>



                <asp:Label ID="lblPro_ccosto" CssClass="lblForm" runat="server" Text="centro de costo"></asp:Label>
                <asp:Panel ID="pnCcosto" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlCcosto" DataTextField=nom_cco DataValueField=mae_cco runat="server">
                    </asp:DropDownList>
                </asp:Panel>

                <asp:Label ID="lblCtaDescuento" CssClass="lblForm" runat="server" Text="Cuenta descuento"></asp:Label>
                <asp:Panel ID="pnCtaDescuento" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlCtaDescuento" DataTextField="nom_cta" DataValueField="mae_cue" runat="server">
                    </asp:DropDownList>
                </asp:Panel>

                <asp:Label ID="lblGrupo" CssClass="lblForm" runat="server" Text="Grupo"></asp:Label>
                <asp:Panel ID="pnGrupo" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlGrupo" DataTextField="descripcion" DataValueField="id" runat="server">
                    </asp:DropDownList>
                </asp:Panel>


                <asp:Label ID="lblEstado" CssClass="lblForm" runat="server" Text="Estado"></asp:Label>
                <asp:Panel ID="pnChkP" runat="server" CssClass="pnFormChk">
                    <asp:CheckBox ID=chkEstadoP TextAlign=Left runat="server" />
                </asp:Panel>

                <asp:Button ID=btnGpro runat="server" Text="Grabar" CssClass="btnForm" OnClick="btnGpro_Click" />
                <asp:Button ID=btnRpro runat="server" Text="Regresar" CssClass="btnForm" OnClick="btnRpro_Click" />
            </fieldset>

        </asp:Panel>

    </asp:Panel>
</asp:Content>

