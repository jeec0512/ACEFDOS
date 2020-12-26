<%@ Page Title="" Language="C#" MasterPageFile="~/Escuela/mpEscuela.master" AutoEventWireup="true" CodeFile="recuperacionPuntos.aspx.cs" Inherits="Escuela_recuperacionPuntos"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <!-- MENSAJE!-->
    <asp:Panel ID="pnMensaje2" CssClass="" runat="server" Visible="true">
        <asp:Label ID="lblMensaje" runat="server" Text="" Visible="true"></asp:Label>
        
    </asp:Panel>
    <asp:Panel ID="pnActualizacion" runat="server">
        <asp:Label ID="Label1" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>
        <asp:Panel ID="pnAsignacion" runat="server">
            <fieldset id="fsAsignacion">
                <legend>Creación de cupos</legend>
                <asp:TextBox ID="txtVeh_id" CssClass="txtForm" runat="server" Visible="false"></asp:TextBox>
                <asp:Label ID="lblSucursal" CssClass="lblForm" runat="server" Text="Sucursal" Visible="true"></asp:Label>

                <asp:Panel ID="pnSucursal" runat="server" CssClass="pnFormDdl" Visible="true">
                    <asp:DropDownList ID="ddlSucursal" DataTextField="nom_suc" DataValueField="sucursal" runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged">
                    </asp:DropDownList>
                </asp:Panel>
                <asp:Label ID="lblModalidad" CssClass="lblForm" runat="server" Text="Modalidad"></asp:Label>
                <asp:Panel ID="pnModalidad" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlModalidad" DataTextField="nom_suc" DataValueField="mod_id" runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlModalidad_SelectedIndexChanged">
                        <asp:ListItem Value="-1">Seleccione modalidad</asp:ListItem>
                        <asp:ListItem Value="1">15 días</asp:ListItem>
                        <asp:ListItem Value="2">7 días</asp:ListItem >
                        <asp:ListItem Value="3">Fines de semana</asp:ListItem>
                        <asp:ListItem Value="4">Curso corporativo</asp:ListItem>
                        <asp:ListItem Value="5">Recuperación de puntos regular</asp:ListItem>
                        <asp:ListItem Value="6">Recuperación de puntos fin de semana</asp:ListItem>
                        
                    </asp:DropDownList>
                                    </asp:Panel>
       
                 <asp:Label ID="lblCurso" CssClass="lblForm" runat="server" Text="Curso" Visible="true"></asp:Label>
                <asp:Panel ID="pnCurso" runat="server" CssClass="pnFormDdl" Visible="true">
                    <asp:DropDownList ID="ddlCurso" DataTextField="cur_nomeNclatura" DataValueField="cur_id" runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlCurso_SelectedIndexChanged">
                    </asp:DropDownList>
                </asp:Panel>

                <asp:Label ID="lblMateria" CssClass="lblForm" runat="server" Text="Materia" Visible="true"></asp:Label>
                <asp:Panel ID="pnMateria" runat="server" CssClass="pnFormDdl" Visible="true">
                    <asp:DropDownList ID="ddlMateria" DataTextField="mat_descripcion" DataValueField="mat_id" runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlMateria_SelectedIndexChanged">
                    </asp:DropDownList>
                </asp:Panel>


                <asp:Panel ID="pnBotonera" runat="server" CssClass="pnFormBotonera">
                    <asp:Button ID="btnImpRecupInic" runat="server" Text="Reporte inicial" Visible="true" OnClick="btnImpRecupInic_Click" />
                    <asp:Button ID="btnImpRecupFinal" runat="server" Text="Reporte final" Visible="true" OnClick="btnImpRecupFinal_Click" />
                    <asp:HyperLink ID="blRegresar" runat="server" Text="Regresar" NavigateUrl="~/catalogo/inicioCatalogo.aspx"></asp:HyperLink>
                </asp:Panel>

            </fieldset>
        </asp:Panel>
    </asp:Panel>
</asp:Content>

