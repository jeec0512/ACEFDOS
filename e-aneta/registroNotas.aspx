﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Escuela/mpEscuela.master" AutoEventWireup="true" CodeFile="registroNotas.aspx.cs" Inherits="Escuela_registroNotas" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">
     <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <!-- MENSAJE-->
    <asp:Panel ID="pnMensaje2" CssClass="" runat="server" Visible="true">
        <asp:Label ID="lblMensaje" runat="server" Text="" Visible="true"></asp:Label>
        <asp:Button ID="btnIngresaProv" runat="server" Text="Ingrese el proveedor" Visible="false" />
    </asp:Panel>

    <!-- CABECERA INGRESO DE SUCURSAL Y FECHAs  -->
    <asp:Panel ID="pnTitulos" CssClass="" runat="server" Visible="true">

        <fieldset id="fdTitulos" class="fieldset-principal">
            <legend>Comprobantes electrónicos</legend>

                <asp:Button ID="btnNotas" runat="server" Text="Ingreso de notas" CssClass="btnProceso" 
                    OnClick="btnNotas_Click" />
                
                <asp:Button ID="btnGeneral" runat="server" Text="General" CssClass="btnProceso" OnClick="btnGeneral_Click" />

                <asp:Button ID="betEspecifico" runat="server" Text="Específico" CssClass="btnProceso" OnClick="betEspecifico_Click" />

        </fieldset>

        <!-- <iframe width="90%" height="315" src="https://www.youtube.com/embed/hzbG0JSX6Z0" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen id="ifnotas" name="ifNotas"></iframe> -->
        
    </asp:Panel>
</asp:Content>

