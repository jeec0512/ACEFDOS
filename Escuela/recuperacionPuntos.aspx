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
                    <asp:DropDownList ID="ddlModalidad" DataTextField="mod_descripcion" DataValueField="mod_id" runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlModalidad_SelectedIndexChanged">
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
    <asp:Panel ID="pnCursoDetalle" runat="server" Visible="false">
            <fieldset id="Fieldset1">
                <asp:Button ID="btnExcelRf" runat="server" CssClass="btnLargoForm " Text="A Excel" Visible="true" OnClick="btnExcelRf_Click"  />
    <asp:GridView ID="grvCursoDetalle" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                    BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                    Width="90%"
                    AllowSorting="True" PageSize="10" CssClass="Rojo">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField DataField="ROW" Visible="true" HeaderText="No" />
                        <asp:BoundField DataField="NOM_SUC" Visible="true" HeaderText="NOMBRE DE LA ESCUELA" />
                        <asp:BoundField DataField="ALU_IDENTIFICACION" Visible="true" HeaderText="NÚMERO DE IDENTIFICACIÓN DEL CONDUCTOR (CÉDULA-PASAPORTE-CARNÉ DE" />
                        <asp:BoundField DataField="NOMBRES" Visible="true" HeaderText="NOBRES DEL CONDUCTOR" />
                        <asp:BoundField DataField="REG_CERTIFICADOCOND" Visible="true" HeaderText="N° CERTIFICADO DE CONDUCTOR" />
                        <asp:BoundField DataField="REG_CERTIFICADOCOND" Visible="true" HeaderText="N° CERTIFICADO DE CONDUCTOR" />
                        <asp:BoundField DataField="REG_EMISIONCERTIFICADO" Visible="true" HeaderText="FECHA EMISIÓN CERTIFICADO" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="REG_FECHA" Visible="true" HeaderText="FECHA DE MATRÍCULA (DD-MM-AA)" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="CUR_FECHA_INICIO" Visible="true" HeaderText="FECHA DE INICIO DEL CURSO" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="CUR_FECHA_FIN" Visible="true" HeaderText="CUR_FECHA_FIN" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="JORNADA" Visible="true" HeaderText="JORNADA lunes a viernes ó fines de semana" />
                        <asp:BoundField DataField="HORARIO" Visible="true" HeaderText="HORARIO DE CLASES" />
                        <asp:BoundField DataField="AUL_DESCRIPCION" Visible="true" HeaderText="PARALELO" />
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="True" Font-Size="Medium"
                        Font-Strikeout="False" />
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

    <asp:Panel ID="pnNotasEstudiantiles" runat="server" Visible="false">
            <fieldset id="Fieldset2">
                <asp:Button ID="btnExcelPe" runat="server" CssClass="btnLargoForm " Text="A Excel" Visible="true" OnClick="btnExcelPe_Click"  />
    <asp:GridView ID="grvNotasEstudiantiles" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                    BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                    Width="90%"
                    AllowSorting="True" PageSize="10" CssClass="Rojo">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField DataField="ROW" Visible="true" HeaderText="No" />
                        <asp:BoundField DataField="NOM_SUC" Visible="true" HeaderText="NOMBRE DE LA ESCUELA" />
                        <asp:BoundField DataField="ALU_IDENTIFICACION" Visible="true" HeaderText="NÚMERO DE IDENTIFICACIÓN DEL CONDUCTOR (CÉDULA-PASAPORTE-CARNÉ DE" />
                        <asp:BoundField DataField="NOMBRES" Visible="true" HeaderText="NOBRES DEL CONDUCTOR" />
                        <asp:BoundField DataField="REG_FECHA" Visible="true" HeaderText="FECHA DE MATRÍCULA (DD-MM-AA)" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="CUR_FECHA_FIN" Visible="true" HeaderText="CUR_FECHA_FIN" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="RNOTRC_EVIAL_NOTA_FINAL" Visible="true" HeaderText="EDUCACIÓN VIAL" />
                        <asp:BoundField DataField="RNOTRC_LEYES_NOTA_FINAL" Visible="true" HeaderText="LEY TRANSITO" />
                        <asp:BoundField DataField="RNOTRC_PSICOLOGIA_NOTA_FINAL" Visible="true" HeaderText="PSICOLOGÍA APLICADA" />
                        <asp:BoundField DataField="RNOTRC_PAUXILIOS_NOTA_FINAL" Visible="true" HeaderText="PRIMEROS AUXILIOS" />
                         <asp:BoundField DataField="RNOTRC_ASISTENCIA_TOTAL" Visible="true" HeaderText="ASISTENCIA" />
                        <asp:BoundField DataField="RNOTRC_APROBADO" Visible="true" HeaderText="Observación: APROBADO RERPOBADO" />
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="True" Font-Size="Medium"
                        Font-Strikeout="False" />
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

