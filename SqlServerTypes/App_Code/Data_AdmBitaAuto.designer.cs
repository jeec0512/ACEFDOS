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



[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="AdmBitaAuto")]
public partial class Data_AdmBitaAutoDataContext : System.Data.Linq.DataContext
{
	
	private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
	
  #region Definiciones de métodos de extensibilidad
  partial void OnCreated();
  partial void InsertSUCURSAL(SUCURSAL instance);
  partial void UpdateSUCURSAL(SUCURSAL instance);
  partial void DeleteSUCURSAL(SUCURSAL instance);
  partial void InsertVEHICULO(VEHICULO instance);
  partial void UpdateVEHICULO(VEHICULO instance);
  partial void DeleteVEHICULO(VEHICULO instance);
  #endregion
	
	public Data_AdmBitaAutoDataContext() : 
			base(global::System.Configuration.ConfigurationManager.ConnectionStrings["AdmBitaAutoConnectionString"].ConnectionString, mappingSource)
	{
		OnCreated();
	}
	
	public Data_AdmBitaAutoDataContext(string connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public Data_AdmBitaAutoDataContext(System.Data.IDbConnection connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public Data_AdmBitaAutoDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public Data_AdmBitaAutoDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public System.Data.Linq.Table<SUCURSAL> SUCURSAL
	{
		get
		{
			return this.GetTable<SUCURSAL>();
		}
	}
	
	public System.Data.Linq.Table<VEHICULO> VEHICULO
	{
		get
		{
			return this.GetTable<VEHICULO>();
		}
	}
}

[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.SUCURSAL")]
public partial class SUCURSAL : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private decimal _SUC_ID;
	
	private string _SUC_DESCRIPCION;
	
	private EntitySet<VEHICULO> _VEHICULO;
	
    #region Definiciones de métodos de extensibilidad
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnSUC_IDChanging(decimal value);
    partial void OnSUC_IDChanged();
    partial void OnSUC_DESCRIPCIONChanging(string value);
    partial void OnSUC_DESCRIPCIONChanged();
    #endregion
	
	public SUCURSAL()
	{
		this._VEHICULO = new EntitySet<VEHICULO>(new Action<VEHICULO>(this.attach_VEHICULO), new Action<VEHICULO>(this.detach_VEHICULO));
		OnCreated();
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SUC_ID", AutoSync=AutoSync.OnInsert, DbType="Decimal(18,0) NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
	public decimal SUC_ID
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
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SUC_DESCRIPCION", DbType="VarChar(500)")]
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
	
	[global::System.Data.Linq.Mapping.AssociationAttribute(Name="SUCURSAL_VEHICULO", Storage="_VEHICULO", ThisKey="SUC_ID", OtherKey="SUC_ID")]
	public EntitySet<VEHICULO> VEHICULO
	{
		get
		{
			return this._VEHICULO;
		}
		set
		{
			this._VEHICULO.Assign(value);
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
	
	private void attach_VEHICULO(VEHICULO entity)
	{
		this.SendPropertyChanging();
		entity.SUCURSAL = this;
	}
	
	private void detach_VEHICULO(VEHICULO entity)
	{
		this.SendPropertyChanging();
		entity.SUCURSAL = null;
	}
}

[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VEHICULO")]
public partial class VEHICULO : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private decimal _VEH_ID;
	
	private string _VEH_MARCA;
	
	private string _VEH_MODELO;
	
	private System.Nullable<decimal> _VEH_ANIO;
	
	private string _VEH_NUMERO;
	
	private string _VEH_CHASIS;
	
	private string _VEH_MOTOR;
	
	private string _VEH_PLACA;
	
	private System.Nullable<decimal> _SUC_ID;
	
	private System.Nullable<decimal> _TVE_ID;
	
	private System.Nullable<decimal> _PER_ID;
	
	private System.Nullable<bool> _VEH_ESTADO;
	
	private EntityRef<SUCURSAL> _SUCURSAL;
	
    #region Definiciones de métodos de extensibilidad
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnVEH_IDChanging(decimal value);
    partial void OnVEH_IDChanged();
    partial void OnVEH_MARCAChanging(string value);
    partial void OnVEH_MARCAChanged();
    partial void OnVEH_MODELOChanging(string value);
    partial void OnVEH_MODELOChanged();
    partial void OnVEH_ANIOChanging(System.Nullable<decimal> value);
    partial void OnVEH_ANIOChanged();
    partial void OnVEH_NUMEROChanging(string value);
    partial void OnVEH_NUMEROChanged();
    partial void OnVEH_CHASISChanging(string value);
    partial void OnVEH_CHASISChanged();
    partial void OnVEH_MOTORChanging(string value);
    partial void OnVEH_MOTORChanged();
    partial void OnVEH_PLACAChanging(string value);
    partial void OnVEH_PLACAChanged();
    partial void OnSUC_IDChanging(System.Nullable<decimal> value);
    partial void OnSUC_IDChanged();
    partial void OnTVE_IDChanging(System.Nullable<decimal> value);
    partial void OnTVE_IDChanged();
    partial void OnPER_IDChanging(System.Nullable<decimal> value);
    partial void OnPER_IDChanged();
    partial void OnVEH_ESTADOChanging(System.Nullable<bool> value);
    partial void OnVEH_ESTADOChanged();
    #endregion
	
	public VEHICULO()
	{
		this._SUCURSAL = default(EntityRef<SUCURSAL>);
		OnCreated();
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VEH_ID", AutoSync=AutoSync.OnInsert, DbType="Decimal(18,0) NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
	public decimal VEH_ID
	{
		get
		{
			return this._VEH_ID;
		}
		set
		{
			if ((this._VEH_ID != value))
			{
				this.OnVEH_IDChanging(value);
				this.SendPropertyChanging();
				this._VEH_ID = value;
				this.SendPropertyChanged("VEH_ID");
				this.OnVEH_IDChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VEH_MARCA", DbType="VarChar(100)")]
	public string VEH_MARCA
	{
		get
		{
			return this._VEH_MARCA;
		}
		set
		{
			if ((this._VEH_MARCA != value))
			{
				this.OnVEH_MARCAChanging(value);
				this.SendPropertyChanging();
				this._VEH_MARCA = value;
				this.SendPropertyChanged("VEH_MARCA");
				this.OnVEH_MARCAChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VEH_MODELO", DbType="VarChar(100)")]
	public string VEH_MODELO
	{
		get
		{
			return this._VEH_MODELO;
		}
		set
		{
			if ((this._VEH_MODELO != value))
			{
				this.OnVEH_MODELOChanging(value);
				this.SendPropertyChanging();
				this._VEH_MODELO = value;
				this.SendPropertyChanged("VEH_MODELO");
				this.OnVEH_MODELOChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VEH_ANIO", DbType="Decimal(18,0)")]
	public System.Nullable<decimal> VEH_ANIO
	{
		get
		{
			return this._VEH_ANIO;
		}
		set
		{
			if ((this._VEH_ANIO != value))
			{
				this.OnVEH_ANIOChanging(value);
				this.SendPropertyChanging();
				this._VEH_ANIO = value;
				this.SendPropertyChanged("VEH_ANIO");
				this.OnVEH_ANIOChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VEH_NUMERO", DbType="VarChar(100)")]
	public string VEH_NUMERO
	{
		get
		{
			return this._VEH_NUMERO;
		}
		set
		{
			if ((this._VEH_NUMERO != value))
			{
				this.OnVEH_NUMEROChanging(value);
				this.SendPropertyChanging();
				this._VEH_NUMERO = value;
				this.SendPropertyChanged("VEH_NUMERO");
				this.OnVEH_NUMEROChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VEH_CHASIS", DbType="VarChar(100)")]
	public string VEH_CHASIS
	{
		get
		{
			return this._VEH_CHASIS;
		}
		set
		{
			if ((this._VEH_CHASIS != value))
			{
				this.OnVEH_CHASISChanging(value);
				this.SendPropertyChanging();
				this._VEH_CHASIS = value;
				this.SendPropertyChanged("VEH_CHASIS");
				this.OnVEH_CHASISChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VEH_MOTOR", DbType="VarChar(100)")]
	public string VEH_MOTOR
	{
		get
		{
			return this._VEH_MOTOR;
		}
		set
		{
			if ((this._VEH_MOTOR != value))
			{
				this.OnVEH_MOTORChanging(value);
				this.SendPropertyChanging();
				this._VEH_MOTOR = value;
				this.SendPropertyChanged("VEH_MOTOR");
				this.OnVEH_MOTORChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VEH_PLACA", DbType="VarChar(100)")]
	public string VEH_PLACA
	{
		get
		{
			return this._VEH_PLACA;
		}
		set
		{
			if ((this._VEH_PLACA != value))
			{
				this.OnVEH_PLACAChanging(value);
				this.SendPropertyChanging();
				this._VEH_PLACA = value;
				this.SendPropertyChanged("VEH_PLACA");
				this.OnVEH_PLACAChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SUC_ID", DbType="Decimal(18,0)")]
	public System.Nullable<decimal> SUC_ID
	{
		get
		{
			return this._SUC_ID;
		}
		set
		{
			if ((this._SUC_ID != value))
			{
				if (this._SUCURSAL.HasLoadedOrAssignedValue)
				{
					throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
				}
				this.OnSUC_IDChanging(value);
				this.SendPropertyChanging();
				this._SUC_ID = value;
				this.SendPropertyChanged("SUC_ID");
				this.OnSUC_IDChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TVE_ID", DbType="Decimal(18,0)")]
	public System.Nullable<decimal> TVE_ID
	{
		get
		{
			return this._TVE_ID;
		}
		set
		{
			if ((this._TVE_ID != value))
			{
				this.OnTVE_IDChanging(value);
				this.SendPropertyChanging();
				this._TVE_ID = value;
				this.SendPropertyChanged("TVE_ID");
				this.OnTVE_IDChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PER_ID", DbType="Decimal(18,0)")]
	public System.Nullable<decimal> PER_ID
	{
		get
		{
			return this._PER_ID;
		}
		set
		{
			if ((this._PER_ID != value))
			{
				this.OnPER_IDChanging(value);
				this.SendPropertyChanging();
				this._PER_ID = value;
				this.SendPropertyChanged("PER_ID");
				this.OnPER_IDChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VEH_ESTADO", DbType="Bit")]
	public System.Nullable<bool> VEH_ESTADO
	{
		get
		{
			return this._VEH_ESTADO;
		}
		set
		{
			if ((this._VEH_ESTADO != value))
			{
				this.OnVEH_ESTADOChanging(value);
				this.SendPropertyChanging();
				this._VEH_ESTADO = value;
				this.SendPropertyChanged("VEH_ESTADO");
				this.OnVEH_ESTADOChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.AssociationAttribute(Name="SUCURSAL_VEHICULO", Storage="_SUCURSAL", ThisKey="SUC_ID", OtherKey="SUC_ID", IsForeignKey=true)]
	public SUCURSAL SUCURSAL
	{
		get
		{
			return this._SUCURSAL.Entity;
		}
		set
		{
			SUCURSAL previousValue = this._SUCURSAL.Entity;
			if (((previousValue != value) 
						|| (this._SUCURSAL.HasLoadedOrAssignedValue == false)))
			{
				this.SendPropertyChanging();
				if ((previousValue != null))
				{
					this._SUCURSAL.Entity = null;
					previousValue.VEHICULO.Remove(this);
				}
				this._SUCURSAL.Entity = value;
				if ((value != null))
				{
					value.VEHICULO.Add(this);
					this._SUC_ID = value.SUC_ID;
				}
				else
				{
					this._SUC_ID = default(Nullable<decimal>);
				}
				this.SendPropertyChanged("SUCURSAL");
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
