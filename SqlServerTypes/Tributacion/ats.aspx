<%@ Page Title="" Language="C#" MasterPageFile="~/Tributacion/mpTributacion.master" AutoEventWireup="true" CodeFile="ats.aspx.cs" Inherits="Tributacion_ats" EnableEventValidation="false" %>

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
            <legend>Documento ATS</legend>
            <asp:Panel ID="pnDatos" CssClass="pnPeq" runat="server" Visible="true">

                <asp:Label ID="lblSuc" runat="server" Text="Sucursal" CssClass="lblPeq" Visible="false"></asp:Label>

                <asp:DropDownList ID="ddlSucursal2" runat="server" CssClass="peqDdl" DataTextField="nom_suc" DataValueField="sucursal" Visible="false">
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



                <asp:Button ID="btnConsultar" runat="server" CssClass="btnProceso" Text="Consultar" visible="false"
                    OnClick="btnConsultar_Click" />
                <asp:Button ID="btnListar" runat="server" CssClass="btnProceso" Text="ver ATS" 
                    OnClick="btnListar_Click" />




            </asp:Panel>
        </fieldset>
    </asp:Panel>

     <!-- cabeceraATS  !-->
    <asp:Panel ID="pnCabeceraAts" CssClass="" runat="server" Visible="true">

        <fieldset id="fdCabeceraAts" class="fieldset-principal">
            <legend></legend>
            <asp:Panel ID="pnExcel" CssClass="" runat="server" Wrap="False">
                <asp:Button ID="btnExcelFe" runat="server" CssClass="btnProceso " Text="A Excel" Visible="true" OnClick="btnExcelFe_Click" />
            </asp:Panel>
            <asp:GridView ID="grvCabeceraAts" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="90%"
                AllowSorting="True" PageSize="50" >
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    
                    <asp:BoundField DataField="id_cabAts" HeaderText="	id_cabAts	 " Visible="false" />
                        <asp:BoundField DataField="consecutivo" HeaderText="	consecutivo	 " Visible="true" />
                        <asp:BoundField DataField="codCompra" HeaderText="	codCompra	 " Visible="true" />
                        <asp:BoundField DataField="ano" HeaderText="	ano	 " Visible="false" />
                        <asp:BoundField DataField="periodo" HeaderText="	periodo	 " Visible="false" />
                        <asp:BoundField DataField="tipo" HeaderText="	tipo	 " Visible="true" />
                        <asp:BoundField DataField="numero" HeaderText="	numero	 " Visible="true" />
                        <asp:BoundField DataField="codSustento" HeaderText="	codSustento	 " Visible="true" />
                        <asp:BoundField DataField="tipoIdentProvee" HeaderText="	tipoIdentProvee	 " Visible="true" />
                        <asp:BoundField DataField="numIdentProvee" HeaderText="numIdentProvee"  DataFormatString="{0:g}" Visible="true" />
                        <asp:BoundField DataField="codTipoComp" HeaderText="	codTipoComp	 " Visible="true" />
                        <asp:BoundField DataField="tipoProvee" HeaderText="	tipoProvee	 " Visible="true" />
                        <asp:BoundField DataField="parteRelacionada" HeaderText="	parteRelacionada	 " Visible="true" />
                        <asp:BoundField DataField="fechaRegistro" HeaderText="	fechaRegistro	 " Visible="true" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="facEstab" HeaderText="	facEstab	 " Visible="true" />
                        <asp:BoundField DataField="facPtoemi" HeaderText="	facPtoemi	 " Visible="true" />
                        <asp:BoundField DataField="facSecuencial" HeaderText="	facSecuencial	 " Visible="true" />
                        <asp:BoundField DataField="fechaEmision" HeaderText="	fechaEmision	 " Visible="true" DataFormatString="{0:d}"/>
                        <asp:BoundField DataField="numAutorizacion" HeaderText="	numAutorizacion	 " Visible="true" />
                        <asp:BoundField DataField="biNOobjIva" HeaderText="	biNOobjIva	 " Visible="true" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="biTarifaCero" HeaderText="	biTarifaCero	 " Visible="true" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="biGravada" HeaderText="	biGravada	 " Visible="true" DataFormatString="{0:#,##0.00}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="bExenta" HeaderText="	bExenta	 " Visible="true" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="montoIce" HeaderText="	montoIce	 " Visible="true" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="montoIva" HeaderText="	montoIva	 " Visible="true" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="retIvaBienes10" HeaderText="	retIvaBienes10	 " Visible="true" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="retIvaServicios20" HeaderText="	retIvaServicios20	 " Visible="true" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="retIvaBienes30" HeaderText="	retIvaBienes30	 " Visible="true" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="retIvaBienes50" HeaderText="	retIvaBienes50	 " Visible="true" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="retIvaServicios70" HeaderText="	retIvaServicios70	 " Visible="true" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="retIvaServicios100" HeaderText="	retIvaServicios100	 " Visible="true" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="totalBi" HeaderText="	totalBi	 " Visible="true" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="pagoLocalExterior" HeaderText="	pagoLocalExterior	 " Visible="true" />
                        <asp:BoundField DataField="tipoRegimenExterior" HeaderText="	tipoRegimenExterior	 " Visible="true" />
                        <asp:BoundField DataField="paisResidenciaRegimen" HeaderText="	paisResidenciaRegimen	 " Visible="true" />
                        <asp:BoundField DataField="paisResidenciaParaiso" HeaderText="	paisResidenciaParaiso	 " Visible="true" />
                        <asp:BoundField DataField="denominacionRegimenFiscal" HeaderText="	denominacionRegimenFiscal	 " Visible="true" />
                        <asp:BoundField DataField="paisPago" HeaderText="	paisPago	 " Visible="true" />
                        <asp:BoundField DataField="convenioDTributacion" HeaderText="	convenioDTributacion	 " Visible="true" />
                        <asp:BoundField DataField="pagoExterior" HeaderText="	pagoExterior	 " Visible="true" />
                        <asp:BoundField DataField="retEstab" HeaderText="	retEstab	 " Visible="true" />
                        <asp:BoundField DataField="retPtoEmi" HeaderText="	retPtoEmi	 " Visible="true" />
                        <asp:BoundField DataField="retSecuencial" HeaderText="	retSecuencial	 " Visible="true" />
                        <asp:BoundField DataField="retAutorizacion" HeaderText="	retAutorizacion	 " Visible="true" />
                        <asp:BoundField DataField="retFechaEmision" HeaderText="	retFechaEmision	 " Visible="true" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="proveeTipoDoc_nc" HeaderText="	proveeTipoDoc_nc	 " Visible="true" />
                        <asp:BoundField DataField="proveeEstab_nc" HeaderText="	proveeEstab_nc	 " Visible="true" />
                        <asp:BoundField DataField="proveePtoEmi_nc" HeaderText="	proveePtoEmi_nc	 " Visible="true" />
                        <asp:BoundField DataField="proveeSecuencial_nc" HeaderText="	proveeSecuencial_nc	 " Visible="true" />
                        <asp:BoundField DataField="proveeAutorizacion_nc" HeaderText="	proveeAutorizacion_nc	 " Visible="true" />
                        <asp:BoundField DataField="retencion" HeaderText="	retencion	 " Visible="false" />
                   
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

    <!-- detalleATS  !-->
    <asp:Panel ID="pnDetalleATS" CssClass="" runat="server" Visible="true">

        <fieldset id="fsDetalleATS" class="fieldset-principal">
            <legend></legend>
            <asp:Panel ID="pnBotonera" CssClass="" runat="server" Wrap="False">
                <asp:Button ID="btnDetalle" runat="server" CssClass="btnProceso " Text="A Excel" Visible="true" 
                    OnClick="btnDetalle_Click"  />
            </asp:Panel>
            <asp:GridView ID="grvDetalleATS" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="90%"
                AllowSorting="True" PageSize="50" >
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    
                    <asp:BoundField DataField="id_detAts" HeaderText="	id_detAts	 " Visible="false" />
                    <asp:BoundField DataField="id_cabAts" HeaderText="	id_cabAts	 " Visible="false" />
                    <asp:BoundField DataField="codCompra" HeaderText="	codCompra	 " Visible="true" />
                    <asp:BoundField DataField="retencion" HeaderText="	retencion	 " Visible="false" />
                    <asp:BoundField DataField="retCodigo" HeaderText="	retCodigo	 " Visible="true" />
                    <asp:BoundField DataField="retBase" HeaderText="	retBase	 " Visible="true" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right"/>
                    <asp:BoundField DataField="retPorcentaje" HeaderText="	retPorcentaje	 " Visible="true" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right"/>
                    <asp:BoundField DataField="retValor" HeaderText="	retValor	 " Visible="true" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right"/>
                    <asp:BoundField DataField="retFechaDividendo" HeaderText="	retFechaDividendo	 " Visible="true" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="retImpRtaAsociado" HeaderText="	retImpRtaAsociado	 " Visible="true" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right"/>
                    <asp:BoundField DataField="anoUtilidadDividendo" HeaderText="	anoUtilidadDividendo	 " Visible="true" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right"/>

                   
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

