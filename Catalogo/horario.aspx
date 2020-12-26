<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo/mpCatalogo.master" AutoEventWireup="true"
    CodeFile="horario.aspx.cs" Inherits="Catalogo_horario" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server" >
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <asp:Panel ID="pnActualizacion" runat="server">
       <asp:Label ID="lblMensaje" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>
        <asp:Panel ID="pnHorario" runat="server">
            <fieldset id="fsHorario">
                <legend>Horarios</legend>
                <asp:TextBox ID="txtHor_id" CssClass="txtForm" runat="server" Visible="false"></asp:TextBox>
                <asp:Label ID="lblSucursal" CssClass="lblForm" runat="server" Text="Sucursal"></asp:Label>
                <asp:Panel ID="pnSucursal" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlSucursal" DataTextField=nom_suc DataValueField=sucursal runat="server" 
                        AutoPostBack="True" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged1">
                    </asp:DropDownList>
                </asp:Panel>
                <asp:Label ID="lblModalidad" CssClass="lblForm" runat="server" Text="Modalidad"></asp:Label>
                <asp:Panel ID="pnModalidad" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlModalidad" DataTextField="mod_descripcion" DataValueField="mod_id" runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlModalidad_SelectedIndexChanged">
                    </asp:DropDownList>
                </asp:Panel>
                
                <asp:Label ID="lblMateria" CssClass="lblForm" runat="server" Text="Materia"></asp:Label>
                 <asp:Panel ID="pnMateria" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlMateria" DataTextField=mat_descripcion DataValueField=mat_id runat="server"  AutoPostBack="True" OnSelectedIndexChanged="ddlMateria_SelectedIndexChanged">
                    </asp:DropDownList>
                </asp:Panel>

                <asp:Label ID="lblTurno" CssClass="lblForm" runat="server" Text="Turno"></asp:Label>
                <asp:Panel ID="pnTurno" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlTurno" DataTextField="tur_descripcion" DataValueField="tur_id" runat="server" AutoPostBack="True" >
                    </asp:DropDownList>
                </asp:Panel>

                <asp:Label ID="lblHoraInicio" CssClass="lblForm" runat="server" Text="Hora de inicio"></asp:Label>
                <asp:TextBox ID="txtHoraInicio" CssClass="txtForm" runat="server"></asp:TextBox>
                <act1:MaskedEditExtender ID="mskSuperPhone" runat="server"
                    TargetControlID="txtHoraInicio"
                    ClearMaskOnLostFocus="false"
                    MaskType="None"
                    Mask="99:99"
                    MessageValidatorTip="true"
                    InputDirection="LeftToRight"
                    ErrorTooltipEnabled="True"></act1:MaskedEditExtender>

                <asp:Label ID="lblHoraFin" CssClass="lblForm" runat="server" Text="Hora de Finalización"></asp:Label>
                <asp:TextBox ID="txtHoraFin" CssClass="txtForm" runat="server"></asp:TextBox>
                <act1:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                    TargetControlID="txtHoraFin"
                    ClearMaskOnLostFocus="false"
                    MaskType="None"
                    Mask="99:99"
                    MessageValidatorTip="true"
                    InputDirection="LeftToRight"
                    ErrorTooltipEnabled="True"></act1:MaskedEditExtender>
               
                <asp:Label ID="lblEstado" CssClass="lblForm" runat="server" Text="Estado"></asp:Label>
                <asp:Panel ID="pnEstado" runat="server" CssClass="pnFormChk">
                    <asp:CheckBox ID=chkEstado TextAlign=Left runat="server" />
                </asp:Panel>
                 <asp:Panel ID="pnBotonera" runat="server" CssClass="pnFormBotonera">
                    <asp:Button ID=btnGuardar runat="server" Text="Grabar" CssClass="btnForm" OnClick="btnGuardar_Click"/>
                    <asp:HyperLink ID=blRegresar runat="server" Text="Regresar" NavigateUrl="~/catalogo/inicioCatalogo.aspx"></asp:HyperLink>
                </asp:Panel>
            </fieldset>
        </asp:Panel>
    </asp:Panel>

    <asp:Panel ID="pnDetalleHorario" CssClass="" runat="server" Visible="true">

        <fieldset id="fdDetalleHorario" class="fieldset-principal">
            <legend></legend>
            <asp:GridView ID="grvHorarioDetalle" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="90%"
                AllowSorting="True" PageSize="50" OnRowCommand="grvHorarioDetalle_RowCommand"
                OnRowDataBound="grvHorarioDetalle_RowDataBound" ShowFooter="false">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:ButtonField HeaderText="Modificar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/mod.ico"
                        CommandName="modReg" ItemStyle-Width="60" />
                    <asp:ButtonField HeaderText="Eliminar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/garbage.png"
                        CommandName="eliReg" ItemStyle-Width="60" />
                    
                    <asp:BoundField DataField="hor_id" HeaderText="Código" Visible="true" ItemStyle-CssClass="DisplayNone"
                        HeaderStyle-CssClass="DisplayNone" />
                    <asp:BoundField DataField="hor_inicio" HeaderText="Hora de inicio" Visible="true" />

                    <asp:BoundField DataField="hor_fin" HeaderText="Hora de finalización" Visible="true" />
                    <asp:BoundField DataField="hor_estado" HeaderText="Estado" Visible="true" />

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

    <SCRIPT >
        var timerID = null;
        var timerRunning = false;
        function stopclock() {
            if (timerRunning)
                clearTimeout(timerID);
            timerRunning = false;
        }
        function showtime() {
            var now = new Date();
            var hours = now.getHours();
            var minutes = now.getMinutes();
            var seconds = now.getSeconds();
            var timeValue = "" + ((hours > 12) ? hours - 12 : hours)

            if (timeValue == "0") timeValue = 12;
            timeValue += ((minutes < 10) ? ":0" : ":") + minutes
            timeValue += ((seconds < 10) ? ":0" : ":") + seconds
            timeValue += (hours >= 12) ? " P.M." : " A.M."
            document.getElementById('lblHoraInicio').innerText = timeValue;

            timerID = setTimeout("showtime()", 1000);
            timerRunning = true;
        }
        function startclock() {
            stopclock();
            showtime();
        }
        
</SCRIPT>
</asp:Content>

