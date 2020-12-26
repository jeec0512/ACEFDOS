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



[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="COMISIONES")]
public partial class DataComisionesDataContext : System.Data.Linq.DataContext
{
	
	private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
	
  #region Definiciones de métodos de extensibilidad
  partial void OnCreated();
  partial void InsertTB_MEMBRESIA(TB_MEMBRESIA instance);
  partial void UpdateTB_MEMBRESIA(TB_MEMBRESIA instance);
  partial void DeleteTB_MEMBRESIA(TB_MEMBRESIA instance);
  partial void InsertTB_VENDEDOR(TB_VENDEDOR instance);
  partial void UpdateTB_VENDEDOR(TB_VENDEDOR instance);
  partial void DeleteTB_VENDEDOR(TB_VENDEDOR instance);
  #endregion
	
	public DataComisionesDataContext() : 
			base(global::System.Configuration.ConfigurationManager.ConnectionStrings["COMISIONESConnectionString"].ConnectionString, mappingSource)
	{
		OnCreated();
	}
	
	public DataComisionesDataContext(string connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public DataComisionesDataContext(System.Data.IDbConnection connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public DataComisionesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public DataComisionesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public System.Data.Linq.Table<TB_MEMBRESIA> TB_MEMBRESIA
	{
		get
		{
			return this.GetTable<TB_MEMBRESIA>();
		}
	}
	
	public System.Data.Linq.Table<TB_VENDEDOR> TB_VENDEDOR
	{
		get
		{
			return this.GetTable<TB_VENDEDOR>();
		}
	}
}

[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.TB_MEMBRESIA")]
public partial class TB_MEMBRESIA : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private int _MEM_ID;
	
	private string _MEM_NOMBRE;
	
	private System.Nullable<decimal> _MEM_PRECIO;
	
	private System.Nullable<bool> _MEM_ACTIVO;
	
	private System.Nullable<int> _US_ID;
	
	private System.Nullable<int> _US_ID_MODIFICA;
	
	private System.Nullable<System.DateTime> _MEM_FECHA_MODIFICA;
	
    #region Definiciones de métodos de extensibilidad
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnMEM_IDChanging(int value);
    partial void OnMEM_IDChanged();
    partial void OnMEM_NOMBREChanging(string value);
    partial void OnMEM_NOMBREChanged();
    partial void OnMEM_PRECIOChanging(System.Nullable<decimal> value);
    partial void OnMEM_PRECIOChanged();
    partial void OnMEM_ACTIVOChanging(System.Nullable<bool> value);
    partial void OnMEM_ACTIVOChanged();
    partial void OnUS_IDChanging(System.Nullable<int> value);
    partial void OnUS_IDChanged();
    partial void OnUS_ID_MODIFICAChanging(System.Nullable<int> value);
    partial void OnUS_ID_MODIFICAChanged();
    partial void OnMEM_FECHA_MODIFICAChanging(System.Nullable<System.DateTime> value);
    partial void OnMEM_FECHA_MODIFICAChanged();
    #endregion
	
	public TB_MEMBRESIA()
	{
		OnCreated();
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MEM_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
	public int MEM_ID
	{
		get
		{
			return this._MEM_ID;
		}
		set
		{
			if ((this._MEM_ID != value))
			{
				this.OnMEM_IDChanging(value);
				this.SendPropertyChanging();
				this._MEM_ID = value;
				this.SendPropertyChanged("MEM_ID");
				this.OnMEM_IDChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MEM_NOMBRE", DbType="VarChar(50)")]
	public string MEM_NOMBRE
	{
		get
		{
			return this._MEM_NOMBRE;
		}
		set
		{
			if ((this._MEM_NOMBRE != value))
			{
				this.OnMEM_NOMBREChanging(value);
				this.SendPropertyChanging();
				this._MEM_NOMBRE = value;
				this.SendPropertyChanged("MEM_NOMBRE");
				this.OnMEM_NOMBREChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MEM_PRECIO", DbType="Decimal(10,4)")]
	public System.Nullable<decimal> MEM_PRECIO
	{
		get
		{
			return this._MEM_PRECIO;
		}
		set
		{
			if ((this._MEM_PRECIO != value))
			{
				this.OnMEM_PRECIOChanging(value);
				this.SendPropertyChanging();
				this._MEM_PRECIO = value;
				this.SendPropertyChanged("MEM_PRECIO");
				this.OnMEM_PRECIOChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MEM_ACTIVO", DbType="Bit")]
	public System.Nullable<bool> MEM_ACTIVO
	{
		get
		{
			return this._MEM_ACTIVO;
		}
		set
		{
			if ((this._MEM_ACTIVO != value))
			{
				this.OnMEM_ACTIVOChanging(value);
				this.SendPropertyChanging();
				this._MEM_ACTIVO = value;
				this.SendPropertyChanged("MEM_ACTIVO");
				this.OnMEM_ACTIVOChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_US_ID", DbType="Int")]
	public System.Nullable<int> US_ID
	{
		get
		{
			return this._US_ID;
		}
		set
		{
			if ((this._US_ID != value))
			{
				this.OnUS_IDChanging(value);
				this.SendPropertyChanging();
				this._US_ID = value;
				this.SendPropertyChanged("US_ID");
				this.OnUS_IDChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_US_ID_MODIFICA", DbType="Int")]
	public System.Nullable<int> US_ID_MODIFICA
	{
		get
		{
			return this._US_ID_MODIFICA;
		}
		set
		{
			if ((this._US_ID_MODIFICA != value))
			{
				this.OnUS_ID_MODIFICAChanging(value);
				this.SendPropertyChanging();
				this._US_ID_MODIFICA = value;
				this.SendPropertyChanged("US_ID_MODIFICA");
				this.OnUS_ID_MODIFICAChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MEM_FECHA_MODIFICA", DbType="DateTime")]
	public System.Nullable<System.DateTime> MEM_FECHA_MODIFICA
	{
		get
		{
			return this._MEM_FECHA_MODIFICA;
		}
		set
		{
			if ((this._MEM_FECHA_MODIFICA != value))
			{
				this.OnMEM_FECHA_MODIFICAChanging(value);
				this.SendPropertyChanging();
				this._MEM_FECHA_MODIFICA = value;
				this.SendPropertyChanged("MEM_FECHA_MODIFICA");
				this.OnMEM_FECHA_MODIFICAChanged();
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

[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.TB_VENDEDOR")]
public partial class TB_VENDEDOR : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private int _VEN_ID;
	
	private string _VEN_NOMBRE;
	
	private string _VEN_APELLIDO;
	
	private System.Nullable<bool> _VEN_ACTIVO;
	
	private System.Nullable<int> _TPC_ID;
	
	private string _VEN_IDENTIFICACION;
	
	private System.Nullable<int> _SUC_ID;
	
	private string _SUC_DESCRIPCION;
	
    #region Definiciones de métodos de extensibilidad
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnVEN_IDChanging(int value);
    partial void OnVEN_IDChanged();
    partial void OnVEN_NOMBREChanging(string value);
    partial void OnVEN_NOMBREChanged();
    partial void OnVEN_APELLIDOChanging(string value);
    partial void OnVEN_APELLIDOChanged();
    partial void OnVEN_ACTIVOChanging(System.Nullable<bool> value);
    partial void OnVEN_ACTIVOChanged();
    partial void OnTPC_IDChanging(System.Nullable<int> value);
    partial void OnTPC_IDChanged();
    partial void OnVEN_IDENTIFICACIONChanging(string value);
    partial void OnVEN_IDENTIFICACIONChanged();
    partial void OnSUC_IDChanging(System.Nullable<int> value);
    partial void OnSUC_IDChanged();
    partial void OnSUC_DESCRIPCIONChanging(string value);
    partial void OnSUC_DESCRIPCIONChanged();
    #endregion
	
	public TB_VENDEDOR()
	{
		OnCreated();
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VEN_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
	public int VEN_ID
	{
		get
		{
			return this._VEN_ID;
		}
		set
		{
			if ((this._VEN_ID != value))
			{
				this.OnVEN_IDChanging(value);
				this.SendPropertyChanging();
				this._VEN_ID = value;
				this.SendPropertyChanged("VEN_ID");
				this.OnVEN_IDChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VEN_NOMBRE", DbType="VarChar(100)")]
	public string VEN_NOMBRE
	{
		get
		{
			return this._VEN_NOMBRE;
		}
		set
		{
			if ((this._VEN_NOMBRE != value))
			{
				this.OnVEN_NOMBREChanging(value);
				this.SendPropertyChanging();
				this._VEN_NOMBRE = value;
				this.SendPropertyChanged("VEN_NOMBRE");
				this.OnVEN_NOMBREChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VEN_APELLIDO", DbType="VarChar(100)")]
	public string VEN_APELLIDO
	{
		get
		{
			return this._VEN_APELLIDO;
		}
		set
		{
			if ((this._VEN_APELLIDO != value))
			{
				this.OnVEN_APELLIDOChanging(value);
				this.SendPropertyChanging();
				this._VEN_APELLIDO = value;
				this.SendPropertyChanged("VEN_APELLIDO");
				this.OnVEN_APELLIDOChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VEN_ACTIVO", DbType="Bit")]
	public System.Nullable<bool> VEN_ACTIVO
	{
		get
		{
			return this._VEN_ACTIVO;
		}
		set
		{
			if ((this._VEN_ACTIVO != value))
			{
				this.OnVEN_ACTIVOChanging(value);
				this.SendPropertyChanging();
				this._VEN_ACTIVO = value;
				this.SendPropertyChanged("VEN_ACTIVO");
				this.OnVEN_ACTIVOChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TPC_ID", DbType="Int")]
	public System.Nullable<int> TPC_ID
	{
		get
		{
			return this._TPC_ID;
		}
		set
		{
			if ((this._TPC_ID != value))
			{
				this.OnTPC_IDChanging(value);
				this.SendPropertyChanging();
				this._TPC_ID = value;
				this.SendPropertyChanged("TPC_ID");
				this.OnTPC_IDChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VEN_IDENTIFICACION", DbType="VarChar(15)")]
	public string VEN_IDENTIFICACION
	{
		get
		{
			return this._VEN_IDENTIFICACION;
		}
		set
		{
			if ((this._VEN_IDENTIFICACION != value))
			{
				this.OnVEN_IDENTIFICACIONChanging(value);
				this.SendPropertyChanging();
				this._VEN_IDENTIFICACION = value;
				this.SendPropertyChanged("VEN_IDENTIFICACION");
				this.OnVEN_IDENTIFICACIONChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SUC_ID", DbType="Int")]
	public System.Nullable<int> SUC_ID
	{
		get
		{
			return this._SUC_ID;
		}
		set
		{
			if ((this._SUC_ID != value))
			{
				this.OnSUC_IDChanging(value);
				this.SendPropertyChanging();
				this._SUC_ID = value;
				this.SendPropertyChanged("SUC_ID");
				this.OnSUC_IDChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SUC_DESCRIPCION", DbType="VarChar(100)")]
	public string SUC_DESCRIPCION
	{
		get
		{
			return this._SUC_DESCRIPCION;
		}
		set
		{
			if ((this._SUC_DESCRIPCION != value))
			{
				this.OnSUC_DESCRIPCIONChanging(value);
				this.SendPropertyChanging();
				this._SUC_DESCRIPCION = value;
				this.SendPropertyChanged("SUC_DESCRIPCION");
				this.OnSUC_DESCRIPCIONChanged();
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
