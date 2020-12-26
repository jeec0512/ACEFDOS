<%@ Page Title="" Language="C#" MasterPageFile="~/Escuela/mpEscuela.master" AutoEventWireup="true" CodeFile="handSomeTable.aspx.cs" Inherits="Escuela_handSomeTable"  %>




<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
   
    <script src="https://cdn.jsdelivr.net/npm/handsontable@7.0.0/dist/handsontable.full.min.js"></script>

    <link type="text/css" rel="stylesheet" href="https://cdn.jsdelivr.net/npm/handsontable@7.0.0/dist/handsontable.full.min.css">

    <!-- FUNCIONES EXCEL-->
    <script src="../js/ruleJS.all.full.js"></script>

    <script src="../js/handsontable.formula.js"></script>


 <h1>hola</h1>

     <div id="calificacion" ></div>



    <script >
        key: 'non-commercial-and-evaluation'
        calificaciones = [1,2,3,4,5];



        configuraciones = {
            data: calificaciones,
            colHeaders:true
        };

        tblExcel = new Handsontable(document.getElementById('calificacion'), configuraciones);
        tblExcel.render();


    </script>
</asp:Content>

