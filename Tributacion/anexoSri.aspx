<%@ Page Title="" Language="C#" MasterPageFile="~/Tributacion/mpTributacion.master" AutoEventWireup="true" CodeFile="anexoSri.aspx.cs" Inherits="Tributacion_anexoSri" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>



    <!-- MENSAJE!-->
    <asp:Panel ID="pnMensaje2" CssClass="" runat="server" Visible="true">
        <asp:Label ID="lblMensaje" runat="server" Text="" Visible="true"></asp:Label>
        <asp:Button ID="btnIngresaProv" runat="server" Text="Ingrese el proveedor" Visible="false" />
    </asp:Panel>

    <!-- DATOS INICIALES PARA LA CONSULTA  !-->
    <asp:Panel ID="pnTitulos" CssClass="" runat="server" Visible="true">

        <fieldset id="fdTitulos" class="fieldset-principal">
            <legend>Control de cajas</legend>
            <asp:Panel ID="pnDatos" CssClass="pnPeq" runat="server" Visible="true">

                <asp:Label ID="lblSuc" runat="server" Text="Sucursal" CssClass="lblPeq"></asp:Label>

                <asp:DropDownList ID="ddlSucursal2" runat="server" CssClass="peqDdl" DataTextField="nom_suc" DataValueField="sucursal">
                </asp:DropDownList>

               <asp:Panel ID="pnAno" runat="server" CssClass="pnPeqDdl">
                    <asp:UpdatePanel ID="upAno" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlAno" runat="server" CssClass="peqDdl" Visible="true" Enabled="true">
                                
                                <asp:ListItem Value=-1>Seleccione el año</asp:ListItem>
                                <asp:ListItem Value=2014>2014</asp:ListItem>
                                <asp:ListItem Value=2015>2015</asp:ListItem>
                                <asp:ListItem Value=2016>2016</asp:ListItem>
                                <asp:ListItem Value=2017>2017</asp:ListItem>
                                <asp:ListItem Value=2018>2018</asp:ListItem>
                                <asp:ListItem Value=2019>2019</asp:ListItem>
                                <asp:ListItem Value=2020>2020</asp:ListItem>
                                <asp:ListItem Value=2021>2021</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>

                <asp:Panel ID="pnMes" runat="server" CssClass="pnPeqDdl">
                    <asp:UpdatePanel ID="upMes" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlMes" runat="server" CssClass="peqDdl" Visible="true" Enabled="true">
                                <asp:ListItem Value=-1>Seleccione el mes</asp:ListItem>
                                <asp:ListItem Value=1>Enero</asp:ListItem>
                                <asp:ListItem Value=2>Febrero</asp:ListItem>
                                <asp:ListItem Value=3>Marzo</asp:ListItem>
                                <asp:ListItem Value=4>Abril</asp:ListItem>
                                <asp:ListItem Value=5>Mayo</asp:ListItem>
                                <asp:ListItem Value=6>Junio</asp:ListItem>
                                <asp:ListItem Value=7>Julio</asp:ListItem>
                                <asp:ListItem Value=8>Agosto</asp:ListItem>
                                <asp:ListItem Value=9>Septiembre</asp:ListItem>
                                <asp:ListItem Value=10>Octubre</asp:ListItem>
                                <asp:ListItem Value=11>Noviembre</asp:ListItem>
                                <asp:ListItem Value=12>Diciembre</asp:ListItem>

                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>



                <asp:Button ID="btnConsultar" runat="server" CssClass="btnProceso" Text="Consultar" 
                    OnClick="btnConsultar_Click" />




            </asp:Panel>
        </fieldset>
    </asp:Panel>

     <!-- DETALLE DEL ANEXO  !-->
    <asp:Panel ID="pnDetalleCaja" CssClass="" runat="server" Visible="true">

        <fieldset id="fdDetalleCaja" class="fieldset-principal">
            <legend></legend>
            <asp:Panel ID="pnExcel" CssClass="" runat="server" Wrap="False">
                <asp:Button ID="btnExcelFe" runat="server" CssClass="btnProceso " Text="A Excel" Visible="true" OnClick="btnExcelFe_Click" />
            </asp:Panel>
            <asp:GridView ID="grvEgresosDetalle" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="90%"
                AllowSorting="True" PageSize="50" >
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    

                        <asp:BoundField DataField="ID_INFOTRIBUTARIA" HeaderText="id" Visible="true" />
                        <asp:BoundField DataField="ESTAB" HeaderText="Estab." Visible="true" />
                        <asp:BoundField DataField="PTOEMI" HeaderText="Pto.Emi" Visible="true" />
                        <asp:BoundField DataField="SECUENCIAL" HeaderText="Secuencial" Visible="true" />
                        <asp:BoundField DataField="FECHADOCUMENTO" HeaderText="FecDoc" Visible="true" />
                        <asp:BoundField DataField="PERIODO" HeaderText="Per." Visible="true" />
                        <asp:BoundField DataField="CODDOCSUSTENTO" HeaderText="CodDoc" Visible="true" />
                        <asp:BoundField DataField="TIPOIDENTIFICACIONSUJETORETENIDO" HeaderText="TipoIdent." Visible="true" />
                        <asp:BoundField DataField="IDENTIFICACIONSUJETORETENIDO" HeaderText="RUC" Visible="true" />
                        <asp:BoundField DataField="NUMDOCSUSTENTO" HeaderText="# Doc" Visible="true" />
                        <asp:BoundField DataField="FECHAEMISIONDOCSUSTENTO" HeaderText="Fec.Emi." Visible="true" />
                        <asp:BoundField DataField="AUTORIZACION" HeaderText="Autorización" Visible="true" />
                        <asp:BoundField DataField="FECHAEMISIONDOCCADUCA" HeaderText="FecCaduc" Visible="true" />
                        <asp:BoundField DataField="BASE0" HeaderText="Base0" Visible="true" />
                        <asp:BoundField DataField="BASE_IVA" HeaderText="BaseIva" Visible="true" />
                        <asp:BoundField DataField="BASE_OTROS" HeaderText="BaseOtros" Visible="true" />
                        <asp:BoundField DataField="PORCENTAJE_IVA" HeaderText="%Iva" Visible="true" />
                        <asp:BoundField DataField="codigo10" HeaderText="Cod10" Visible="true" />
                        <asp:BoundField DataField="porcentaje10" HeaderText="%10" Visible="true" />
                        <asp:BoundField DataField="base10" HeaderText="Base10" Visible="true" />
                        <asp:BoundField DataField="valor10" HeaderText="Valor10" Visible="true" />
                        <asp:BoundField DataField="codigo20" HeaderText="Cod20" Visible="true" />
                        <asp:BoundField DataField="porcentaje20" HeaderText="%20" Visible="true" />
                        <asp:BoundField DataField="base20" HeaderText="Base20" Visible="true" />
                        <asp:BoundField DataField="valor20" HeaderText="Valor20" Visible="true" />
                        <asp:BoundField DataField="codigo30" HeaderText="Cod30" Visible="true" />
                        <asp:BoundField DataField="porcentaje30" HeaderText="%30" Visible="true" />
                        <asp:BoundField DataField="base30" HeaderText="Base30" Visible="true" />
                        <asp:BoundField DataField="valor30" HeaderText="Valor30" Visible="true" />
                        <asp:BoundField DataField="codigo70" HeaderText="Cod70" Visible="true" />
                        <asp:BoundField DataField="porcentaje70" HeaderText="%70" Visible="true" />
                        <asp:BoundField DataField="base70" HeaderText="Base70" Visible="true" />
                        <asp:BoundField DataField="valor70" HeaderText="Valor70" Visible="true" />
                        <asp:BoundField DataField="codigo100" HeaderText="Cod100" Visible="true" />
                        <asp:BoundField DataField="porcentaje100" HeaderText="%100" Visible="true" />
                        <asp:BoundField DataField="base100" HeaderText="Base100" Visible="true" />
                        <asp:BoundField DataField="valor100" HeaderText="Valor100" Visible="true" />
                        <asp:BoundField DataField="CODIGORETENCIONb" HeaderText="RetB" Visible="true" />
                        <asp:BoundField DataField="PORCENTAJERETENERb" HeaderText="%B" Visible="true" />
                        <asp:BoundField DataField="baseb" HeaderText="BaseB" Visible="true" />
                        <asp:BoundField DataField="valorb" HeaderText="ValorB" Visible="true" />
                        <asp:BoundField DataField="CODIGORETENCIONs" HeaderText="RetS" Visible="true" />
                        <asp:BoundField DataField="PORCENTAJERETENERs" HeaderText="%S" Visible="true" />
                        <asp:BoundField DataField="bases" HeaderText="BaseS" Visible="true" />
                        <asp:BoundField DataField="valors" HeaderText="ValorS" Visible="true" />



                   
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

