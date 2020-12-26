<%@ Page Language="C#" AutoEventWireup="true" CodeFile="imprimirRehabilitacionVial.aspx.cs" Inherits="Escuela_imprimirRehabilitacionVial" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <header>
            <h3>NÓMINA DE ESTUDIANTES MATRICULADOS EN EL CURSO DE CONCIENCIACIÓN, REEDUCACIÓN Y REHABILITACIÓN VIAL</h3>
            <h3>NO. CONVENIO <span>NUMCONVENIO</span></h3>
            <h3>FECHA FIRMA DE CONVENIO <span>FECHACONVENIO</span></h3>
            <h3>NO. OFICIO DE AUTORIZACIÓN DE INICIO DE CLASES: <span>NUMOFICIO</span></h3>
            <h3>NO. RUC DE LA ESCUELA <span>RUCESCUELA</span></h3>
        </header>
        <main>
            <asp:GridView ID="grvCursoDetalle" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                    BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                    Width="90%"
                    AllowSorting="True" PageSize="10" CssClass="Rojo">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField DataField="RNOTC_id" Visible="false"
                            ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone">
                            <HeaderStyle CssClass="DisplayNone" />
                            <ItemStyle CssClass="DisplayNone" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NUM" Visible="true" HeaderText="No" />
                        <asp:BoundField DataField="sucursal" Visible="true" HeaderText="NOMBRE DE LA ESCUELA" />
                        <asp:BoundField DataField="RNOTC_CIRUC" Visible="true" HeaderText="NÚMERO DE IDENTIFICACIÓN DEL CONDUCTOR (CÉDULA-PASAPORTE-CARNÉ DE" />
                        <asp:BoundField DataField="REG_FACTURANUMERO" Visible="true" HeaderText="APELLIDOS Y NOBRES DEL CONDUCTOR" />
                        <asp:BoundField DataField="MATRICULA" Visible="true" HeaderText="NO.CERTIFICADO DEL CONDUCTOR" />
                        <asp:BoundField DataField="PERMISO" Visible="true" HeaderText="FECHA EMISIÓN CERTIFICADO DE CONDUCTOR" />
                        <asp:BoundField DataField="RNOTC_EDUC_VIAL_NOTA" Visible="true" HeaderText="FECHA DE MATRÍCULA (DD-MM-AA)" />
                        <asp:BoundField DataField="RNOTC_EDUC_VIAL_SUP1" Visible="true" HeaderText="FECHA DE INICIO DEL CURSO" />
                        <asp:BoundField DataField="RNOTC_EDUC_VIAL_SUP2" Visible="true" HeaderText="FECHA DE FIN DE CURSO" />
                        <asp:BoundField DataField="RNOTC_EDUC_VIAL_ASIS" Visible="true" HeaderText="JORNADA lunes a viernes ó ffines de semana" />
                        <asp:BoundField DataField="MEC_NOTA" Visible="true" HeaderText="HORARIOSDE CLASES" />
                        <asp:BoundField DataField="MEC_SUP1" Visible="true" HeaderText="PARALELO O NUM" />
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
        </main>
        <footer>
            <p>Declaro que los datos de identificación de los estudiantes (apellidos, nombres y números de cédula) que constan es
               esta nómina fueron validados con la información de la página web oficial del Registro Civil (https://servicios.registrocivil.gob.ec/cdd/).</p>
            <p>Para constancia firman:</p>
            <img src="../images/firmas/fabito.jpg" style="height: 90px; width: 199px" />
            <p>Ing. Fabio Tamayo</p>
            <p>DIRECTOR NACIONAL DE ESCUELAS</p>
        </footer>
    
    </div>
    </form>
</body>
</html>
