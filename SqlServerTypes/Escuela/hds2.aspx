<%@ Page Title="" Language="C#" MasterPageFile="~/Escuela/mpEscuela.master" AutoEventWireup="true" CodeFile="hds2.aspx.cs" Inherits="Escuela_hds2" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">

    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/2.0.0/jquery.min.js"></script>

    <script src="https://cdn.jsdelivr.net/npm/handsontable@7.0.0/dist/handsontable.full.min.js"></script>

    <link href="../App_Themes/Estilos/handsontable.full.css" rel="stylesheet" media="screen" />
    <script src="../js/handsontable.full.js"></script>



    <!--MANIPULACION DE DATOS EN LAS TABLAS -->
    <link href="../App_Themes/Estilos/pikaday.css" rel="stylesheet" />
    <script src="../js/moment.js"></script>
    <script src="../js/pikaday.js"></script>

    <!--FUNCIONES EXCEL (RULES JS)-->
    <script src="../js/ruleJS.all.full.js"></script>
    <script src="../js/handsontable.formula.js"></script>
    <style>
        td.azul {
            background: #1E90FF;
            color: white;
        }
    </style>

    <asp:Label runat="server" ID="lblMensaje"></asp:Label>

    <!--<asp:TextBox runat="server" ID="buscador" placeholder="Buscar"></asp:TextBox>-->

    <input type="search" id="buscador" placeholder="Buscar" />


    <div id="calificacion"></div>


    <div class="share-video" style="background:black">
        <div class="share_fb">
            <a href="https://www.facebook.com/sharer/sharer.php?http://aneta.org.ec:5090/acefdos/" target="_blank">Compartir</a>
        </div>
        <div class="share_tw">
            <a href="https://www.twitter.com/intent/tweet?url=http://aneta.org.ec:5090/acefdos/&amp;via=jeec&ammp;text=Curso%20Conducción%20Informe:%2001%20-%20" target="_blank">Twittear</a>
        </div>

         <div class="share_wp">
            <a href="whatsapp://send?text=Aneta curso de condcción http://aneta.org.ec:5090/acefdos" target="_blank">WhatsAppear</a>
        </div>
        <div></div>
    </div>


    <script>
       
        datosAlumnos =<%=obtenerRegistros()%>
            /*[           {"RNOTC_ID":2,"RNOTC_CIRUC": "1708", "RNOTC_APELLIDOS":'ESPI', "RNOTC_NOMBRES":"JOSE", "RNOTC_LICENCIA":"A", "RNOTC_EDUC_VIAL_ASIS":10,"RNOTC_EDUC_VIAL_NOTA": 16,"RNOTC_APROBADO": true}        ];*/

        configuraciones = {
            data: datosAlumnos,
            contextMenu: true,
            formulas: true,
            search: { searchResultClass: 'azul' },
            colHeaders: ['ID', 'IDENTIDAD', 'APELLIDOS', 'NOMBRES', 'LICENCIA', 'ASISTENCIA', 'NOTA', 'ACTIVO'],
            columns: [
                {
                    data:'RNOTC_ID',
                    type: 'numeric',
                    readOnly: true

                },
                {
                    data: 'RNOTC_CIRUC',
                    readOnly: true
                },
                {
                    data: 'RNOTC_APELLIDOS',
                    readOnly: true
                },
                {
                    data: 'RNOTC_NOMBRES',
                    readOnly: true
                },
                {
                    data: 'RNOTC_LICENCIA',
                    readOnly: true
                },
                {
                    data: 'RNOTC_EDUC_VIAL_ASIS',
                    type: 'numeric'
                },
                {
                    data: 'RNOTC_EDUC_VIAL_NOTA',
                    type: 'numeric',
                    format: '0,0.00'
                },
                {
                    data: 'RNOTC_APROBADO',
                    type: 'checkbox',
                    readOnly: true
                }
            ],
            afterCreateRow: function (index, numberOfRows) {
                datosAlumnos.splice(index, numberOfRows)
            },
            afterChange: function (resgistrosModificados,accionesHandsonTable){
                if (accionesHandsonTable != 'loadData') {
                    //leer todos los registros modificados
                    resgistrosModificados.forEach(function (elemento) {
                        //console.log(elemento);
                        var fila = tblExcel.getData()[elemento[0]];
                        console.log(fila);
                        $.ajax({
                            type: "POST",
                            url: "hds2.aspx/modificarRegistro",
                            data: JSON.stringify({ tblExcel: [fila] }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (respuesta) { console.log("Información actualizada:" + respuesta.d); },
                            failure: function (respuesta) { console.log("Existe un fallo:" + respuesta.d); }
                        });
                    })
                }
            }
        };

        tblExcel = new Handsontable(document.getElementById('calificacion'), configuraciones);
        tblExcel.render();

        /*BUSCA UN DATO EN PARTICULAR*/

        var txtBuscador = document.getElementById('buscador');

        Handsontable.Dom.addEvent(txtBuscador, 'keyup', function (event) {
            var ResultadoBusqueda = tblExcel.search.query(this.value);
            tblExcel.render();
        });

        /* desabilita una celda especifica
            tblExcel.updateSettings({
            cells: function (row, col, prop) {
                var propiedadesCeldas = {};
                if((row==1)&&(col==1))
                {
                    propiedadesCeldas.readOnly = 'true';
                }
                return propiedadesCeldas;
            }
        });*/



      /*  configuraciones = {
            data: datosAlumnos,
            contextMenu: true,
            formulas: true,
            search: { searchResultClass: 'azul' },
            colHeaders: ['ID', 'NOMBRE', 'FECHA', 'PUESTO', 'DEPARTAMENTO', 'CONTRASEÑA', 'SUELDO', 'ACTIVO'],
            columns: [
                {
                    type: 'numeric',
                    readOnly: true

                },
                {
                },
                {
                    type: 'date',
                    dateFormat: 'DD/MM/YYYY'
                },
                {
                    type: 'dropdown',
                    source: ['Desarrollador', 'Gerente', 'Coordinador', 'Diseñador']
                },
                {
                    type: 'autocomplete',
                    source: ['Sistemas', 'Publicidad', 'Finanzas', 'R.humanos']
                },
                {
                    type: 'password',
                    hashLength: 10
                },
                {
                    type: 'numeric',
                    format: '$0,0.00'
                },
                {
                    type: 'checkbox'
                }
            ],
            customBorders: [{
                range: {
                    from: {
                        row: 0,
                        col: 4
                    },
                    to: {
                        row: 7,
                        col: 4
                    }

                },
                top: {
                    with: 2,
                    color: '#5292F7'
                },
                left: {
                    with: 2,
                    color: '#5292f7'
                },
                bottom: {
                    with: 2,
                    color: '#5292f7'
                },
                right: {
                    with: 2,
                    color: '#5292f7'
                }


            }
            ],
            afterCreateRow: function (index, numberOfRows) {
                datosAlumnos.splice(index, numberOfRows)
            }

        };*/

    </script>
</asp:Content>

