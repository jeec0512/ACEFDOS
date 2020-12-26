<%@ Page Title="" Language="C#" MasterPageFile="~/Escuela/mpEscuela.master" AutoEventWireup="true" CodeFile="disponibilidadAutos2.aspx.cs"
    Inherits="Escuela_disponibilidadAutos2" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <link href="../App_Themes/Estilos/anexo4.css" rel="stylesheet" />
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <!-- MENSAJE!-->

   

    <asp:Panel ID="pnMensaje" CssClass="" runat="server" Visible="true">
        <asp:Label ID="lblMensaje" runat="server" Text="" Visible="true" ForeColor="Red" Font-Size="Medium"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="pnMatricula" CssClass="pnFormSocio" Style="width: 90vw;  display: flex; flex-direction:column; flex-wrap:wrap; " runat="server" Visible="true">
        <asp:Label runat="server" ID="lblDispAutos" Text="CUADRO ASIGNACIÓN DE CURSOS" Style="display: block; margin: 0.5rem; text-align: center; color: orange; font-size: 1.5rem; font-weight: 700"></asp:Label>

        <asp:Panel runat="server" ID="Fieldset1" Style="display: flex; justify-content: space-between; margin-bottom: 1rem;">

            <asp:Panel ID="pnEscuela" runat="server" Style="display: flex; flex-direction:column; flex-wrap:wrap; justify-content:space-evenly; width:40vw;" ForeColor="#0033cc">


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

                    <asp:Panel ID="pnCiudad" runat="server" CssClass="pnFormDdl" Style="margin-bottom: 1rem;" Visible="true">
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
                            Style="font-size: 1rem; width: 80%; display: block; margin: 0.5rem; text-align: left; color: blue; font-weight: 600">
                        </asp:DropDownList>
                    </asp:Panel>

                    
                        <asp:Panel ID="pnModalidad" runat="server" CssClass="pnFormDdl"  Style="margin-bottom: 1rem;" Visible="true">
                            <asp:Label ID="lblModalidad" CssClass="lblPeq" runat="server" Text="Modalidad"></asp:Label>
                            <asp:DropDownList ID="ddlModalidad" DataTextField="mod_descripcion" DataValueField="mod_id" runat="server"
                                AutoPostBack="True" Style="font-size: 1rem; width: 80%; display: block; margin: 0.5rem; text-align: left; color: blue; font-weight: 600" 
                                OnSelectedIndexChanged="ddlModalidad_SelectedIndexChanged">
                            </asp:DropDownList>
                        </asp:Panel>

                    <asp:Panel ID="pnCurso" runat="server" CssClass="pnFormDdl" Style="margin-bottom: 1rem;" Visible="true">
                        <asp:Label ID="lblCurso" CssClass="lblPeq" runat="server" Text="Curso" Visible="true"></asp:Label>
                        <asp:DropDownList ID="ddlCurso" DataTextField="cur_nomeNclatura" DataValueField="cur_id" runat="server"
                            AutoPostBack="True" Style="font-size: 1rem; width: 80%; display: block; margin: 0.5rem; text-align: left; color: blue; font-weight: 600"
                            OnSelectedIndexChanged="ddlCurso_SelectedIndexChanged">
                        </asp:DropDownList>
                    </asp:Panel>

            </asp:Panel>


            <asp:Panel ID="pnCalendario" runat="server" Style="display: flex; flex-direction:column;justify-content:space-between; width: 30vw;" ForeColor="#0033cc">
                <asp:Panel ID="Panel1" runat="server">

                    <asp:Label ID="lblFechIni" runat="server" Style="display: block; margin: 0.5rem; text-align: left; color: blue; font-size: 1rem; font-weight: 600" Text="Fecha de inicio:"></asp:Label>
                    <asp:Label ID="lblFechFin" runat="server" Style="display: block; margin: 0.5rem; text-align: left; color: blue; font-size: 1rem; font-weight: 600" Text="Fecha de finalización:"></asp:Label>


                </asp:Panel>
                <asp:Panel ID="pnHojas" CssClass="" runat="server" Visible="true">
                    <asp:Button ID="btnCliente" runat="server" Text="REFRESCAR" OnClick="btnCliente_Click" />
                </asp:Panel>
            </asp:Panel>

        </asp:Panel>
    </asp:Panel>



    <asp:Panel ID="pnPractica1" runat="server" GroupingText="" ForeColor="#0033cc"  style ="margin-top:2rem;">
        <fieldset id="fsPractica">
            <legend>Práctica</legend>

            <asp:GridView ID="GridView1" runat="server" CellPadding="4" DataSourceID="SqlDataSource1" 
                ForeColor="#333333" GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="VEH_ID" HeaderText="VEH_ID" SortExpression="VEH_ID" />
                    <asp:BoundField DataField="06:00" HeaderText="06:00" SortExpression="06:00" />
                    <asp:BoundField DataField="07:00" HeaderText="07:00" SortExpression="07:00" />
                    <asp:BoundField DataField="08:00" HeaderText="08:00" SortExpression="08:00" />
                    <asp:BoundField DataField="09:00" HeaderText="09:00" SortExpression="09:00" />
                    <asp:BoundField DataField="10:00" HeaderText="10:00" SortExpression="10:00" />
                    <asp:BoundField DataField="11:00" HeaderText="11:00" SortExpression="11:00" />
                    <asp:BoundField DataField="12:00" HeaderText="12:00" SortExpression="12:00" />
                    <asp:BoundField DataField="13:00" HeaderText="13:00" SortExpression="13:00" />
                    <asp:BoundField DataField="14:00" HeaderText="14:00" SortExpression="14:00" />
                    <asp:BoundField DataField="15:00" HeaderText="15:00" SortExpression="15:00" />
                    <asp:BoundField DataField="16:00" HeaderText="16:00" SortExpression="16:00" />
                    <asp:BoundField DataField="17:00" HeaderText="17:00" SortExpression="17:00" />
                    <asp:BoundField DataField="18:00" HeaderText="18:00" SortExpression="18:00" />
                    <asp:BoundField DataField="19:00" HeaderText="19:00" SortExpression="19:00" />
                    <asp:BoundField DataField="20:00" HeaderText="20:00" SortExpression="20:00" />
                    <asp:BoundField DataField="21:00" HeaderText="21:00" SortExpression="21:00" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>


            <asp:Panel ID="pnAuto15" runat="server" Style="max-height: 60vh; overflow-y: scroll; margin-bottom: 1.5rem;">
                <asp:GridView ID="grvAuto15" runat="server" BackColor="White" BorderColor="#3366CC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="4" HorizontalAlign="Center"
                    Width="98%"
                    AllowSorting="True" PageSize="5"
                    >
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
            <asp:Panel ID="pnAuto7" runat="server" Style="max-height: 60vh; overflow-y: scroll; margin-bottom: 1.5rem;">
                
               
                <asp:GridView ID="grvAuto7" runat="server" BackColor="White" BorderColor="#3366CC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="4" HorizontalAlign="Center"
                    Width="98%"
                    AllowSorting="True" PageSize="5"
                    >
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
            <asp:Panel ID="pnAutoFS" runat="server" Style="max-height: 60vh; overflow-y: scroll; margin-bottom: 1.5rem;">
                <asp:GridView ID="grvAutoFS" runat="server" BackColor="White" BorderColor="#3366CC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="4" HorizontalAlign="Center"
                    Width="98%"
                    AllowSorting="True" PageSize="5"
                    >
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

            <!--MOTOS-->
            <asp:Panel ID="pnMotoAM10" runat="server" Style="max-height: 60vh; overflow-y: scroll; margin-bottom: 1.5rem;">
                <asp:GridView ID="grvMotoAM10" runat="server" BackColor="White" BorderColor="#3366CC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="4" HorizontalAlign="Center"
                    Width="98%"
                    AllowSorting="True" PageSize="5"
                    >
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

            <asp:Panel ID="pnMotoMI5" runat="server" Style="max-height: 60vh; overflow-y: scroll; margin-bottom: 1.5rem;">
                <asp:GridView ID="grvMotoMI5" runat="server" BackColor="White" BorderColor="#3366CC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="4" HorizontalAlign="Center"
                    Width="98%"
                    AllowSorting="True" PageSize="5"
                    >
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


            <asp:Panel ID="pnMotoMF" runat="server" Style="max-height: 60vh; overflow-y: scroll; margin-bottom: 1.5rem;">
                <asp:GridView ID="grvMotoMF" runat="server" BackColor="White" BorderColor="#3366CC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="4" HorizontalAlign="Center"
                    Width="98%"
                    AllowSorting="True" PageSize="5"
                    >
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
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DB_ESCUELAConnectionString %>" SelectCommand="sp_autosUtilizadosEjecutar15" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:Parameter DefaultValue="accion" Name="accion" Type="String" />
                    <asp:FormParameter DefaultValue="" FormField="ddlSucursal.SelectedValue" Name="sucursal" Type="String" />
                    <asp:FormParameter DefaultValue="" FormField="ddlMateria.SelectedValue" Name="materia" Type="Int32" />
                    <asp:FormParameter DefaultValue="" FormField="ddlCurso.SelectedValue" Name="curso" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>

            

        </fieldset>
    </asp:Panel>
</asp:Content>

