<%@ Page Title="" Language="C#" MasterPageFile="~/Herramienta/mpHerramienta.master" AutoEventWireup="true" CodeFile="retencion.aspx.cs" Inherits="Herramienta_retencion" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">
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
            <legend>Listado de retenciones</legend>
            <asp:Panel ID="pnCabeceraFactura" CssClass="pnAccionGrid" runat="server">
                <asp:Label ID="lblRuc" runat="server" Text="RUC" Font-Bold="True" Font-Size="Larger" ForeColor="darkblue"></asp:Label>
                <asp:TextBox ID="txtBuscaRuc" runat="server" Font-Size="Larger" ForeColor="darkblue"></asp:TextBox>
                <asp:Label ID="lblSuc" runat="server" Text="Sucursal" Font-Bold="True" Font-Size="Larger" ForeColor="darkblue"></asp:Label>
                <asp:DropDownList ID="ddlSucursal2" runat="server" CssClass="" DataTextField="nom_suc" DataValueField="sucursal">
                </asp:DropDownList>
                <asp:Label ID="lblSecuencial" runat="server" Text="Secuencial" Font-Bold="True" Font-Size="Larger" ForeColor="darkblue"></asp:Label>
                <asp:TextBox ID="txtBuscaSecuencial" runat="server" Font-Size="Larger" ForeColor="darkblue"></asp:TextBox>
                <asp:Button ID="btnListarRuc" runat="server" Text="Listar" CssClass="btnProceso" OnClick="btnListarRuc_Click" />
            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnListadoRetencion" CssClass="" runat="server" Visible="true">

        <fieldset id="Fieldset1" class="fieldset-principal">
            <legend></legend>
            <asp:GridView ID="grvListadoRetencion" runat="server" AutoGenerateColumns="False"
            BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px"
            CellPadding="4" HorizontalAlign="Center" Width="101%" AllowSorting="True"
            PageSize="5" DataKeyNames="id_infotributaria" OnSelectedIndexChanged="grvListadoRetencion_SelectedIndexChanged" >
            <Columns>
                <asp:CommandField ShowSelectButton="true" />
                <asp:TemplateField HeaderText="ID" FooterStyle-Wrap="False" Visible="false">
                    <ItemTemplate>
                        <%# Eval("id_infotributaria") %>
                    </ItemTemplate>
                    <FooterStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Secuencial" FooterStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("documento") %>
                    </ItemTemplate>
                    <FooterStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fecha" FooterStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("fechaDocumento","{0:d}".ToString()) %>
                    </ItemTemplate>
                    <FooterStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Razon Social" FooterStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("razonSocialSujetoRetenido") %>
                    </ItemTemplate>
                    <FooterStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Valor Factura" FooterStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("totalFactura","{0:#,##0.##}".ToString()) %>
                    </ItemTemplate>
                    <FooterStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total retenido" FooterStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("totRetenido","{0:#,##0.##}".ToString()) %>
                    </ItemTemplate>
                    <FooterStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Estado" FooterStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("cre_sri") %>
                    </ItemTemplate>
                    <FooterStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Descripción" FooterStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("campoAdicional") %>
                    </ItemTemplate>
                    <FooterStyle Wrap="False" />
                </asp:TemplateField>

            </Columns>
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" />
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" />
        </asp:GridView>
            </fieldset>
        </asp:Panel>

    <asp:Panel ID="pnRetencion" CssClass="" runat="server" Visible="false">

        <fieldset id="Fieldset2" class="fieldset-principal">
            <legend></legend>
            <asp:HiddenField ID="hEstado" runat="server" />
            <asp:Label ID="lblDocumento" CssClass="lblForm" runat="server" Text="# Documento"></asp:Label>
            <asp:TextBox ID="txtDocumento" CssClass="txtForm" runat="server" Enabled="false"></asp:TextBox>
            <asp:Label ID="lblFecha" CssClass="lblForm" runat="server" Text="Fecha"></asp:Label>
            <asp:TextBox ID="txtFecha" CssClass="txtForm" runat="server" Enabled="false"></asp:TextBox>
            <asp:Label ID="lblRucComprador" CssClass="lblForm" runat="server" Text="R.U.C."></asp:Label>
            <asp:TextBox ID="txtRucComprador" CssClass="txtForm" runat="server" Enabled="false"></asp:TextBox>
            <asp:Label ID="lblNombres" CssClass="lblForm" runat="server" Text="Nombres"></asp:Label>
            <asp:TextBox ID="txtNombres" CssClass="txtForm" runat="server" Enabled="false"></asp:TextBox>
            <asp:Label ID="lblObservacion" CssClass="lblForm" runat="server" Text="Observación"></asp:Label>
            <asp:TextBox ID="txtObservacion" CssClass="txtForm" runat="server" Enabled="false"></asp:TextBox>

            <asp:GridView ID="grRetencionDetalle" runat="server" AutoGenerateColumns="False"
                CellPadding="4" GridLines="None" HorizontalAlign="Center" Width="95%" AllowSorting="True"
                PageSize="5" DataKeyNames="id_infoCompRetencion" ForeColor="#333333">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:CommandField ShowSelectButton="false" />
                    <asp:TemplateField HeaderText="ID" FooterStyle-Wrap="False" Visible="false">
                        <ItemTemplate>
                            <%# Eval("id_infoCompRetencion") %>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Código" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("codigo") %>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Retención" FooterStyle-Wrap="false">
                        <ItemTemplate>
                            <%# Eval("codigoRetencion") %>
                        </ItemTemplate>
                        <FooterStyle Wrap="True" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Base Imp." FooterStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%# Eval("baseImponible","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="% Ret." FooterStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%# Eval("porcentajeRetener","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Valor" FooterStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%# Eval("valorRetenido","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="# Documento" FooterStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%# Eval("numDocSustento") %>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fec/Doc" FooterStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%# Eval("fechaEmisionDocSustento","{0:d}".ToString()) %>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" />
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
            <asp:Panel ID="pnBotones" CssClass="pnListarGridRecacaudacion" runat="server"
                BackColor="white" ScrollBars="None" GroupingText="Botones">
                <asp:Panel ID="pnOpcion" CssClass="pnAccion" runat="server">
                    <asp:Button ID="btnBorrarRet" CssClass="btnProceso" runat="server" Text="Borrar retención" OnClick="btnBorrarRet_Click"  />
                    <asp:Button ID="btnAnularRet" CssClass="btnProceso" runat="server" Text="Anular retención" OnClick="btnAnularRet_Click" />
                    <asp:Button ID="btnImprimir" CssClass="btnProceso" runat="server" Text="Imprimir retención" 
                        OnClick="btnImprimir_Click"  />
                    <asp:Button ID="btnCancelarRet" CssClass="btnProceso" runat="server" Text="Regresar" OnClick="btnCancelarRet_Click" />
                    <asp:Label ID="Label1" CssClass="lblForm" runat="server" Text=""></asp:Label>
                </asp:Panel>
            </asp:Panel>
            </fieldset>
        </asp:Panel>
</asp:Content>

