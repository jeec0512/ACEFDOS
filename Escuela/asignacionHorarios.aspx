<%@ Page Title="" Language="C#" MasterPageFile="~/Escuela/mpEscuela.master" AutoEventWireup="true" CodeFile="asignacionHorarios.aspx.cs"
    Inherits="Escuela_asignacionHorarios" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <asp:Panel ID="pnActualizacion" runat="server">
        <asp:Label ID="lblMensaje" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>
        <asp:Panel ID="pnAsignacion" runat="server">
            <fieldset id="fsAsignacion">
                <legend>Asignación de horarios</legend>
                <asp:TextBox ID="txtVeh_id" CssClass="txtForm" runat="server" Visible="false"></asp:TextBox>
                <asp:Label ID="lblSucursal" CssClass="lblForm" runat="server" Text="Sucursal" Visible="true"></asp:Label>

                <asp:Panel ID="pnSucursal" runat="server" CssClass="pnFormDdl" Visible="true">
                    <asp:DropDownList ID="ddlSucursal" DataTextField=nom_suc DataValueField=sucursal runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged2">
                    </asp:DropDownList>
                </asp:Panel>
                <asp:Label ID="lblModalidad" CssClass="lblForm" runat="server" Text="Modalidad"></asp:Label>
                <asp:Panel ID="pnModalidad" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlModalidad" DataTextField=nom_suc DataValueField=mod_id runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlModalidad_SelectedIndexChanged1">
                        <asp:ListItem Value=-1>Seleccione modalidad</asp:ListItem>
                        <asp:ListItem Value=1>15 días</asp:ListItem>
                        <asp:ListItem Value=2>7 días</asp:ListItem>
                        <asp:ListItem Value=3>Fines de semana</asp:ListItem>
                        <asp:ListItem Value=4>Curso corporativo</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
                <asp:Label ID="lblCurso" CssClass="lblForm" runat="server" Text="Curso" Visible="true"></asp:Label>
                <asp:Panel ID="pnCurso" runat="server" CssClass="pnFormDdl" Visible="true">
                    <asp:DropDownList ID="ddlCurso" DataTextField=cur_nomeNclatura DataValueField=cur_id runat="server"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </asp:Panel>

                <asp:Label ID="lblMateria" CssClass="lblForm" runat="server" Text="Materia" Visible="true"></asp:Label>
                <asp:Panel ID="pnMateria" runat="server" CssClass="pnFormDdl" Visible="true">
                    <asp:DropDownList ID="ddlMateria" DataTextField=mat_descripcion DataValueField=mat_id runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlMateria_SelectedIndexChanged1">
                    </asp:DropDownList>
                </asp:Panel>


                <asp:Panel ID="pnBotonera" runat="server" CssClass="pnFormBotonera">
                    <asp:Button ID=btnGuardar runat="server" Text="Asignar" CssClass="btnForm"
                        OnClick="btnGuardar_Click" />
                    <asp:HyperLink ID=blRegresar runat="server" Text="Regresar" NavigateUrl="~/catalogo/inicioCatalogo.aspx"></asp:HyperLink>
                </asp:Panel>

            </fieldset>
        </asp:Panel>
    </asp:Panel>

    <asp:Panel runat="server">

        <asp:Panel ID="pnAutoDetalle" CssClass="pnPeq" runat="server" Visible="true" BorderStyle="Double">


            <asp:GridView ID="grvAutoDetalle" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="90%"
                AllowSorting="True" PageSize="50" OnRowCommand="grvAutoDetalle_RowCommand"
                OnRowDataBound="grvAutoDetalle_RowDataBound" CssClass="Rojo">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:ButtonField HeaderText="Modificar" Text="..." ButtonType="Image"
                        ImageUrl="~/images/iconos/mod.ico" CommandName="modReg" ItemStyle-Width="60" Visible="false">
                        <ItemStyle Width="60px" />
                    </asp:ButtonField>
                    <asp:BoundField DataField="veh_id" HeaderText="Código" Visible="true"
                        ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone">
                        <HeaderStyle CssClass="DisplayNone" />
                        <ItemStyle CssClass="DisplayNone" />
                    </asp:BoundField>
                    <asp:BoundField DataField="veh_numero" HeaderText="# vehículo" Visible="true" />
                    <asp:BoundField DataField="veh_placa" HeaderText="Placa" Visible="false" />
                    <asp:BoundField DataField="veh_marca" HeaderText="Marca" Visible="false" />
                    <asp:BoundField DataField="veh_modelo" HeaderText="Modelo" Visible="false" />
                    <asp:BoundField DataField="veh_anio" HeaderText="Año" Visible="false" />
                    <asp:BoundField DataField="veh_motor" HeaderText="Motor" Visible="false" />
                    <asp:BoundField DataField="veh_chasis" HeaderText="Chasis" Visible="false" />

                    <asp:CheckBoxField DataField="veh_estado" HeaderText="Estado" Visible="true" ReadOnly="False"
                        ControlStyle-BorderStyle=Solid ControlStyle-ForeColor="#FF0066" ControlStyle-Width=20px
                        ItemStyle-Width=20px HeaderStyle-VerticalAlign=Top ControlStyle-Height=20px FooterStyle-Height=20PX
                        ItemStyle-BackColor=White InsertVisible=False ShowHeader=False>
                        <ControlStyle Width="40px" BackColor="Blue" Font-Bold="True" Font-Size="Large" Height=40px
                            Font-Overline="True" Font-Strikeout="True" />

                        <ItemStyle BackColor="Red" BorderColor="Red" Font-Bold="True" ForeColor="red"
                            Font-Names="Arial Black" Width="40px" BorderStyle="Double" Font-Size="Larger" />
                    </asp:CheckBoxField>

                    <asp:ButtonField HeaderText="Marcar" Text="..." ButtonType="Image"
                        ImageUrl="~/images/iconos/grabar.ico" CommandName="Jusveh" ItemStyle-Width="60">
                        <ItemStyle Width="60px" />
                    </asp:ButtonField>
                    <asp:BoundField DataField="sucursal" HeaderText="Sucursal" Visible="false" />





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

        </asp:Panel>

        <asp:Panel ID="pnAulaDetalle" CssClass="pnPeq" runat="server" Visible="true" BorderStyle="Double">


            <asp:GridView ID="grvAulaDetalle" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="90%"
                AllowSorting="True" PageSize="50" ShowFooter="false" OnRowCommand="grvAulaDetalle_RowCommand"
                OnRowDataBound="grvAulaDetalle_RowDataBound">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:ButtonField HeaderText="Modificar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/mod.ico"
                        CommandName="modReg" ItemStyle-Width="60" Visible="false" />
                    <asp:BoundField DataField="aul_id" HeaderText="Código" Visible="true" ItemStyle-CssClass="DisplayNone"
                        HeaderStyle-CssClass="DisplayNone" />
                    <asp:BoundField DataField="aul_escuela" HeaderText="Escuela" Visible="true" />
                    <asp:BoundField DataField="aul_descripcion" HeaderText="Descripción aula" Visible="true" />
                    <asp:BoundField DataField="aul_capacidad" HeaderText="Capacidad" Visible="true" />
                    <asp:CheckBoxField DataField="aul_activo" HeaderText="Estado" Visible="true" />
                    <asp:ButtonField HeaderText="Marcar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/grabar.ico"
                        CommandName="Jusaula" ItemStyle-Width="60" />
                    <asp:BoundField DataField="sucursal" HeaderText="Sucursal" Visible="false" />

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

        </asp:Panel>

        <asp:Panel ID="pnHorarioDetalle" CssClass="pnPeq" runat="server" Visible="true" BorderStyle="Double">


            <asp:GridView ID="grvHorarioDetalle" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="90%"
                AllowSorting="True" PageSize="50" ShowFooter="false"
                OnRowCommand="grvHorarioDetalle_RowCommand1" OnRowDataBound="grvHorarioDetalle_RowDataBound1">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:ButtonField HeaderText="Modificar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/mod.ico"
                        CommandName="modReg" ItemStyle-Width="60" Visible="false" />
                    <asp:BoundField DataField="hor_id" HeaderText="Código" Visible="true" ItemStyle-CssClass="DisplayNone"
                        HeaderStyle-CssClass="DisplayNone" />

                    <asp:BoundField DataField="hor_inicio" HeaderText="Hora de inicio" Visible="true" />



                    <asp:BoundField DataField="hor_fin" HeaderText="Hora de finalización" Visible="true" />
                    <asp:CheckBoxField DataField="hor_estado" HeaderText="Estado" Visible="true" />
                    <asp:ButtonField HeaderText="Marcar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/grabar.ico"
                        CommandName="Jushorario" ItemStyle-Width="60" />
                    <asp:BoundField DataField="sucursal" HeaderText="Sucursal" Visible="false" />


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

        </asp:Panel>
        <asp:Panel ID="pnFechasDetalle" CssClass="pnPeq" runat="server" Visible="true" BorderStyle="Double">


            <asp:GridView ID="grvTallerDetalle" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="90%"
                AllowSorting="True" PageSize="50" ShowFooter="false"
                OnRowCommand="grvTallerDetalle_RowCommand" OnRowDataBound="grvTallerDetalle_RowDataBound">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:ButtonField HeaderText="Modificar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/mod.ico"
                        CommandName="modReg" ItemStyle-Width="60" Visible="false" />
                    <asp:BoundField DataField="tal_id" HeaderText="Código" Visible="true" ItemStyle-CssClass="DisplayNone"
                        HeaderStyle-CssClass="DisplayNone" />
                    <asp:BoundField DataField="TAL_FECHA" HeaderText="Fecha" Visible="true" DataFormatString="{0:D}" />
                    <asp:CheckBoxField DataField="TAL_ESTADO" HeaderText="Estado" Visible="true" />
                    <asp:ButtonField HeaderText="Marcar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/grabar.ico"
                        CommandName="Jusfecha" ItemStyle-Width="60" />
                    <asp:BoundField DataField="sucursal" HeaderText="Sucursal" Visible="false" />



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

        </asp:Panel>

    </asp:Panel>

    <asp:Panel ID=pnAsignaciones runat="server">
        <asp:GridView ID="grvAsignaciones" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
            BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
            Width="90%"
            AllowSorting="True" PageSize="50" ShowFooter="false">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:ButtonField HeaderText="Modificar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/mod.ico"
                    CommandName="modReg" ItemStyle-Width="60" Visible="false" />
                <asp:BoundField DataField="asm_id" HeaderText="Código" Visible="true" ItemStyle-CssClass="DisplayNone"
                    HeaderStyle-CssClass="DisplayNone" />
                <asp:BoundField DataField="aul_escuela" HeaderText="Aula" Visible="true" />
                <asp:BoundField DataField="veh_id" HeaderText="# vehículo" Visible="true" />
                <asp:BoundField DataField="hor_inicio" HeaderText="Hora inicio" Visible="true" />
                <asp:BoundField DataField="hor_fin" HeaderText="Hora fin" Visible="true" />
                <asp:BoundField DataField="asm_disponible" HeaderText="Disponibilidad" Visible="true" />
                <asp:BoundField DataField="asm_registrado" HeaderText="# de registrados" Visible="true" />
                <asp:BoundField DataField="tal_fecha" HeaderText="Fecha de taller" Visible="true" DataFormatString="{0:D}" />


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

    </asp:Panel>
</asp:Content>

