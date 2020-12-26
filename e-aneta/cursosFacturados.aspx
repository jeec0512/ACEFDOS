<%@ Page Title="" Language="C#" MasterPageFile="~/Escuela/mpEscuela.master" AutoEventWireup="true" CodeFile="cursosFacturados.aspx.cs"
    Inherits="Escuela_cursosFacturados" EnableEventValidation="false" %>

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
            <legend>Factura</legend>
            <asp:Panel ID="pnCabeceraFactura" CssClass="pnAccionGrid" runat="server">
                <asp:Label ID="lblSuc" runat="server" Text="Sucursal" Font-Bold="True" Font-Size="Larger" ForeColor="darkblue"></asp:Label>
                <asp:DropDownList ID="ddlSucursal2" runat="server" CssClass="" DataTextField="nom_suc" DataValueField="sucursal">
                </asp:DropDownList>
                <asp:Label ID="lblCurso" runat="server" Text="Cursos" Font-Bold="True" Font-Size="Larger" ForeColor="darkblue"></asp:Label>
                <asp:DropDownList ID="ddlCursos" runat="server" Font-Size="Larger" ForeColor="darkblue" DataTextField="CUR_DESCRIPCION" DataValueField="CUR_ID">
                </asp:DropDownList>
            </asp:Panel>
            <asp:Panel ID="Panel3" CssClass="pnAccionGrid" runat="server" Wrap="False">
                <asp:Button ID="btnEscTotal" runat="server" CssClass="btnLargoForm" Text="Cursos facturados ANETA" Visible="true"
                    OnClick="btnEscTotal_Click" />
                <asp:Button ID="btnEscxSuc" runat="server" CssClass="btnLargoForm " Text="Cursos facturados por sucursal"
                    OnClick="btnEscxSuc_Click" />
            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnEscuela" CssClass="" runat="server" Visible="true">

        <fieldset id="Fieldset1" class="fieldset-principal">
            <legend>Factura</legend>

            <asp:Button ID="btnExcelCA" runat="server" CssClass="btnLargoForm " Text="A Excel" Visible="true" OnClick="btnExcelCA_Click" />

            <asp:GridView ID="grvEscuela" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="95%" AllowPaging="false" AllowSorting="True" PageSize="50">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:TemplateField HeaderText="Código">
                        <ItemTemplate>
                            <%# Eval("codsuc") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sucursal" ItemStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("sucursal") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fecha" ItemStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("fecha","{0:d}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#/Factura" ItemStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("factura") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Curso" ItemStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("cur_descripcion") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cliente" ItemStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("cliente") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Precio">
                        <ItemTemplate>
                            <%# Eval("precio","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="I.V.A.">
                        <ItemTemplate>
                            <%# Eval("iva","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total">
                        <ItemTemplate>
                            <%# Eval("total","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Abono/Fac">
                        <ItemTemplate>
                            <%# Eval("recaudado","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Diferencia">
                        <ItemTemplate>
                            <%# Eval("diferencia","{0:#,##0.##}".ToString()) %>
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
            </asp:GridView>
        </fieldset>
    </asp:Panel>

</asp:Content>

