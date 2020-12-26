<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo/mpCatalogo.master" AutoEventWireup="true" CodeFile="sucursal.aspx.cs" Inherits="Catalogo_sucursal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">

    <asp:Panel ID="pnVer" runat="server">
        <asp:Panel ID="pnVsucursal" runat="server" CssClass="pnForm">
            <asp:GridView ID="grvVerSuc"
                runat="server" AutoGenerateColumns="False"
                CellPadding="5"
                GridLines="Vertical"
                HorizontalAlign="Center"
                Width="100%"
                AllowPaging="True"
                AllowSorting="True"
                DataKeyNames="sucursal"
                PageSize="15" OnPageIndexChanging="grvVerSuc_PageIndexChanging"
                OnSelectedIndexChanged="grvVerSuc_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:TemplateField HeaderText="Código">
                        <ItemTemplate>
                            <%# Eval("sucursal") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Sucursal">
                        <ItemTemplate>
                            <%# Eval("nom_suc") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Establecimiento">
                        <ItemTemplate>
                            <%# Eval("estab") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Punto de emisión">
                        <ItemTemplate>
                            <%# Eval("ptoemi") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Dirección sucursal">
                        <ItemTemplate>
                            <%# Eval("dirEstablecimiento") %>
                        </ItemTemplate>
                    </asp:TemplateField>
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
            <asp:Panel ID="pnBsuc" runat="server" CssClass="pnFormHijo">
                <asp:Button ID=btnSuc runat="server" Text="Nueva sucursal" CssClass="btnForm"
                    OnClick="btnSuc_Click" />
                <asp:Button ID=btnMsuc runat="server" Text="Modifica Sucursal" CssClass="btnForm"
                    OnClick="btnMsuc_Click" />
            </asp:Panel>
        </asp:Panel>
        <asp:Panel ID="pnHijos" runat="server" CssClass="pnForm">

            <asp:Panel ID="pnVaulas" runat="server" CssClass="pnFormHijo">
                <asp:GridView ID="grvVaulas"
                    runat="server" AutoGenerateColumns="False"
                    CssClass="gridForm"
                    CellPadding="5"
                    GridLines="Vertical" HorizontalAlign="Center"
                    Width="15%"
                    AllowPaging="True"
                    AllowSorting="True"
                    DataKeyNames="cod_aula"
                    PageSize="20" OnSelectedIndexChanged="grvVaulas_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:TemplateField HeaderText="Código">
                            <ItemTemplate>
                                <%# Eval("cod_aula") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Capacidad">
                            <ItemTemplate>
                                <%# Eval("capacidad") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <%# Eval("activo") %>
                            </ItemTemplate>
                        </asp:TemplateField>
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
                <asp:Panel ID=pnBaula runat=server CssClass="pnFormHijo">
                    <asp:Button ID=btnAula runat="server" Text="Nueva aula" CssClass="btnForm"
                        OnClick="btnAula_Click" />
                </asp:Panel>
            </asp:Panel>

            <asp:Panel ID="pnVautos" runat="server" CssClass="pnFormHijo">
                <asp:GridView ID="grvVautos"
                    runat="server" AutoGenerateColumns="False"
                    CssClass="pnFormHijo"
                    CellPadding="5"
                    GridLines="Vertical" HorizontalAlign="Center"
                    Width="90%"
                    AllowPaging="True"
                    AllowSorting="True"
                    DataKeyNames="id_auto"
                    PageSize="20" OnSelectedIndexChanged="grvVautos_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:TemplateField HeaderText="id auto" Visible=false>
                            <ItemTemplate>
                                <%# Eval("id_auto") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="# auto">
                            <ItemTemplate>
                                <%# Eval("numeroAuto") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Marca">
                            <ItemTemplate>
                                <%# Eval("marca") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Modelo">
                            <ItemTemplate>
                                <%# Eval("modelo") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Placa">
                            <ItemTemplate>
                                <%# Eval("placa") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <%# Eval("activo") %>
                            </ItemTemplate>
                        </asp:TemplateField>

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
                <asp:Panel ID=pnBauto runat=server CssClass="pnFormHijo">
                    <asp:Button ID=btnAuto runat="server" Text="Nuevo auto" CssClass="btnForm"
                        OnClick="btnAuto_Click" />
                </asp:Panel>
            </asp:Panel>

            <asp:Panel ID="pnVpsicotecnicos" runat="server" CssClass="pnFormHijo">
                <asp:GridView ID="grvVpsicotecnicos"
                    runat="server" AutoGenerateColumns="False"
                    CssClass="gridForm"
                    CellPadding="5"
                    GridLines="Vertical" HorizontalAlign="Center"
                    Width="90%"
                    AllowPaging="True"
                    AllowSorting="True"
                    DataKeyNames="serie"
                    PageSize="20" OnSelectedIndexChanged="grvVpsicotecnicos_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:TemplateField HeaderText="Serie">
                            <ItemTemplate>
                                <%# Eval("serie") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <%# Eval("activo") %>
                            </ItemTemplate>
                        </asp:TemplateField>

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
                <asp:Panel ID=pnBpsico runat=server CssClass="pnFormHijo">
                    <asp:Button ID=btnPsico runat="server" Text="Nuevo psicotécnico" CssClass="btnForm" 
                        OnClick="btnPsico_Click" />
                </asp:Panel>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="pnActualizacion" runat="server">
        <asp:Label ID="lblmensaje" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>
        <asp:Panel ID="pnSucursal" runat="server">
            <fieldset id="sucursal">
                <legend>Datos de la sucursal</legend>
                <asp:Label ID="lblCodSuc" CssClass="lblForm" runat="server" Text="Código "></asp:Label>
                <asp:TextBox ID="txtCodSuc" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblNombre" CssClass="lblForm" runat="server" Text="Nombre"></asp:Label>
                <asp:TextBox ID="txtNombre" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblEstablecimiento" CssClass="lblForm" runat="server" Text="Establecimiento"></asp:Label>
                <asp:TextBox ID="txtEstablecimiento" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblPtoVta" CssClass="lblForm" runat="server" Text="Punto de venta"></asp:Label>
                <asp:TextBox ID="txtPtoVta" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblConvenio" CssClass="lblForm" runat="server" Text="Convenio"></asp:Label>
                <asp:TextBox ID="txtConvenio" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblCiudad" CssClass="lblForm" runat="server" Text="Ciudad"></asp:Label>
                <asp:TextBox ID="txtCiudad" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblDir" CssClass="lblForm" runat="server" Text="Dirección"></asp:Label>
                <asp:TextBox ID="txtDir" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblTel" CssClass="lblForm" runat="server" Text="Teléfono"></asp:Label>
                <asp:TextBox ID="txtTel" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblEmail" CssClass="lblForm" runat="server" Text="E-mail"></asp:Label>
                <asp:TextBox ID="txtEmail" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblAdmin" CssClass="lblForm" runat="server" Text="Nombre del Administrador(a)"></asp:Label>
                <asp:TextBox ID="txtAdmin" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblId" CssClass="lblForm" runat="server" Text="Id"></asp:Label>
                <asp:Panel ID="Panel1" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddliD" runat="server" DataTextField="nom_suc"
                                    DataValueField="idEscuela" >
                        <asp:ListItem Value="SUCURSAL">Sucursal</asp:ListItem>
                        <asp:ListItem Value="ISLA">Isla</asp:ListItem>
                        <asp:ListItem Value="PTOVENTA">Punto de venta</asp:ListItem>
                        <asp:ListItem Value="CTROASIST">CENTRO ASITENCIA</asp:ListItem>
                        <asp:ListItem Value="ESCUELA">ESCUELA</asp:ListItem>
                        <asp:ListItem Value="GASOLINERA">GASOLINERA</asp:ListItem>
                        <asp:ListItem Value="TALLER">TALLER</asp:ListItem>


                        


                    </asp:DropDownList>
                </asp:Panel>
                <asp:Label ID="Label1" CssClass="lblForm" runat="server" Text="Estado"></asp:Label>

                <asp:Panel ID="pnChkS" runat="server" CssClass="pnFormChk">
                    <asp:CheckBox ID=chkEstadoS TextAlign=Left runat="server" />
                </asp:Panel>
                <asp:Button ID=btnGsuc runat="server" Text="Grabar" CssClass="btnForm" OnClick="btnGsuc_Click" />
                <asp:Button ID=btnRSuc runat="server" Text="Regresar" CssClass="btnForm" OnClick="btnRSuc_Click" />
            </fieldset>
        </asp:Panel>

        <asp:Panel runat="server" ID="pnAula">
            <fieldset>
                <legend>Aulas</legend>
                <asp:Label ID="lblCodAula" CssClass="lblForm" runat="server" Text="Código"></asp:Label>
                <asp:TextBox ID="txtCodAula" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblCapAula" CssClass="lblForm" runat="server" Text="Capacidad"></asp:Label>
                <asp:TextBox ID="txtCapAula" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblEstAula" CssClass="lblForm" runat="server" Text="Estado"></asp:Label>
                <asp:Panel ID="pnChkAul" runat="server" CssClass="pnFormChk">
                    <asp:CheckBox ID="chkEstadoAul" TextAlign=Left runat="server" />
                </asp:Panel>
                <asp:Panel ID=pnAAula runat=server>
                    <asp:Button ID=btnGaula runat="server" Text="Grabar" CssClass="btnForm" OnClick="btnGaula_Click" />
                    <asp:Button ID=btnRaula runat="server" Text="Regresar" CssClass="btnForm"
                        OnClick="btnRaula_Click" />
                </asp:Panel>

            </fieldset>
        </asp:Panel>

        <asp:Panel runat="server" ID="pnAuto">
            <fieldset>
                <legend>Autos</legend>
                <asp:Label ID="lblNumeroAuto" CssClass="lblForm" runat="server" Text="Número"></asp:Label>
                <asp:TextBox ID="txtNumeroAuto" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblMarca" CssClass="lblForm" runat="server" Text="Marca"></asp:Label>
                <asp:Panel ID="pnMarca" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlMarca" runat="server">
                        <asp:ListItem Value="CHEVROLET">CHEVROLET</asp:ListItem>
                        <asp:ListItem Value="PEUGEOT">PEUGEOT</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
                <asp:Label ID="lblModelo" CssClass="lblForm" runat="server" Text="Modelo"></asp:Label>
                <asp:Panel ID="pnModelo" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlModelo" runat="server" >
                        <asp:ListItem Value="AVEO">AVEO</asp:ListItem>
                        <asp:ListItem Value="SAIL">SAIL</asp:ListItem>
                        <asp:ListItem Value="206">206</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
                <asp:Label ID="lblPlaca" CssClass="lblForm" runat="server" Text="Placa"></asp:Label>
                <asp:TextBox ID="txtPlaca" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblEstadoAuto" CssClass="lblForm" runat="server" Text="Estado"></asp:Label>
                <asp:Panel ID="pnChkAuto" runat="server" CssClass="pnFormChk">
                    <asp:CheckBox ID="chkEstadoAuto" TextAlign=Left runat="server" />
                </asp:Panel>
                <asp:Panel ID=pnAAuto runat=server>
                    <asp:Button ID=btnGauto runat="server" Text="Grabar" CssClass="btnForm" OnClick="btnGauto_Click" />
                    <asp:Button ID=btnRauto runat="server" Text="Regresar" CssClass="btnForm"
                        OnClick="btnRauto_Click" />
                </asp:Panel>

            </fieldset>
        </asp:Panel>

        <asp:Panel runat="server" ID="pnPsicotecnico">
            <fieldset>
                <legend>Psicoténicos</legend>
                <asp:Label ID="lblSerie" CssClass="lblForm" runat="server" Text="Serie"></asp:Label>
                <asp:TextBox ID="txtSerie" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblEstadoPsico" CssClass="lblForm" runat="server" Text="Estado"></asp:Label>
                 <asp:Panel ID="pnEstadoPsico" runat="server" CssClass="pnFormChk">
                    <asp:CheckBox ID="chkEstadoPsico" TextAlign=Left runat="server" />
                </asp:Panel>
                <asp:Panel ID=pnAPsico runat=server>
                    <asp:Button ID=btnApsico runat="server" Text="Grabar" CssClass="btnForm" 
                        OnClick="btnApsico_Click" />
                    <asp:Button ID=btnRpsico runat="server" Text="Regresar" CssClass="btnForm" 
                        OnClick="btnRpsico_Click" />
                </asp:Panel>
            </fieldset>
        </asp:Panel>
    </asp:Panel>

</asp:Content>

