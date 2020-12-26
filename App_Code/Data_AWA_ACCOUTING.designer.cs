﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;



[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="AWA_ACCOUNTING")]
public partial class Data_AWA_ACCOUTINGDataContext : System.Data.Linq.DataContext
{
	
	private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
	
  #region Definiciones de métodos de extensibilidad
  partial void OnCreated();
  partial void InsertAWA_CONTROL_PERIODOS(AWA_CONTROL_PERIODOS instance);
  partial void UpdateAWA_CONTROL_PERIODOS(AWA_CONTROL_PERIODOS instance);
  partial void DeleteAWA_CONTROL_PERIODOS(AWA_CONTROL_PERIODOS instance);
  #endregion
	
	public Data_AWA_ACCOUTINGDataContext() : 
			base(global::System.Configuration.ConfigurationManager.ConnectionStrings["AWA_ACCOUNTINGConnectionString"].ConnectionString, mappingSource)
	{
		OnCreated();
	}
	
	public Data_AWA_ACCOUTINGDataContext(string connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public Data_AWA_ACCOUTINGDataContext(System.Data.IDbConnection connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public Data_AWA_ACCOUTINGDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public Data_AWA_ACCOUTINGDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public System.Data.Linq.Table<tmp_mayorizacion> tmp_mayorizacion
	{
		get
		{
			return this.GetTable<tmp_mayorizacion>();
		}
	}
	
	public System.Data.Linq.Table<tmp_cntmov_docc> tmp_cntmov_docc
	{
		get
		{
			return this.GetTable<tmp_cntmov_docc>();
		}
	}
	
	public System.Data.Linq.Table<tmp_cntmov_docm> tmp_cntmov_docm
	{
		get
		{
			return this.GetTable<tmp_cntmov_docm>();
		}
	}
	
	public System.Data.Linq.Table<AWA_CONTROL_PERIODOS> AWA_CONTROL_PERIODOS
	{
		get
		{
			return this.GetTable<AWA_CONTROL_PERIODOS>();
		}
	}
	
	[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_MayorizacionCalculo_V5")]
	public ISingleResult<tmp_mayorizacion> usp_MayorizacionCalculo_V5([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> ano, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> periodo)
	{
		IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), ano, periodo);
		return ((ISingleResult<tmp_mayorizacion>)(result.ReturnValue));
	}
	
	[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.sp_contabilizacionEgresos_v2")]
	public ISingleResult<tmp_cntmov_docc> sp_contabilizacionEgresos_v2([global::System.Data.Linq.Mapping.ParameterAttribute(Name="ACCION", DbType="VarChar(10)")] string aCCION, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="DateTime")] System.Nullable<System.DateTime> fechaInicio, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="DateTime")] System.Nullable<System.DateTime> fechaFin, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> usuario)
	{
		IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), aCCION, fechaInicio, fechaFin, usuario);
		return ((ISingleResult<tmp_cntmov_docc>)(result.ReturnValue));
	}
	
	[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.sp_contabilizacionDetalleEgresos_v2")]
	public int sp_contabilizacionDetalleEgresos_v2([global::System.Data.Linq.Mapping.ParameterAttribute(Name="FECHAINI", DbType="VarChar(10)")] string fECHAINI, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="FECHAFIN", DbType="VarChar(10)")] string fECHAFIN, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="SUCURSAL", DbType="VarChar(3)")] string sUCURSAL, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="CAJA", DbType="VarChar(1)")] string cAJA, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> id_cntmov_docc, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> tipo, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(13)")] string numero, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> usuario)
	{
		IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), fECHAINI, fECHAFIN, sUCURSAL, cAJA, id_cntmov_docc, tipo, numero, usuario);
		return ((int)(result.ReturnValue));
	}
	
	[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.sp_contabilizacionEgresos_v3")]
	public ISingleResult<tmp_cntmov_docc> sp_contabilizacionEgresos_v3([global::System.Data.Linq.Mapping.ParameterAttribute(Name="ACCION", DbType="VarChar(10)")] string aCCION, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="DateTime")] System.Nullable<System.DateTime> fechaInicio, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="DateTime")] System.Nullable<System.DateTime> fechaFin, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> usuario, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(3)")] string sucursal, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(1)")] string caja)
	{
		IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), aCCION, fechaInicio, fechaFin, usuario, sucursal, caja);
		return ((ISingleResult<tmp_cntmov_docc>)(result.ReturnValue));
	}
	
	[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.sp_visualizacionEgresos")]
	public ISingleResult<tmp_cntmov_docc> sp_visualizacionEgresos([global::System.Data.Linq.Mapping.ParameterAttribute(Name="ACCION", DbType="VarChar(10)")] string aCCION, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="DateTime")] System.Nullable<System.DateTime> fechaInicio, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="DateTime")] System.Nullable<System.DateTime> fechaFin, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> usuario)
	{
		IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), aCCION, fechaInicio, fechaFin, usuario);
		return ((ISingleResult<tmp_cntmov_docc>)(result.ReturnValue));
	}
	
	[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.sp_contabilizacionEgresos_v4")]
	public int sp_contabilizacionEgresos_v4([global::System.Data.Linq.Mapping.ParameterAttribute(Name="ACCION", DbType="VarChar(10)")] string aCCION, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="DateTime")] System.Nullable<System.DateTime> fechaInicio, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="DateTime")] System.Nullable<System.DateTime> fechaFin, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> usuario)
	{
		IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), aCCION, fechaInicio, fechaFin, usuario);
		return ((int)(result.ReturnValue));
	}
	
	[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.sp_visualizacionEgresosDet")]
	public ISingleResult<tmp_cntmov_docm> sp_visualizacionEgresosDet([global::System.Data.Linq.Mapping.ParameterAttribute(Name="ACCION", DbType="VarChar(10)")] string aCCION, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="DateTime")] System.Nullable<System.DateTime> fechaInicio, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="DateTime")] System.Nullable<System.DateTime> fechaFin, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> idCabecera, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> usuario)
	{
		IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), aCCION, fechaInicio, fechaFin, idCabecera, usuario);
		return ((ISingleResult<tmp_cntmov_docm>)(result.ReturnValue));
	}
	
	[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.sp_visualizacionEgresosCab")]
	public ISingleResult<tmp_cntmov_docc> sp_visualizacionEgresosCab([global::System.Data.Linq.Mapping.ParameterAttribute(Name="ACCION", DbType="VarChar(10)")] string aCCION, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="DateTime")] System.Nullable<System.DateTime> fechaInicio, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="DateTime")] System.Nullable<System.DateTime> fechaFin, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> usuario, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(3)")] string sucursalFiltro)
	{
		IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), aCCION, fechaInicio, fechaFin, usuario, sucursalFiltro);
		return ((ISingleResult<tmp_cntmov_docc>)(result.ReturnValue));
	}
}

[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.tmp_mayorizacion")]
public partial class tmp_mayorizacion
{
	
	private string _CODIGO_CUENTA_CONTABLE;
	
	private string _NOMBRE_CUENTA_CONTABLE;
	
	private System.Nullable<decimal> _DEBE;
	
	private System.Nullable<decimal> _HABER;
	
	private System.Nullable<decimal> _SALDO;
	
	public tmp_mayorizacion()
	{
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CODIGO_CUENTA_CONTABLE", DbType="VarChar(50)")]
	public string CODIGO_CUENTA_CONTABLE
	{
		get
		{
			return this._CODIGO_CUENTA_CONTABLE;
		}
		set
		{
			if ((this._CODIGO_CUENTA_CONTABLE != value))
			{
				this._CODIGO_CUENTA_CONTABLE = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NOMBRE_CUENTA_CONTABLE", DbType="VarChar(200)")]
	public string NOMBRE_CUENTA_CONTABLE
	{
		get
		{
			return this._NOMBRE_CUENTA_CONTABLE;
		}
		set
		{
			if ((this._NOMBRE_CUENTA_CONTABLE != value))
			{
				this._NOMBRE_CUENTA_CONTABLE = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DEBE", DbType="Money")]
	public System.Nullable<decimal> DEBE
	{
		get
		{
			return this._DEBE;
		}
		set
		{
			if ((this._DEBE != value))
			{
				this._DEBE = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HABER", DbType="Money")]
	public System.Nullable<decimal> HABER
	{
		get
		{
			return this._HABER;
		}
		set
		{
			if ((this._HABER != value))
			{
				this._HABER = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SALDO", DbType="Money")]
	public System.Nullable<decimal> SALDO
	{
		get
		{
			return this._SALDO;
		}
		set
		{
			if ((this._SALDO != value))
			{
				this._SALDO = value;
			}
		}
	}
}

[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.tmp_cntmov_docc")]
public partial class tmp_cntmov_docc
{
	
	private int _id_cntmov_docc;
	
	private int _year;
	
	private string _periodo;
	
	private int _tipo_documento;
	
	private string _secuencial_tipo_documento;
	
	private System.Nullable<int> _secuencial_ejercicio;
	
	private System.Nullable<System.DateTime> _fecha;
	
	private string _detalle;
	
	private System.Nullable<decimal> _tot_deb;
	
	private System.Nullable<decimal> _tot_cre;
	
	private System.Nullable<decimal> _difer;
	
	private string _fac_prov;
	
	private System.Nullable<bool> _modif;
	
	private System.Nullable<System.DateTime> _created_at;
	
	private System.Nullable<int> _user_create;
	
	private System.Nullable<System.DateTime> _updated_at;
	
	private System.Nullable<int> _user_update;
	
	private bool _activo;
	
	private System.Nullable<bool> _periodo_bloqueado;
	
	private System.Nullable<int> _id_sucursal;
	
	private System.Nullable<int> _id_documento;
	
	private System.Nullable<System.DateTime> _fechaIni;
	
	private System.Nullable<System.DateTime> _fechaFin;
	
	private System.Nullable<bool> _esAutomatico;
	
	public tmp_cntmov_docc()
	{
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id_cntmov_docc", AutoSync=AutoSync.Always, DbType="Int NOT NULL IDENTITY", IsDbGenerated=true)]
	public int id_cntmov_docc
	{
		get
		{
			return this._id_cntmov_docc;
		}
		set
		{
			if ((this._id_cntmov_docc != value))
			{
				this._id_cntmov_docc = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_year", DbType="Int NOT NULL")]
	public int year
	{
		get
		{
			return this._year;
		}
		set
		{
			if ((this._year != value))
			{
				this._year = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_periodo", DbType="VarChar(2) NOT NULL", CanBeNull=false)]
	public string periodo
	{
		get
		{
			return this._periodo;
		}
		set
		{
			if ((this._periodo != value))
			{
				this._periodo = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_tipo_documento", DbType="Int NOT NULL")]
	public int tipo_documento
	{
		get
		{
			return this._tipo_documento;
		}
		set
		{
			if ((this._tipo_documento != value))
			{
				this._tipo_documento = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_secuencial_tipo_documento", DbType="VarChar(20)")]
	public string secuencial_tipo_documento
	{
		get
		{
			return this._secuencial_tipo_documento;
		}
		set
		{
			if ((this._secuencial_tipo_documento != value))
			{
				this._secuencial_tipo_documento = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_secuencial_ejercicio", DbType="Int")]
	public System.Nullable<int> secuencial_ejercicio
	{
		get
		{
			return this._secuencial_ejercicio;
		}
		set
		{
			if ((this._secuencial_ejercicio != value))
			{
				this._secuencial_ejercicio = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_fecha", DbType="DateTime")]
	public System.Nullable<System.DateTime> fecha
	{
		get
		{
			return this._fecha;
		}
		set
		{
			if ((this._fecha != value))
			{
				this._fecha = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_detalle", DbType="VarChar(200)")]
	public string detalle
	{
		get
		{
			return this._detalle;
		}
		set
		{
			if ((this._detalle != value))
			{
				this._detalle = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_tot_deb", DbType="Decimal(12,6)")]
	public System.Nullable<decimal> tot_deb
	{
		get
		{
			return this._tot_deb;
		}
		set
		{
			if ((this._tot_deb != value))
			{
				this._tot_deb = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_tot_cre", DbType="Decimal(12,6)")]
	public System.Nullable<decimal> tot_cre
	{
		get
		{
			return this._tot_cre;
		}
		set
		{
			if ((this._tot_cre != value))
			{
				this._tot_cre = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_difer", DbType="Decimal(12,6)")]
	public System.Nullable<decimal> difer
	{
		get
		{
			return this._difer;
		}
		set
		{
			if ((this._difer != value))
			{
				this._difer = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_fac_prov", DbType="VarChar(40)")]
	public string fac_prov
	{
		get
		{
			return this._fac_prov;
		}
		set
		{
			if ((this._fac_prov != value))
			{
				this._fac_prov = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_modif", DbType="Bit")]
	public System.Nullable<bool> modif
	{
		get
		{
			return this._modif;
		}
		set
		{
			if ((this._modif != value))
			{
				this._modif = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_created_at", DbType="DateTime")]
	public System.Nullable<System.DateTime> created_at
	{
		get
		{
			return this._created_at;
		}
		set
		{
			if ((this._created_at != value))
			{
				this._created_at = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_user_create", DbType="Int")]
	public System.Nullable<int> user_create
	{
		get
		{
			return this._user_create;
		}
		set
		{
			if ((this._user_create != value))
			{
				this._user_create = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_updated_at", DbType="DateTime")]
	public System.Nullable<System.DateTime> updated_at
	{
		get
		{
			return this._updated_at;
		}
		set
		{
			if ((this._updated_at != value))
			{
				this._updated_at = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_user_update", DbType="Int")]
	public System.Nullable<int> user_update
	{
		get
		{
			return this._user_update;
		}
		set
		{
			if ((this._user_update != value))
			{
				this._user_update = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_activo", DbType="Bit NOT NULL")]
	public bool activo
	{
		get
		{
			return this._activo;
		}
		set
		{
			if ((this._activo != value))
			{
				this._activo = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_periodo_bloqueado", DbType="Bit")]
	public System.Nullable<bool> periodo_bloqueado
	{
		get
		{
			return this._periodo_bloqueado;
		}
		set
		{
			if ((this._periodo_bloqueado != value))
			{
				this._periodo_bloqueado = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id_sucursal", DbType="Int")]
	public System.Nullable<int> id_sucursal
	{
		get
		{
			return this._id_sucursal;
		}
		set
		{
			if ((this._id_sucursal != value))
			{
				this._id_sucursal = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id_documento", DbType="Int")]
	public System.Nullable<int> id_documento
	{
		get
		{
			return this._id_documento;
		}
		set
		{
			if ((this._id_documento != value))
			{
				this._id_documento = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_fechaIni", DbType="DateTime")]
	public System.Nullable<System.DateTime> fechaIni
	{
		get
		{
			return this._fechaIni;
		}
		set
		{
			if ((this._fechaIni != value))
			{
				this._fechaIni = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_fechaFin", DbType="DateTime")]
	public System.Nullable<System.DateTime> fechaFin
	{
		get
		{
			return this._fechaFin;
		}
		set
		{
			if ((this._fechaFin != value))
			{
				this._fechaFin = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_esAutomatico", DbType="Bit")]
	public System.Nullable<bool> esAutomatico
	{
		get
		{
			return this._esAutomatico;
		}
		set
		{
			if ((this._esAutomatico != value))
			{
				this._esAutomatico = value;
			}
		}
	}
}

[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.tmp_cntmov_docm")]
public partial class tmp_cntmov_docm
{
	
	private int _id_cntmov_docm;
	
	private System.Nullable<int> _id_cntmov_docc;
	
	private System.Nullable<int> _id_cod_tercero;
	
	private string _cod_cta;
	
	private string _cod_suc;
	
	private string _cod_cco;
	
	private string _cod_ter;
	
	private string _des_mov;
	
	private string _num_che;
	
	private System.Nullable<decimal> _deb_mov;
	
	private System.Nullable<decimal> _cre_mov;
	
	private string _doc_ref;
	
	private System.Nullable<System.DateTime> _created_at;
	
	private System.Nullable<int> _user_create;
	
	private System.Nullable<System.DateTime> _updated_at;
	
	private System.Nullable<int> _user_update;
	
	private System.Nullable<bool> _activo;
	
	private string _nom_cta;
	
	public tmp_cntmov_docm()
	{
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id_cntmov_docm", AutoSync=AutoSync.Always, DbType="Int NOT NULL IDENTITY", IsDbGenerated=true)]
	public int id_cntmov_docm
	{
		get
		{
			return this._id_cntmov_docm;
		}
		set
		{
			if ((this._id_cntmov_docm != value))
			{
				this._id_cntmov_docm = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id_cntmov_docc", DbType="Int")]
	public System.Nullable<int> id_cntmov_docc
	{
		get
		{
			return this._id_cntmov_docc;
		}
		set
		{
			if ((this._id_cntmov_docc != value))
			{
				this._id_cntmov_docc = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id_cod_tercero", DbType="Int")]
	public System.Nullable<int> id_cod_tercero
	{
		get
		{
			return this._id_cod_tercero;
		}
		set
		{
			if ((this._id_cod_tercero != value))
			{
				this._id_cod_tercero = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_cod_cta", DbType="VarChar(20)")]
	public string cod_cta
	{
		get
		{
			return this._cod_cta;
		}
		set
		{
			if ((this._cod_cta != value))
			{
				this._cod_cta = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_cod_suc", DbType="VarChar(10)")]
	public string cod_suc
	{
		get
		{
			return this._cod_suc;
		}
		set
		{
			if ((this._cod_suc != value))
			{
				this._cod_suc = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_cod_cco", DbType="VarChar(10)")]
	public string cod_cco
	{
		get
		{
			return this._cod_cco;
		}
		set
		{
			if ((this._cod_cco != value))
			{
				this._cod_cco = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_cod_ter", DbType="VarChar(20)")]
	public string cod_ter
	{
		get
		{
			return this._cod_ter;
		}
		set
		{
			if ((this._cod_ter != value))
			{
				this._cod_ter = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_des_mov", DbType="VarChar(200)")]
	public string des_mov
	{
		get
		{
			return this._des_mov;
		}
		set
		{
			if ((this._des_mov != value))
			{
				this._des_mov = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_num_che", DbType="VarChar(20)")]
	public string num_che
	{
		get
		{
			return this._num_che;
		}
		set
		{
			if ((this._num_che != value))
			{
				this._num_che = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_deb_mov", DbType="Decimal(12,6)")]
	public System.Nullable<decimal> deb_mov
	{
		get
		{
			return this._deb_mov;
		}
		set
		{
			if ((this._deb_mov != value))
			{
				this._deb_mov = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_cre_mov", DbType="Decimal(12,6)")]
	public System.Nullable<decimal> cre_mov
	{
		get
		{
			return this._cre_mov;
		}
		set
		{
			if ((this._cre_mov != value))
			{
				this._cre_mov = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_doc_ref", DbType="VarChar(200)")]
	public string doc_ref
	{
		get
		{
			return this._doc_ref;
		}
		set
		{
			if ((this._doc_ref != value))
			{
				this._doc_ref = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_created_at", DbType="DateTime")]
	public System.Nullable<System.DateTime> created_at
	{
		get
		{
			return this._created_at;
		}
		set
		{
			if ((this._created_at != value))
			{
				this._created_at = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_user_create", DbType="Int")]
	public System.Nullable<int> user_create
	{
		get
		{
			return this._user_create;
		}
		set
		{
			if ((this._user_create != value))
			{
				this._user_create = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_updated_at", DbType="DateTime")]
	public System.Nullable<System.DateTime> updated_at
	{
		get
		{
			return this._updated_at;
		}
		set
		{
			if ((this._updated_at != value))
			{
				this._updated_at = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_user_update", DbType="Int")]
	public System.Nullable<int> user_update
	{
		get
		{
			return this._user_update;
		}
		set
		{
			if ((this._user_update != value))
			{
				this._user_update = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_activo", DbType="Bit")]
	public System.Nullable<bool> activo
	{
		get
		{
			return this._activo;
		}
		set
		{
			if ((this._activo != value))
			{
				this._activo = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_nom_cta", DbType="VarChar(200)")]
	public string nom_cta
	{
		get
		{
			return this._nom_cta;
		}
		set
		{
			if ((this._nom_cta != value))
			{
				this._nom_cta = value;
			}
		}
	}
}

[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.AWA_CONTROL_PERIODOS")]
public partial class AWA_CONTROL_PERIODOS : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private int _ID;
	
	private int _YEAR;
	
	private int _PERIODO;
	
	private System.DateTime _FECHA;
	
	private System.DateTime _CREATED_AT;
	
	private System.Nullable<int> _USER_CREATE;
	
	private System.Nullable<System.DateTime> _UPDATED_AT;
	
	private System.Nullable<int> _USER_UPDATE;
	
	private bool _ACTIVO;
	
    #region Definiciones de métodos de extensibilidad
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnYEARChanging(int value);
    partial void OnYEARChanged();
    partial void OnPERIODOChanging(int value);
    partial void OnPERIODOChanged();
    partial void OnFECHAChanging(System.DateTime value);
    partial void OnFECHAChanged();
    partial void OnCREATED_ATChanging(System.DateTime value);
    partial void OnCREATED_ATChanged();
    partial void OnUSER_CREATEChanging(System.Nullable<int> value);
    partial void OnUSER_CREATEChanged();
    partial void OnUPDATED_ATChanging(System.Nullable<System.DateTime> value);
    partial void OnUPDATED_ATChanged();
    partial void OnUSER_UPDATEChanging(System.Nullable<int> value);
    partial void OnUSER_UPDATEChanged();
    partial void OnACTIVOChanging(bool value);
    partial void OnACTIVOChanged();
    #endregion
	
	public AWA_CONTROL_PERIODOS()
	{
		OnCreated();
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
	public int ID
	{
		get
		{
			return this._ID;
		}
		set
		{
			if ((this._ID != value))
			{
				this.OnIDChanging(value);
				this.SendPropertyChanging();
				this._ID = value;
				this.SendPropertyChanged("ID");
				this.OnIDChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_YEAR", DbType="Int NOT NULL")]
	public int YEAR
	{
		get
		{
			return this._YEAR;
		}
		set
		{
			if ((this._YEAR != value))
			{
				this.OnYEARChanging(value);
				this.SendPropertyChanging();
				this._YEAR = value;
				this.SendPropertyChanged("YEAR");
				this.OnYEARChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PERIODO", DbType="Int NOT NULL")]
	public int PERIODO
	{
		get
		{
			return this._PERIODO;
		}
		set
		{
			if ((this._PERIODO != value))
			{
				this.OnPERIODOChanging(value);
				this.SendPropertyChanging();
				this._PERIODO = value;
				this.SendPropertyChanged("PERIODO");
				this.OnPERIODOChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FECHA", DbType="DateTime NOT NULL")]
	public System.DateTime FECHA
	{
		get
		{
			return this._FECHA;
		}
		set
		{
			if ((this._FECHA != value))
			{
				this.OnFECHAChanging(value);
				this.SendPropertyChanging();
				this._FECHA = value;
				this.SendPropertyChanged("FECHA");
				this.OnFECHAChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CREATED_AT", DbType="DateTime NOT NULL")]
	public System.DateTime CREATED_AT
	{
		get
		{
			return this._CREATED_AT;
		}
		set
		{
			if ((this._CREATED_AT != value))
			{
				this.OnCREATED_ATChanging(value);
				this.SendPropertyChanging();
				this._CREATED_AT = value;
				this.SendPropertyChanged("CREATED_AT");
				this.OnCREATED_ATChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_USER_CREATE", DbType="Int")]
	public System.Nullable<int> USER_CREATE
	{
		get
		{
			return this._USER_CREATE;
		}
		set
		{
			if ((this._USER_CREATE != value))
			{
				this.OnUSER_CREATEChanging(value);
				this.SendPropertyChanging();
				this._USER_CREATE = value;
				this.SendPropertyChanged("USER_CREATE");
				this.OnUSER_CREATEChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UPDATED_AT", DbType="DateTime")]
	public System.Nullable<System.DateTime> UPDATED_AT
	{
		get
		{
			return this._UPDATED_AT;
		}
		set
		{
			if ((this._UPDATED_AT != value))
			{
				this.OnUPDATED_ATChanging(value);
				this.SendPropertyChanging();
				this._UPDATED_AT = value;
				this.SendPropertyChanged("UPDATED_AT");
				this.OnUPDATED_ATChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_USER_UPDATE", DbType="Int")]
	public System.Nullable<int> USER_UPDATE
	{
		get
		{
			return this._USER_UPDATE;
		}
		set
		{
			if ((this._USER_UPDATE != value))
			{
				this.OnUSER_UPDATEChanging(value);
				this.SendPropertyChanging();
				this._USER_UPDATE = value;
				this.SendPropertyChanged("USER_UPDATE");
				this.OnUSER_UPDATEChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ACTIVO", DbType="Bit NOT NULL")]
	public bool ACTIVO
	{
		get
		{
			return this._ACTIVO;
		}
		set
		{
			if ((this._ACTIVO != value))
			{
				this.OnACTIVOChanging(value);
				this.SendPropertyChanging();
				this._ACTIVO = value;
				this.SendPropertyChanged("ACTIVO");
				this.OnACTIVOChanged();
			}
		}
	}
	
	public event PropertyChangingEventHandler PropertyChanging;
	
	public event PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		if ((this.PropertyChanging != null))
		{
			this.PropertyChanging(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(String propertyName)
	{
		if ((this.PropertyChanged != null))
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
#pragma warning restore 1591