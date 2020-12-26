<%@ Page Title="" Language="C#" MasterPageFile="~/Escuela/mpEscuela.master" AutoEventWireup="true" CodeFile="pensumAcademico.aspx.cs" Inherits="Escuela_pensumAcademico" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <link href="../App_Themes/Estilos/anexo4.css" rel="stylesheet" />
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <!-- MENSAJE!-->
    <asp:Panel ID="pnMensaje2" CssClass="" runat="server" Visible="true">
        <asp:Label ID="lblMensaje" runat="server" Text="" Visible="true"></asp:Label>
        <asp:Label ID="lblTipoConsulta" runat="server" Text="" Visible="true"></asp:Label>
    </asp:Panel>

    <!-- CABECERA INGRESO DE SUCURSAL Y FECHAs  !-->
    <asp:Panel ID="pnTitulos" CssClass="" runat="server" Visible="true">

        <fieldset id="fdTitulos" class="fieldset-principal">
            <legend>Estado del estudiante</legend>
            <asp:Panel ID="pnDatos" CssClass="pnPeqSoc" runat="server" Visible="true" style="display:inline-block">
                <asp:Label ID="lblSuc" runat="server" Text="Sucursal" Font-Bold="True" Font-Size="Larger" ForeColor="darkblue"
                    Visible="false"></asp:Label>

                <asp:Label ID="Label3" runat="server" Text="Buscar por:" Font-Bold="True" Font-Size="Larger" ForeColor="darkblue"
                    Visible="True"></asp:Label>
                <asp:DropDownList ID="ddlTipoBusqueda" runat="server" Visible="true" Font-Size="Larger" ForeColor="White"
                    BackColor="#9aaff1">
                    <asp:ListItem Value="0">RUC/C.C.</asp:ListItem>
                    <asp:ListItem Value="1">Apellidos-Nombres</asp:ListItem>
                </asp:DropDownList>

                <asp:TextBox runat="server" ID="txtBuscar" Font-Size="Larger" ForeColor="darkblue"
                    Style="text-transform: uppercase"
                    BorderColor="#9aaff1"></asp:TextBox>

                <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/images/iconos/219.png" Width="27px" ToolTip="Buscar"
                    BorderColor="#9aaff1" OnClick="imgBuscar_Click" />

            </asp:Panel>
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="pnGeneral" CssClass="" runat="server" Visible="true">
        <asp:Panel ID="pnEstudiante" runat="server"
            ScrollBars="Vertical" Wrap="False" GroupingText="NUEVOS">
            <asp:GridView ID="grvEstudiante" runat="server" AutoGenerateColumns="False" CellPadding="5" GridLines="Vertical"
                HorizontalAlign="Center" Width="95%" AllowPaging="false" AllowSorting="True" PageSize="50">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="RNOTC_ID" HeaderText="RNOTC_ID" Visible="false" />
                    <asp:BoundField DataField="REG_ID" HeaderText="REG_ID" Visible="false" />
                    <asp:BoundField DataField="RNOTC_CIRUC" HeaderText="#Identificación" Visible="true" />
                    <asp:BoundField DataField="RNOTC_APELLIDOS" HeaderText="Apellidos" Visible="true" />
                    <asp:BoundField DataField="RNOTC_NOMBRES" HeaderText="Nombres" Visible="true" />
                    <asp:BoundField DataField="RNOTC_LICENCIA" HeaderText="LIC" Visible="true" />
                    <asp:BoundField DataField="RNOTC_FACT" HeaderText="#FACT" Visible="true" />
                    <asp:BoundField DataField="RNOTC_PETR_ESTA" HeaderText="PETR" Visible="true" />
                    <asp:BoundField DataField="RNOTC_TITULO" HeaderText="#TITULO" Visible="true" />
                    <asp:BoundField DataField="RNOTC_ACTA" HeaderText="#ACTA" Visible="true" />
                    <asp:BoundField DataField="RNOTC_EDUC_VIAL_ASIS" HeaderText="AEDUCVIAL" Visible="true" />
                    <asp:BoundField DataField="RNOTC_EDUC_VIAL_NOTA" HeaderText="NEDUCVIAL" Visible="true" />
                    <asp:BoundField DataField="RNOTC_EDUC_VIAL_SUP1" HeaderText="S1EDUCVIAL" Visible="true" />
                    <asp:BoundField DataField="RNOTC_EDUC_VIAL_SUP2" HeaderText="S2EDUC_VIAL" Visible="true" />
                    <asp:BoundField DataField="RNOTC_EDUC_VIAL_ESTA" HeaderText="RNOTC_EDUC_VIAL_ESTA" Visible="true" />
                    <asp:BoundField DataField="RNOTC_PRAC_ASIS" HeaderText="APRAC" Visible="true" />
                    <asp:BoundField DataField="RNOTC_PRAC_NOTA" HeaderText="NPRAC" Visible="true" />
                    <asp:BoundField DataField="RNOTC_PRAC_SUP1" HeaderText="S1PRAC" Visible="true" />
                    <asp:BoundField DataField="RNOTC_PRAC_SUP2" HeaderText="S2PRAC" Visible="true" />
                    <asp:BoundField DataField="RNOTC_PRAC_ESTA" HeaderText="RNOTC_PRAC_ESTA" Visible="true" />
                    <asp:BoundField DataField="RNOTC_PSIC_ASIS" HeaderText="APSIC" Visible="true" />
                    <asp:BoundField DataField="RNOTC_PSIC_ESTA" HeaderText="RNOTC_PSIC_ESTA" Visible="true" />
                    <asp:BoundField DataField="RNOTC_PAUX_ASIS" HeaderText="APAUX" Visible="true" />
                    <asp:BoundField DataField="RNOTC_PAUX_ESTA" HeaderText="RNOTC_PAUX_ESTA" Visible="true" />
                    <asp:BoundField DataField="RNOTC_MEC_ASIS" HeaderText="AMEC" Visible="true" />
                    <asp:BoundField DataField="RNOTC_MEC_ESTA" HeaderText="RNOTC_MEC_ESTA" Visible="true" />
                    <asp:BoundField DataField="RNOTC_APROBADO" HeaderText="APROBADO" Visible="true" /> 
                  <asp:BoundField DataField="RNOTC_OBSERVACIONES" HeaderText="OBSERVACIONES" Visible="true" />

                    <asp:BoundField DataField="RNOTC_FACT_ESTA" HeaderText="FACTESTA" Visible="false" />
                   <asp:BoundField DataField="RNOTC_OP1" HeaderText="RNOTC_OP1" Visible="false" />
                    <asp:BoundField DataField="RNOTC_OP3" HeaderText="RNOTC_OP3" Visible="false" />
                    <asp:BoundField DataField="RNOTC_OP4" HeaderText="RNOTC_OP4" Visible="false" />
                    <asp:BoundField DataField="RNOTC_OP5" HeaderText="RNOTC_OP5" Visible="false" />
                    <asp:BoundField DataField="RNOTC_OP6" HeaderText="RNOTC_OP6" Visible="false" />
                    <asp:BoundField DataField="RNOTC_OP7" HeaderText="RNOTC_OP7" Visible="false" />
                    <asp:BoundField DataField="RNOTC_USU_MODIFICA" HeaderText="RNOTC_USU_MODIFICA" Visible="false" />
                    <asp:BoundField DataField="RNOTC_PEDIDO_TITULOS" HeaderText="#PEDTIT" Visible="false" />
                    <asp:BoundField DataField="RNOTC_CONFIRMACION" HeaderText="CONFIRMA" Visible="false" />
                    <asp:BoundField DataField="RNOTC_ENVIADO" HeaderText="CONFIRMA" Visible="false" />
                    
                </Columns>

            </asp:GridView>
        </asp:Panel>
        </asp:Panel>

        <asp:Panel ID="pnHistorico" CssClass="" runat="server" Visible="true" ScrollBars="Vertical" Wrap="False" GroupingText="HISTORICOS">
            <asp:GridView ID="grvHistorico" runat="server" AutoGenerateColumns="False" CellPadding="5" GridLines="Vertical"
                HorizontalAlign="Center" Width="95%" AllowPaging="false" AllowSorting="True" PageSize="50">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="cuhomat" HeaderText="cuhomat" Visible="false" />
                    <asp:BoundField DataField="mae_suc" HeaderText="Suc" Visible="true" />
                    <asp:BoundField DataField="cliente" HeaderText="#Identificación" Visible="true" />
                    <asp:BoundField DataField="rso" HeaderText="Nombres" Visible="true" />
                    <asp:BoundField DataField="numero" HeaderText="#Factura" Visible="true" />
                    <asp:BoundField DataField="mcurso" HeaderText="mcurso" Visible="true" />
                    <asp:BoundField DataField="permiso" HeaderText="permiso" Visible="true" />
                    <asp:BoundField DataField="titulo" HeaderText="titulo" Visible="true" />
                    <asp:BoundField DataField="acta" HeaderText="acta" Visible="true" />
                    <asp:BoundField DataField="psico" HeaderText="psico" Visible="true" />
                    <asp:BoundField DataField="edu" HeaderText="edu" Visible="true" />
                    <asp:BoundField DataField="pra" HeaderText="pra" Visible="true" />
                    <asp:BoundField DataField="mec" HeaderText="mec" Visible="true" />
                    <asp:BoundField DataField="pau" HeaderText="pau" Visible="true" />
                    <asp:BoundField DataField="psi" HeaderText="psi" Visible="true" />
                    <asp:BoundField DataField="nedu" HeaderText="nedu" Visible="true" />
                    <asp:BoundField DataField="npra" HeaderText="npra" Visible="true" />
                    <asp:BoundField DataField="nmec" HeaderText="nmec" Visible="true" />
                    <asp:BoundField DataField="nacta" HeaderText="nacta" Visible="true" />
                    <asp:BoundField DataField="ntitu" HeaderText="ntitu" Visible="true" />
                    <asp:BoundField DataField="s1edu" HeaderText="s1edu" Visible="true" />
                    <asp:BoundField DataField="s2edu" HeaderText="s2edu" Visible="true" />
                    <asp:BoundField DataField="s1pra" HeaderText="s1pra" Visible="true" />
                    <asp:BoundField DataField="s2pra" HeaderText="s2pra" Visible="true" />
                    <asp:BoundField DataField="estado" HeaderText="estado" Visible="true" />
                    <asp:BoundField DataField="observa" HeaderText="observa" Visible="true" /> 
                    <asp:BoundField DataField="observa2" HeaderText="observa2" Visible="true" />
                    <asp:BoundField DataField="observa3" HeaderText="observa3" Visible="true" />
                    <asp:BoundField DataField="obs_pra" HeaderText="obs_pra" Visible="true" />
                    
                    <asp:BoundField DataField="mae_aul" HeaderText="mae_aul" Visible="false" />
                    <asp:BoundField DataField="horario" HeaderText="horario" Visible="false" />
                    <asp:BoundField DataField="horpra" HeaderText="horpra" Visible="false" />
                    <asp:BoundField DataField="horasab" HeaderText="horasab" Visible="false" />
                    <asp:BoundField DataField="tip_con" HeaderText="tip_con" Visible="false" />
                    <asp:BoundField DataField="tpcurso" HeaderText="tpcurso" Visible="false" />
                    <asp:BoundField DataField="mae_veh" HeaderText="mae_veh" Visible="false" />
                    <asp:BoundField DataField="item" HeaderText="item" Visible="false" />
                    <asp:BoundField DataField="nota" HeaderText="nota" Visible="false" />
                    <asp:BoundField DataField="asistencia" HeaderText="asistencia" Visible="false" />
                    

                    <asp:BoundField DataField="nombre" HeaderText="nombre" Visible="false" />
                    <asp:BoundField DataField="revdoc" HeaderText="revdoc" Visible="false" />
                    
                   
                    
                    <asp:BoundField DataField="fechapsico" HeaderText="fechapsico" Visible="false" />
                    <asp:BoundField DataField="fecharevis" HeaderText="fecharevis" Visible="false" />
                    <asp:BoundField DataField="fechimpper" HeaderText="fechimpper" Visible="false" />
                    <asp:BoundField DataField="grupoperm" HeaderText="grupoperm" Visible="false" />
                    <asp:BoundField DataField="estadoest" HeaderText="estadoest" Visible="false" />
                    
                    <asp:BoundField DataField="usuario" HeaderText="usuario" Visible="false" />
                    <asp:BoundField DataField="num_pedido" HeaderText="num_pedido" Visible="false" />
                    <asp:BoundField DataField="cur_pedido" HeaderText="cur_pedido" Visible="false" />
                    
                    
                    <asp:BoundField DataField="obs_edu" HeaderText="obs_edu" Visible="false" />
                    <asp:BoundField DataField="obs_mec" HeaderText="obs_mec" Visible="false" />
                    <asp:BoundField DataField="obs_pau" HeaderText="obs_pau" Visible="false" />
                    <asp:BoundField DataField="obs_psi" HeaderText="obs_psi" Visible="false" />
                    <asp:BoundField DataField="ped_tit" HeaderText="ped_tit" Visible="false" />
                    <asp:BoundField DataField="reg_acta" HeaderText="reg_acta" Visible="false" />
                    <asp:BoundField DataField="reg_psic" HeaderText="reg_psic" Visible="false" />
                    <asp:BoundField DataField="reg_ctep" HeaderText="reg_ctep" Visible="false" />
                    <asp:BoundField DataField="reg_docu" HeaderText="reg_docu" Visible="false" />
                    <asp:BoundField DataField="reg_con" HeaderText="reg_con" Visible="false" />
                    <asp:BoundField DataField="obs_estadi" HeaderText="obs_estadi" Visible="false" />
                    <asp:BoundField DataField="cur_cierra" HeaderText="cur_cierra" Visible="false" />
                    <asp:BoundField DataField="pago_pcia" HeaderText="pago_pcia" Visible="false" />
                    <asp:BoundField DataField="ctrol_titu" HeaderText="ctrol_titu" Visible="false" />
                    <asp:BoundField DataField="autza_pago" HeaderText="autza_pago" Visible="false" />
                    <asp:BoundField DataField="titulado" HeaderText="titulado" Visible="false" />
                    <asp:BoundField DataField="cerrado" HeaderText="cerrado" Visible="false" />
                    <asp:BoundField DataField="pagado" HeaderText="pagado" Visible="false" />
                    <asp:BoundField DataField="controlado" HeaderText="controlado" Visible="false" />
                    <asp:BoundField DataField="serie" HeaderText="serie" Visible="false" />
                    <asp:BoundField DataField="est_reg" HeaderText="est_reg" Visible="false" />
                    <asp:BoundField DataField="ano" HeaderText="ano" Visible="false" />
                    <asp:BoundField DataField="fserie" HeaderText="fserie" Visible="false" />
                    <asp:BoundField DataField="solper" HeaderText="solper" Visible="false" />
                    <asp:BoundField DataField="feccaduc" HeaderText="feccaduc" Visible="false" />
                    <asp:BoundField DataField="psic" HeaderText="psic" Visible="false" />
                    <asp:BoundField DataField="cpt" HeaderText="cpt" Visible="false" />
                    <asp:BoundField DataField="con" HeaderText="con" Visible="false" />
                    <asp:BoundField DataField="iacta" HeaderText="iacta" Visible="false" />
                    <asp:BoundField DataField="doc" HeaderText="doc" Visible="false" />
                    <asp:BoundField DataField="matricula" HeaderText="matricula" Visible="false" />
                    <asp:BoundField DataField="fechmat" HeaderText="fechmat" Visible="false" />
                    <asp:BoundField DataField="seriemat" HeaderText="seriemat" Visible="false" />
                    
                    <asp:BoundField DataField="factura" HeaderText="factura" Visible="false" />
                    <asp:BoundField DataField="id" HeaderText="id" Visible="false" />
                    <asp:BoundField DataField="habilitado" HeaderText="habilitado" Visible="false" />

                </Columns>

            </asp:GridView>

    </asp:Panel>
</asp:Content>

