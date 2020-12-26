<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo/mpCatalogo.master" AutoEventWireup="true" CodeFile="menuDinamico.aspx.cs" Inherits="Catalogo_menuDinamico" EnableEventValidation="false" %>

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

    <asp:Button ID="btnMostrarmenudinamico" runat="server" text="Mostrar el detalle"/>
    
    <input type="search" id="buscador" placeholder="Buscar" />


    <div id="IDmenuDimamico"></div>


    <div class="share-video" style="background: black">
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
        function menuDinamico2(PEPE)
        {
            console.log(`hola MUNDO ${PEPE}`)
        }

        const menuDinamico = () => 
            {
                datosMenuDinamico =<%=obtenerRegistros()%>
                     configuraciones = {
                         data: datosMenuDinamico,
                         contextMenu: true,
                         formulas: true,
                         search: { searchResultClass: 'azul' },
                         colHeaders: ['id', 'llave', 'padre', 'menu', 'submenu', 'boton', 'sis', 'ger', 'cntGen', 'cntAux', 'adm', 'counter', 'secCnt', 'secEscGen', 'secEsc'
                                         , 'secEscTeo', 'secEscPra', 'socGen', 'socAux', 'vendedor', 'especial', 'externo', 'aud', 'tallAdm', 'socios', 'visitAneta', 'visitPub'
                                         , 'secacadteoria', 'secacadpractica', 'secacadtalleres', 'certificadoIso', 'operadorSico', 'jefeFlota', 'calidad'],
                         columns: [
                             {data: 'id_UsuarioMenuDinamico',type: 'numeric',readOnly: true}
                             , {data: 'llave',type: 'numeric',readOnly: true}
                             , {data: 'padre',type: 'numeric'}
                             , {data: 'menu',readOnly: true}
                             , {data: 'submenu',readOnly: true}
                             , {data: 'boton',readOnly: true}
                             , {data: 'sis',type: 'checkbox'}
                             , {data: 'ger', type: 'checkbox'}
                             , { data: 'cntGen', type: 'checkbox' }
                             , { data: 'cntAux', type: 'checkbox' }
                             , { data: 'adm', type: 'checkbox' }
                             , { data: 'counter', type: 'checkbox' }
                             , { data: 'secCnt', type: 'checkbox' }
                             , { data: 'secEscGen', type: 'checkbox' }
                             , { data: 'secEsc', type: 'checkbox' }
                             , { data: 'secEscTeo', type: 'checkbox' }
                             , { data: 'secEscPra', type: 'checkbox' }
                             , { data: 'socGen', type: 'checkbox' }
                             , { data: 'socAux', type: 'checkbox' }
                             , { data: 'vendedor', type: 'checkbox' }
                             , { data: 'especial', type: 'checkbox' }
                             , { data: 'externo', type: 'checkbox' }
                             , { data: 'aud', type: 'checkbox' }
                             , { data: 'tallAdm', type: 'checkbox' }
                             , { data: 'socios', type: 'checkbox' }
                             , { data: 'visitAneta', type: 'checkbox' }
                             , { data: 'visitPub', type: 'checkbox' }
                             , { data: 'secacadteoria', type: 'checkbox' }
                             , { data: 'secacadpractica', type: 'checkbox' }
                             , { data: 'secacadtalleres', type: 'checkbox' }
                             , { data: 'certificadoIso', type: 'checkbox' }
                             , { data: 'operadorSico', type: 'checkbox' }
                             , { data: 'jefeFlota', type: 'checkbox' }
                             , { data: 'calidad', type: 'checkbox' }

                         ],
                         afterCreateRow: function (index, numberOfRows) {
                             datosMenuDinamico.splice(index, numberOfRows)
                         },
                         afterChange: function (resgistrosModificados, accionesHandsonTable) {
                             if (accionesHandsonTable != 'loadData') {
                                 //leer todos los registros modificados
                                 resgistrosModificados.forEach(function (elemento) {
                                     //console.log(elemento);
                                     var fila = tblExcel.getData()[elemento[0]];
                                     console.log(fila);
                                     $.ajax({
                                         type: "POST",
                                         url: "menuDinamico.aspx/modificarRegistro",
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

                    tblExcel = new Handsontable(document.getElementById('IDmenuDimamico'), configuraciones);
                    tblExcel.render();

                    /*BUSCA UN DATO EN PARTICULAR*/

                    var txtBuscador = document.getElementById('buscador');

                    Handsontable.Dom.addEvent(txtBuscador, 'keyup', function (event) {
                        var ResultadoBusqueda = tblExcel.search.query(this.value);
                        tblExcel.render();
                    });
                    }


        //menuDinamico();

        const btnMostrarmenudinamico = document.getElementById("contenidoPrincipal_contenidoPrincipal_btnMostrarmenudinamico");
        //console.log(btnMostrarmenudinamico);
        btnMostrarmenudinamico.addEventListener("click",()=> {
            console.log('hola MUNDO')
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

