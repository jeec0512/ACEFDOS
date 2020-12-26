<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="Default.aspx.cs"
    Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contenidoMenuContextual" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <div id="idCabIngreso" style="display: flex; flex-direction: column;">
        <h2 style="text-align: left; color: #f79a36; font-weight: 700;">INFORMATIVO</h2>
    </div>

    <div style="width: 90vw; background: #FEF5EB; display: flex; justify-content: space-evenly;">

        <section id="sImagenes" style="margin-right: 1rem; padding: 2rem;">
            <h2>Noticias ANETA</h2>
            <div style="display: flex; flex-direction: column">

                <article style="margin-bottom: 2rem">
                    <asp:Button ID="btnImgP" runat="server" Text="Parar secuencia de imágenes" Visible="true" OnClick="btnImgP_Click" Style="color: #094697; margin: 0.5rem; background: #FEF5EB;" />
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="Panel1" CssClass="" runat="server" Visible="true">
                                <asp:Timer ID="Timer1" runat="server" Interval="10000" OnTick="Timer1_Tick">
                                </asp:Timer>
                                <asp:Image ID="Image1" runat="server" Style="width: 30vw; height: 35vh;" />
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </article>

                <article style="margin-bottom: 2rem; border: 0.1rem solid #094697">
                    <asp:Label ID="lblImageName" CssClass="lblPeq" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblImageOrder" CssClass="lblPeq" runat="server" Visible="false"></asp:Label>
                    <div style="display: flex; flex-direction: column; padding: 1rem;">
                        <div>
                            <asp:FileUpload ID="fuInicio" runat="server" Visible="true" Style="color: white; background: #094697;" />
                            <asp:Button ID="btnUpload" runat="server" Text="Cargar" OnClick="btnUpload_Click" Visible="true" Style="color: #094697; margin: 0.5rem; background: #FEF5EB;" />
                        </div>

                        <asp:GridView ID="grvArchivos" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                            AutoGenerateColumns="False" OnRowCommand="grvArchivos_RowCommand">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Archivo">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("Archivo") %>'
                                            CommandName="downLoad" Text='<%# Eval("Archivo") %>' ForeColor="Blue"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Dimension" HeaderText="Dimensión en Bytes" />
                                <asp:BoundField DataField="Tipo" HeaderText="Tipo de archivo" />
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
                    </div>
                </article>

                <asp:Panel ID="Panel2" CssClass="" runat="server" Visible="true" Style="margin-bottom: 2rem">
                    <asp:Label ID="Label3" CssClass="lblPeq" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="Label4" CssClass="lblPeq" runat="server" Visible="false"></asp:Label>
                </asp:Panel>

                <article runat="server" style="margin-bottom: 2rem" visible="false">
                    <asp:Panel ID="pnSicotecnicos" CssClass="pnAccionGrid" runat="server" Wrap="False" Visible="true">
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="https://goo.gl/forms/WtOJuZS11qxXmSl33"
                            Target="_blank" Text="De un click para ir a las encuestas"
                            ForeColor="DarkBlue" BorderColor="black" BorderWidth="1"></asp:HyperLink>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="https://goo.gl/forms/WtOJuZS11qxXmSl33"
                            Target="_blank" Text="De un click para ir a las encuestas"
                            ForeColor="DarkBlue" BorderColor="black" BorderWidth="0" ImageUrl="~/images/iconos/car3.ico"></asp:HyperLink>
                    </asp:Panel>
                </article>
            </div>
        </section>

        <section id="nomina" style="margin-right: 1rem; padding: 2rem;">
            <div style="display: flex; flex-direction: column;">
                <h2  id="tNomina">Listado de cumpleañeros de esta semana</h2>
                <asp:Label runat="server" ID="lblNomina"></asp:Label>
                <asp:GridView ID="grvCumplaniero" runat="server"
                    DataKeyNames="cedula"
                    AutoGenerateColumns="False"
                    CssClass="grillaGral" BorderColor="Blue" BorderStyle="Double"
                    ForeColor="Blue" OnSelectedIndexChanged="grvCumplaniero_SelectedIndexChanged">
                    <Columns>
                        <asp:CommandField ShowSelectButton="true" />


                        <asp:BoundField DataField="nom_suc" HeaderText="Sucursal" />
                        <asp:BoundField DataField="nombrecorto" HeaderText="Nombres" />
                        <asp:BoundField DataField="dia" HeaderText="Día" />


                        <asp:BoundField DataField="numdia" HeaderText="#" />
                        <asp:BoundField DataField="mes" HeaderText="Mes" HeaderStyle-CssClass="ancho" ItemStyle-CssClass="ancho" />

                        <asp:ImageField DataImageUrlField="foto" HeaderText="Imagen" AlternateText="Foto no encontrada" DataAlternateTextField="textoAlterno" NullImageUrl="~/admArchivos/nomina/sinImagen.jpg" NullDisplayText="Path de imagen es NULL" ControlStyle-CssClass="foto" ControlStyle-Font-Size="12px" ControlStyle-Width="150px" ControlStyle-Height="150px">
                            <ControlStyle CssClass="foto" />
                        </asp:ImageField>
                    </Columns>

                    <EditRowStyle ForeColor="Blue" />
                    <EmptyDataRowStyle Font-Bold="True" ForeColor="Blue" />

                </asp:GridView>
            </div>
            

        </section>

        <section id="sVideos" style=" padding-left: 1rem; padding-top: 2rem;">
             <h2>Videos ANETA</h2>
            <article>

                <iframe width="90%" height="315" src="https://www.youtube.com/embed/EEAyjQk0x2s" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
                <iframe width="90%" height="315" src="https://www.youtube.com/embed/pDvQ-5z_KiI" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
                <iframe width="90%" height="315" src="https://www.youtube.com/embed/DgAExFlWXa0" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
                <iframe width="90%" height="315" src="https://www.youtube.com/embed/xagZP77N-hE" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
                <iframe width="90%" height="315" src="https://www.youtube.com/embed/hzbG0JSX6Z0" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe>
                <iframe width="90%" height="315" src="https://www.youtube.com/embed/zkN88iPpKE4" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe>
                <iframe width="90%" height="315" src="https://www.youtube.com/embed/grXHkrcMOW8" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe>
                <iframe width="90%" height="315" src="https://www.youtube.com/embed/wUdcWNpPKHs" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe>
                <iframe width="90%" height="290" src="https://www.youtube.com/embed/khT1ZAGmxSY" frameborder="0" gesture="media" allow="encrypted-media" allowfullscreen></iframe>
                <iframe width="90%" height="290" src="https://www.youtube.com/embed/VuI2K_5PQeA" frameborder="0" gesture="media" allow="encrypted-media" allowfullscreen></iframe>

            </article>


        </section>



    </div>
    <asp:Label ID="lblMensaje" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>

</asp:Content>

