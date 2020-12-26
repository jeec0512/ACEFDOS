<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo/mpCatalogo.master" AutoEventWireup="true" CodeFile="auto2.aspx.cs" Inherits="Catalogo_auto2" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <link href="../App_Themes/Estilos/anexo4.css" rel="stylesheet" />
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <!-- MENSAJE!-->



    <asp:Panel ID="pnMensaje" CssClass="" runat="server" Visible="true">
        <asp:Label ID="lblMensaje" runat="server" Text="" Visible="true" ForeColor="Red" Font-Size="Medium"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="pnMatricula" CssClass="pnFormSocio" Style="width: 90vw; display: flex; flex-direction: column; flex-wrap: wrap;" runat="server" Visible="true">
        <asp:Label runat="server" ID="lblDispAutos" Text="VEHÍCULOS Y PSICOS ASIGNADOS POR ESCUELA" Style="display: block; margin: 0.5rem; text-align: center; color: orange; font-size: 1.5rem; font-weight: 700"></asp:Label>
        <asp:Panel runat="server" ID="Fieldset1" Style="display: flex; justify-content: space-between; margin-bottom: 1rem;">

            <asp:Panel ID="pnEscuela" runat="server" Style="display: flex; flex-direction: column; flex-wrap: wrap; justify-content: space-evenly; width: 40vw;" ForeColor="#0033cc">


                <asp:Panel ID="pnSucursal" runat="server" CssClass="pnPeqDdl" Visible="false">
                    <asp:UpdatePanel ID="upSucursal" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblSucursal" runat="server" Text="Sucursal de emisión de la factura:" Visible="true" CssClass="lblPeq"></asp:Label>
                            <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="pnSocioDdl" DataTextField="nom_suc"
                                DataValueField="sucursal" AutoPostBack="True">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>

                <asp:Panel ID="pnCiudad" runat="server" CssClass="pnFormDdl" Style="margin-bottom: 1rem;" Visible="false" >
                    <asp:Label ID="lblCiudad" CssClass="lblPeq" runat="server" Text="Ciudad" Visible="true"></asp:Label>
                    <asp:DropDownList ID="ddlCiudad" DataTextField="ciudad" DataValueField="ciudad" runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlCiudad_SelectedIndexChanged"
                        Style="font-size: 1rem; width: 80%; display: block; margin: 0.5rem; text-align: left; color: blue; font-weight: 600">
                    </asp:DropDownList>
                </asp:Panel>

                <asp:Panel ID="pnEscuela2" runat="server" CssClass="pnFormDdl" Style="margin-bottom: 1rem;" Visible="true">
                    <asp:Label ID="lblEscuela" CssClass="lblPeq" runat="server" Text="Escuela" Visible="true"></asp:Label>
                    <asp:DropDownList ID="ddlEscuela" DataTextField="nom_suc" DataValueField="sucursal" runat="server"
                        AutoPostBack="True"
                        Style="font-size: 1rem; width: 80%; display: block; margin: 0.5rem; text-align: left; color: blue; font-weight: 600" OnSelectedIndexChanged="ddlEscuela_SelectedIndexChanged">
                    </asp:DropDownList>
                </asp:Panel>


                <asp:Panel ID="pnModalidad" runat="server" CssClass="pnFormDdl">
                    <asp:Label ID="lblModalidad" CssClass="lblPeq" runat="server" Text="Tipo"></asp:Label>
                    <asp:DropDownList ID="ddlModalidad" DataTextField="tv_descripcion" DataValueField="TV_ID" runat="server"
                        AutoPostBack="True" Style="font-size: 1rem; width: 80%; display: block; margin: 0.5rem; text-align: left; color: blue; font-weight: 600"
                        OnSelectedIndexChanged="ddlModalidad_SelectedIndexChanged">
                    </asp:DropDownList>
                </asp:Panel>

            </asp:Panel>


            <asp:Panel ID="pnCalendario" runat="server" Style="display: flex; flex-direction: column; justify-content: space-between; width: 30vw;" ForeColor="#0033cc">
                <asp:Panel ID="Panel1" runat="server">

                    <asp:Button ID="btnNuevoVehiculo" runat="server" Text="Nuevo " style="height:2rem;width:15rem;background:blue;color:white;font-size:1.5rem" OnClick="btnNuevoVehiculo_Click"/>
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
                        <asp:BoundField DataField="Veh_id" HeaderText="Código" Visible="true" ItemStyle-CssClass="DisplayNone"
                            HeaderStyle-CssClass="DisplayNone" />
                        <asp:BoundField DataField="veh_numero" HeaderText="# vehículo" Visible="true" />
                        <asp:BoundField DataField="veh_placa" HeaderText="Placa" Visible="true" />
                        <asp:BoundField DataField="veh_marca" HeaderText="Marca" Visible="true" />
                        <asp:BoundField DataField="veh_modelo" HeaderText="Modelo" Visible="true" />
                        <asp:BoundField DataField="veh_anio" HeaderText="Año" Visible="true" />
                        <asp:BoundField DataField="veh_motor" HeaderText="Motor" Visible="true" />
                        <asp:BoundField DataField="veh_chasis" HeaderText="Chasis" Visible="true" />
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

    <asp:Panel ID="pnActualizacion" runat="server" Visible="false" Style="margin-top:1rem;">
        <asp:Label ID="Label1" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>
        <asp:Panel ID="pnAuto" runat="server">
            <fieldset id="fsAuto">
                <legend>Acualización</legend>
                <asp:TextBox ID="txtVeh_id" CssClass="txtForm" runat="server" Visible="false"></asp:TextBox>
                
                <asp:Label ID="lblMarca" CssClass="lblForm" runat="server" Text="Marca"></asp:Label>
                <asp:TextBox ID="txtMarca" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblModelo" CssClass="lblForm" runat="server" Text="Modelo"></asp:Label>
                <asp:TextBox ID="txtModelo" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblAno" CssClass="lblForm" runat="server" Text="Año"></asp:Label>
                <asp:TextBox ID="txtAno" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblNumero" CssClass="lblForm" runat="server" Text="Número"></asp:Label>
                <asp:TextBox ID="txtNumero" CssClass="txtForm" runat="server" Enabled="false"></asp:TextBox>
                <asp:Label ID="lblChasis" CssClass="lblForm" runat="server" Text="Chasis"></asp:Label>
                <asp:TextBox ID="txtChasis" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblMotor" CssClass="lblForm" runat="server" Text="Motor"></asp:Label>
                <asp:TextBox ID="txtMotor" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblPlaca" CssClass="lblForm" runat="server" Text="Placa"></asp:Label>
                <asp:TextBox ID="txtPlaca" CssClass="txtForm" runat="server"></asp:TextBox>
                <!--<asp:Label ID="lblSuc_Id" CssClass="lblForm" runat="server" Text="SucId"></asp:Label>
                <asp:TextBox ID="txtSuc_Id" CssClass="txtForm" runat="server"></asp:TextBox>-->
                <!--<asp:Label ID="lblTve_id" CssClass="lblForm" runat="server" Text="Tve_id"></asp:Label>
                <asp:TextBox ID="txtTve_id" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblPer_id" CssClass="lblForm" runat="server" Text="Per_id"></asp:Label>
                <asp:TextBox ID="txtPer_id" CssClass="txtForm" runat="server"></asp:TextBox>-->
                <asp:Label ID="lblEstado" CssClass="lblForm" runat="server" Text="Estado"></asp:Label>
                <asp:Panel ID="pnEstado" runat="server" CssClass="pnFormChk">
                    <asp:CheckBox ID="chkEstado" TextAlign="Left" runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnBotonera" runat="server" CssClass="pnFormBotonera">
                    <asp:Button ID="btnGuardar" runat="server" Text="Grabar" CssClass="btnForm" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btnForm" OnClick="btnCancelar_Click" />
                    <asp:HyperLink ID="blRegresar" runat="server" Text="Regresar" NavigateUrl="~/catalogo/inicioCatalogo.aspx"></asp:HyperLink>
                </asp:Panel>

            </fieldset>
        </asp:Panel>
    </asp:Panel>
</asp:Content>

